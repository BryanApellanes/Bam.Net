if(!JSUI)
    alert("The core JSUI.js file was not loaded.");

JsonInput = {};
JsonInput.ParameterSources = {};
JsonInput.RegisterAsSource = function(strInputJsonId, strTagName){
    //alert(strInputJsonId + ": " + strTagName);
    JsonInput.ParameterSources[strInputJsonId] = new JsonInput.ParameterSource(strInputJsonId, strTagName);
}

JsonInput.GetParameterSource = function(strInputJsonId){
    var paramSource = JsonInput.ParameterSources[strInputJsonId];
    if(!paramSource){
        var message = "The JsonInput control with jsonid of '" + strInputJsonId + "' was not found.";
        if(MessageBox != null && MessageBox != 'undefined')
            MessageBox.Show(message);
        else
            alert(message);
    
        return false;
    }
    return paramSource;
}

JsonInput.ParameterSource = function(strInputJsonId, strTagName){
    this.InputJsonId = strInputJsonId;
    this.TagName = strTagName;
}

JSUI.Silarize(JsonInput, JSUI);