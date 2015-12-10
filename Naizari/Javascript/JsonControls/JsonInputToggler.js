if(!JSUI)
    alert("The core JSUI.js file was not loaded");
    
var JsonInputToggler = {};
JSUI.RegisterJsonTag("span");
JSUI.RegisterJsonTag("input");


JsonInputToggler.TogglePair = function(hideId, showId, updateLabel){
    JSUI.HideElement(hideId);
    JSUI.ShowElement(showId, "inline");
    if(!updateLabel){
        var showElement = JSUI.GetElement(showId);
        if(showElement.select)
            showElement.select();
    }
    else{ // a potential change
        var newTextElement = JSUI.GetElement(hideId);
        var labelElement = JSUI.GetElement(showId); 
    
        var oldText;
        var propName;
        if(labelElement.innerText){
            oldText = labelElement.innerText;                               
            propName = "innerText";                                
        }else{
            oldText = labelElement.innerHTML;
            propName = "innerHTML";
        }
    
        var newText;
        if(newTextElement.selectedIndex > 0){
            newText = newTextElement.options[newTextElement.selectedIndex].text;
        }else{
            newText = newTextElement.value;
        }
        
        if(oldText.trim() != newText.trim()){
            labelElement[propName] = newText;
            
        }            
    }
}
