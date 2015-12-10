if(JSUI === undefined || JSUI === null)
    alert("The core JSUI.js file was not loaded.");

var MSInterop = {};
MSInterop.BeforePostBackListeners = [];
MSInterop.AfterPostBackListeners = [];

MSInterop.AddBeforePostBackListener = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    
    for(var i = 0; i < this.BeforePostBackListeners.length; i++){
        if(this.BeforePostBackListeners[i] == funcPointer)
            return;
    }
    MSInterop.BeforePostBackListeners.push(funcPointer);
}

MSInterop.AddAfterPostBackListener = function(funcPointer){
    JSUI.IsFunctionOrDie(funcPointer);
    
    for(var i = 0; i< this.AfterPostBackListeners.length; i++){
        if(this.AfterPostBackListeners[i] == funcPointer)
            return;
    }
    
    MSInterop.AfterPostBackListeners.push(funcPointer);
}

MSInterop.GetDocumentBody = function(){
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}

MSInterop.OnBeforePostBack = function(sender, args){
    for(var i = 0 ; i < JSUI.BeforePostBackListeners.length;i++){
        MSInterop.BeforePostBackListeners[i](sender, args);
    }
}

MSInterop.OnAfterPostBack = function(sender, args){
    for(var i =  0; i < JSUI.AfterPostBackListeners.length; i++){
        MSInterop.AfterPostBackListeners[i](sender, args);
    }
}

MSInterop.Initialize = function(){
try {
        if(Sys !== undefined){
            if(Sys.WebForms){
                if(Sys.WebForms.PageRequestManager){
                    var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
                    if( pageRequestManager ){
                        pageRequestManager.add_initializeRequest(MSInterop.OnBeforePostBack);
                        pageRequestManager.add_endRequest(MSInterop.OnAfterPostBack);
                    }
                }
            }
        }
    }catch(ex){}
}    
    
//MSInterop.OnWindowUnloadFunctions = [];

//MSInterop.addOnWindowUnload = function(funcPointer){
//    JSUI.IsFunctionOrDie(funcPointer);
//    if(!JSUI.ArrayContains(JSUI.OnWindowUnloadFunctions, funcPointer)){
//        MSInterop.OnWindowUnloadFunctions.push(funcPointer);
//    }
//    window.onUnload = JSUI.OnUnload;
//}

//MSInterop.OnWindowUnload = function(){
//    for(var i = 0; i < JSUI.OnWindowUnloadFunctions.length;i++){
//        JSUI.OnWindowUnloadFunctions[i]();
//    }
//}

MSInterop.CleanUpEvents = function(){
    for(var i = 0; i < JSUI.AttachedEvents.length; i++){
        var attachedEvent = JSUI.AttachedEvents[i];
        if( attachedEvent.Element && attachedEvent.Function && attachedEvent.EventName ){
            JSUI.RemoveEventHandler(attachedEvent.Element, attachedEvent.Function, attachedEvent.EventName);
        }
    }
}

JSUI.Assimilate(MSInterop);
JSUI.addOnWindowLoad(MSInterop.Initialize);
JSUI.addOnWindowUnload(MSInterop.CleanUpEvents);