   
function JsonProxy(){this.multiInvoke = false;};
JsonProxy.prototype.Extends = function(baseConstructor){
    var base = new baseConstructor();
    for(prop in base){
        this[prop] = base[prop];
    }
}

JsonProxy.prototype.RunningMethods = {};

JsonProxy.prototype.InvokeMethodEx = function(methodName, arrParams, options, callBack) {
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

    request.onreadystatechange = function() {
        if (request.readyState == 4 && request.status == 200) {
            var text = request.responseText;
            if (format == "box") {
                config.target.innerHTML = text;
                jQuery('script[language=javascript]', config.target).each(function(k, script) {
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

JsonProxy.prototype.InvokeMethod = function(methodName, arrParams, callBack, format){    
    if( this.MethodEndpoints[methodName] == null ||
       this.MethodEndpoints[methodName] == 'undefined' ){
        JSUI.ThrowException("Method end point not defined.");
    }
    
    if(!format)
        format = this.ResponseFormat;
    
    var endpoint = this.MethodEndpoints[methodName];
    var verb = this.MethodVerbs[methodName];
    var postParams = "";

    if( arrParams != null && arrParams != 'undefined' ){    
        endpoint += "&params=";
        for(var i = 0; i < arrParams.length; i++){
            if(verb == "GET"){
                endpoint += encodeURIComponent(arrParams[i]);
                if( i != arrParams.length - 1 )
                    endpoint += encodeURIComponent("$#%"); // constant defined in JavascriptServer "ParameterDelimiter"
            }else{ // verb == "POST"
                postParams += JSON.stringify(arrParams[i]) + "$#%";
            }
        }         
    }
    postParams = postParams == "" ? null : postParams;
    
    endpoint += "&ienocache=" + JSUI.GetRandomId(8);
    var append = "&f=json";
    if(format == "box" && this.DataTemplate){
        append = "&f=" + this.ResponseFormat; // set in jsoncallback.js
        append += "&dbx=" + this.DataTemplate;
        append += "&ck=" + this.ClientKey;
    }else{
        format = "json";
    }

    endpoint += append;
    if (this.RunningMethods[methodName] !== undefined && this.MethodEndpoints[methodName].multiInvoke == false) {
        this.RunningMethods[methodName].abort();
    }
    
    var request = JSUI.NewHttpRequest();
    var proxy = this;     
    
    request.onreadystatechange = function(){
         if( request.readyState == 4 && request.status == 200 ){
            var text = request.responseText;
            if(callBack){
                if(format == "json"){
                    callBack(eval("(" + text + ")"));
                    return;
                }
                
                if(format == "box"){
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

ProxyUtil.AddServerProxy = function(proxyName, proxyInstance, throwIfAlreadyRegistered) {
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

ProxyUtil.getProxy = function(locator, throwIfNull) {
    var proxy = ProxyUtil.getProxyByName(locator);
    if (proxy == null)
        proxy == ProxyUtil.getProxyByType(locator);

    if (proxy == null && throwIfNull) {
        JSUI.ThrowException("Proxy of type or name " + locator + " was not registered with the JSUI framework.");
    }
    return proxy;
}

ProxyUtil.getProxyByName = function(name) {
    if (ProxyUtil.Proxies[name] !== null && ProxyUtil.Proxies[name] !== undefined)
        return ProxyUtil.Proxies[name];

    return null;
}

ProxyUtil.getProxyByType = function(type) {
    if (ProxyUtil.ProxiesByType[type] !== null && ProxyUtil.ProxiesByType[type] !== undefined)
        return ProxyUtil.ProxiesByType[type];

    return null;
}

ProxyUtil.GetProxy = function(name, tin) {
    return ProxyUtil.getProxy(name, tin);
}

JSUI.getJson = function(el) {
    el = JSUI.getElement(el);
    var ret = {};
    jQuery("[jsonproperty]", el).each(function(key, value) {
        var name = value.getAttribute("jsonproperty");
        var val = value.getAttribute("value");
        ret[name] = val;
    });

    return ret;
}

JSUI.resetJson = function (el) {
    el = JSUI.getElement(el);
    jQuery("[jsonproperty]", el).each(function (key, val) {
        val.value = "";
    });
}

JSUI.getJsonQueryString = function (el) {
    var val = JSUI.getJson(el);
    var ret = "";
    for (prop in val) {
        if (!JSUI.isNullOrUndef(val[prop]) && !val[prop].isBlank())
            ret += prop + "=" + encodeURIComponent(val[prop]) + "&";
    }
    return ret.substr(0, ret.length - 1);
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
        function(ctorParams) {
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

ProxyUtil.HandleCallbackError = function(jsonResult) {
    if (jsonResult.Status == "Error") {
        alert(jsonResult.ErrorMessage);
        return true;
    }
    return false;
}


ProxyUtil.CallbackSubscribers = {}; // subscribers should be JsonFunction classes on the server identified by their JsonId.
ProxyUtil.RegisterCallbackSubscriber = function(strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId) {
    JSUI.IsFunctionOrDie(funcCallback); // this will be checked by the constructor but having an error here will bring me more quickly to this line.
    var callBackInfo = JSUI.construct("CallbackSubscriberInfo", [strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId]);
    ProxyUtil.CallbackSubscribers[strCallbackJsonId] = callBackInfo;
}

ProxyUtil.ProxyMethodQueue = {};
ProxyUtil.EnQueue = function(clsMethodInvokeInfo) {
    var proxyName = clsMethodInvokeInfo.ProxyName;
    var methodName = clsMethodInvokeInfo.MethodName;


    if (!ProxyUtil.ProxyMethodQueue[proxyName])
        ProxyUtil.ProxyMethodQueue[proxyName] = {};

    if (!ProxyUtil.ProxyMethodQueue[proxyName][methodName]) {
        ProxyUtil.ProxyMethodQueue[proxyName][methodName] = [];
        ProxyUtil.ProxyMethodQueue[proxyName][methodName].push(clsMethodInvokeInfo);

    }
}

ProxyUtil.DeQueue = function(strProxyName) {
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

ProxyUtil.InvokeMethodWithCallback = function(strSubscriberId, arrParams, objDataRequestInfo) {
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
        proxy.InvokeMethod(methodName, arrParams, function(resultHTMLOrJson) {

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

ProxyUtil.ensureProxies = function() {
    var missingVarnames = [];
    if (!JSUI.isNullOrUndef(JSUI.providerInfo)) {
        JSUI.forEach(JSUI.proxyReadyListeners, function(varname, fn) {
            var providerInfo = JSUI.providerInfo[varname];
            if (!JSUI.isNullOrUndef(providerInfo)) {
                JSUI.ensureProxy(providerInfo.VarName, providerInfo.ClientTypeName);
            } else {
                missingVarnames.push(varname);
            }
        });
    }
    if (missingVarnames.length > 0) {
        ProxyUtil.onProxyReady("jsLogger", function(inf) {
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
    return ProxyUtil.ensureProxies;
}

ProxyUtil.proxyReady = function(proxyInfo) {
    var listeners = ProxyUtil.proxyReadyListeners[proxyInfo.varname];
    proxyInfo.proxy = ProxyUtil.getProxy(proxyInfo.varname);
    if (!JSUI.isNullOrUndef(listeners)) {
        while (listeners.length > 0) {
            var fn = listeners.shift();
            fn(proxyInfo);
        }
    }
}

ProxyUtil.ensureProxy = function(strProxyName, type, strCallback) {
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