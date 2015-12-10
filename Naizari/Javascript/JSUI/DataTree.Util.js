JSUI.DataTree = {};
JSUI.DataTree.actionImpl = function(jsonid, action, el, pos) {
    JSUI.DataTree[action](jsonid, el);
}

JSUI.DataTree.refresh = function(jsonid) {
    JSUI[jsonid].GetHtmlEx(jsonid, function(result) {
        if (result.Status == "Success") {
            jQuery(JSUI[jsonid].el).html(result.Data);
            JSUI[jsonid].init();
        }
    });
}
JSUI.DataTree.newFolder = function(jsonid, el) {
    JSUI.Conf.usingResource(["naizari.javascript.jsui.prompt.js"], function(s) {
        JSUI.prompt.show("New Folder", "Please enter a name for the new folder.", function(pResult, text) {
            if (pResult == "Cancel")
                return;
            JSUI.Conf.usingProxy("dataTree", function(inf) {
                var dataTree = ProxyUtil.getProxy("dataTree");
                var selfId = jQuery(el).attr("dataid");
                dataTree.NewFolderEx(selfId, text, "", function(result) {
                    if (result.Status == "Success") {
                        if (result.Data.Success) {
                            JSUI.DataTree.refresh(jsonid);
                        } else if (!result.Data.Success && result.Data.Code == "ItemExists") {
                            JSUI.Conf.usingResource(["naizari.javascript.jsui.messagebox.js"], function(s) {
                                MessageBox.show("New Folder", "There is already an item with the same name in that folder.");
                            });
                        }
                    }
                });
            });
        });
    });
}

JSUI.DataTree.newFile = function(jsonid, el) {
    JSUI.Conf.usingResource(["naizari.javascript.jsui.prompt.js"], function(s) {
        JSUI.prompt.show("New File", "Please enter a name for the new file.", function(pResult, text) {
            if (pResult == "Cancel")
                return;

            JSUI.Conf.usingProxy("dataTree", function(inf) {
                var dataTree = inf.proxy;
                var selfId = jQuery(el).attr("dataid");
                dataTree.NewFileEx(selfId, text, "", function(r) {
                    if (r.Status == "Success") {
                        if (r.Data.Success) {
                            JSUI.DataTree.refresh(jsonid);
                        } else if (!JSUI.isNullOrUndef(r.Data) && !r.Data.Success && result.Data.Code == "ItemExists") {
                            JSUI.Conf.UsingResource(["naizari.javascript.jsui.messagebox.js"], function(s) {
                                MessageBox.show("New File", "There is already an item with the same name in that folder.");
                            });
                        }
                    }
                });
            });
        });
    });
}


JSUI.DataTree.rename = function(jsonid, el) {
    JSUI.Conf.usingResource(["naizari.javascript.jsui.prompt.js"], function(s) {
        JSUI.prompt.show("Rename", "Please enter the new name.", function(pR, text) {
            if (pR == "Cancel")
                return;
            if (text.trim().isBlank()) {
                JSUI.DataTree.rename(jsonid, el);
                return;
            }

            JSUI.Conf.usingProxy("dataTree", function(inf) {
                inf.proxy.RenameNodeEx(jQuery(el).attr("dataid"), text, function(r) {
                    if (r.Status == "Success") {
                        JSUI.DataTree.refresh(jsonid);
                    } else if (r.Status == "Error") {
                        MessageBox.show("Error", "An error occurred on the server.  We are working to correct this and apologize for the inconvenience.  Please refresh your browser.");
                    }
                });
            });
        });
    });
}

JSUI.DataTree.deleteFile = function(jsonid, el) {
    JSUI.Conf.usingProxy("dataTree", function(inf) {
        inf.proxy.DeleteFileEx(jQuery(el).attr("dataid"), function(r) {
            if (r.Status == "Success") {
                JSUI.DataTree.refresh(jsonid);
            }
        });
    });
}

JSUI.DataTree.renameFolder = JSUI.DataTree.rename;

JSUI.DataTree.renameFile = JSUI.DataTree.rename;

JSUI.DataTree.deleteFolder = function(jsonid, el) {
    var deleteFn = function() {
        JSUI.Conf.usingProxy("dataTree", function(inf) {
            var dataTree = ProxyUtil.getProxy("dataTree");
            var selfId = jQuery(el).attr("dataid");
            dataTree.DeleteFolderEx(selfId, function(result) {
                if (result.Status == "Success") {
                    JSUI.DataTree.refresh(jsonid);
                }
            });
        });
    }

    if (!JSUI[jsonid].deleteFolderPrompt.isBlank()) {
        JSUI.Conf.usingResource(["naizari.javascript.jsui.confirm.js"], function(s) {
            JSUI.confirm.show("Delete Folder", "The folder and all its contents will be deleted.<br/><br/>Delete this folder?", function(pResult) {
                if (pResult == "Cancel")
                    return;
                deleteFn();
            });
        });
    } else {
        deleteFn();
    }
}