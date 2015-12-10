
$$JsonId$$.Init = function(){
    //$$JsonId$$.DateTime = JSUI.Construct("JsonDateTime", "$$Time$$");
    var date = new Date(Date.UTC($$Year$$, $$Month$$ - 1, $$DayOfMonth$$, $$Hour$$, $$Minute$$, $$Second$$, 0));
    var text = JSUI.GetElement('$$DomId$$_text');
    var input = JSUI.GetElement('$$JsonId$$');
    
    var now = new Date();
    var offset = -(now.getTimezoneOffset()/60);
    var utc = date.getTime() + (date.getTimezoneOffset() * 60000);
    var localDate = new Date(utc + (3600000 * offset));
    var val = localDate.toLocaleString();
    
    input.value = val;
    input.setAttribute("value", val);        
    text.innerHTML = val;
}
$$JsonId$$.Init();
