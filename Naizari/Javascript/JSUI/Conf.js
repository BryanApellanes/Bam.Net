if (JSUI === undefined) {
    alert("The core JUI.js file was not loaded.");
}

JSUI.Ctors = {};

JSUI.addConstructor = function(strClassName, ctor) {
    JSUI.Ctors[strClassName] = ctor;
};

JSUI.construct = function(strClassName, arrParams) {
    return new JSUI.Ctors[strClassName](arrParams);
};

JSUI.addConstructor("Conf", function() {
    this.LoadedScripts = {};
    this.ScriptReadyListeners = {};

    var thisObj = this;

    this.scriptReady = function(id, scrName) {
        thisObj.LoadedScripts[scrName] = true;
        var lstnr = thisObj.ScriptReadyListeners[id];
        delete lstnr.waitingfor[scrName]
        if (JSUI.countKeys(lstnr.waitingfor) == 0) {
            lstnr.callBack(lstnr.scripts);
            delete thisObj.ScriptReadyListeners[id];
        }
    }

    this.addScriptReadyListener = function(listener) {
        JSUI.isObjectOrDie(listener);
        if (thisObj.ScriptReadyListeners[listener.id] === null || thisObj.ScriptReadyListeners[listener.id] === undefined) {
            thisObj.ScriptReadyListeners[listener.id] = listener;
        }
        JSUI.forEach(listener.waitingfor, function(key, value) {
            var path = window.location.toString().split("?")[0] + "?script=" + key;
            path += encodeURI("&cb=JSUI.Conf.scriptReady('" + listener.id + "', '" + key + "');");
            thisObj.writeScriptTag(path);
        });
    }

    this.writeScriptTag = function(srcUrl) {
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        script.setAttribute("src", srcUrl);
        JSUI.GetDocumentBody().appendChild(script);
    };
});

JSUI.Conf = JSUI.construct("Conf");

JSUI.Conf.usingResource = function(arrScripts, uid, callBack) {
    JSUI.isArrayOrDie(arrScripts);
    if (JSUI.isFunction(uid))
        callBack = uid, uid = JSUI.randomString(4);
    var readyListener = { scripts: arrScripts, callBack: callBack, waitingfor: {}, id: uid };
    var pending = false;
    JSUI.forEach(arrScripts, function(key, value) {
        if (JSUI.Conf.LoadedScripts[value] === null || JSUI.Conf.LoadedScripts[value] === undefined) {
            readyListener.waitingfor[value] = true;
            pending = true;
        }
    });

    if (pending) {
        JSUI.Conf.addScriptReadyListener(readyListener);
    } else if (JSUI.isFunction(callBack)) {
        callBack(arrScripts);
    }
}

JSUI.Assimilate(JSUI.Conf);//, JSUI);
