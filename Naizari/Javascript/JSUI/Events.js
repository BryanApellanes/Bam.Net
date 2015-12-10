if(!JSUI)
    alert("The core JSUI.js file was not loaded.");
var Events = {};

Events.EventSources = {};
Events.RegisterEventSource = function(strName, objScriptObject){
    Events.EventSources[strName] = objScriptObject;
    JSUI.RegisterScriptObject(strName, objScriptObject);
}

Events.GetEventSource = function(strSourceName){
    return Events.EventSources[strSourceName];
}

Events.FireEvent = function(strSourceName, strEventName, arrArgs){
    if(Events.EventSources[strSourceName]){
        var eventSource = Events.EventSources[strSourceName];
        if(eventSource[strEventName]){
            JSUI.CallFunctions(eventSource[strEventName], strEventName, arrArgs);
        }
    }
}
    
Events.OnWindowLoadFunctions = [];
Events.OnWindowUnloadFunctions = [];
Events.OnWindowResizeFunctions = [];
Events.OnWindowScrollFunctions = [];

Events.AttachedEvents = []; // required for MSAjaxInterop.js

Events.AttachedEventClass = function(objElement, funcPointer, strEventName){
    this.Element = objElement;
    this.Function = funcPointer;
    this.EventName = strEventName;
}

Events.AddEventHandler = function(objectToAttachTo, funcPointer, strEventName){
    objectToAttachTo = JSUI.GetElement(objectToAttachTo);
    var ieFunc = null;
    if( objectToAttachTo.addEventListener )
        objectToAttachTo.addEventListener(strEventName, funcPointer, false);
    else if (objectToAttachTo.attachEvent){
        ieFunc = function(){funcPointer.call(objectToAttachTo)};
        objectToAttachTo.attachEvent("on" + strEventName, ieFunc);
    }
    else {
        if(!objectToAttachTo[strEventName])
            objectToAttachTo[strEventName] = [];
        objectToAttachTo[strEventName].push(funcPointer);
    }        
    Events.AttachedEvents.push(new Events.AttachedEventClass(objectToAttachTo, ieFunc == null ? funcPointer: ieFunc, strEventName));
}

Events.addEventHandler = function(o, f, n) {
    Events.AddEventHandler(o, f, n);
}
  
Events.FireElementEvent = function(strElementOrId, strEventName, args){
    objectToFire = JSUI.GetElement(strElementOrId);
    if(objectToFire[strEventName] && JSUI.IsFunction(objectToFire[strEventName]))
        objectToFire[strEventName](args);        
    if(objectToFire["on" + strEventName] && JSUI.IsFunction(objectToFire["on" + strEventName]))
        objectToFire["on" + strEventName](args);
        
    throw new JSUI.Exception("Unable to find the specified event " + strEventName + " on element " + strElementOrId);
}


Events.RemoveEventHandler = function(objectToDetachFrom, funcPointer, strEventName){
        objectToDetachFrom = JSUI.GetElement(objectToDetachFrom);
        if( objectToDetachFrom.removeEventListener )
            objectToDetachFrom.removeEventListener(strEventName, funcPointer, false);
        else if (objectToDetachFrom.detachEvent){
            for(var i = 0 ; i < Events.AttachedEvents.length;i++){
                var ev = Events.AttachedEvents[i];
                if(ev.Element == objectToDetachFrom && ev.EventName == strEventName){
                    funcPointer = ev.Function;
                }
            }
            objectToDetachFrom.detachEvent("on" + strEventName, funcPointer);
        }
        else
            objectToDetachFrom[strEventName] = function(){};
}

Events.clearEventHandlers = function(el, evtName) {
    JSUI.forEach(Events.AttachedEvents, function(key, value) {
        if (value.Element.id == el.id && value.EventName == evtName) {
            Events.RemoveEventHandler(value.Element, value.Function, value.EventName);
        }
    });
}    
     
Events.OnWindowLoad = function(){
    for(var i = 0; i < Events.OnWindowLoadFunctions.length; i++){
        Events.OnWindowLoadFunctions[i]();  
    }    
}

Events.OnWindowScroll = function(){
    for(var i = 0; i < Events.OnWindowScrollFunctions.length; i++){
        Events.OnWindowScrollFunctions[i]();
    }
}

Events.OnWindowUnload = function(){
    for(var i = 0; i < Events.OnWindowUnloadFunctions.length; i++){
        Events.OnWindowUnloadFunctions[i]();
    }
}

Events.OnWindowResize = function(){
    for(var i = 0; i < Events.OnWindowResizeFunctions.length; i++){
        Events.OnWindowResizeFunctions[i]();
    }
}

Events.addOnWindowScroll = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    if(!JSUI.ArrayContains(Events.OnWindowScrollFunctions, funcPointer)){
        Events.OnWindowScrollFunctions.push(funcPointer);
        window.onscroll = Events.OnWindowScroll;
    }
}

Events.addOnWindowLoad = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    if(!JSUI.ArrayContains(Events.OnWindowLoadFunctions, funcPointer)){
        Events.OnWindowLoadFunctions.push(funcPointer);
        window.onload = Events.OnWindowLoad;
    }
}

Events.addOnWindowUnload = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    if(!JSUI.ArrayContains(Events.OnWindowUnloadFunctions, funcPointer)){
        Events.OnWindowUnloadFunctions.push(funcPointer);
        window.onunload = Events.OnWindowUnload;
    }
}

Events.addOnWindowResize = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    if(!JSUI.ArrayContains(Events.OnWindowResizeFunctions, funcPointer)){
        Events.OnWindowResizeFunctions.push(funcPointer);
        window.onresize = Events.OnWindowResize;
    }
}

Events.AbortAsyncRequests = function(){
    if(JSUI.HttpRequests.length > 0){
        for(var i = 0; i < JSUI.HttpRequests.length; i++){
            JSUI.HttpRequests[i].abort();
        }
    }
}

Events.addOnWindowUnload(Events.AbortAsyncRequests);

JSUI.Assimilate(Events, JSUI);