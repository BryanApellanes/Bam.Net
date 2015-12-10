if(JSUI === undefined || JSUI === null)
    alert("The core JSUI.js file was not loaded");

Functions = {};
Functions.RegisterFunction = function(strFunctionId, func){
    JSUI.IsFunctionOrDie(func);
    Functions[strFunctionId] = func;
}

Functions.GetFunction = function(strFunctionId){
    if(Functions[strFunctionId])
        return Functions[strFunctionId];
    else
        JSUI.ThrowException("function named " + strFunctionId + " was not found.");
}

JSUI.Assimilate(Functions);//, JSUI);
JSUI.Functions = Functions;