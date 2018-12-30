
if (window.JSUI === null ||
    window.JSUI === undefined) {

    window.JSUI = {};
    JSUI = window.JSUI;


    JSUI.HttpRequests = [];

    JSUI.NewHttpRequest = function () {
        var ret = false;
        if (typeof (XMLHttpRequest) != 'undefined') {
            ret = new XMLHttpRequest();
        }

        try {
            ret = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                ret = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) { }
        }
        JSUI.HttpRequests.push(ret);
        return ret;
    }

    JSUI.AsyncRequestClass = function (strUrl, funcCallback) {
        this.Url = strUrl;
        this.Callback = funcCallback;
        this.Verb = "GET";

        var thisObj = this;
        var request = null;
        this.Send = function (postData) {
            if (!thisObj.Url)
                throw new JSUI.Exception("Url was not specified.");

            JSUI.IsFunctionOrDie(thisObj.Callback);

            request = JSUI.NewHttpRequest();
            request.onreadystatechange = function () {
                if (request.readyState == 4 && request.status == 200) {
                    var responseText = request.responseText;
                    thisObj.Callback(responseText);
                }
            }
            request.open(thisObj.Verb, encodeURI(thisObj.Url), true);
            request.send(postData);
            return request;
        }

        this.Abort = function () {
            if (request)
                request.abort();
        }

        this.abort = function () {
            thisObj.Abort();
        }
    }

    JSUI.callFunctions = function (arrFunctions, arg, arrParams) {
        for (var i = 0; i < arrFunctions.length; i++) {
            if (JSUI.IsFunction(arrFunctions[i]))
                arrFunctions[i](arg, arrParams);
        }
    }

    JSUI.CallFunctions = function (arrFn, arg, arrParams) {
        JSUI.callFunctions(arrFn, arg, arrParams);
    }

    // this is used primarily to allow "pre-invoke" functions on databoxes.
    JSUI.FireDelegate = function (arrFunctions) {
        for (var i = 0; i < arrFunctions.length; i++) {
            if (JSUI.IsFunction(arrFunctions[i])) {
                if (!arrFunctions[i]())
                    return false; // this will allow the processing of an invocation if a preinvoke returns false
            }
        }
    }

    JSUI.stackTrace = function () {
        var callstack = [];
        var isCallstackPopulated = false;
        try {
            i.dont.exist += 0; //doesn't exist- that's the point
        } catch (e) {
            if (e.stack) { //Firefox
                var lines = e.stack.split('\n');
                for (var i = 0, len = lines.length; i < len; i++) {
                    if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                        callstack.push(lines[i]);
                    }
                }
                //Remove call to stackTrace()
                callstack.shift();
                isCallstackPopulated = true;
            }
            else if (window.opera && e.message) { //Opera
                var lines = e.message.split('\n');
                for (var i = 0, len = lines.length; i < len; i++) {
                    if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                        var entry = lines[i];
                        //Append next line also since it has the file info
                        if (lines[i + 1]) {
                            entry += " at " + lines[i + 1];
                            i++;
                        }
                        callstack.push(entry);
                    }
                }
                //Remove call to stackTrace()
                callstack.shift();
                isCallstackPopulated = true;
            }
        }
        if (!isCallstackPopulated) { //IE and Safari
            var currentFunction = arguments.callee.caller;
            while (currentFunction) {
                var fn = currentFunction.toString();
                var fname = fn.substring(fn.indexOf("function") + 8, fn.indexOf('')) || 'anonymous';
                callstack.push(fname);
                currentFunction = currentFunction.caller;
            }
        }
        return callstack;
    }


    JSUI.Exception = function (msg) {
        this.name = "JSUI.Exception";
        this.message = msg === null || msg === undefined || msg == "" ? "An Exception was thrown." : msg;
        this.message += "\r\n***** stack *****\r\n" + JSUI.stackTrace() + "\r\n***** end stack *****\r\n";
        this.toString = function () {
            return this.message;
        }
        if (jsLogger !== undefined && jsLogger !== null) {
            jsLogger.addErrorEntry({ msgSig: this.message });
        }
    }

    JSUI.throwException = function (msg) {
        throw new JSUI.Exception(msg);
    }

    JSUI.ThrowException = function (msg) {
        JSUI.throwException(msg);
    }

    JSUI.throwIfNull = function (o, msg) {
        if (typeof (o) == 'undefined')
            JSUI.ThrowException(msg);
    }

    JSUI.isNullOrUndef = function (o) {
        return (o === null || o === undefined);
    }

    JSUI.throwIfNullOrUndef = function (o, msg) {
        msg = JSUI.isNullOrUndef(msg) ? "The specified object was null or undefined" : msg;
        JSUI.isNullOrUndef(o) ? JSUI.throwException(msg) : "";
        return JSUI;
    }

    JSUI.ThrowIfNull = function (o, msg) {
        JSUI.throwIfNull(o, msg);
    }

    JSUI.GetDaysInMonth = function (month, year) {
        var dd = new Date(year, month, 0);
        return dd.getDate();
    }

    JSUI.getEvent = function (event) {
        var e = event;
        if (window.event) {
            e = window.event;
        }
        return e;
    }

    JSUI.GetEvent = function (e) {
        return JSUI.getEvent(e);
    }

    JSUI.getEventSource = function (event) {
        var e = JSUI.GetEvent(event);
        var returnElement;

        if (e.srcElement)
            returnElement = e.srcElement; // IE

        if (e.target)
            returnElement = e.target; // Firefox

        if (returnElement.nodeType != null && returnElement.nodeType != 'undefined' && returnElement.nodeType == 3)
            returnElement = returnElement.parentNode; //safari bug return the parent of the text node if text node was the srcElement/target

        return returnElement;
    }

    JSUI.GetEventSourceElement = function (evt) {
        return JSUI.getEventSource(evt);
    }

    JSUI.GetWebDirectory = function () {
        var path = document.location.href.toString().split("?")[0];
        return path.substr(0, path.lastIndexOf('/') + 1);
    }

    JSUI.GetFilePath = function () {
        return window.location.protocol + "//" + window.location.host + window.location.pathname;
    }

    JSUI.silarize = function (objectWithPowers, silar) {
        for (power in objectWithPowers) {
            if (JSUI.IsFunction(objectWithPowers[power])) {
                silar[power] = objectWithPowers[power];
            }
        }
    }

    JSUI.burglarize = function (objectWithProperties, burglar) {
        for (property in objectWithProperties) {
            if (!JSUI.IsFunction(objectWithProperties[property])) {
                burglar[property] = objectWithProperties[property];
            }
        }
    }

    JSUI.Silarize = function (o, s) {
        JSUI.silarize(o, s);
    }

    JSUI.initProperties = function (src, target, val, transForm) {
        for (prop in src) {
            if (JSUI.isFunction(transForm)) {
                transForm(prop, src[prop]);
            }
            target[prop] = val;
        }
    }

    JSUI.Burglarize = function (o, b) {
        JSUI.burglarize(o, b);
    }

    JSUI.Assimilate = function (target) {
        JSUI.Silarize(target, JSUI);
        JSUI.Burglarize(target, JSUI);
    }

    JSUI.assimilate = function (src, dst) {
        if (dst === null || dst === undefined) {
            dst = JSUI;
        }
        JSUI.silarize(src, dst);
        JSUI.burglarize(src, dst);
    }

    JSUI.write = function (el, msg) {
        el = JSUI.getElement(el);
        el.appenChild(document.createTextNode(msg));
    }
    JSUI.FunctionIsInArray = function (funcPointer, arr) {
        // should probably do a JSUI.IsFunctionOrDie here.  Review further later.
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] === funcPointer) {
                return true;
            }
        }
        return false;
    }

    JSUI.isFunction = function (varToTest, boolThrowIfFalse) {
        if (JSUI.isNullOrUndef(varToTest))
            return false;
        if (typeof varToTest != 'function' && boolThrowIfFalse) {
            JSUI.ThrowException(varToTest.toString() + " is not a function");
        } else if (typeof varToTest != 'function') {
            return false;
        }

        return true;
    }

    JSUI.IsFunction = JSUI.isFunction;

    JSUI.classOf = function (o) {
        if (o === undefined) return "undefined";
        if (o === null) return "null";
        return {}.toString.call(o).slice(8, -1);
    }

    JSUI.isArray = function (o, boolThrowIfFalse) {
        var retVal = JSUI.classOf(o) == "Array";
        if (!retVal && boolThrowIfFalse) {
            JSUI.ThrowException(o.toString() + " is not an Array");
        } else if (!retVal) {
            return false;
        }

        return true;
    }

    JSUI.isArrayOrDie = function (o) {
        return JSUI.isArray(o, true);
    }

    JSUI.forEach = function (arr, callBack) {
        if (JSUI.isArray(arr) && arr.length == 0)
            return;
        for (prop in arr) {
            callBack(prop, arr[prop]);
        }
    }

    JSUI.isFunctionOrDie = function (funcPointer) {
        this.IsFunction(funcPointer, true);
    }

    JSUI.IsFunctionOrDie = function (funcPointer) {
        return JSUI.isFunctionOrDie(funcPointer);
    }

    JSUI.isNumber = function (str) {
        if (JSUI.isFunction(str))
            return false;
        else if (JSUI.isObject(str))
            return false;

        return (parseInt(str) != NaN);
    }

    JSUI.IsNumber = function (str) {
        return JSUI.isNumber(str);
    }

    JSUI.isObject = function (o) {
        return JSUI.classOf(o) == "Object";
    }

    JSUI.isObjectOrDie = function (o, msg) {
        if (msg === null || msg === undefined) {
            msg = o.constructor.toString() + " is not an Object";
        }
        if (!JSUI.isObject(o)) {
            throw new JSUI.Exception(msg);
        }
    }

    JSUI.isString = function (o) {
        return JSUI.classOf(o) == "String";
    }

    JSUI.isStringOrDie = function (o) {
        if (!JSUI.isString(o))
            throw new JSUI.Exception(o.toString() + " is not a string.");
    }

    JSUI.isNumberOrDie = function (num, msg) {
        var ret = JSUI.isNumber(num);
        if (!ret)
            JSUI.ThrowException(num.toString() + " is not a number" + msg !== null ? ":" + msg : "");
        return ret;
    }

    JSUI.copyProperties = function (src, dst) {
        for (prop in src) {
            if (dst[prop] !== undefined && dst[prop] !== null) {
                dst[prop] = src[prop];
            }
        }
    }

    JSUI.copyUnsetProperties = function (src, dst) {
        for (prop in src) {
            if (dst[prop] == undefined || dst[prop] == null) {
                dst[prop] = src[prop];
            }
        }
    }

    JSUI.isTrueOrDie = function (test) {
        if (!test)
            JSUI.throwException("Value was not true");
    }

    JSUI.toDictionary = function (keyProp, objArr) {
        JSUI.isArrayOrDie(objArr);
        var retVal = {};
        JSUI.forEach(objArr, function (i, value) {
            retVal[value[keyProp].toString()] = value;
        });
        return retVal;
    }

    JSUI.countKeys = function (o) {
        var retval = 0;
        for (prop in o) {
            retval++;
        }

        return retval;
    }

    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, "");
    }

    String.prototype.ltrim = function () {
        return this.replace(/^\s+/, "");
    }

    String.prototype.rtrim = function () {
        return this.replace(/\s+$/, "");
    }

    String.prototype.startsWith = function (str) {
        return (this.match("^" + str) == str);
    }

    String.prototype.endsWith = function (str) {
        return (this.match(str + "$") == str);
    }

    String.prototype.checkIfBlank = function () {
        if (this.trim() == "") {
            return "&nbsp;";
        }
        return this;
    }

    String.prototype.isBlank = function () {
        return this.checkIfBlank() == "&nbsp;";
    }

    String.prototype.pascalCase = function (dlmtr) {
        var split = this.split(dlmtr);
        var retVal = "";
        for (var i = 0; i < split.length; i++) {
            var word = split[i];
            for (var ii = 0; ii < word.length; ii++) {
                if (i != 0 && ii == 0) {
                    retVal += word.charAt(ii).toString().toUpperCase();
                } else {
                    retVal += word.charAt(ii);
                }
            }
        }

        return retVal.toString();
    }

    String.prototype.keyValuesToObject = function (entrySep, kvSep) {
        var entries = this.split(entrySep);
        var retVal = {};
        JSUI.forEach(entries, function (k, v) {
            var keyValue = v.split(kvSep);
            JSUI.isTrueOrDie(keyValue.length == 2);
            retVal[keyValue[0]] = keyValue[1];
        });
        return retVal;
    }

    JSUI.arrayContains = function (arrayToCheck, value) {
        for (var i = 0; i < arrayToCheck.length; i++) {
            if (arrayToCheck[i] == value)
                return true;
        }

        return false;
    }

    JSUI.ArrayContains = function (arrayToCheck, value) {
        return JSUI.arrayContains(arrayToCheck, value);
    }

    JSUI.RandomLetter = function () {
        var chars = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
        return chars[Math.floor(Math.random() * 26)]
    }

    JSUI.RandomNumber = function () {
        var nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];
        return nums[Math.floor(Math.random() * 10)];
    }

    JSUI.RandomBool = function () {
        return Math.random() > .5;
    }

    JSUI.randomString = function (intLength) {
        var ret = "";
        for (var i = 0; i < intLength; i++) {
            if (JSUI.RandomBool()) {
                ret += JSUI.RandomLetter();
            } else {
                ret += JSUI.RandomLetter().toUpperCase();
            }
        }
        return ret;
    }

    JSUI.GetRandomId = function (ln) {
        return JSUI.randomString(ln);
    }

    JSUI.RandomString = function (intLength) {
        return JSUI.GetRandomId(intLength);
    }


    var addStack = function (str) {
        return str + "\r\n***** stack *****\r\n" + JSUI.stackTrace() + "\r\n***** end stack *****\r\n";
    }
    JSUI.logInfo = function (msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addInfoEntry("JSUI.logInfo:: " + msg);
        }
    }

    JSUI.logWarn = function (msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addWarnEntry("JSUI.logWarn:: " + msg);
        }
    }

    JSUI.logError = function (msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            msg = msg.toString(); // in case it's an object
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addErrorEntry("JSUI.logError:: " + msg);
        }
    }
}

JSUI = window.JSUI;


JSUI.Ctors = {};

JSUI.addConstructor = function (strClassName, ctor) {
    JSUI.Ctors[strClassName] = ctor;
};

JSUI.construct = function (strClassName, arrParams) {
    return new JSUI.Ctors[strClassName](arrParams);
};

JSUI.addConstructor("Conf", function () {
    this.LoadedScripts = {};
    this.ScriptReadyListeners = {};

    var thisObj = this;

    this.scriptReady = function (id, scrName) {
        thisObj.LoadedScripts[scrName] = true;
        var lstnr = thisObj.ScriptReadyListeners[id];
        delete lstnr.waitingfor[scrName]
        if (JSUI.countKeys(lstnr.waitingfor) == 0) {
            lstnr.callBack(lstnr.scripts);
            delete thisObj.ScriptReadyListeners[id];
        }
    }

    this.addScriptReadyListener = function (listener) {
        JSUI.isObjectOrDie(listener);
        if (thisObj.ScriptReadyListeners[listener.id] === null || thisObj.ScriptReadyListeners[listener.id] === undefined) {
            thisObj.ScriptReadyListeners[listener.id] = listener;
        }
        JSUI.forEach(listener.waitingfor, function (key, value) {
            var path = window.location.toString().split("?")[0] + "?script=" + key;
            path += encodeURI("&cb=JSUI.Conf.scriptReady('" + listener.id + "', '" + key + "');");
            thisObj.writeScriptTag(path);
        });
    }

    this.writeScriptTag = function (srcUrl) {
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        script.setAttribute("src", srcUrl);
        JSUI.GetDocumentBody().appendChild(script);
    };
});

JSUI.Conf = JSUI.construct("Conf");

JSUI.Conf.usingResource = function (arrScripts, uid, callBack) {
    JSUI.isArrayOrDie(arrScripts);
    if (JSUI.isFunction(uid))
        callBack = uid, uid = JSUI.randomString(4);
    var readyListener = { scripts: arrScripts, callBack: callBack, waitingfor: {}, id: uid };
    var pending = false;
    JSUI.forEach(arrScripts, function (key, value) {
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

JSUI.Assimilate(JSUI.Conf); //, JSUI);


function JsonProxy() { this.multiInvoke = false; };
JsonProxy.prototype.Extends = function (baseConstructor) {
    var base = new baseConstructor();
    for (prop in base) {
        this[prop] = base[prop];
    }
}

JsonProxy.prototype.RunningMethods = {};

JsonProxy.prototype.InvokeMethodEx = function (methodName, arrParams, options, callBack) {
    if (this.MethodEndpoints[methodName] === null ||
       this.MethodEndpoints[methodName] === undefined) {
        JSUI.ThrowException("Method end point not defined.");
    }

    var config = { format: "json", template: "default", target: null };
    if (JSUI.isFunction(options) && (callBack === null || callBack === undefined))
        callBack = options;
    else
        JSUI.assimilate(options, config);

    if (config.format == "html")
        config.format = "box";
    if (config.format == "box" && config.target == null) {
        throw new JSUI.Exception("target must be specified for html requests.");
    }
    config.clientkey = JSUI.GetRandomId(8);

    var endpoint = this.MethodEndpoints[methodName];
    var verb = this.MethodVerbs[methodName];
    var postParams = "";

    if (JSUI.isArray(arrParams)) {
        endpoint += "&params=";
        for (var i = 0; i < arrParams.length; i++) {
            if (verb == "GET") {
                endpoint += encodeURIComponent(arrParams[i]);
                if (i != arrParams.length - 1)
                    endpoint += encodeURIComponent("$#%"); // constant defined in JavascriptServer "ParameterDelimiter"
            } else { // verb == "POST"
                postParams += JSON.stringify(arrParams[i]) + "$#%";
            }
        }
    }
    postParams = postParams == "" ? null : postParams; // required, stringify needs an empty string, but this must be null if there are no parameters

    endpoint += "&ienocache=" + JSUI.GetRandomId(8);
    endpoint += "&f=" + config.format;
    endpoint += "&dbx=" + config.template;
    endpoint += "&ck=" + config.clientkey;

    if (this.RunningMethods[methodName] !== undefined && this.MethodEndpoints[methodName].multiInvoke == false) {
        this.RunningMethods[methodName].abort();
    }

    var request = JSUI.NewHttpRequest();
    var proxy = this;
    var format = config.format;

    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            var text = request.responseText;
            if (format == "box") {
                config.target.innerHTML = text;
                jQuery('script[language=javascript]', config.target).each(function (k, script) {
                    eval(script.text);
                });
                DataBox.getScripts(config.clientkey);
                if (JSUI.isFunction(callBack)) {
                    callBack(config.target);
                }
                return;
            }

            if (callBack) {
                if (format == "json") {
                    callBack(eval("(" + text + ")"));
                    return;
                }
                throw new JSUI.Exception("Invalid response format specified.");
            }
        }
    }
    request.open(verb, encodeURI(endpoint), true);
    request.send(postParams);

    this.RunningMethods[methodName] = request;
}

JsonProxy.prototype.InvokeMethod = function (methodName, arrParams, callBack, format) {
    if (this.MethodEndpoints[methodName] == null ||
       this.MethodEndpoints[methodName] == 'undefined') {
        JSUI.ThrowException("Method end point not defined.");
    }

    if (!format)
        format = this.ResponseFormat;

    var endpoint = this.MethodEndpoints[methodName];
    var verb = this.MethodVerbs[methodName];
    var postParams = "";

    if (arrParams != null && arrParams != 'undefined') {
        endpoint += "&params=";
        for (var i = 0; i < arrParams.length; i++) {
            if (verb == "GET") {
                endpoint += encodeURIComponent(arrParams[i]);
                if (i != arrParams.length - 1)
                    endpoint += encodeURIComponent("$#%"); // constant defined in JavascriptServer "ParameterDelimiter"
            } else { // verb == "POST"
                postParams += JSON.stringify(arrParams[i]) + "$#%";
            }
        }
    }
    postParams = postParams == "" ? null : postParams;

    endpoint += "&ienocache=" + JSUI.GetRandomId(8);
    var append = "&f=json";
    if (format == "box" && this.DataTemplate) {
        append = "&f=" + this.ResponseFormat; // set in jsoncallback.js
        append += "&dbx=" + this.DataTemplate;
        append += "&ck=" + this.ClientKey;
    } else {
        format = "json";
    }

    endpoint += append;
    if (this.RunningMethods[methodName] !== undefined && this.MethodEndpoints[methodName].multiInvoke == false) {
        this.RunningMethods[methodName].abort();
    }

    var request = JSUI.NewHttpRequest();
    var proxy = this;

    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            var text = request.responseText;
            if (callBack) {
                if (format == "json") {
                    callBack(eval("(" + text + ")"));
                    return;
                }

                if (format == "box") {
                    callBack(text);
                    return;
                }

                throw new JSUI.Exception("Invalid response format specified.");
            }
        }
    }
    request.open(verb, encodeURI(endpoint), true);
    request.send(postParams);

    this.RunningMethods[methodName] = request;
}

var ProxyUtil = {};
ProxyUtil.Proxies = {};
ProxyUtil.ProxiesByType = {};

ProxyUtil.AddServerProxy = function (proxyName, proxyInstance, throwIfAlreadyRegistered) {
    if (!ProxyUtil.Proxies[proxyName]) {
        ProxyUtil.Proxies[proxyName] = proxyInstance;
    } else if (throwIfAlreadyRegistered) {
        JSUI.ThrowException(proxyName + " has already been registered.");
    }

    if (!ProxyUtil.ProxiesByType[proxyInstance.type]) {
        ProxyUtil.ProxiesByType[proxyInstance.type] = proxyInstance;
    } else if (throwIfAlreadyRegistered) {
        JSUI.ThrowException(proxyInstance.type + " has already been registered.");
    }
}

ProxyUtil.getProxy = function (locator, throwIfNull) {
    var proxy = ProxyUtil.getProxyByName(locator);
    if (proxy == null)
        proxy == ProxyUtil.getProxyByType(locator);

    if (proxy == null && throwIfNull) {
        JSUI.ThrowException("Proxy of type or name " + locator + " was not registered with the JSUI framework.");
    }
    return proxy;
}

ProxyUtil.getProxyByName = function (name) {
    if (ProxyUtil.Proxies[name] !== null && ProxyUtil.Proxies[name] !== undefined)
        return ProxyUtil.Proxies[name];

    return null;
}

ProxyUtil.getProxyByType = function (type) {
    if (ProxyUtil.ProxiesByType[type] !== null && ProxyUtil.ProxiesByType[type] !== undefined)
        return ProxyUtil.ProxiesByType[type];

    return null;
}

ProxyUtil.GetProxy = function (name, tin) {
    return ProxyUtil.getProxy(name, tin);
}

JSUI.getJson = function (el) {
    el = JSUI.getElement(el);
    var ret = {};
    jQuery("jsonproperty", el).each(function (key, value) {
        var name = value.getAttribute("jsonproperty");
        var value = value.getAttribute("value");
        retVal[name] = value;
    });

    return ret;
}
var JsonResponseFormats = {};
JsonResponseFormats.json = "json";
JsonResponseFormats.box = "box";

// This class represents the client side "proxy" for the JsonCallback control
JSUI.addConstructor("CallbackSubscriberInfo",
        function (arrParams) {
            var strCallbackJsonId = arrParams[0];
            var strClientInstanceName = arrParams[1];
            var strProxyType = arrParams[2];
            var strMethodName = arrParams[3];
            var funcCallback = arrParams[4];
            var preInvokeFuncJsonId = arrParams[5];
            JSUI.IsFunctionOrDie(funcCallback);
            this.SubscriberId = strCallbackJsonId;
            this.Proxy = JSUI.GetProxy(strClientInstanceName, false);
            this.ProxyName = strClientInstanceName;
            this.ProxyType = strProxyType;
            this.MethodName = strMethodName;
            this.CallbackFunction = funcCallback;
            this.PreInvokeFunctionJsonId = preInvokeFuncJsonId;
        });

// Objects of this type will be placed into the callback queue
// when the requested proxy hasn't been loaded yet.
JSUI.addConstructor("MethodInvokeInfo",
        function (ctorParams) {
            var strSubscriberId = ctorParams[0];
            var strProxyName = ctorParams[1];
            var strMethodName = ctorParams[2];
            var arrParams = ctorParams[3];
            var funcCallback = ctorParams[4];
            var objDataRequestInfo = ctorParams[5];
            if (funcCallback)
                JSUI.IsFunctionOrDie(funcCallback);
            this.SubscriberId = strSubscriberId; // this is used specifically for JsonCallback controls    
            this.ProxyName = strProxyName;
            this.MethodName = strMethodName;
            this.Params = arrParams;
            this.RequestInfo = objDataRequestInfo;
            this.Callback = funcCallback;
        });

ProxyUtil.HandleCallbackError = function (jsonResult) {
    if (jsonResult.Status == "Error") {
        alert(jsonResult.ErrorMessage);
        return true;
    }
    return false;
}


ProxyUtil.CallbackSubscribers = {}; // subscribers should be JsonFunction classes on the server identified by their JsonId.
ProxyUtil.RegisterCallbackSubscriber = function (strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId) {
    JSUI.IsFunctionOrDie(funcCallback); // this will be checked by the constructor but having an error here will bring me more quickly to this line.
    var callBackInfo = JSUI.construct("CallbackSubscriberInfo", [strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId]);
    ProxyUtil.CallbackSubscribers[strCallbackJsonId] = callBackInfo;
}

ProxyUtil.ProxyMethodQueue = {};
ProxyUtil.EnQueue = function (clsMethodInvokeInfo) {
    var proxyName = clsMethodInvokeInfo.ProxyName;
    var methodName = clsMethodInvokeInfo.MethodName;


    if (!ProxyUtil.ProxyMethodQueue[proxyName])
        ProxyUtil.ProxyMethodQueue[proxyName] = {};

    if (!ProxyUtil.ProxyMethodQueue[proxyName][methodName]) {
        ProxyUtil.ProxyMethodQueue[proxyName][methodName] = [];
        ProxyUtil.ProxyMethodQueue[proxyName][methodName].push(clsMethodInvokeInfo);

    }
}

ProxyUtil.DeQueue = function (strProxyName) {
    var proxy = JSUI.GetProxy(strProxyName);
    var proxyName = proxy.Name;
    for (methodName in ProxyUtil.ProxyMethodQueue[proxyName]) {
        var methodDataArray = ProxyUtil.ProxyMethodQueue[proxyName][methodName];
        while (methodDataArray.length > 0) {
            var methodInvokeInfo = methodDataArray.shift();
            var subscriberId = methodInvokeInfo.SubscriberId;
            var params = methodInvokeInfo.Params;
            var requestInfo = methodInvokeInfo.RequestInfo;
            ProxyUtil.CallbackSubscribers[subscriberId].Proxy = proxy;
            ProxyUtil.InvokeMethodWithCallback(subscriberId, params, requestInfo);
        }
    }
}

JSUI.addConstructor("DataRequestInfo", function DataRequestInfo(arrParams) {
    var jsonResponseFormat = arrParams[0];
    var strDataTemplateName = arrParams[1];
    this.InvokeOnce = arrParams[2];
    this.ResponseFormat = jsonResponseFormat;
    this.DataTemplate = strDataTemplateName;
    this.ClientKey = JSUI.RandomString(8);

});

ProxyUtil.InvokeMethodWithCallback = function (strSubscriberId, arrParams, objDataRequestInfo) {
    var callBackInfo = ProxyUtil.CallbackSubscribers[strSubscriberId];
    JSUI.ThrowIfNull(callBackInfo, "JsonControl '" + strSubscriberId + "' was not registered as a callback subscriber.");
    var preInvokeFunction = JSUI.Functions[callBackInfo.PreInvokeFunctionJsonId];
    var methodName = callBackInfo.MethodName;
    var proxy = ProxyUtil.GetProxy(callBackInfo.ProxyName); //callBackInfo.Proxy;
    var proxyName = callBackInfo.ProxyName;
    var callBackFunction = callBackInfo.CallbackFunction;

    if (!proxy) {
        ProxyUtil.EnQueue(JSUI.construct('MethodInvokeInfo', [strSubscriberId, proxyName, methodName, arrParams, callBackFunction, objDataRequestInfo]));
        ProxyUtil.EnsureProxy(callBackInfo.ProxyName, callBackInfo.ProxyType);
    } else {
        if (preInvokeFunction) {
            if (preInvokeFunction() == false) {
                return;
            }
        }
        proxy.ResponseFormat = objDataRequestInfo.ResponseFormat;
        proxy.DataTemplate = objDataRequestInfo.DataTemplate;
        proxy.ClientKey = objDataRequestInfo.ClientKey;
        proxy.InvokeMethod(methodName, arrParams, function (resultHTMLOrJson) {

            if (objDataRequestInfo.ResponseFormat == JsonResponseFormats.json) {
                if (ProxyUtil.HandleCallbackError(resultHTMLOrJson)) {
                    return;
                } else {
                    callBackFunction(resultHTMLOrJson.Data);
                }
            }

            if (objDataRequestInfo.ResponseFormat == JsonResponseFormats.box) {
                var html = resultHTMLOrJson;
                var clientKey = objDataRequestInfo.ClientKey;
                callBackFunction(html, clientKey);
            }
        });
    }
}

ProxyUtil.Ensuring = {};
ProxyUtil.proxyReadyListeners = {}; // functions keyed by "varname" waiting for that proxy to become available

ProxyUtil.ensureProxies = function () {
    var missingVarnames = [];
    if (!JSUI.isNullOrUndef(JSUI.providerInfo)) {
        JSUI.forEach(JSUI.proxyReadyListeners, function (varname, fn) {
            var providerInfo = JSUI.providerInfo[varname];
            if (!JSUI.isNullOrUndef(providerInfo)) {
                JSUI.ensureProxy(providerInfo.VarName, providerInfo.ClientTypeName);
            } else {
                missingVarnames.push(varname);
            }
        });
    }
    if (missingVarnames.length > 0) {
        ProxyUtil.onProxyReady("jsLogger", function (inf) {
            jsLogger.addErrorEntry({ msgSig: "The provider for one or more proxies were not registered on the server: {0}", values: [missingVarnames.toString()] });
        });
    }
}

ProxyUtil.onProxyReady = function (varname, fn) {
    JSUI.isStringOrDie(varname);
    JSUI.isFunctionOrDie(fn);
    var proxy = ProxyUtil.getProxy(varname, false);
    if (!JSUI.isNullOrUndef(proxy)) {
        fn({ varname: varname, type: proxy.type, proxy: proxy });
    } else {
        if (JSUI.isNullOrUndef(ProxyUtil.proxyReadyListeners[varname]))
            ProxyUtil.proxyReadyListeners[varname] = [];

        ProxyUtil.proxyReadyListeners[varname].push(fn);
    }
}

ProxyUtil.proxyReady = function (proxyInfo) {
    var listeners = ProxyUtil.proxyReadyListeners[proxyInfo.varname];
    proxyInfo.proxy = ProxyUtil.getProxy(proxyInfo.varname);
    if (!JSUI.isNullOrUndef(listeners)) {
        while (listeners.length > 0) {
            var fn = listeners.shift();
            fn(proxyInfo);
        }
    }
}

ProxyUtil.ensureProxy = function (strProxyName, type, strCallback) {
    if (ProxyUtil.Ensuring[strProxyName])
        return;
    ProxyUtil.Ensuring[strProxyName] = true;

    if (JSUI.isNullOrUndef(strCallback))
        strCallback = "ProxyUtil.DeQueue('" + strProxyName + "')";

    var proxy = JSUI.GetProxy(strProxyName);
    if (!proxy) {
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = window.location.toString().split("?")[0] + "?init=true&t=" + type + "&varname=" + strProxyName
        path += "&cb=ProxyUtil.proxyReady({varname:'" + strProxyName + "', type: '" + type + "'});" + strCallback;
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
    }
}

ProxyUtil.EnsureProxy = ProxyUtil.ensureProxy;
JSUI.Assimilate(ProxyUtil);
JSUI.Conf.usingProxy = ProxyUtil.onProxyReady;
jQuery(document).ready(JSUI.ensureProxies);


var DOMMgr = {};

DOMMgr.GetElementByAttributeValue = function (strTagFilter, strAttributeName, strAttributeValue) {
    var retVal = jQuery(strTagFilter + '[' + strAttributeName + '=' + strAttributeValue + ']');
    if (retVal.length > 1)
        throw new JSUI.Exception("More than one element found with the attribute '" + strAttributeName + "' having a value of '" + strAttributeValue + "'");

    return retVal[0];

}

DOMMgr.GetElementsByAttributeValue = function (topElementOrId, strAttributeName, strAttributeValue) {
    return jQuery('[' + strAttributeName + '=' + strAttributeValue + ']', topElementOrId);

}

DOMMgr.RegisteredJsonTags = [];
DOMMgr.RegisterJsonTag = function (strTagName) {
    if (!JSUI.ArrayContains(DOMMgr.RegisteredJsonTags, strTagName))
        DOMMgr.RegisteredJsonTags.push(strTagName);
}

DOMMgr.ScriptObjects = {};
DOMMgr.RegisterScriptObject = function (strName, objScriptObject) {
    DOMMgr.ScriptObjects[strName] = objScriptObject;
}

DOMMgr.elementIs = function (objElement, strTagNameToCheck) {
    return objElement.tagName.toLowerCase() == strTagNameToCheck;
}

DOMMgr.ElementIs = function (el, tag) {
    return DOMMgr.elementIs(el, tag);
}
DOMMgr.GetJsonElement = function (strJsonId, optionalTagNameFilter) {
    if (DOMMgr.ScriptObjects[strJsonId])
        return DOMMgr.ScriptObjects[strJsonId];

    var tagFilter = optionalTagNameFilter ? optionalTagNameFilter : null;
    var tagFilters = [];
    if (!tagFilter) {
        tagFilters.concat(DOMMgr.RegisteredJsonTags);
        for (var i = 0; i < DOMMgr.RegisteredJsonTags.length; i++) {
            tagFilters.push(DOMMgr.RegisteredJsonTags[i]);
        }
    } else {
        tagFilters.push(tagFilter);
    }
    for (var i = 0; i < tagFilters.length; i++) {
        var element = DOMMgr.GetElementByAttributeValue(tagFilters[i], "jsonid", strJsonId);
        if (element)
            return element;
    }
    var el = jQuery('[jsonid=' + strJsonId + ']')[0];
    if (!JSUI.isNullOrUndef(el))
        return el;

    throw new JSUI.Exception("JsonElement with id '" + strJsonId + "' was not found.");
}

DOMMgr.getElement = function (strElementOrId) {
    if (!JSUI.isString(strElementOrId))
        return strElementOrId;
    var element = document.getElementById(strElementOrId);
    if (element)
        return element;
    else {
        return DOMMgr.GetJsonElement(strElementOrId);
    }
}

DOMMgr.GetElement = function (el) {
    return DOMMgr.getElement(el);
}

DOMMgr.DisableTextSelect = function (elementOrId) {
    if (elementOrId === undefined || elementOrId === null)
        elementOrId = document.body;
    var element = DOMMgr.GetElement(elementOrId);
    if (element.onselectstart != null && element.onselectstart != 'undefined') { //IE
        element.onselectstart = function () { return false; }
    } else if (element.style.MozUserSelect != null && element.style.MozUserSelect != 'undefined') {
        element.style.MozUserSelect = 'none';
    }
}

DOMMgr.GetElementsByClassName = function (elementOrId, strClassName) {
    var element = DOMMgr.GetElement(elementOrId);
    var childElements = element.getElementsByTagName("*");
    var retVal = [];
    for (var i = 0; i < childElements.length; i++) {
        if (childElements[i].className == strClassName)
            retVal.push(childElements[i]);
    }

    return retVal;
}

DOMMgr.GetElementPosition = function (elementOrId) {
    var element = DOMMgr.GetElement(elementOrId);
    var curleft = curtop = 0;
    if (element.offsetParent) {
        curleft = element.offsetLeft
        curtop = element.offsetTop
        while (element = element.offsetParent) {
            curleft += element.offsetLeft
            curtop += element.offsetTop
        }
    }
    return { Left: curleft, Top: curtop };
}

DOMMgr.GetElementsByAttribute = function (strAttributeName) {
    var allElements = document.body.getElementsByTagName('*');
    var retVal = [];
    for (var i = 0; i < allElements.length; i++) {
        if (allElements[i].getAttribute(strAttributeName))
            retVal.push(allElements[i]);
    }

    return retVal;
}

DOMMgr.GetElementsWithAttribute = function (parentElementOrId, strAttributeName) {
    var parentElement = DOMMgr.GetElement(parentElementOrId);
    var allChildElements = parentElement.getElementsByTagName('*');

    var retVal = [];
    for (var i = 0; i < allChildElements.length; i++) {
        var element = allChildElements[i];
        if (element.getAttribute(strAttributeName))
            retVal.push(element);
    }

    return retVal;
}

DOMMgr.GetInnerText = function (elementOrId) {
    var ele = DOMMgr.GetElement(elementOrId);
    if (document.all) {
        return ele.innerText;
    }
    else {
        return ele.textContent;
    }
}

DOMMgr.GetTextContent = function (elementOrId) {
    return DOMMgr.GetInnerText(elementOrId);
}

DOMMgr.setText = function (elementOrId, text) {
    //try{
    var ele = DOMMgr.GetElement(elementOrId);
    ele.innerText = text;
    ele.textContent = text;
    //}catch(e){}// added try catch for IE
}

DOMMgr.SetInnerText = function (el, text) {
    DOMMgr.setText(el, text);
}

DOMMgr.SetTextContent = function (elementorId, text) {
    DOMMgr.SetInnerText(elementorId, text);
}

DOMMgr.SetElementAsFirstChild = function (strNewParentDomId, strTargetDomId) {
    var child = DOMMgr.GetElement(strTargetDomId);
    var parent = DOMMgr.GetElement(strNewParentDomId);

    child.parentNode.removeChild(child);
    if (parent.hasChildNodes)
        parent.insertBefore(child, parent.firstChild);
    else
        parent.appendChild(child);
}

DOMMgr.insertAfter = function (parent, toInsert, reference) {
    parent.insertBefore(toInsert, reference.nextSibling);
}

DOMMgr.SetElementAsLastChild = function (strNewParentDomId, strTargetDomId) {
    var child = DOMMgr.GetElement(strTargetDomId);
    var parent = DOMMgr.GetElement(strNewParentDomId);

    parent.appendChild(child);
}

DOMMgr.show = function (elementOrId, options) {
    var config = { fade: false, display: 'block', end: 10, fps: 12 };
    JSUI.copyProperties(options, config);
    if (Effects !== null && Effects !== undefined && config.fade) {
        Effects.fadeIn(elementOrId, config);
    } else {
        JSUI.showElement(elementOrId, config.display);
    }
}

DOMMgr.hide = function (el, options) {
    var config = { fade: false, display: 'block', end: 0, fps: 12 };
    JSUI.copyProperties(options, config);
    if (Effects !== null && Effects !== undefined && config.fade) {
        Effects.fadeOut(el, config);
    } else {
        DOMMgr.hideElement(el);
    }
}

DOMMgr.hideElement = function (el) {
    DOMMgr.showElement(el, 'none');
}

DOMMgr.showElement = function (elementOrId, displayValue) {
    if (displayValue === null || displayValue === undefined) {
        displayValue = 'block';
    }
    DOMMgr.GetElement(elementOrId).style.display = displayValue;
}

DOMMgr.ShowElement = function (el, v) {
    DOMMgr.showElement(el, v);
}

DOMMgr.ShowElementInline = function (elementOrId) {
    DOMMgr.GetElement(elementOrId).style.display = 'inline';
}

DOMMgr.ShowElementBlock = function (elementOrId) {
    DOMMgr.GetElement(elementOrId).style.display = 'block';
}

DOMMgr.mouseIsOverElement = function (elementOrId, event) {
    var element = DOMMgr.GetElement(elementOrId);
    var e = event ? event : window.event;

    var outHorizontally = false;
    var outVertically = false;
    var body = JSUI.GetDocumentBody();
    if (e.clientX > (DOMMgr.getElementWidth(element.id) + parseInt(element.offsetLeft)))
        outHorizontally = true;
    if (e.clientX < parseInt(element.offsetLeft))
        outHorizontally = true;
    if (e.clientY > (DOMMgr.getElementHeight(element.id) + parseInt(element.offsetTop - body.scrollTop)))
        outVertically = true;
    if (e.clientY < parseInt(element.offsetTop - body.scrollTop))
        outVertically = true;

    if (!outHorizontally && !outVertically)
        return true;
    else
        return false;
}

DOMMgr.MouseIsOverElement = function (el, ev) {
    return DOMMgr.mouseIsOverElement(el, ev);
}

DOMMgr.HideElement = function (elementOrId) {
    DOMMgr.GetElement(elementOrId).style.display = 'none';
}

DOMMgr.MakeElementInvisible = function (elementOrId) {
    DOMMgr.GetElement(elementOrId).style.visibility = "hidden";
}

DOMMgr.MakeElementVisible = function (elementOrId) {
    DOMMgr.GetElement(elementOrId).style.visibility = "visible";
}

DOMMgr.ToggleElementDisplay = function (strElementOrId, strInlineOrBlock) {
    if (!strInlineOrBlock)
        strInlineOrBlock = "block";

    var targetElement = DOMMgr.GetElement(strElementOrId);
    var display = targetElement.style.display;
    var show = true;
    if (display == null || display == 'undefined' || display != "none")
        show = false;

    if (show)
        DOMMgr.ShowElement(targetElement, strInlineOrBlock);
    else
        DOMMgr.HideElement(targetElement);
}

DOMMgr.toggle = function (el, styleName, value) {
    var cur = el[stylName];
    var newVal = value;
    if (JSUI.isNullOrUndef(el.toggle))
        el.toggle = {};
    if (JSUI.isNullOrUndef(el.toggle[styleName])) {
        el.toggle[styleName] = cur;
        newVal = value;
    } else {
        newVal = el.toggle[styleName];
    }
    el[styleName] = newVal;
    return JSUI;
}


DOMMgr.getStyleNum = function (elementOrId, styleName, defaultValue) {
    if (defaultValue === null || defaultValue === undefined) {
        defaultValue = -1;
    }

    JSUI.isNumberOrDie(defaultValue);
    var ele = DOMMgr.GetElement(elementOrId);
    if (ele.style[styleName] != "") {
        var trimNum = 2; // px;
        if (ele.style[styleName].toString().endsWith("%")) {
            trimNum = 1;
        }
        var retVal = ele.style[styleName].substr(0, ele.style[styleName].length - trimNum);
        JSUI.isNumberOrDie(retVal);
        return parseInt(retVal);
    } else {
        return defaultValue;
    }
}

DOMMgr.getStyleUnit = function (elementOrId, styleName) {
    var ele = DOMMgr.GetElement(elementOrId);
    if (ele.style[styleName] != "") {
        if (ele.style[styleName].toString().endsWith("%"))
            return "%";

        if (ele.style[styleName].toString().endsWith("px"))
            return "px";

        if (ele.style[styleName].toString().endsWith("pt"))
            return "pt";
    }
    return "px";
}


DOMMgr.setStyleNum = function (elementOrId, styleName, value) {
    styleName = styleName.pascalCase("-");
    var ele = DOMMgr.GetElement(elementOrId);
    var unit = DOMMgr.getStyleUnit(ele, styleName);
    ele.style[styleName] = value + unit;
}

DOMMgr.incrementStyle = function (elementOrId, styleName, incrementValue) {
    JSUI.isNumberOrDie(incrementValue);
    styleName = styleName.pascalCase("-");
    var element = DOMMgr.GetElement(elementOrId);

    if (styleName.toLowerCase().endsWith("color")) {
        var currentColor = JSUI.Color.from(element.style[styleName]);
        var newColor = JSUI.Color.from(currentColor);
        //        JSUI.Assimilate(currentColor, newColor);
        var newR = currentColor.R + incrementValue;
        var newG = currentColor.G + incrementValue;
        var newB = currentColor.B + incrementValue;
        if (newR >= 255) newR = 255;
        if (newR <= 0) newR = 0;
        if (newG >= 255) newG = 255;
        if (newG <= 0) newG = 255;
        if (newB >= 255) newB = 255;
        if (newB <= 0) newB = 255;

        newColor.R = newR;
        newColor.G = newG;
        newColor.B = newB;
        var htmlColor = JSUI.Color.from(newColor).toString();
        element.style[styleName] = htmlColor;
    } else {
        //        var unit = DOMMgr.getStyleUnit(elementOrId, styleName);
        var oldValue = DOMMgr.getStyleNum(element, styleName, incrementValue);
        var newValue = oldValue + incrementValue;
        DOMMgr.setStyleNum(element, styleName, newValue);
    }
}

DOMMgr.decrementStyle = function (elementOrId, styleName, decrementValue) {
    DOMMgr.incrementStyle(elementOrId, styleName, -decrementValue);
}

DOMMgr.getElementWidth = function (elementOrId, defaultValue) {
    return DOMMgr.getStyleNum(elementOrId, "width", defaultValue);
}

DOMMgr.GetElementWidth = function (e, dv) {
    return DOMMgr.getElementWidth(e, dv);
}

DOMMgr.getElementHeight = function (elementOrId, defaultValue) {
    return DOMMgr.getStyleNum(elementOrId, "height", defaultValue);
}

DOMMgr.getDimensions = function (el) {
    return { height: DOMMgr.getElementHeight(el), width: DOMMgr.getElementWidth(el) };
}

DOMMgr.GetElementHeight = function (e, dv) {
    return DOMMgr.getElementHeight(e, dv);
}

DOMMgr.DisableElement = function (strElementOrId) {
    DOMMgr.GetElement(strElementOrId).disabled = true;
}

DOMMgr.EnableElement = function (strElementOrId) {
    DOMMgr.GetElement(strElementOrId).disabled = false;
}

DOMMgr.getDocumentBody = function () {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}

DOMMgr.GetDocumentBody = function () {
    return DOMMgr.getDocumentBody();
}

DOMMgr.createSelect = function (arr, options) {
    JSUI.isArrayOrDie(arr);
    var config = { value: "value", text: "text" };
    JSUI.assimilate(options, config);
    var select = document.createElement("select");
    JSUI.forEach(arr, function (key, value) {
        if (!JSUI.isNullOrUndef(value[config.value])) {
            var option = document.createElement("option");
            option.setAttribute("value", value[config.value].toString());
            option.appendChild(document.createTextNode(value[config.text]));
            select.appendChild(option);
        }
    });

    return select;
}

DOMMgr.createCheckBoxes = function (arr, options) {
    JSUI.isArrayOrDie(arr);
    var config = { value: "value", text: "text", cssclass: "" };
    JSUI.assimilate(options, config);
    var ret = [];
    JSUI.forEach(arr, function (key, value) {
        var span = document.createElement('span');
        span.setAttribute("class", config.cssclass);
        span.id = JSUI.randomString(8);
        var chk = document.createElement('input');
        chk.id = JSUI.randomString(8);
        chk.setAttribute("type", "checkbox");
        chk.setAttribute("value", value[config.value]);

        span.appendChild(chk);
        var label = document.createElement('label');
        label.setAttribute('for', chk.id);
        label.appendChild(document.createTextNode(value[config.text]));

        span.appendChild(label);
        ret.push(span);
    });

    return ret;
}

JSUI.Assimilate(DOMMgr);

var Events = {};

Events.EventSources = {};
Events.RegisterEventSource = function (strName, objScriptObject) {
    Events.EventSources[strName] = objScriptObject;
    JSUI.RegisterScriptObject(strName, objScriptObject);
}

Events.GetEventSource = function (strSourceName) {
    return Events.EventSources[strSourceName];
}

Events.FireEvent = function (strSourceName, strEventName, arrArgs) {
    if (Events.EventSources[strSourceName]) {
        var eventSource = Events.EventSources[strSourceName];
        if (eventSource[strEventName]) {
            JSUI.CallFunctions(eventSource[strEventName], strEventName, arrArgs);
        }
    }
}

Events.OnWindowLoadFunctions = [];
Events.OnWindowUnloadFunctions = [];
Events.OnWindowResizeFunctions = [];
Events.OnWindowScrollFunctions = [];

Events.AttachedEvents = []; // required for MSAjaxInterop.js

Events.AttachedEventClass = function (objElement, funcPointer, strEventName) {
    this.Element = objElement;
    this.Function = funcPointer;
    this.EventName = strEventName;
}

Events.AddEventHandler = function (objectToAttachTo, funcPointer, strEventName) {
    objectToAttachTo = JSUI.GetElement(objectToAttachTo);
    var ieFunc = null;
    if (objectToAttachTo.addEventListener)
        objectToAttachTo.addEventListener(strEventName, funcPointer, false);
    else if (objectToAttachTo.attachEvent) {
        ieFunc = function () { funcPointer.call(objectToAttachTo) };
        objectToAttachTo.attachEvent("on" + strEventName, ieFunc);
    }
    else {
        if (!objectToAttachTo[strEventName])
            objectToAttachTo[strEventName] = [];
        objectToAttachTo[strEventName].push(funcPointer);
    }
    Events.AttachedEvents.push(new Events.AttachedEventClass(objectToAttachTo, ieFunc == null ? funcPointer : ieFunc, strEventName));
}

Events.addEventHandler = function (o, f, n) {
    Events.AddEventHandler(o, f, n);
}

Events.FireElementEvent = function (strElementOrId, strEventName, args) {
    objectToFire = JSUI.GetElement(strElementOrId);
    if (objectToFire[strEventName] && JSUI.IsFunction(objectToFire[strEventName]))
        objectToFire[strEventName](args);
    if (objectToFire["on" + strEventName] && JSUI.IsFunction(objectToFire["on" + strEventName]))
        objectToFire["on" + strEventName](args);

    throw new JSUI.Exception("Unable to find the specified event " + strEventName + " on element " + strElementOrId);
}


Events.RemoveEventHandler = function (objectToDetachFrom, funcPointer, strEventName) {
    objectToDetachFrom = JSUI.GetElement(objectToDetachFrom);
    if (objectToDetachFrom.removeEventListener)
        objectToDetachFrom.removeEventListener(strEventName, funcPointer, false);
    else if (objectToDetachFrom.detachEvent) {
        for (var i = 0; i < Events.AttachedEvents.length; i++) {
            var ev = Events.AttachedEvents[i];
            if (ev.Element == objectToDetachFrom && ev.EventName == strEventName) {
                funcPointer = ev.Function;
            }
        }
        objectToDetachFrom.detachEvent("on" + strEventName, funcPointer);
    }
    else
        objectToDetachFrom[strEventName] = function () { };
}

Events.clearEventHandlers = function (el, evtName) {
    JSUI.forEach(Events.AttachedEvents, function (key, value) {
        if (value.Element.id == el.id && value.EventName == evtName) {
            Events.RemoveEventHandler(value.Element, value.Function, value.EventName);
        }
    });
}

Events.OnWindowLoad = function () {
    for (var i = 0; i < Events.OnWindowLoadFunctions.length; i++) {
        Events.OnWindowLoadFunctions[i]();
    }
}

Events.OnWindowScroll = function () {
    for (var i = 0; i < Events.OnWindowScrollFunctions.length; i++) {
        Events.OnWindowScrollFunctions[i]();
    }
}

Events.OnWindowUnload = function () {
    for (var i = 0; i < Events.OnWindowUnloadFunctions.length; i++) {
        Events.OnWindowUnloadFunctions[i]();
    }
}

Events.OnWindowResize = function () {
    for (var i = 0; i < Events.OnWindowResizeFunctions.length; i++) {
        Events.OnWindowResizeFunctions[i]();
    }
}

Events.addOnWindowScroll = function (funcPointer) {
    JSUI.IsFunctionOrDie(funcPointer);
    if (!JSUI.ArrayContains(Events.OnWindowScrollFunctions, funcPointer)) {
        Events.OnWindowScrollFunctions.push(funcPointer);
        window.onscroll = Events.OnWindowScroll;
    }
}

Events.addOnWindowLoad = function (funcPointer) {
    JSUI.IsFunctionOrDie(funcPointer);
    if (!JSUI.ArrayContains(Events.OnWindowLoadFunctions, funcPointer)) {
        Events.OnWindowLoadFunctions.push(funcPointer);
        window.onload = Events.OnWindowLoad;
    }
}

Events.addOnWindowUnload = function (funcPointer) {
    JSUI.IsFunctionOrDie(funcPointer);
    if (!JSUI.ArrayContains(Events.OnWindowUnloadFunctions, funcPointer)) {
        Events.OnWindowUnloadFunctions.push(funcPointer);
        window.onunload = Events.OnWindowUnload;
    }
}

Events.addOnWindowResize = function (funcPointer) {
    JSUI.IsFunctionOrDie(funcPointer);
    if (!JSUI.ArrayContains(Events.OnWindowResizeFunctions, funcPointer)) {
        Events.OnWindowResizeFunctions.push(funcPointer);
        window.onresize = Events.OnWindowResize;
    }
}

Events.AbortAsyncRequests = function () {
    if (JSUI.HttpRequests.length > 0) {
        for (var i = 0; i < JSUI.HttpRequests.length; i++) {
            JSUI.HttpRequests[i].abort();
        }
    }
}

Events.addOnWindowUnload(Events.AbortAsyncRequests);

JSUI.Assimilate(Events, JSUI);


JSUI.addConstructor("Color", function () {
    var hexNums = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"];
    var self = this;

    var toHex = function (rgOrb) {
        if (rgOrb < 0 || rgOrb > 255)
            throw new JSUI.Exception("Value must be from 0 to 255: " + rgOrb);

        var left = hexNums[Math.floor(rgOrb / 16)];
        var right = hexNums[rgOrb % 16];
        return left + right;
    }

    var toRGBInt = function (hexColor) {
        var hexColor = hexColor.toString();
        if (hexColor.length != 2 ||
            (hexColor.charAt(0).toUpperCase() > "F" || hexColor.charAt(1).toUpperCase() > "F"))
            throw new JSUI.Exception("Value must be a valid 2 character hex value 00 to FF");

        return parseInt(hexColor, 16);
    }

    var color = function (color) {
        this.Html = "";
        this.R = 255;
        this.G = 255;
        this.B = 255;

        var self = this;
        this.enact = function () {
            if (this.R > 255) this.R = 255;
            if (this.R < 0) this.R = 0;
            if (this.G > 255) this.G = 255;
            if (this.G < 0) this.G = 0;
            if (this.B > 255) this.B = 255;
            if (this.B < 0) this.B = 0;
            this.Html = "#" + toHex(this.R) + toHex(this.G) + toHex(this.B);
        }

        this.toString = function () {
            return this.Html;
        }

        this.toRGBString = function () {
            return 'rgb(' + this.R + ', ' + this.G + ', ' + this.B + ')';
        }

        this.moveToward = function (colorTwo, step) {
            return JSUI.Color.moveToward(self, colorTwo, step);
        }

        this.setColor = function (elementOrId, colorStyle) {
            var element = JSUI.GetElement(elementOrId);
            element.style[colorStyle] = this.toString();
        }

        this.setAnimationStyle = function (elementOrId, styleName) {
            self.setColor(elementOrId, styleName);
        }
        // init
        if (JSUI.isString(color) && color.startsWith("rgb")) {
            var values = color.toString().replace(/rgb\(/, '').replace(/\)/, '');
            var splitValues = values.split(",");
            var retVal = {};
            for (var i = 0; i < splitValues.length; i++) {
                JSUI.isNumberOrDie(splitValues[i]);
            }

            this.R = parseInt(splitValues[0]);
            this.G = parseInt(splitValues[1]);
            this.B = parseInt(splitValues[2]);
            this.enact();

        } else if (JSUI.isString(color) && (color.startsWith("#") || color.length == 6)) {
            // html color #000000
            if (color.startsWith("#"))
                color = color.substr(1, color.length - 1);
            this.R = toRGBInt(color.substr(0, 2));
            this.G = toRGBInt(color.substr(2, 2));
            this.B = toRGBInt(color.substr(4, 2));
            this.Html = "#" + color;
        } else if (JSUI.isObject(color)) {
            // color object
            JSUI.copyProperties(color, this);
            this.enact();
        }
    }

    this.from = function (c) {
        return new color(c);
    }

    this.moveToward = function (colorOne, colorTwo, step) {
        if (step === null || step === undefined)
            step = 10;

        var stepUp = step;
        var stepDown = -step;

        var clrOne = new color(colorOne);
        var clrTwo = new color(colorTwo);

        var rStep = clrTwo.R >= clrOne.R ? stepUp : -stepDown;
        var gStep = clrTwo.G >= clrOne.G ? stepUp : -stepDown;
        var bStep = clrTwo.B >= clrOne.B ? stepUp : -stepDown;

        clrOne.R = clrOne.R + rStep;
        if (rStep == stepUp && clrOne.R > clrTwo.R)
            clrOne.R = clrTwo.R;
        else if (rStep == stepDown && clrOne.R < clrTwo.R)
            clrOne.R = clrTwo.R;

        clrOne.G = clrOne.G + gStep;
        if (gStep == stepUp && clrOne.G > clrTwo.G)
            clrOne.G = clrTwo.G;
        else if (gStep == stepDown && clrOne.G < clrTwo.G)
            clrOne.G = clrTwo.G;

        clrOne.B = clrOne.B + bStep;
        if (bStep == stepUp && clrOne.B > clrTwo.B)
            clrOne.B = clrTwo.B;
        else if (bStep == stepDown && clrOne.B < clrTwo.B)
            clrOne.B = clrTwo.B;

        clrOne.enact();
        return clrOne;
    }
});

var Effects = {};
Effects.ImageSequenceClassInstances = {};

Effects.Color = JSUI.construct("Color");

Effects.setMouseCursor = function (elementOrId, strCursor) {
    var cursorName;
    if (strCursor == 'hand' || strCursor == 'pointer') {
        Effects.handCursor(elementOrId);
    } else {
        JSUI.GetElement(elementOrId).style.cursor = strCursor;
    }
    return JSUI;
}

Effects.SetMouseCursor = function (el, c) {
    Effects.setMouseCursor(el, c);
}

Effects.handCursor = function (elementOrId) {
    var pointer = "pointer";
    if (window.event)
        pointer = "hand";
    JSUI.GetElement(elementOrId).style.cursor = pointer;
    return JSUI;
}

Effects.SetHandCursor = function (el) {
    Effects.handCursor(el);
}

Effects.moveCursor = function (el) {
    return Effects.setMouseCursor(el, "move");
}

Effects.SetMoveCursor = function (elementOrId) {
    Effects.moveCursor(elementOrId);
}

Effects.defaultCursor = function (el) {
    return Effects.setMouseCursor(el, "default");
}

Effects.SetDefaultCursor = function (elementOrId) {
    Effects.setMouseCursor(elementOrId, "default");
}

Effects.ImageSwapify = function (strFromImageElementOrId, imageName) {
    if (!JSUI.Images[imageName] || !JSUI.Images[imageName + "over"]) {
        throw new JSUI.Exception("The images for the requested image swap were not preloaded.  Use JSUI.PreLoadImage('" + imageName + "', <src path>) and JSUI.PreLoadImage('" + strToImageName + "over', <src path>)");
    }

    var imageElement = JSUI.GetElement(strFromImageElementOrId);
    JSUI.AddEventHandler(imageElement,
        function () {
            imageElement.src = JSUI.Images[imageName + "over"].src;
        },
        "mouseover"
    )

    JSUI.AddEventHandler(imageElement,
        function () {
            imageElement.src = JSUI.Images[imageName].src;
        },
        "mouseout"
    )
}

Effects.GetImageSequence = function (strDomId) {
    return Effects.ImageSequenceClassInstances[strDomId];
}

/// This is intended to be an ImageSequence constructor
Effects.ImageSequenceClass = function (strDomId, boolClickThrough) {
    Effects.ImageSequenceClassInstances[strDomId] = this;
    var thisObj = this;
    var currentIndex = 0;
    var targetImg = JSUI.GetElement(strDomId);
    JSUI.handCursor(targetImg);

    this.Images = [];
    this.AddImage = function (strImageName, strSrc) {
        var imgObj = JSUI.PreLoadImage(strImageName, strSrc, false);
        thisObj.Images.push(imgObj);
    }

    this.Next = function () {
        currentIndex++;
        if (currentIndex == thisObj.Images.length)
            currentIndex = 0;
        return thisObj.Images[currentIndex];
    }

    this.ShowImage = function (strImageName) {
        var image = JSUI.Images[strImageName];
        targetImg.src = image.src;
    }

    this.CurrentIndex = function () {
        return currentIndex;
    }

    this.SetImageIndex = function (intIndex) {
        var image = thisObj.Images[intIndex];
        targetImg.src = image.src;
    }

    var showNext = function () {
        var nextImage = thisObj.Next();
        targetImg.src = nextImage.src;
    }

    this.EnableClick = function () {
        JSUI.AddEventHandler(targetImg,
            showNext,
            "click"
        )
    }

    this.DisableClick = function () {
        JSUI.removeEventHandler(targetImg, showNext, "click");
    }

    this.ShowNext = showNext;

    if (boolClickThrough) {
        this.EnableClick();
    }
}

Effects.setOpacity = function (elementOrId, oneToTen) {
    JSUI.isNumberOrDie(oneToTen);
    var value = parseInt(oneToTen);
    //    if (value < 1 || value > 10)
    //        throw new JSUI.Exception("Invalid opacity value specified (" + oneToTen + "), must be from 1 to 10.");
    var element = JSUI.GetElement(elementOrId);
    element.style.opacity = value / 10;
    element.style.filter = 'alpha(opacity=' + value * 10 + ')';
    return JSUI;
}

Effects.SetOpacity = function (elementOrId, oneToTen) {
    Effects.setOpacity(elementOrId, oneToTen);
}

JSUI.addConstructor("Fader", function () {
    // fader ctor
    var fader = function (options) {
        JSUI.throwIfNull(options.element, "options.element can't be null");
        targetElement = options.element;
        this.dir = 1;
        this.end = 10;
        this.fps = 12;
        this.speed = 1;
        this.timeout = 1000 / this.fps;
        this.callBack = null;
        this.start = 0;
        this.next = 0;

        JSUI.copyProperties(options, this);
        var self = this;
        this.enact = function () {
            JSUI.isNumberOrDie(self.end);
            JSUI.isNumberOrDie(self.fps);
            JSUI.isNumberOrDie(self.dir);
            JSUI.isNumberOrDie(self.start);
            JSUI.isNumberOrDie(self.next);
            if (self.end > 10 || self.end < 0)
                throw new JSUI.Exception("Invalid end opacity specified");

            if (self.dir != -1 && self.dir != 1)
                throw new JSUI.Exception("Invalid direction specified 1=in, -1=out");

            if (self.dir == -1) {
                self.start = 10;
                self.next = 10;
            }

            if (self.dir == 1) {
                self.start = 0;
                self.next = 0;
            }

            self.timeout = (1000 / self.fps) / self.speed;
        }

        var handle = null;
        this.doFade = function () {
            Effects.setOpacity(targetElement, self.next);
            if (handle !== null)
                clearTimeout(handle);

            if (!(self.next == self.end)) {
                self.next += self.dir;
                handle = setTimeout(self.doFade, self.timeout);
            } else {
                if (JSUI.isFunction(self.callBack))
                    self.callBack(targetElement);
            }
        }
    }
    // end fader ctor
    function getConfig(elementOrId, options) {
        var defaults = {
            element: JSUI.GetElement(elementOrId),
            fps: 12,
            end: 10,
            start: 0,
            speed: 1,
            callBack: function () { }
        }
        if (JSUI.isObject(options))
            JSUI.copyProperties(options, defaults);
        else if (JSUI.isNumber(options))
            defaults.fps = options;
        else if (JSUI.isFunction(options))
            defaults.callBack = options;

        return defaults;
    }

    this.fadeIn = function (elementOrId, options) {
        var defaults = getConfig(elementOrId, options);
        var f = new fader(defaults);
        f.enact();
        f.doFade();
        return JSUI;
    }

    this.fadeOut = function (elementOrId, options) {//fps, callBack) {
        var defaults = getConfig(elementOrId, options);
        defaults.dir = -1;
        defaults.start = 10;
        defaults.end = 0;
        var f = new fader(defaults);
        f.enact();
        f.doFade();
        return JSUI;
    }


});

Effects.Fader = JSUI.construct("Fader");
JSUI.Assimilate(Effects.Fader); //, JSUI);

Effects.transition = function (elementOrId, endStyles, options) {//fps, toggle) {
    var config = { fps: 12, toggle: false, callBack: function () { }, speed: 1 };
    if (JSUI.isNumber(options)) {
        config.fps = options;
    } else if (JSUI.isFunction(options)) {
        config.callBack = options;
    } else if (JSUI.isObject(options)) {
        JSUI.copyProperties(options, config);
    }

    var targetElement = JSUI.GetElement(elementOrId);
    targetElement.maxSize = {};
    var timeout = (1000 / config.fps) / config.speed;
    var callBack = config.callBack;

    if (config.toggle !== null && config.toggle !== undefined && config.toggle == true) {
        if (targetElement.toggleStart === null || targetElement.toggleStart === undefined) {
            targetElement.toggleStart = {};
            for (prop in endStyles) {
                if (prop.toString().toLowerCase().endsWith("color")) {
                    targetElement.toggleStart[prop] = JSUI.Color.from(targetElement.style[prop]);
                } else {
                    targetElement.toggleStart[prop] = JSUI.getStyleNum(targetElement, prop);
                }
            }
            targetElement.toggleEnd = endStyles;
        }
        endStyles = targetElement.toggleEnd;
    }

    var swapToggle = function (key) {
        var start = targetElement.toggleStart[key];
        var end = targetElement.toggleEnd[key];
        targetElement.toggleStart[key] = end;
        targetElement.toggleEnd[key] = start;
    }

    var isDone = function (doneKeys) {
        for (prop in doneKeys) {
            if (doneKeys[prop] == false)
                return false;
        }
        return true;
    }

    var doneKeys = {};
    JSUI.initProperties(endStyles, doneKeys, false, function (key, val) {
        targetElement.maxSize[key] = endStyles[key] > JSUI.getStyleNum(targetElement, key) ? endStyles[key] : JSUI.getStyleNum(targetElement, key);
    });
    var handle = null;
    var doTransition = function () {
        JSUI.forEach(endStyles, function (key, value) {
            key = key.pascalCase("-");
            if (doneKeys[key] != true) {
                if (key.toString().toLowerCase().endsWith("color")) {
                    var colorValue = JSUI.Color.from(value);
                    var newColor = JSUI.Color.from(targetElement.style[key]).moveToward(colorValue);
                    newColor.setColor(targetElement, key);
                    if (newColor.toString() == colorValue.toString()) {
                        if (config.toggle == true) {
                            swapToggle(key);
                        }
                        doneKeys[key] = true;
                    }
                } else {
                    var curVal = JSUI.getStyleNum(targetElement, key, value);
                    var endVal = value;
                    var changeNum = targetElement.maxSize[key] / config.fps;
                    var stepUp = changeNum;
                    var stepDown = -changeNum;
                    var step = curVal >= endVal ? stepDown : stepUp;

                    var newVal = (curVal * 1) + (step * 1);

                    if ((step == stepUp && newVal >= endVal) ||
                    step == stepDown && newVal <= endVal) {
                        JSUI.setStyleNum(targetElement, key, endVal);
                        if (config.toggle == true)
                            swapToggle(key);
                        doneKeys[key] = true;
                    } else {
                        JSUI.setStyleNum(targetElement, key, newVal);
                    }
                }
            }
        });

        if (handle !== null)
            clearTimeout(handle);

        if (!isDone(doneKeys)) {
            handle = setTimeout(doTransition, timeout);
        } else {
            if (config.callBack !== null) {
                config.callBack(targetElement);
            }
        }
    }

    doTransition();
}

JSUI.Assimilate(Effects); //, JSUI);


var Scrollables = {};
Scrollables.makeScrollable = function (strElementId, dimensions) {
    var scrollable = new ScrollableElement(strElementId, dimensions);
    Scrollables[strElementId] = scrollable;
    return scrollable;
}

Scrollables.MakeScrollable = function (el, dims) {
    return Scrollables.makeScrollable(el, dims);
}

Scrollables.GetScrollable = function (strElementId) {
    if (Scrollables[strElementId]) {
        return Scrollables[strElementId];
    }
    return null;
}

function ScrollableElement(strElementId, dimensions) {
    this.TargetElement = null; //set later placed here for visibility
    this.Width = null;   //set later placed here for visibility
    this.WidthString = "";
    this.Height = null;  //set later placed here for visibility
    this.HeightString = "";
    this.BorderWidth = 1;
    this.ScrollContainer = null;
    this.Head = null;
    this.Parent = null; // this is the outer div containing all the elements making the scrollable, may be null if target is not a table
    this.ItemTag = "tr"; // used to set the height of each "item".  The default is table row because this is intended to scrollify a table
    //but if we were scrollifying a div this could be changed to the tagname of whatever contains the "items" in the div.
    this.ItemHeight = 15;

    var thisObj = this;

    var height = 200;
    var width = 200;
    var headHeight = 15;
    if (dimensions) {
        height = dimensions.Height ? parseInt(dimensions.Height) : height;
        width = dimensions.Width ? parseInt(dimensions.Width) : width;
    }

    this.Width = width;
    this.Height = height;

    var targetElement = JSUI.GetElement(strElementId); //document.getElementById(strElementId);
    targetElement.style.width = "100%";
    this.TargetElement = targetElement;

    var head = targetElement.getElementsByTagName("thead")[0];
    var newHeadTable = null;

    if (head) {
        var headID = "scroll_head" + JSUI.RandomString(8);
        newHeadTable = document.createElement("table");
        newHeadTable.style.border = "1px";
        newHeadTable.setAttribute("id", headID);
        newHeadTable.setAttribute("cellspacing", "0");
        newHeadTable.setAttribute("cellpadding", "0");
        newHeadTable.style.width = "100%";

        targetElement.removeChild(head);

        newHeadTable.appendChild(head);
        targetElement.parentNode.insertBefore(newHeadTable, targetElement);
        this.Head = newHeadTable;
    } else {
        headHeight = 0;
    }

    function setColumnWidths(table, arrWidths, strTagName) {
        var rows = table.getElementsByTagname("tr");
        if (rows != 'undefined') {
            var firstRow = rows[0];
            var cells = firstRow.getElementsByTagName(strTagName);
            for (var i = 0; i < arrWidths.length - 1; i++) {
                cells[i].width = arrWidths[i] + "px"; // sets the width of all but the last column to prevent them from snapping back to original widths
            }
        }
    }

    this.SetColumnWidths = function (arrWidths) {
        if (newHeadTable) {
            setColumnWidths(newHeadTable, arrWidths, "th");
        }

        setColumnWidths(targetElement, arrWidths, "td");

        return thisObj;
    }

    this.SetHeadHeight = function (intHeight) {
        if (thisObj.Head) {
            thisObj.Head.style.height = intHeight + "px";
            headHeight = intHeight;
            thisObj.ScrollContainer.style.height = (thisObj.Height - headHeight) + "px";
        }

        return thisObj;
    }

    this.SetItemHeight = function (intHeight) {
        var items = thisObj.ScrollContainer.getElementsByTagName(thisObj.ItemTag);
        for (var i = 0; i < items.length; i++) {
            items[i].style.height = intHeight + "px";
        }

        return thisObj;
    }

    this.SetBorderStyle = function (strBorderStyle) {
        var ele;
        if (thisObj.Parent) {
            ele = JSUI.GetElement(thisObj.Parent.id);
        }
        else {
            ele = JSUI.GetElement(thisObj.ScrollContainer.id);
        }
        ele.style.borderStyle = strBorderStyle;
        return thisObj;
    }

    this.SetBorderWidth = function (intBorderWidth) {
        if (thisObj.Parent) {
            JSUI.GetElement(thisObj.Parent.id).style.borderWidth = intBorderWidth + "px";
        } else {
            JSUI.GetElement(thisObj.ScrollContainer.id).style.borderWidth = intBorderWidth + "px";
        }

        thisObj.BorderWidth = intBorderWidth;

        JSUI.GetElement(thisObj.ScrollContainer.id).style.height = (thisObj.Height - headHeight) - intBorderWidth + "px";

        return thisObj;
    }

    this.SetBorderColor = function (strBorderColor) {
        if (thisObj.Parent)
            JSUI.GetElement(thisObj.Parent.id).style.borderColor = strBorderColor;
        else
            JSUI.GetElement(thisObj.ScrollContainer.id).style.borderColor = strBorderColor;

        return thisObj;
    }

    this.SetWidth = function (width) {
        if (!JSUI.IsNumber(width))
            return;

        thisObj.Width = parseInt(width);

        if (width.toString().trim().endsWith("%") || width.toString().trim().endsWith("px")) {
            thisObj.WidthString = width;
        } else {
            thisObj.WidthString = width + "px";
        }

        if (thisObj.Parent) {
            thisObj.Parent.style.width = thisObj.WidthString;
            thisObj.ScrollContainer.style.width = "100%";
        } else {
            thisObj.ScrollContainer.style.width = thisObj.Width - (thisObj.BorderWidth * 2) + "px";
        }
    }

    this.SetHeight = function (height) {
        if (!JSUI.IsNumber(height))
            return;

        thisObj.Height = parseInt(height);

        if (height.toString().trim().endsWith("%") || height.toString().trim().endsWith("px")) {
            thisObj.HeightString = height;
        }
        else {
            thisObj.HeightString = height + "px";
        }

        if (thisObj.Parent)
            thisObj.Parent.style.height = thisObj.HeightString;

        thisObj.ScrollContainer.style.height = (thisObj.Height - headHeight) - thisObj.BorderWidth + "px";

        return thisObj;
    }

    var scrollContainer;

    if (JSUI.ElementIs(targetElement, "div")) {
        scrollContainer = targetElement;
    } else {
        scrollContainer = document.createElement("div");
        var scrollID = "scroll_container_" + JSUI.RandomString(8);
        scrollContainer.setAttribute("id", scrollID);

        targetElement.parentNode.insertBefore(scrollContainer, targetElement.nextSibling);
        var parent = document.createElement("div");
        parent.setAttribute("id", "scroll_outerdiv_" + JSUI.RandomString(8));

        targetElement.parentNode.insertBefore(parent, targetElement.nextSibling);
        targetElement.parentNode.removeChild(targetElement);
        scrollContainer.appendChild(targetElement);
        if (this.Head) {
            this.Head.parentNode.removeChild(this.Head);
            this.Head.style.width = "100%";
            parent.appendChild(this.Head);
        }

        parent.appendChild(scrollContainer);
        parent.style.width = this.Width + "px";
        parent.style.height = this.Height + "px";
        this.Parent = parent;
    }

    scrollContainer.style.width = "100%";
    scrollContainer.style.height = (this.Height - headHeight) + "px";
    scrollContainer.style.overflow = "auto";

    this.ScrollContainer = scrollContainer;

    this.SetHeadHeight(headHeight);
}


JSUI.Assimilate(Scrollables); //, JSUI);


JSUI.Orientation = {};
JSUI.Orientation.normalize = function (retVal) {
    if (retVal.Top < 0) {
        retVal.Top = 0;
    }

    if (retVal.Left < 0) {
        retVal.Left = 0;
    }

    retVal.top = retVal.Top;
    retVal.left = retVal.Left;
}

JSUI.Orientation.TopLeftMouse = function (elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height)) + body.scrollTop, Left: evt.clientX - (width) };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left += overlap;
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.TopRightMouse = function (elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height)) + body.scrollTop, Left: evt.clientX };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left -= overlap;
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomRightMouse = function (elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY + (height)) + body.scrollTop, Left: evt.clientX };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left -= overlap;
        retVal.Top -= overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomLeftMouse = function (elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY + (height)) + body.scrollTop, Left: evt.clientX - (width) };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left += overlap;
        retVal.Top -= overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.CenterMouse = function (elementOrId, evt) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height / 2)) + body.scrollTop, Left: evt.clientX - (width / 2) };

    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.TopLeft = function (toPosition, refEl, overlap) {
    var moving = JSUI.GetElement(toPosition);
    var movingDims = JSUI.getDimensions(moving);
    var position = JSUI.GetElementPosition(refEl);

    var retVal = {};
    retVal.Top = position.Top - movingDims.height;
    retVal.Left = position.Left - movingDims.width;
    if (overlap !== undefined && overlap !== null) {
        retVal.Left = retVal.Left + overlap;
        retVal.Top = retVal.Top + overlap;
    }
    JSUI.Orientation.normalize(retVal, overlap);
    return retVal;
}

JSUI.Orientation.TopRight = function (toPosition, refEl, overlap) {
    var moving = JSUI.GetElement(toPosition);
    var movingDims = JSUI.getDimensions(moving);
    var position = JSUI.GetElementPosition(refEl);

    var retVal = {};
    retVal.Top = position.Top - movingDims.height;
    retVal.Left = position.Left + movingDims.width;
    if (overlap !== null && overlap !== undefined) {
        retVal.Left = retVal.Left - overlap;
        retVal.Top = retVal.Top + overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomCenter = function (toPosition, refEl, overlap) {
    var height = JSUI.GetElementHeight(refEl);
    var refPosition = JSUI.GetElementPosition(refEl);
    var elToPositionDims = JSUI.getDimensions(toPosition);
    var refDims = JSUI.getDimensions(refEl);
    var refCenter = refPosition.Left + (refDims.width / 2);
    var retVal = {};
    retVal.Top = refPosition.Top + height;
    retVal.Left = refCenter - (elToPositionDims.width / 2);

    if (JSUI.isNumber(overlap)) {
        if (overlap !== null && overlap !== undefined) {
            retVal.Top -= overlap;
        }
    } else if (JSUI.isObject(overlap)) {
        if (overlap.left !== null && overlap.left !== undefined) {
            retVal.Left -= overlap.left;
        }
        if (overlap.top !== null && overlap.top !== undefined) {
            retVal.Top -= overlap.top;
        }
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.TopCenter = function (toPosition, refEl, overlap) {
    var refPosition = JSUI.GetElementPosition(refEl);
    var movingDims = JSUI.getDimensions(toPosition);
    var refCenter = refPosition.Left - refDims.width;
    var retVal = {};
    retVal.Top = refPosition.Top - movingDims.height;
    retVal.Left = refCenter - (movingDims.width / 2);

    if (overlap !== null && overlap !== undefined) {
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.BottomLeft = function (toPosition, refEl, overlap) {
    var height = JSUI.GetElementHeight(refEl);
    var position = JSUI.GetElementPosition(refEl);
    var elToPositionDims = JSUI.getDimensions(toPosition);
    var retVal = {};
    retVal.Top = position.Top + height;
    retVal.Left = position.Left - elToPositionDims.width;

    if (overlap !== null && overlap !== undefined) {
        retVal.Left = retVal.Left + overlap;
        retVal.Top = retVal.Top - overlap;
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.BottomRight = function (toPosition, refEl, overlap) {
    var height = JSUI.GetElementHeight(refEl);
    var position = JSUI.GetElementPosition(refEl);
    var refElDims = JSUI.getDimensions(refEl);
    var retVal = {};
    retVal.Top = position.Top + height;
    retVal.Left = position.Left + refElDims.width;

    if (overlap !== null && overlap !== undefined) {
        retVal.Left = retVal.Left - overlap;
        retVal.Top = retVal.Top - overlap;
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.CenterScreen = function (elementOrId, intPlusTop, intPlusLeft) {
    var retVal = {};
    var height = JSUI.GetElementHeight(elementOrId);
    var width = JSUI.GetElementWidth(elementOrId);

    var docBody = JSUI.GetDocumentBody();
    retVal.Top = ((docBody.clientHeight / 2) - (height / 2)) + docBody.scrollTop;
    retVal.Left = (docBody.clientWidth / 2) - (width / 2);

    if (intPlusTop != null && intPlusTop != 'undefined')
        retVal.Top = retVal.Top + intPlusTop;

    if (intPlusLeft != null && intPlusLeft != 'undefined')
        retVal.Left = retVal.Left + intPlusLeft;

    if (retVal.Top <= 0) {
        retVal.Top = 0;
    }

    if (retVal.Left <= 0) {
        retVal.Left = 0;
    }
    retVal.top = retVal.Top;
    retVal.left = retVal.Left;

    return retVal;
}

JSUI.Orientation.CenterPage = function (elementOrId, intPlusTop, intPlusLeft) {
    var retVal = {};
    var height = JSUI.GetElementHeight(elementOrId);
    var width = JSUI.GetElementWidth(elementOrId);

    var docBody = JSUI.GetDocumentBody();
    retVal.Top = (docBody.clientHeight / 2) - (height / 2);
    retVal.Left = (docBody.clientWidth / 2) - (width / 2);

    if (intPlusTop != null && intPlusTop != 'undefined')
        retVal.Top = retVal.Top + intPlusTop;

    if (intPlusLeft != null && intPlusLeft != 'undefined')
        retVal.Left = retVal.Left + intPlusLeft;

    if (retVal.Top <= 0) {
        retVal.Top = 0;
    }

    if (retVal.Left <= 0) {
        retVal.Left = 0;
    }

    retVal.top = retVal.Top;
    retVal.left = retVal.Left;
    return retVal;
}

JSUI.orient = function (toPosition, options) {
    var config = { toBePositioned: JSUI.GetElement(toPosition), reference: "", orientation: "CenterPage", event: "", overlap: 0 };
    JSUI.copyProperties(options, config);
    if (JSUI.isString(config.reference) && config.reference != "")
        config.reference = JSUI.GetElement(config.reference);
    var func = JSUI.Orientation[config.orientation];
    JSUI.isFunctionOrDie(func);
    var position = {};
    if (config.event !== null && config.event != "") {
        position = func(config.toBePositioned, config.event, config.overlap);
    } else {
        position = func(config.toBePositioned, config.reference, config.overlap, config.event);
    }
    config.toBePositioned.parentNode.removeChild(config.toBePositioned);
    JSUI.getDocumentBody().appendChild(config.toBePositioned);
    config.toBePositioned.style.position = 'absolute';
    config.toBePositioned.style.top = position.top + "px";
    config.toBePositioned.style.left = position.left + "px";
};

JSUI.Assimilate(JSUI.Orientation);

var BoxMgr = {};
BoxMgr.Images = {};

BoxMgr.createImageElement = function (strImageName) {
    if (!BoxMgr.Images[strImageName])
        BoxMgr.ThrowException(strImageName + " was not preloaded, use JSUI.PreLoadImage('" + strImageName + "', <src>) to load the image");

    var img = document.createElement("img");
    img.setAttribute("src", BoxMgr.Images[strImageName].src);
    return img;
}

BoxMgr.CreateImageElement = function (strImageName) {
    return BoxMgr.createImageElement(strImageName);
}

BoxMgr.preLoadImage = function (strName, strImageSrc, boolThrowIfLoaded) {
    if (BoxMgr.Images[strName]) {
        if (boolThrowIfLoaded == true) {
            JSUI.ThrowException("Image named " + strName + " has already been loaded, specify a different name to load a different image");
        } else {
            return BoxMgr.Images[strName];
        }
    }
    var image = new Image();
    image.src = strImageSrc;
    BoxMgr.Images[strName] = image;
    return image;
}

BoxMgr.PreLoadImage = function (name, src, throwIfLoaded) {
    return BoxMgr.preLoadImage(name, src, throwIfLoaded);
}


BoxMgr.Dialogs = {};
BoxMgr.IEBuggyControls = [];

BoxMgr.PreLoadImage("closebutton", "images/closebutton.png");
BoxMgr.PreLoadImage("closebuttonover", "images/closebuttonover.png");
BoxMgr.PreLoadImage("defaultbutton", "images/defaultbutton.gif");
BoxMgr.PreLoadImage("defaultbuttonover", "images/defaultbuttonover.gif");



// create a div that covers the whole screen to catch
// the mouse during a drag
BoxMgr.CreateScreenDiv = function () {
    var returnElement = document.createElement("div");
    returnElement.style.position = "absolute";
    returnElement.style.left = "0px";
    returnElement.style.top = "0px";
    returnElement.style.width = "100%";
    returnElement.style.height = "100%";
    returnElement.style.zIndex = -1;
    return returnElement;
}

BoxMgr.ModalScreen = BoxMgr.CreateScreenDiv();

BoxMgr.ModalScreen.Modalize = function (color) {
    if (!JSUI.isString(color))
        color = "#000000";
    BoxMgr.ModalScreen.style.backgroundColor = color;
    JSUI.SetOpacity(BoxMgr.ModalScreen, 5);
    JSUI.toFront(BoxMgr.ModalScreen);
}

BoxMgr.ModalScreen.DropBack = function (color) {
    if (!JSUI.isString(color))
        color = "#FFFFFF";
    BoxMgr.ModalScreen.style.backgroundColor = color;
    BoxMgr.ModalScreen.style.zIndex = -2;
    JSUI.SetOpacity(BoxMgr.ModalScreen, 0);

}

BoxMgr.AppendScreen = function () {
    document.body.appendChild(BoxMgr.ModalScreen);
    BoxMgr.SetScreenSize();
}

BoxMgr.ScreenSetter = null;
BoxMgr.SetScreenSize = function () {
    var ieBody = (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
    BoxMgr.ModalScreen.style.top = document.all ? ieBody.scrollTop + "px" : pageYOffset + "px";
    BoxMgr.ModalScreen.style.left = document.all ? ieBody.scrollLeft + "px" : pageXOffset + "px";
}

JSUI.addOnWindowScroll(BoxMgr.SetScreenSize);
JSUI.addOnWindowResize(BoxMgr.SetScreenSize);
JSUI.addOnWindowLoad(BoxMgr.AppendScreen);

BoxMgr.RegisterIEBuggyControl = function (idOrElement) {
    BoxMgr.IEBuggyControls.push(JSUI.GetElement(idOrElement));
}

BoxMgr.HideBuggyControls = function () {
    for (var i = 0; i < BoxMgr.IEBuggyControls.length; i++) {
        JSUI.HideElement(BoxMgr.IEBuggyControls[i]);
    }
}

BoxMgr.ShowBuggyControls = function () {
    for (var i = 0; i < BoxMgr.IEBuggyControls.length; i++) {
        JSUI.ShowElementInline(BoxMgr.IEBuggyControls[i]);
    }
}

BoxMgr.topZIndex = 99;
BoxMgr.getTopZIndex = function () {
    BoxMgr.topZIndex++;
    return BoxMgr.topZIndex;
}

BoxMgr.toFront = function (el) {
    el = JSUI.GetElement(el);
    el.style.zIndex = BoxMgr.getTopZIndex();
    return JSUI;
}

BoxMgr.toBack = function (el) {
    el = JSUI.GetElement(el);
    el.style.zIndex = -99;
    return JSUI;
}

BoxMgr.SetDimensions = function (strTargetElementOrId, objDimensions) {
    if (objDimensions.Width && objDimensions.Height) {
        var targetElement = JSUI.GetElement(strTargetElementOrId);
        targetElement.style.width = objDimensions.Width + "px";
        targetElement.style.height = objDimensions.Height + "px";
    }
}

BoxMgr.dropListeners = {};

BoxMgr.addDropListener = function (droppable, func) {
    JSUI.isFunctionOrDie(func);
    JSUI.isStringOrDie(droppable.id);
    BoxMgr.dropListeners[droppable.id] = { func: func, droppable: droppable };
}

BoxMgr.onDropped = function (dropEvent) {
    JSUI.forEach(BoxMgr.dropListeners, function (key, value) {
        if (BoxMgr.dragging === null || BoxMgr.dragging === undefined)
            return;
        var fn = value.func;
        var el = value.droppable;
        if (JSUI.mouseIsOverElement(el, dropEvent)) {
            try {
                fn(BoxMgr.dragging);
            } catch (e) {
                JSUI.logError(e, true);
            }
        }
    });


}

BoxMgr.draggable = function (el, options) {
    var element = JSUI.GetElement(el);
    var clone = jQuery(element).clone().appendTo(element.parentNode)[0]; //element.cloneNode(true);

    jQuery('img', clone).show();
    JSUI.handCursor(element).handCursor(clone);
    var config = { startDrag: function (el) { }, endDrag: function (el) { }, copy: clone, clonestyles: {} };
    JSUI.copyProperties(options, config);
    if (JSUI.isNullOrUndef(config.clonestyles.height))
        config.clonestyles.height = 15;

    if (JSUI.isNullOrUndef(config.clonestyles.width))
        config.clonestyles.width = 125;

    config.copy.style.display = 'none';
    config.copy.style.position = 'absolute';

    for (styleName in config.clonestyles) {
        if (styleName != "width" && styleName != "height" && styleName != "opacity") {
            config.copy.style[styleName] = config.clonestyles[styleName];
        }
        else if (styleName == "opacity") {
            JSUI.isNumberOrDie(config.clonestyles[styleName]);
            JSUI.setOpacity(config.copy, config.clonestyles[styleName]);
        }
        else if (styleName == "width" || styleName == "height") {
            JSUI.setStyleNum(config.copy, styleName, config.clonestyles[styleName]);
        }
    }

    // -- start mousedown
    JSUI.AddEventHandler(element,
        function (event) {
            BoxMgr.dragging = config.copy;
            config.startDrag(config.copy);
            JSUI.show(config.copy);
            BoxMgr.toFront(BoxMgr.ModalScreen); //.style.zIndex = BoxMgr.getTopZIndex();
            BoxMgr.toFront(config.copy); //.style.zIndex = BoxMgr.getTopZIndex();
            JSUI.orient(config.copy, { orientation: "CenterMouse", event: event });

            JSUI.DisableTextSelect(); //config.copy);
            var e = JSUI.GetEvent(event);
            var offsetX = (parseInt(e.clientX) - parseInt(config.copy.offsetLeft));
            var offsetY = (parseInt(e.clientY) - parseInt(config.copy.offsetTop));

            var moveFunction = function (moveEvent) {
                var moveOffsetX = (parseInt(e.clientX) - parseInt(config.copy.offsetLeft));
                var moveOffsetY = (parseInt(e.clientY) - parseInt(config.copy.offsetTop));
                //BoxMgr.MouseMove(moveEvent, targetElement, offsetX, offsetY);
                moveEvent = JSUI.GetEvent(moveEvent);
                var newLeft = moveEvent.clientX - offsetX;
                var newTop = moveEvent.clientY - offsetY;
                if (newTop <= 0)
                    newTop = 0;

                config.copy.style.left = (newLeft) + "px";
                config.copy.style.top = (newTop) + "px";
            }

            var upFunction = function (upEvent) {
                JSUI.RemoveEventHandler(config.copy, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
                BoxMgr.onDropped(upEvent);
                BoxMgr.dragging = null;
                BoxMgr.toBack(BoxMgr.ModalScreen);
                JSUI.hide(config.copy);
                config.endDrag(config.copy);
            }

            // -- start mousemove ---
            JSUI.AddEventHandler(config.copy, moveFunction, "mousemove");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");

            // -- end mousemove

            // -- start mouseup ---
            JSUI.AddEventHandler(config.copy, upFunction, "mouseup");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, upFunction, "mouseup");

            // -- end mouseup


        },
        "mousedown"
    );
    // -- end mousedown

}

BoxMgr.droppable = function (elementOrId, options) {
    var element = JSUI.GetElement(elementOrId);
    var config = { ondrop: function (ele) { } };
    if (JSUI.isFunction(options))
        config.ondrop = options;
    else if (JSUI.isObject(options))
        JSUI.copyProperties(options, config);

    config.self = element;
    BoxMgr.addDropListener(config.self, config.ondrop);
}

BoxMgr.createDialog = function (draggableElementOrId, options) {
    var title = null;
    var body = null;
    if (JSUI.isNullOrUndef(draggableElementOrId) && JSUI.isNullOrUndef(options)) {
        draggableElementOrId = document.createElement("div");
        draggableElementOrId.id = JSUI.randomString(8);
        jQuery(draggableElementOrId).addClass("dialog");
        dragHandle = document.createElement("div");
        jQuery(dragHandle).addClass("dialog-title");
        draggableElementOrId.appendChild(dragHandle);
        var dialogbody = document.createElement("div");
        jQuery(dialogbody).addClass("dialog-body");
        draggableElementOrId.appendChild(dialogbody);
        title = dragHandle;
        body = dialogbody;
        JSUI.getDocumentBody().appendChild(draggableElementOrId);
        options = { dragHandle: dragHandle };
    }
    var config = { dragHandle: draggableElementOrId, modal: false };
    if (JSUI.isString(options)) {
        config.dragHandle = JSUI.GetElement(draggableElementOrId);
    } else if (JSUI.isObject(options)) {
        JSUI.copyProperties(options, config);
    }
    var dragHandleElementOrId = config.dragHandle;
    var element = JSUI.GetElement(draggableElementOrId);
    var draggableId = element.id;
    var dialog = new BoxMgr.Dialog(draggableElementOrId, dragHandleElementOrId);
    dialog.IsModal = config.modal;
    BoxMgr.Dialogs[draggableId] = dialog;
    dialog.id = draggableId;
    dialog.title = title;
    dialog.body = body;
    element.dialog = dialog;
    return dialog;
}

BoxMgr.CreateDialog = function (el, options) {
    return BoxMgr.createDialog(el, options);
}

BoxMgr.SetPosition = function (elementOrId, objPosition) {
    objPosition.Top = objPosition.top;
    objPosition.Left = objPosition.left;
    var element = JSUI.GetElement(elementOrId);
    element.style.top = objPosition.Top + "px";
    element.style.left = objPosition.Left + "px";
}

BoxMgr.Dialog = function (targetElementOrId, dragByElementOrId) {
    var targetElement = JSUI.GetElement(targetElementOrId);
    var dragByElement = null;
    var thisObj = this;
    var dialogButtonsRendered = false;

    this.IsModal = false;
    this.OKOnly = false;
    this.DialogResult = "Cancel";
    this.OKButton = null;
    this.CancelButton = null;
    this.DefaultFocus = null;

    this.CloseListeners = [];

    if (dragByElementOrId !== null && dragByElementOrId !== undefined) {
        dragByElement = JSUI.GetElement(dragByElementOrId);
    } else {
        dragByElement = targetElement;
    }

    targetElement.style.position = "absolute";

    JSUI.SetMoveCursor(dragByElement);

    this.SetPosition = function (objPosition) {
        BoxMgr.SetPosition(targetElement, objPosition);
    }

    this.CenterScreen = function (intPlusTop, intPlusLeft) {
        var centerPosition = JSUI.Orientation.CenterScreen(targetElement, intPlusTop, intPlusLeft);
        BoxMgr.SetPosition(targetElement, centerPosition);
    }

    this.CenterPage = function (intPlusTop, intPlusLeft) {
        var centerPagePosition = JSUI.Orientation.CenterPage(targetElement, intPlusTop, intPlusLeft);
        BoxMgr.SetPosition(targetElement, centerPagePosition);
    }

    this.SetDimensions = function (objDimensions) {
        BoxMgr.SetDimensions(targetElement, objDimensions);
    }

    this.Show = function (boolModal) {
        if (boolModal !== null && boolModal != 'undefined') {
            thisObj.IsModal = boolModal;
        }
        BoxMgr.HideBuggyControls();
        JSUI.ShowElement(targetElement);
    }

    this.Activate = function (boolModal) {
        if (boolModal != null && boolModal != 'undefined') {
            thisObj.IsModal = boolModal;
        }
        BoxMgr.ModalScreen.style.zIndex = BoxMgr.getTopZIndex();
        targetElement.style.zIndex = BoxMgr.getTopZIndex();
        dragByElement.style.zIndex = BoxMgr.getTopZIndex();
        thisObj.Show(boolModal);
        if (thisObj.DefaultFocus && thisObj.DefaultFocus.focus) {
            thisObj.DefaultFocus.focus();
        }
    }

    this.Hide = function () {
        JSUI.HideElement(targetElement);
        BoxMgr.ModalScreen.DropBack();
        BoxMgr.ShowBuggyControls();
    }

    this.OnClose = function () {
        thisObj.Hide();
        BoxMgr.ModalScreen.DropBack();
        for (var i = 0; i < thisObj.CloseListeners.length; i++) {
            thisObj.CloseListeners[i](thisObj.DialogResult);
        }
    }

    var okayClicked = function (evt) {
        thisObj.DialogResult = "OK";
        thisObj.OnClose();
    }

    var cancelClicked = function (evt) {
        thisObj.DialogResult = "Cancel";
        thisObj.OnClose();
    }

    this.RenderDialogButtons = function () {
        if (!dialogButtonsRendered) {
            thisObj.IsModal = true;

            var ok = new BoxMgr.ButtonClass("OK");
            ok.AddClickListener(okayClicked);
            //            ok.RenderIn(targetElement);

            var cancel = new BoxMgr.ButtonClass("Cancel");
            cancel.AddClickListener(cancelClicked);
            //cancel.RenderIn(targetElement);

            var buttons = new BoxMgr.ButtonStrip();
            buttons.AddButton(ok);
            if (!thisObj.OKOnly)
                buttons.AddButton(cancel);
            buttons.RenderIn(targetElement);
            thisObj.OKButton = ok;
            thisObj.CancelButton = cancel;
            dialogButtonsRendered = true;
        }
    }

    this.SetOkId = function (strId) {
        var okElement = JSUI.GetElement(strId);
        JSUI.SetHandCursor(okElement);
        JSUI.AddEventHandler(okElement, okayClicked, "click");
    }

    this.SetCancelId = function (strId) {
        var cancelElement = JSUI.GetElement(strId);
        JSUI.SetHandCursor(cancelElement);
        JSUI.AddEventHandler(cancelElement, cancelClicked, "click");
    }

    this.ShowDialog = function (boolWithButtons) {
        if (!dialogButtonsRendered && boolWithButtons) {
            thisObj.RenderDialogButtons();
        }
        BoxMgr.ModalScreen.Modalize();
        thisObj.IsModal = true;
        thisObj.Activate(true);
    }

    this.AddCloseListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (!JSUI.ArrayContains(thisObj.CloseListeners, funcPointer)) {
            thisObj.CloseListeners.push(funcPointer);
        }
    }

    this.RemoveCloseListener = function (funcPointer) {
        if (JSUI.ArrayContains(thisObj.CloseListeners, funcPointer)) {
            thisObj.CloseListeners.splice(thisObj.CloseListeners.indexOf(funcPointer), 1);
        }
    }

    // -- start mousedown
    JSUI.AddEventHandler(dragByElement,
        function (event) {
            thisObj.Activate();
            JSUI.DisableTextSelect(); //dragByElement);
            var e = JSUI.GetEvent(event);
            var offsetX = (parseInt(e.clientX) - parseInt(targetElement.offsetLeft));
            var offsetY = (parseInt(e.clientY) - parseInt(targetElement.offsetTop));

            var moveFunction = function (moveEvent) {
                var moveOffsetX = (parseInt(e.clientX) - parseInt(targetElement.offsetLeft));
                var moveOffsetY = (parseInt(e.clientY) - parseInt(targetElement.offsetTop));
                //BoxMaster.MouseMove(moveEvent, targetElement, offsetX, offsetY);
                moveEvent = JSUI.GetEvent(moveEvent);
                targetElement.style.left = (moveEvent.clientX - offsetX) + "px";
                targetElement.style.top = (moveEvent.clientY - offsetY) + "px";
            }

            var upFunction = function (upEvent) {
                JSUI.RemoveEventHandler(dragByElement, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(targetElement, moveFunction, "mousemove");
                //                if (!thisObj.IsModal)
                //                    BoxMgr.ModalScreen.DropBack();
            }

            // -- start mousemove ---
            JSUI.AddEventHandler(dragByElement, moveFunction, "mousemove");
            JSUI.AddEventHandler(targetElement, moveFunction, "mousemove");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
            // -- end mousemove 

            // -- start mouseup ---
            JSUI.AddEventHandler(dragByElement, upFunction, "mouseup");
            JSUI.AddEventHandler(targetElement, upFunction, "mouseup");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, upFunction, "mouseup");
            // -- end mouseup


        },
        "mousedown"
    );
    // -- end mousedown
}

BoxMgr.CreateButton = function (strText) {
    var button = new BoxMgr.ButtonClass(strText);
    return button;
}

BoxMgr.ButtonStrip = function () {
    var div = document.createElement("div");
    div.style.width = "100%";
    var table = document.createElement("table");
    div.appendChild(table);
    table.setAttribute("align", "right");

    if (document.all) {
        var thead = document.createElement("thead");
        table.appendChild(thead);
        var tbody = document.createElement("tbody");
        table.appendChild(tbody);
        table = tbody;
    }


    var tableRow = document.createElement("tr");
    var isRendered = false;
    table.appendChild(tableRow);


    var dockTop = false;
    var dockBottom = true;
    this.setDockTop = function (bool) {
        dockTop = bool;
        dockBottom = !bool;
    }

    this.setDockBottom = function (bool) {
        dockBottom = bool;
        dockTop = !bool;
    }

    this.Buttons = [];
    this.Buttons.items = [];
    var thisObj = this;
    this.AddButton = function (objButtonClass) {
        if (!JSUI.ArrayContains(thisObj.Buttons, objButtonClass)) {
            thisObj.Buttons.push(objButtonClass);
            thisObj.Buttons.items[objButtonClass.Text] = objButtonClass;
            var cell = document.createElement("td");
            tableRow.appendChild(cell);
            objButtonClass.RenderIn(cell);
        }
    }

    this.RenderIn = function (elementOrId) {
        if (!isRendered) {
            var element = JSUI.GetElement(elementOrId);
            if (dockTop) {
                if (element.hasChildNodes) {
                    element.insertBefore(div, element.firstChild);
                } else {
                    element.appendChild(div);
                }
            } else {
                element.appendChild(div);

                //div.style.top = ((JSUI.GetElementHeight(element) * 1) - 75) + "px";
            }

            isRendered = true;
        }
    }
}


BoxMgr.ButtonClass = function (strText) {
    this.Text = strText;
    this.IsRendered = false;
    this.DisableOnClick = false;
    this.Enabled = false;
    this.ButtonElement = null;
    this.ImageName = "defaultbutton";
    var thisObj = this;

    this.ClickListeners = [];

    this.AddClickListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (!JSUI.ArrayContains(thisObj.ClickListeners, funcPointer)) {
            thisObj.ClickListeners.push(funcPointer);
        }
    }

    this.OnClick = function (evt) {
        for (var i = 0; i < thisObj.ClickListeners.length; i++) {
            thisObj.ClickListeners[i](evt);
        }
    }

    var element; // = JSUI.GetElement(elementOrId);
    var buttonDiv; // = document.createElement("div");
    var text = document.createElement("div");
    text.style.position = "relative";
    text.style.top = "-23px";
    text.style.textAlign = "center";
    text.style.width = "100%";
    text.style.height = "100%";
    text.appendChild(document.createTextNode(thisObj.Text));

    var click = function (evt) {
        if (thisObj.DisableOnClick) {
            thisObj.Disable();
        }
        thisObj.OnClick(evt);
    }

    this.Disable = function () {
        if (thisObj.Enabled) {
            JSUI.RemoveEventHandler(text, click, "click");
            thisObj.Enabled = false;
        }
    }

    this.Enable = function () {
        if (!thisObj.Enabled) {
            JSUI.AddEventHandler(buttonDiv, click, "click");
            thisObj.Enabled = true;
        }
    }

    this.RenderIn = function (elementOrId) {
        if (!thisObj.IsRendered) {
            thisObj.IsRendered = true;
            element = JSUI.GetElement(elementOrId);
            buttonDiv = document.createElement("div");

            BoxMgr.SetDimensions(buttonDiv, { Width: 100, Height: 25 });
            var img = JSUI.CreateImageElement(thisObj.ImageName); //"defaultbutton"); 
            JSUI.ImageSwapify(img, thisObj.ImageName);
            img.style.width = "100%";
            img.style.height = "100%";
            buttonDiv.appendChild(img);

            buttonDiv.appendChild(text);

            element.appendChild(buttonDiv);

            var mouseOver = function () {
                img.src = JSUI.Images[thisObj.ImageName + "over"].src;
            }

            var mouseOut = function () {
                img.src = JSUI.Images[thisObj.ImageName].src;
            }

            JSUI.AddEventHandler(text, mouseOver, "mouseover");
            JSUI.AddEventHandler(text, mouseOut, "mouseout");
            //JSUI.AddEventHandler(text, click, "click");
            thisObj.Enable();

            JSUI.SetHandCursor(buttonDiv);
            JSUI.SetHandCursor(text);
            JSUI.SetHandCursor(img);
            thisObj.ButtonElement = element;
        }
        return thisObj.ButtonElement;
    }

}


JSUI.Assimilate(BoxMgr); //, JSUI);


var DataBox = {};
DataBox.Scripts = {};

DataBox.getBoxContent = function (strDomId, strBoxName) {
    var clientKey = JSUI.RandomString(4);
    var callBack = function (html) {
        JSUI.GetElement(strDomId).innerHTML = html;
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = JSUI.GetFilePath() + "?box=" + strBoxName + "&s=y&domid=" + strDomId + "&ck=" + clientKey;
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
    }
    var url = JSUI.GetFilePath() + "?box=" + strBoxName + "&domid=" + strDomId + "&ck=" + clientKey;
    var asyncRequest = new JSUI.AsyncRequestClass(url, callBack);
    asyncRequest.Send();
}

DataBox.GetBoxContent = function (domId, templateName) {
    DataBox.getBoxContent(domin, templateName);
}

DataBox.getScripts = function (strClientKey) {
    if (!DataBox.Scripts[strClientKey]) {
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = JSUI.GetFilePath() + "?dbs=y&ck=" + strClientKey; // databoxscripts = yes
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
        DataBox.Scripts[strClientKey] = true; // make sure the same script isn't loaded multiple times
    }
}

DataBox.GetDataBoxScripts = function (key) {
    DataBox.getScripts(key);
}

JSUI.DataBox = DataBox;

var Cookies = {};
Cookies.setCookie = function (strName, value, expireAfterDays) {
    var expires = "";
    if (expireAfterDays) {
        var date = new Date();
        date.setTime(date.getTime() + (expireAfterDays * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }

    document.cookie = strName + "=" + value + expires + "; path=/";
}

Cookies.clearCookie = function (strName) {
    Cookies.setCookie(strName, "", -1);
}

Cookies.getCookie = function (strName) {
    var cookieArr = document.cookie.split(';');
    for (var i = 0; i < cookieArr.length; i++) {
        var nameValuePair = cookieArr[i].split('=');
        if (nameValuePair[0].trim() == strName)
            return nameValuePair[1].trim();
    }

    return "";
}

if (JSUI !== null && JSUI !== undefined) {
    JSUI.assimilate(Cookies);
}


var ArrowList = {};
ArrowList.ArrowListClass = function (strParentId, strInputElementId, strItemTagName) {
    if (strParentId == null || strParentId == 'undefined' || strInputElementId == null || strInputElementId == 'undefined')
        throw new JSUI.Exception("strParentId and strInputElementId are required parameters");

    this.ItemElements = [];
    this.ItemTagName = strItemTagName ? strItemTagName : "div";
    this.Parent = JSUI.GetElement(strParentId);
    this.InputElement = JSUI.GetElement(strInputElementId);
    this.InputElement.setAttribute("autocomplete", "off");
    this.HighlightBackgroundColor = "#0000FF";
    this.HighlightForeColor = "#FFFFFF";
    this.ForeColor = "#000000";
    this.BackgroundColor = "#FFFFFF";
    this.ResetOnSelect = false;

    var nullElement = {};
    nullElement.UnHighlight = function () { };
    nullElement.Highlight = function () { };
    this.CurrentElement = nullElement;
    this.SelectedIndex = -1;

    var thisObj = this;
    var selectListeners = [];
    var resetListeners = [];

    this.DownArrowKey = function () {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();

        thisObj.SelectedIndex++;
        if (thisObj.SelectedIndex > thisObj.ItemElements.length - 1)
            thisObj.SelectedIndex--;

        var selected = thisObj.ItemElements[thisObj.SelectedIndex];
        if (thisObj.SelectedIndex >= 0 && selected != null && selected != 'undefined')
            selected.Highlight();
    }

    this.UpArrowKey = function () {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();
        thisObj.SelectedIndex--;
        if (thisObj.SelectedIndex == -1)
            thisObj.SelectedIndex = 0;

        var selected = thisObj.ItemElements[thisObj.SelectedIndex];
        if (thisObj.SelectedIndex >= 0 && selected != null && selected != 'undefined')
            selected.Highlight();
    }

    this.OnSelected = function () {
        for (var i = 0; i < selectListeners.length; i++) {
            selectListeners[i](thisObj.CurrentElement);
        }
        if (thisObj.ResetOnSelect) {
            thisObj.Reset();
        }
    }

    this.OnReset = function () {
        for (var i = 0; i < resetListeners.length; i++) {
            resetListeners[i](thisObj);
        }
    }

    this.Reset = function () {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();
        thisObj.OnReset();
    }

    this.EnterKey = function () {
        thisObj.OnSelected();
    }

    this.AddSelectListener = function (funcListener) {
        JSUI.IsFunctionOrDie(funcListener);
        selectListeners.push(funcListener);
    }

    this.AddResetListener = function (funcListener) {
        JSUI.IsFunctionOrDie(funcListener);
        resetListeners.push(funcListener);
    }


    var connectKeyDown = function () {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
                             function (evt) {
                                 var e = JSUI.GetEvent(evt);

                                 // enter key
                                 if (e.keyCode == 13) {
                                     thisObj.EnterKey();
                                     if (e.preventDefault)
                                         e.preventDefault();

                                     e.cancelBubble = true;
                                     e.returnValue = false;
                                     return false;
                                 } else if (e.keyCode == 40) //down arrow
                                 {
                                     thisObj.DownArrowKey();
                                     return false;

                                 } else if (e.keyCode == 38) // up arrow
                                 {
                                     thisObj.UpArrowKey();
                                     return false;
                                 } else if (e.keyCode == 27) // reset on escape key
                                 {
                                     thisObj.Reset();
                                     return false;
                                 }

                                 return true;
                             },
                             "keydown"
        )
    }

    var connectKeyPress = function () {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
           function (evt) {
               var e = JSUI.GetEvent(evt);
               if (e.keyCode == 13 || e.keyCode == "13")
                   return false;
           },
           "keypress"
       )

    }

    var connectBlur = function () {
        JSUI.AddEventHandler(thisObj.InputElement, function () {
            if (thisObj.CurrentElement === null || thisObj.CurrentElement === undefined || thisObj.CurrentElement === nullElement) {
                thisObj.Reset();
            }
        }, "blur");
    }

    var connectKeyUp = function () {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
                function (evt) {
                    var e = evt;
                    if (window.event) {
                        e = window.event;
                    }

                    // enter key
                    if (e.keyCode == 13) {
                        thisObj.EnterKey();
                        return false;
                    } else if (e.keyCode == 40) //down arrow
                    {
                        return false;

                    } else if (e.keyCode == 38) // up arrow
                    {
                        return false;
                    } else if (e.keyCode == 27) // reset on escape key
                    {
                        thisObj.Reset();
                        return false;
                    }
                    return true;
                },
                "keyup"
        )
    }

    this.Initialize = function () {

        var itemElements = thisObj.Parent.getElementsByTagName(thisObj.ItemTagName);
        thisObj.CurrentElement = nullElement; //itemElements[0];
        for (var i = 0; i < itemElements.length; i++) {
            var itemElement = itemElements[i];
            itemElement.Index = i;

            itemElement.UnHighlight = function (evt) {
                target = this;

                target.style.backgroundColor = thisObj.BackgroundColor;
                target.style.color = thisObj.ForeColor;
                thisObj.CurrentElement = nullElement;
            }

            itemElement.Highlight = function (evt) {
                thisObj.CurrentElement.UnHighlight();

                target = this;

                target.style.backgroundColor = thisObj.HighlightBackgroundColor;
                target.style.color = thisObj.HighlightForeColor;
                thisObj.SelectedIndex = target.Index;
                thisObj.CurrentElement = target;

            }



            JSUI.AddEventHandler(itemElement, itemElement.Highlight, "mouseover");
            JSUI.AddEventHandler(itemElement, itemElement.UnHighlight, "mouseout");
            JSUI.AddEventHandler(itemElement, thisObj.OnSelected, "click");
            JSUI.SetHandCursor(itemElement);
            thisObj.ItemElements.push(itemElement);
        }

        connectKeyDown();
        connectBlur();
    }


}

var Docker = {};
Docker.DockedElements = [];

Docker.DockedElementClass = function (strElementId, strDockToId, objDimensions) {
    Docker.DockedElements.push(this);

    this.ElementId = strElementId;
    this.DockToId = strDockToId;
    this.Dimensions = objDimensions;

    var iFrame = document.createElement("iframe");
    iFrame.style.border = '0px';
    iFrame.style.position = 'absolute';
    iFrame.style.zIndex = -1;
    JSUI.GetDocumentBody().appendChild(iFrame);

    var thisObj = this;
    this.ReDock = function () {
        var contentElement = JSUI.GetElement(thisObj.ElementId);
        var targetPosition = JSUI.GetElementPosition(thisObj.DockToId);
        var targetHeight = JSUI.GetElementHeight(thisObj.DockToId);
        if (!targetHeight)
            targetHeight = 15;

        targetHeight += 6;
        var targetLeft = targetPosition.Left;

        var newTop = targetPosition.Top + targetHeight + "px";
        var newLeft = targetLeft + "px";

        contentElement.style.top = newTop;
        contentElement.style.left = newLeft;
        contentElement.style.position = "absolute";

        // this section is specifically for IE6 but breaks IE7 so it's been commented for now
        //        iFrame.style.top = newTop;
        //        iFrame.style.left = newLeft;
        //        iFrame.style.height = JSUI.GetElementHeight(contentElement) + "px";
        //        iFrame.style.width = JSUI.GetElementWidth(contentElement) + "px";
        //        contentElement.parentNode.removeChild(contentElement);
        //        iFrame.parentNode.removeChild(iFrame);
        //        var docBody = JSUI.GetDocumentBody();
        //        docBody.appendChild(iFrame); 
        //        docBody.appendChild(contentElement);               
    }

    JSUI.addOnWindowResize(this.ReDock);
    JSUI.addOnWindowScroll(this.ReDock);
}

var Popper = {};

Popper.ToolTipify = function (targetElementOrId, contentElementOrId, dimensions, boolOrientLeft) {
    JSUI.SetHandCursor(targetElementOrId);
    var tt = new Popper.PopperClass(targetElementOrId, contentElementOrId, dimensions, boolOrientLeft);
    tt.ShowContent = false;
    JSUI.AddEventHandler(JSUI.GetElement(targetElementOrId),
        tt.Poof,
        "mouseout")

    return tt;
}

Popper.Popperize = function (targetElementOrId, contentElementOrId, boolDoPop, boolShowContent, dimensions) {
    //JSUI.Require("Animator");
    var popper = new Popper.PopperClass(targetElementOrId, contentElementOrId, dimensions);
    popper.ShowContent = boolShowContent;
    if (boolDoPop)
        popper.Pop();

    return popper;
}

Docker.DockThisTo = function (strElementId, strDockToElementId, objDimensions) {
    var docked = new Docker.DockedElementClass(strElementId, strDockToElementId, objDimensions);
    docked.ReDock();
    return docked;
}

Popper.PopperClass = function (targetElementOrId, contentElementOrId, popDimensions, orientLeft) { // orientLeft - quick hack this should be changed later
    var targetElement = JSUI.GetElement(targetElementOrId);
    var contentElement = JSUI.GetElement(contentElementOrId);
    contentElement.style.display = "none";

    var iFrame = document.createElement("iframe");
    iFrame.style.border = '0px';
    iFrame.style.position = 'absolute';
    iFrame.style.zIndex = -1;
    JSUI.GetDocumentBody().appendChild(iFrame);

    //    var inserted = false;        
    this.Reposition = function () {
        var targetPosition = JSUI.GetElementPosition(targetElement);
        var targetHeight = JSUI.GetElementHeight(targetElement.id);
        if (!targetHeight)
            targetHeight = 15;

        targetHeight += 6;
        var targetLeft = targetPosition.Left;
        if (orientLeft)
            targetLeft = targetLeft - popDimensions.Width;
        var newTop = targetPosition.Top + targetHeight + "px";
        var newLeft = targetLeft + "px";

        contentElement.style.top = newTop;
        contentElement.style.left = newLeft;
        contentElement.style.position = "absolute";

        //        if(!inserted){
        //            var docBody = JSUI.GetDocumentBody();
        //            docBody.appendChild(iFrame, contentElement);

        iFrame.style.top = newTop;
        iFrame.style.left = newLeft;
        iFrame.style.height = JSUI.GetElementHeight(contentElement) + "px";
        iFrame.style.width = JSUI.GetElementWidth(contentElement) + "px";
        contentElement.parentNode.removeChild(contentElement);
        iFrame.parentNode.removeChild(iFrame);
        docBody.appendChild(iFrame);
        docBody.appendChild(contentElement);
        //            inserted = true;
        //        }
    }

    this.Animate = false;
    this.ShowContent = true;
    this.popCompleteListeners = [];
    this.poofCompleteListeners = [];

    var daObj = this;
    var dimensions = popDimensions;

    this.OnPoofComplete = function () {
        for (var i = 0; i < daObj.poofCompleteListeners.length; i++) {
            daObj.poofCompleteListeners[i]();
        }
        JSUI.AddEventHandler(targetElement, daObj.Pop, "mouseover");
        JSUI.RemoveEventHandler(contentElement, daObj.Poof, "mouseout");
    }

    this.OnPopComplete = function () {
        for (var i = 0; i < daObj.popCompleteListeners.length; i++) {
            daObj.popCompleteListeners[i]();
        }
        JSUI.AddEventHandler(contentElement, daObj.Poof, "mouseout");
        JSUI.RemoveEventHandler(targetElement, daObj.Pop, "mouseover");
    }
    this.Pop = function (e) {
        if (contentElement.style.display == 'none') {
            if (daObj.Animate) {
                var anim = Animation.GrowIn(contentElement, 50, daObj.ShowContent, dimensions);
                anim.AddGrowCompleteListener(daObj.OnPopComplete);
                daObj.Reposition(); //targetElement, contentElement);

            } else {
                //                iFrame.style.dispaly = 'block';
                contentElement.style.display = 'block';

                daObj.Reposition(); //targetElement, contentElement);
                daObj.OnPopComplete();
            }
        }
    }

    this.AddPopCompleteListener = function (funcPointer) {
        JSUI.IsFunction(funcPointer, true);

        if (JSUI.FunctionIsInArray(funcPointer, this.popCompleteListeners))
            return;

        this.popCompleteListeners.push(funcPointer);
    }

    this.AddPoofCompleteListener = function (funcPointer) {
        JSUI.IsFunction(funcPointer, true);

        if (JSUI.FunctionIsInArray(funcPointer, this.poofCompleteListeners))
            return;

        this.poofCompleteListeners.push(funcPointer);
    }


    this.Poof = function (e) {
        var event = window.event ? window.event : e;
        if (JSUI.MouseIsOverElement(contentElement, event))
            return false;

        if (daObj.Animate) {
            //            iFrame.style.display = "none";
            var anim = Animator.ShrinkOut(contentElement, 50, daObj.ShowContent);
            anim.AddShrinkCompleteListener(daObj.OnPoofComplete);
            daObj.Reposition(targetElement, contentElement);
        } else {
            contentElement.style.display = "none";
            //            iFrame.style.display = "none";

            daObj.Reposition(targetElement, contentElement);
            daObj.OnPoofComplete();
        }

    }



    JSUI.AddEventHandler(targetElement, this.Pop, "mouseover");
    JSUI.SetHandCursor(contentElement);
    this.Reposition(targetElementOrId, contentElementOrId);
}

JSUI.Assimilate(Popper); //, JSUI);
JSUI.Assimilate(Docker); //, JSUI);