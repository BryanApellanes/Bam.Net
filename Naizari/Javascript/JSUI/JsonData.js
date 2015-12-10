if(!JSUI)
    alert("The core JSUI.js file was not loaded");
      
var JsonData = {};

// builds an object by looking for input and select elements
// with jsonid and propertyname attributes.  propertyname should 
// correspond to the name of a property of a class defined on the
// server 
JsonData.GetDataObject = function(parentElementOrId){
    var parentElement = JSUI.GetElement(parentElementOrId);
    var retVal = {};
   
    var inputs = parentElement.getElementsByTagName("input");
    var selects = parentElement.getElementsByTagName("select");
    var textAreas = parentElement.getElementsByTagName("textarea");
    for(var i = 0; i < inputs.length; i++){
        var input = inputs[i];
       
        var propertyName = input.getAttribute("jsonproperty");
        if(propertyName){
            var jsonId = input.getAttribute("jsonid");
            if(jsonId)
                retVal[propertyName] = JSUI.GetInputParameterValue(jsonId);
        }
    }
    
    for(var i = 0; i < selects.length; i++){
        var select = selects[i];
        var propertyName = select.getAttribute("jsonproperty");
        if(propertyName){
            var jsonId = select.getAttribute("jsonid");
            if(jsonId)
                retVal[propertyName] = JSUI.GetSelectParameterValue(jsonId);
        }
    }
    
    for(var i = 0; i < textAreas.length; i++){
        var textArea = textAreas[i];
        var propertyName = textArea.getAttribute("jsonproperty");
        if(propertyName){
            var jsonId = textArea.getAttribute("jsonid");
            if(jsonId)
                retVal[propertyName] = JSUI.GetTextAreaParameterValue(jsonId);
        }
    }
    
    var listInputElements = JSUI.GetElementsByAttributeValue(parentElementOrId, "type", "ListInput");
    for(var i = 0; i < listInputElements.length; i++){
        var element = listInputElements[i];
        var jsonId = element.getAttribute("jsonid");
        if(jsonId){
            var listInput = ListInput.InstancesById[jsonId];
            if(listInput.JsonProperty){
                retVal[listInput.JsonProperty] = [];
                for(var ii = 0; ii < listInput.Items.length; ii++){
                    var item = listInput.Items[ii];
                    var newItem = {Text: item.Text, Value: item.Value};
                    retVal[listInput.JsonProperty].push(newItem);
                }
            }
        }
    }

    return retVal;
}

JSUI.Assimilate(JsonData);//, JSUI);