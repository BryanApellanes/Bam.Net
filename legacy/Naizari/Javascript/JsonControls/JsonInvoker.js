if(!JSUI)
    alert("The core JSUI.js file was not loaded");

if(!JsonInput)
    alert("The JsonInput.js file was not loaded");

JSUI.RegisterJsonTag("input");
JSUI.RegisterJsonTag("select");

JsonInvoker = {};                     
JsonInvoker.ParameterDelimiter = "$#%"; // const defined in JavascriptServer

// this will be keyed by the invokerId where values are ParameterSource obejcts
// defined in JsonInput.js
JsonInvoker.RequiredParameterSources = {}; 

JsonInvoker.UseParameterSources = function(strInvokerJsonId, arrSourceIds){
    var requiredParameterSources = [];
    for(var i = 0; i < arrSourceIds.length; i++){
        var sourceId = arrSourceIds[i];
        requiredParameterSources.push(JsonInput.GetParameterSource(sourceId));
    }
    JsonInvoker.RequiredParameterSources[strInvokerJsonId] = requiredParameterSources;   
}

JsonInvoker.GetParameterValues = function(strInvokerJsonId){
    var retVal = [];
    var paramSourceInfoArray = JsonInvoker.RequiredParameterSources[strInvokerJsonId];
    if(paramSourceInfoArray){        
        //loop through the array and get the value for each
        for(var i = 0; i < paramSourceInfoArray.length; i++){
            var paramSourceInfo = paramSourceInfoArray[i];
            if(paramSourceInfo.TagName == "select"){
                retVal.push(JsonInvoker.GetSelectParameterValue(paramSourceInfo.InputJsonId));
                continue;
            }
            
            if(paramSourceInfo.TagName == "input"){
                retVal.push(JsonInvoker.GetInputParameterValue(paramSourceInfo.InputJsonId));
                continue;
            }
                        
            var message = "The ParameterSource information registered by jsonid '" + paramSourceInfo.InputJsonId + " has an unsupported TagName.";
            if(MessageBox != null && MessageBox != 'undefined')
                MessageBox.Show(message);
            else
                alert(message);
        }
    }else{
        var message = strInvokerJsonId + " was not registered as a parameter dependent invoker.";
        if(typeof(MessageBox) != "undefined")
            MessageBox.Show(message);
        else
            alert(message);                                       
    }
    return retVal;
}

JsonInvoker.GetSelectParameterValue = function(strSelectJsonId){
    var paramSource = JSUI.GetElementByAttributeValue("select", "jsonid", strSelectJsonId);
    if(!paramSource)
    {
        var message = "The SELECT element with json id '" + strSelectJsonId + "' was not found.";
        if(MessageBox)
            MessageBox.Show(message);
        else
            alert(message);
    }
    var retVal = paramSource.options[paramSource.selectedIndex].value.replace("$#%", "%#$");
    return retVal;
}

JsonInvoker.GetTextAreaParameterValue = function(strTextAreaJsonId){
    var paramSource = JSUI.GetElementByAttributeValue("textarea", "jsonid", strTextAreaJsonId);
    if(!paramSource)
    {
        var message = "The TEXTAREA element with json id '" + strTextAreaJsonId + "' was not found.";
        if(MessageBox)
            MessageBox.Show(message);
        else
            alert(message);
    }
    var retVal = paramSource.value.replace("$#%", "%#$");
    return retVal;
}

JsonInvoker.GetInputParameterValue = function(strInputJsonId){
    var paramSource = JSUI.GetElementByAttributeValue("input", "jsonid", strInputJsonId);
    if(!paramSource)
    {
        var message = "The INPUT element with json id " + strInputJsonId + " was not found.";
        if(MessageBox !== undefined && MessageBox !== null)
            MessageBox.Show(message);
        else
            alert(message);

        return false;
    }
    var retVal = "";
    if(paramSource.getAttribute("type") == "checkbox"){
        retVal = paramSource.checked ? "true": "false";
    }else{    
        retVal = paramSource.value.replace("$#%", "%#$");
        if(retVal == "")
            retVal = "&nbsp;";
    }
    return retVal;
}


JSUI.Assimilate(JsonInvoker);//, JSUI);