if(JSUI === undefined)
    alert("The core JSUI.js file was not loaded");

var ProxyUtil = {};
var JsonResponseFormats = {};
JsonResponseFormats.json = "json";
JsonResponseFormats.box = "box";

// This class represents the client side "proxy" for the JsonCallback control
JSUI.addConstructor("CallbackSubscriberInfoClass",
    function CallbackSubscriberInfoClass(arrParams) {
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
    function MethodInvokeInfoClass(arrParams) {
        var strSubscriberId = arrParams[0];
        var strProxyName = arrParams[1];
        var strMethodName = arrParams[2];
        var arrParams = arrParams[3];
        var funcCallback = arrParams[4];
        var objDataRequestInfo = arrParams[5];
        if (funcCallback)
            JSUI.IsFunctionOrDie(funcCallback);
        this.SubscriberId = strSubscriberId; // this is used specifically for JsonCallback controls    
        this.ProxyName = strProxyName;
        this.MethodName = strMethodName;
        this.Params = arrParams;
        this.RequestInfo = objDataRequestInfo;
        this.Callback = funcCallback;
});

ProxyUtil.HandleCallbackError = function(jsonResult){
    if(jsonResult.Status == "Error"){
        alert(jsonResult.ErrorMessage);
        return true;
    }
    return false;
}
 
  
ProxyUtil.CallbackSubscribers = {}; // subscribers should be JsonFunction classes on the server identified by their JsonId.
ProxyUtil.RegisterCallbackSubscriber = function(strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId ){
    JSUI.IsFunctionOrDie(funcCallback); // this will be checked by the constructor but having an error here will bring me more quickly to this line.
    var callBackInfo = JSUI.construct("CallbackSubscriberInfoClass", [strCallbackJsonId, strClientInstanceName, strType, strMethodName, funcCallback, preInvokeFuncJsonId]);
    ProxyUtil.CallbackSubscribers[strCallbackJsonId] = callBackInfo;
}

ProxyUtil.ProxyMethodQueue = {};
ProxyUtil.EnQueue = function(clsMethodInvokeInfo){
    var proxyName = clsMethodInvokeInfo.ProxyName;
    var methodName = clsMethodInvokeInfo.MethodName;

    
    if(!ProxyUtil.ProxyMethodQueue[proxyName])
        ProxyUtil.ProxyMethodQueue[proxyName] = {};
        
    if(!ProxyUtil.ProxyMethodQueue[proxyName][methodName]){
        ProxyUtil.ProxyMethodQueue[proxyName][methodName] = [];
        ProxyUtil.ProxyMethodQueue[proxyName][methodName].push(clsMethodInvokeInfo);

    }
}

ProxyUtil.DeQueue = function (strProxyName){
    var proxy = JSUI.GetProxy(strProxyName);
    var proxyName = proxy.Name;
    for(methodName in ProxyUtil.ProxyMethodQueue[proxyName]){
        var methodDataArray = ProxyUtil.ProxyMethodQueue[proxyName][methodName];
        while(methodDataArray.length > 0)
        {
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
    this.ResponseFormat = jsonResponseFormat;
    this.DataTemplate = strDataTemplateName;
    this.ClientKey = JSUI.RandomString(8);
});

ProxyUtil.InvokeMethodWithCallback = function(strSubscriberId, arrParams, objDataRequestInfo){
    var callBackInfo = ProxyUtil.CallbackSubscribers[strSubscriberId];
    JSUI.ThrowIfNull(callBackInfo, "JsonControl '" + strSubscriberId + "' was not registered as a callback subscriber.");
    var preInvokeFunction = JSUI.Functions[callBackInfo.PreInvokeFunctionJsonId];
    var methodName = callBackInfo.MethodName;
    var proxy = ProxyUtil.GetProxy(callBackInfo.ProxyName);//callBackInfo.Proxy;
    var proxyName = callBackInfo.ProxyName;
    var callBackFunction = callBackInfo.CallbackFunction;

    if(!proxy){
        ProxyUtil.EnQueue(JSUI.construct('MethodInvokeInfoClass', [strSubscriberId, proxyName, methodName, arrParams, callBackFunction, objDataRequestInfo]));
        ProxyUtil.EnsureProxy(callBackInfo.ProxyName, callBackInfo.ProxyType);
    }else{
        if(preInvokeFunction){
            if(preInvokeFunction() == false){
                return;
            }
        }
        proxy.ResponseFormat = objDataRequestInfo.ResponseFormat;
        proxy.DataTemplate = objDataRequestInfo.DataTemplate;
        proxy.ClientKey = objDataRequestInfo.ClientKey;
        proxy.InvokeMethod(methodName, arrParams, function(resultHTMLOrJson){
            
            if(objDataRequestInfo.ResponseFormat == JsonResponseFormats.json){
                if(ProxyUtil.HandleCallbackError(resultHTMLOrJson)){
                    return;
                }else{
                    callBackFunction(resultHTMLOrJson.Data);
                }
            }
        
            if(objDataRequestInfo.ResponseFormat == JsonResponseFormats.box){
                var html = resultHTMLOrJson;
                var clientKey = objDataRequestInfo.ClientKey;
                callBackFunction(html, clientKey);
            }       
        });
    }
}

ProxyUtil.Ensuring = {};
ProxyUtil.EnsureProxy = function(strProxyName, type, strCallback){
    if(ProxyUtil.Ensuring[strProxyName])
        return;    
    ProxyUtil.Ensuring[strProxyName] = true;    
    
    if(strCallback == null || strCallback == 'undefined')
        strCallback = "ProxyUtil.DeQueue('" + strProxyName + "')";
        
    var proxy = JSUI.GetProxy(strProxyName);
    if(!proxy){
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = window.location.toString().split("?")[0] + "?init=" + type + "&varname=" + strProxyName
        path += "&cb=" + strCallback;
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
    }
}

JSUI.Assimilate(ProxyUtil, JSUI);