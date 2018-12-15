   
DEBUG = {};
DEBUG.Shallow = false;
DEBUG.AlertObject = function(obj){
    var text = DEBUG.GetProperties(obj);
    alert(text);
}

DEBUG.Pause = function(){
    var x = null;
}

DEBUG.GetProperties = function(obj, intTabCount) {
    if (!intTabCount)
        intTabCount = 0;

    var text = "";
    var tabCount = intTabCount * 1;
    var tabs = DEBUG.AddTabs("", tabCount);
    for (prop in obj) {
        if (prop == 'clone') continue;
        var t = typeof (obj[prop]);
        var deTab = false;
        if (typeof obj[prop] == "object" && !this.Shallow) {
            tabCount++;
            deTab = true;
            text += tabs + DEBUG.GetProperties(obj[prop], tabCount);
        }
        else {
            if (deTab) {
                tabCount--;
                deTab = false;
            }
            text += tabs + prop + ": " + obj[prop] + "\r\n";
        }
    }
    return text;
}

DEBUG.AddTabs = function(strToAddTo, intTabCount){
    var txt = "";
    var tabCount = intTabCount * 1; // lazy way of converting to int
    for(var i = 0 ; i < tabCount; i++){
        txt += "\t";
    }    
    return txt + strToAddTo;
}

JSUI.Silarize(DEBUG, JSUI);

