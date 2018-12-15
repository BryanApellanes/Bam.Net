if(!JSUI)
    alert("The core JSUI.js file was not loaded.");
    
var Collapsible = {};
Collapsible.CollapsibleClassInstances = {};

Collapsible.CollapsibleClass = function(strCollapsibleId, strExpanderId, strCollapserId){
    var collapsibleElement = JSUI.GetElement(strCollapsibleId);
    Collapsible.CollapsibleClassInstances[strCollapsibleId] = this;
    var collapsed = true;
    
     
    if(!strExpanderId)
        throw new JSUI.Exception("parameter 'strExpanderId' was not specified");
        
    if(!strCollapserId)
        strCollapserId = strExpanderId;
        
    var expanderElement = JSUI.GetElement(strExpanderId);
    var collapserElement = JSUI.GetElement(strCollapserId);
    
    this.Collapse = function(){
        
    }
}




JSUI.Assimilate(Collapsible);//, JSUI);