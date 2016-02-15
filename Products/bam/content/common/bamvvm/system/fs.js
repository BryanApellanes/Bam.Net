/*
	Copyright © Bryan Apellanes 2015  
*/
var fs = (function ($, _) {
    "use strict";
    var _local = true;

    var _defaultFolder;
    function defaultFolder(prov) {
        if (!_.isUndefined(prov)) {
            _defaultFolder = prov;
            return this; // if the value is being set, returning this will allow chaining
        }

        if (_.isUndefined(_defaultFolder)) {
            _defaultFolder = Windows.Storage.KnownFolders.documentsLibrary;
        }

        return _defaultFolder;
    }

    var _fileIO;
    function fileIO(prov) {
        if (!_.isUndefined(prov)) {
            _fileIO = prov;
        }

        if (_.isUndefined(_fileIO)) {
            _fileIO = Windows.Storage.FileIO;
        }

        return _fileIO;
    }

    var _launcher;
    function launcher(prov) {
        if (!_.isUndefined(prov)) {
            _launcher = prov;
        }

        if (_.isUndefined(_launcher)) {
            _launcher = Windows.System.Launcher;
        }

        return _launcher;
    }

    function notImplemented() {
        var val = $.Deferred(function () { }).promise();
        val.reject({ message: "Not Implemented" });

        return val;
    }

    function local(v) {// prepping for future remote fs
        if (_.isBoolean(v)) {
            _local = v;
            _remote = !_local;
            return this;
        }
        return _local;
    }

    var _remote = false;
    function remote(v) {
        if (_.isBoolean(v)) {
            _remote = v;
            _local = !_remote;
            return this;
        }
        return _remote;
    }

    function writeRemote(file, data) {
        return notImplemented();
    }

    function write(fileName, text, collisionOpts) {
        if(_.isUndefined(collisionOpts) && !_.isUndefined(Windows)){
            collisionOpts = Windows.Storage.CreationCollisionOption.replaceExisting;
        }

        if (_local) {
            var def = $.Deferred(function () {
                var prom = this;
                exists(fileName)
                    .done(function (answer, fo) { // answer = exists? as bool; fo = file object to be passed to FileIO
                        if (!answer) {
                            defaultFolder().createFileAsync(fileName, collisionOpts).done(
                                function (file) {
                                    fileIO().writeTextAsync(file, text).done(
                                        function () {
                                            prom.resolve(true);
                                        },
                                        function (err) {
                                            prom.reject(err);
                                        });
                                },
                                function (err) {
                                    prom.reject(err);
                                });
                        } else {
                            fileIO().writeTextAsync(fo, text).done(
                                function () {
                                    prom.resolve(true);
                                },
                                function (err) {
                                    prom.reject(err);
                                });
                        }
                    });
            });  
            
            return def.promise();
        } else {
            return writeRemote(file, data);
        }
    }

    function readTextRemote(file) {
        return notImplemented();
    }

    function readText(fileName) {
        if (_local) {
            var def = $.Deferred(function () {
                var prom = this;
                exists(fileName)
                    .done(function (answer, fileOrErr) {
                        if (answer) {
                            fileIO().readTextAsync(fileOrErr)
                                .done(function (txt) {
                                    prom.resolve(txt);
                                });
                        } else {
                            prom.reject("Error occurred reading the file " + fileName + ": " + fileOrErr);
                        }
                    });
            });
            
            return def.promise();
        } else {
            return readTextRemote(file);
        }
    }

    function launchWith(fileName, launchOptions) {
        return notImplemented();
    }

    function launch(fileName) {
        
        var def = $.Deferred(function () {
            var prom = this;
            exists(fileName)
                .done(function (answer, fileOrError) {
                    if (!answer) {
                        prom.reject(fileOrError);
                    } else {
                        launcher().launchFileAsync(fileOrError).then(function (success) {
                            if (success) {
                                prom.resolve(true);
                            } else {
                                prom.resolve(false);
                            }
                        });
                    }
                });
        });

        return def.promise();
    }

    function existsRemote(file) {
        return notImplemented();
    }

    function exists(fileName) {
        if (_local) {
            var def = $.Deferred(function () {
                var prom = this;
                defaultFolder().getFileAsync(fileName).done(
                    function (f) {
                        prom.resolve(true, f); // exists
                    },
                    function (e) {
                        prom.resolve(false, e); // doesn't
                    });
            });

            return def.promise();
        } else {
            return existsRemote(file);
        }
    }

    function deleteFileRemote(file) {
        return notImplemented();
    }

    function deleteFile(file, complete) {
        if (_local) {
            exists(file)
                .done(function (answer, file) {
                    if (answer) {
                        file.deleteAsync().done(complete);
                    } else {
                        complete(false);
                    }
                })                
        } else {
            return deleteFileRemote(file);
        }
    }

    return {
        write: write,
        readText: readText,
        exists: exists,
        deleteFile: deleteFile,
        launch: launch,
        defaultFolder: defaultFolder
    }
})(jQuery, _)