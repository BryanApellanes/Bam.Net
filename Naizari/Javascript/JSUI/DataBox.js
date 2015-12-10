if(JSUI === undefined)
    alert("The core JSUI.js file was not loaded.");

var DataBox = {};
DataBox.Scripts = {};

DataBox.getBoxContent = function(strDomId, strBoxName) {
    var clientKey = JSUI.RandomString(4);
    var callBack = function(html){
        JSUI.GetElement(strDomId).innerHTML = html;
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = JSUI.GetFilePath() + "?box=" + strBoxName + "&s=y&domid=" + strDomId + "&ck=" + clientKey;
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
    }
    var url = JSUI.GetFilePath() + "?box=" + strBoxName + "&domid=" + strDomId + "&ck=" + clientKey;
    var asyncRequest = new JSUI.AsyncRequestClass(url, callBack);
    asyncRequest.Send();
}

DataBox.GetBoxContent = function(domId, templateName) {
    DataBox.getBoxContent(domin, templateName);
}

DataBox.getScripts = function(strClientKey) {
    if(!DataBox.Scripts[strClientKey]){
        var script = document.createElement("script");
        script.setAttribute("language", "javascript");
        script.setAttribute("type", "text/javascript");
        var path = JSUI.GetFilePath() + "?dbs=y&ck=" + strClientKey;// databoxscripts = yes
        script.setAttribute("src", path);
        JSUI.GetDocumentBody().appendChild(script);
        DataBox.Scripts[strClientKey] = true; // make sure the same script isn't loaded multiple times
    }
}

DataBox.GetDataBoxScripts = function(key) {
    DataBox.getScripts(key);
}

JSUI.DataBox = DataBox;
