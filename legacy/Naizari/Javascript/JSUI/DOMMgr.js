if(JSUI === undefined || JSUI === null)
    alert("The core JSUI.js file was not loaded.");

var DOMMgr = {};

DOMMgr.GetElementByAttributeValue = function(strTagFilter, strAttributeName, strAttributeValue) {
    var retVal = jQuery(strTagFilter + '[' + strAttributeName + '=' + strAttributeValue + ']');
    if (retVal.length > 1)
        throw new JSUI.Exception("More than one element found with the attribute '" + strAttributeName + "' having a value of '" + strAttributeValue + "'");

    return retVal[0];

}

DOMMgr.GetElementsByAttributeValue = function(topElementOrId, strAttributeName, strAttributeValue) {
    return jQuery('[' + strAttributeName + '=' + strAttributeValue + ']', topElementOrId);

}

DOMMgr.RegisteredJsonTags = [];
DOMMgr.RegisterJsonTag = function(strTagName){
    if(!JSUI.ArrayContains(DOMMgr.RegisteredJsonTags, strTagName))
        DOMMgr.RegisteredJsonTags.push(strTagName);
}

DOMMgr.ScriptObjects = {};
DOMMgr.RegisterScriptObject = function(strName, objScriptObject){
    DOMMgr.ScriptObjects[strName] = objScriptObject;
}

DOMMgr.elementIs = function(objElement, strTagNameToCheck){
    return objElement.tagName.toLowerCase() == strTagNameToCheck;
}

DOMMgr.ElementIs = function(el, tag) {
    return DOMMgr.elementIs(el, tag);
}
DOMMgr.GetJsonElement = function(strJsonId, optionalTagNameFilter) {
    if (DOMMgr.ScriptObjects[strJsonId])
        return DOMMgr.ScriptObjects[strJsonId];

    var tagFilter = optionalTagNameFilter ? optionalTagNameFilter : null;
    var tagFilters = [];
    if (!tagFilter) {
        tagFilters.concat(DOMMgr.RegisteredJsonTags);
        for (var i = 0; i < DOMMgr.RegisteredJsonTags.length; i++) {
            tagFilters.push(DOMMgr.RegisteredJsonTags[i]);
        }
    } else {
        tagFilters.push(tagFilter);
    }
    for (var i = 0; i < tagFilters.length; i++) {
        var element = DOMMgr.GetElementByAttributeValue(tagFilters[i], "jsonid", strJsonId);
        if (element)
            return element;
    }
    var el = jQuery('[jsonid=' + strJsonId + ']')[0];
    if (!JSUI.isNullOrUndef(el))
        return el;
    
    throw new JSUI.Exception("JsonElement with id '" + strJsonId + "' was not found.");
}

DOMMgr.getElement = function (strElementOrId){ 
     if(!JSUI.isString(strElementOrId))
        return strElementOrId;    
     var element = document.getElementById(strElementOrId);
     if( element )
        return element;    
     else{
        return DOMMgr.GetJsonElement(strElementOrId);
     }
}

DOMMgr.GetElement = function(el) {
    return DOMMgr.getElement(el);
}

DOMMgr.DisableTextSelect = function(elementOrId) {
    if (elementOrId === undefined || elementOrId === null)
        elementOrId = document.body;
    var element = DOMMgr.GetElement(elementOrId);
    if (element.onselectstart != null && element.onselectstart != 'undefined') { //IE
        element.onselectstart = function() { return false; }
    } else if (element.style.MozUserSelect != null && element.style.MozUserSelect != 'undefined') {
        element.style.MozUserSelect = 'none';
    } 
}

DOMMgr.GetElementsByClassName = function(elementOrId, strClassName){
    var element = DOMMgr.GetElement(elementOrId);
    var childElements = element.getElementsByTagName("*");
    var retVal = [];
    for(var i = 0; i < childElements.length; i++){
        if( childElements[i].className == strClassName )
            retVal.push(childElements[i]);        
    }
    
    return retVal;
}

DOMMgr.GetElementPosition = function(elementOrId){
    var element = DOMMgr.GetElement(elementOrId);
	var curleft = curtop = 0;
	if (element.offsetParent) {
		curleft = element.offsetLeft
		curtop = element.offsetTop
		while (element = element.offsetParent){
			curleft += element.offsetLeft
			curtop += element.offsetTop
		}
	}
	return {Left: curleft, Top: curtop};
}

DOMMgr.GetElementsByAttribute = function(strAttributeName){
    var allElements = document.body.getElementsByTagName('*');
    var retVal = [];
    for(var i = 0; i < allElements.length; i++){
        if(allElements[i].getAttribute(strAttributeName))
            retVal.push(allElements[i]);
    }
    
    return retVal;
}  

DOMMgr.GetElementsWithAttribute = function(parentElementOrId, strAttributeName){
    var parentElement = DOMMgr.GetElement(parentElementOrId);
    var allChildElements = parentElement.getElementsByTagName('*');
    
    var retVal = [];
    for(var i = 0; i < allChildElements.length; i++){
        var element = allChildElements[i];
        if(element.getAttribute(strAttributeName))
            retVal.push(element);        
    } 
    
    return retVal;   
}

DOMMgr.GetInnerText = function(elementOrId){
    var ele = DOMMgr.GetElement(elementOrId);
    if(document.all){
        return ele.innerText;
    }
    else{
        return ele.textContent;
    }
}

DOMMgr.GetTextContent = function(elementOrId){
    return DOMMgr.GetInnerText(elementOrId);
}

DOMMgr.setText = function(elementOrId, text){
    //try{
    var ele = DOMMgr.GetElement(elementOrId);
    ele.innerText = text;
    ele.textContent = text;
    //}catch(e){}// added try catch for IE
}

DOMMgr.SetInnerText = function(el, text) {
    DOMMgr.setText(el, text);
}

DOMMgr.SetTextContent = function(elementorId, text){
    DOMMgr.SetInnerText(elementorId, text);
}

DOMMgr.insertFirst = function (strNewParentDomId, strTargetDomId) {
    var child = DOMMgr.GetElement(strTargetDomId);
    var parent = DOMMgr.GetElement(strNewParentDomId);

    if (child.parentNode)
        child.parentNode.removeChild(child);
    if (parent.hasChildNodes)
        parent.insertBefore(child, parent.firstChild);
    else
        parent.appendChild(child);
}

DOMMgr.SetElementAsFirstChild = DOMMgr.insertFirst;

DOMMgr.insertAfter = function (refEl, toInsert) {
    refEl.parentNode.insertBefore(toInsert, refEl.nextSibling);
}
//function(parent, toInsert, reference) {
    //parent.insertBefore(toInsert, reference.nextSibling);
//}

DOMMgr.SetElementAsLastChild = function (strNewParentDomId, strTargetDomId){
    var child = DOMMgr.GetElement(strTargetDomId);
    var parent = DOMMgr.GetElement(strNewParentDomId);
    
    parent.appendChild(child);
}

DOMMgr.show = function(elementOrId, options) {
    var config = { fade: false, display: 'block', end: 10, fps: 12 };
    JSUI.copyProperties(options, config);
    if (Effects !== null && Effects !== undefined && config.fade) {
        Effects.fadeIn(elementOrId, config);
    } else {
        JSUI.showElement(elementOrId, config.display);
    }
}

DOMMgr.hide = function(el, options) {
    var config = { fade: false, display: 'block', end: 0, fps: 12 };
    JSUI.copyProperties(options, config);
    if (Effects !== null && Effects !== undefined && config.fade) {
        Effects.fadeOut(el, config);
    } else {
        DOMMgr.hideElement(el);
    }
}

DOMMgr.hideElement = function(el){
    DOMMgr.showElement(el, 'none');
}

DOMMgr.showElement = function(elementOrId, displayValue) {
    if(displayValue === null || displayValue === undefined){
        displayValue = 'block';
    }
    DOMMgr.GetElement(elementOrId).style.display = displayValue;
}

DOMMgr.ShowElement = function(el, v){
    DOMMgr.showElement(el, v);
}

DOMMgr.ShowElementInline = function(elementOrId){
    DOMMgr.GetElement(elementOrId).style.display = 'inline';
}

DOMMgr.ShowElementBlock = function(elementOrId){
    DOMMgr.GetElement(elementOrId).style.display = 'block';
}

DOMMgr.mouseIsOverElement = function(elementOrId, event){
    var element = DOMMgr.GetElement(elementOrId);
    var e = event ? event: window.event;
    
    var outHorizontally = false;
    var outVertically = false;
    var body = JSUI.GetDocumentBody();
    if( e.clientX > (DOMMgr.getElementWidth(element.id) + parseInt(element.offsetLeft)))
        outHorizontally = true;
    if( e.clientX < parseInt(element.offsetLeft))
        outHorizontally = true;
    if( e.clientY > (DOMMgr.getElementHeight(element.id) + parseInt(element.offsetTop - body.scrollTop)))
        outVertically = true;   
    if( e.clientY < parseInt(element.offsetTop - body.scrollTop) )
        outVertically = true;
    
    if( !outHorizontally && !outVertically )
        return true;
    else
        return false;
}

DOMMgr.MouseIsOverElement = function(el, ev) {
    return DOMMgr.mouseIsOverElement(el, ev);
}

DOMMgr.HideElement = function(elementOrId){
    DOMMgr.GetElement(elementOrId).style.display = 'none';
} 

DOMMgr.MakeElementInvisible = function(elementOrId){
    DOMMgr.GetElement(elementOrId).style.visibility = "hidden";
}

DOMMgr.MakeElementVisible = function(elementOrId){
    DOMMgr.GetElement(elementOrId).style.visibility = "visible";
}

DOMMgr.ToggleElementDisplay = function(strElementOrId, strInlineOrBlock){
    if(!strInlineOrBlock)
        strInlineOrBlock = "block";
        
    var targetElement = DOMMgr.GetElement(strElementOrId);
    var display = targetElement.style.display;
    var show = true;
    if(display == null || display == 'undefined' || display != "none")
        show = false;
        
    if(show)
        DOMMgr.ShowElement(targetElement, strInlineOrBlock);
    else
        DOMMgr.HideElement(targetElement);
}

DOMMgr.toggle = function(el, styleName, value) {
    var cur = el[stylName];
    var newVal = value;
    if (JSUI.isNullOrUndef(el.toggle))
        el.toggle = {};
    if (JSUI.isNullOrUndef(el.toggle[styleName])) {
        el.toggle[styleName] = cur;
        newVal = value;
    } else {
        newVal = el.toggle[styleName];
    }
    el[styleName] = newVal;
    return JSUI;
}


DOMMgr.getStyleNum = function(elementOrId, styleName, defaultValue) {
    if (defaultValue === null || defaultValue === undefined) {
        defaultValue = -1;
    }
    
    JSUI.isNumberOrDie(defaultValue);
    var ele = DOMMgr.GetElement(elementOrId);
    if (ele.style[styleName] != "") {
        var trimNum = 2; // px;
        if (ele.style[styleName].toString().endsWith("%")) {
            trimNum = 1;
        }
        var retVal = ele.style[styleName].substr(0, ele.style[styleName].length - trimNum);
        JSUI.isNumberOrDie(retVal);
        return parseInt(retVal);
    } else {
        return defaultValue;
    }
}

DOMMgr.getStyleUnit = function(elementOrId, styleName) {
    var ele = DOMMgr.GetElement(elementOrId);
    if (ele.style[styleName] != "") {
        if (ele.style[styleName].toString().endsWith("%"))
            return "%";

        if (ele.style[styleName].toString().endsWith("px"))
            return "px";

        if (ele.style[styleName].toString().endsWith("pt"))
            return "pt";
    }
    return "px";
}


DOMMgr.setStyleNum = function(elementOrId, styleName, value) {
    styleName = styleName.pascalCase("-");
    var ele = DOMMgr.GetElement(elementOrId);
    var unit = DOMMgr.getStyleUnit(ele, styleName);
    ele.style[styleName] = value + unit;
}

DOMMgr.incrementStyle = function(elementOrId, styleName, incrementValue) {
    JSUI.isNumberOrDie(incrementValue);
    styleName = styleName.pascalCase("-");
    var element = DOMMgr.GetElement(elementOrId);

    if (styleName.toLowerCase().endsWith("color")) {
        var currentColor = JSUI.Color.from(element.style[styleName]);
        var newColor = JSUI.Color.from(currentColor);
//        JSUI.Assimilate(currentColor, newColor);
        var newR = currentColor.R + incrementValue;
        var newG = currentColor.G + incrementValue;
        var newB = currentColor.B + incrementValue;
        if (newR >= 255) newR = 255;
        if (newR <= 0) newR = 0;
        if (newG >= 255) newG = 255;
        if (newG <= 0) newG = 255;
        if (newB >= 255) newB = 255;
        if (newB <= 0) newB = 255;

        newColor.R = newR;
        newColor.G = newG;
        newColor.B = newB;
        var htmlColor = JSUI.Color.from(newColor).toString();
        element.style[styleName] = htmlColor;
    } else {
//        var unit = DOMMgr.getStyleUnit(elementOrId, styleName);
        var oldValue = DOMMgr.getStyleNum(element, styleName, incrementValue);
        var newValue = oldValue + incrementValue;
        DOMMgr.setStyleNum(element, styleName, newValue);
    }
}

DOMMgr.decrementStyle = function(elementOrId, styleName, decrementValue) {
    DOMMgr.incrementStyle(elementOrId, styleName, -decrementValue);
}

DOMMgr.getElementWidth = function (elementOrId, defaultValue) {
    return jQuery(JSUI.getElement(elementOrId)).width();
}

DOMMgr.GetElementWidth = DOMMgr.getElementWidth;

DOMMgr.getElementHeight = function (elementOrId, defaultValue) {
    return jQuery(JSUI.getElement(elementOrId)).height();
}

DOMMgr.getDimensions = function(el) {
    return { height: jQuery(el).height(), width: jQuery(el).width() };
}

DOMMgr.GetElementHeight = DOMMgr.getElementHeight;

DOMMgr.DisableElement = function(strElementOrId){
    DOMMgr.GetElement(strElementOrId).disabled = true;
}

DOMMgr.EnableElement = function(strElementOrId){
    DOMMgr.GetElement(strElementOrId).disabled = false;
}

DOMMgr.getDocumentBody = function () {
    return document.body;
    //return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}

DOMMgr.GetDocumentBody = function() {
    return DOMMgr.getDocumentBody();
}

DOMMgr.createSelect = function(arr, options) {
    JSUI.isArrayOrDie(arr);
    var config = { value: "value", text: "text" };
    JSUI.assimilate(options, config);
    var select = document.createElement("select");
    JSUI.forEach(arr, function(key, value) {
        if (!JSUI.isNullOrUndef(value[config.value])) {
            var option = document.createElement("option");
            option.setAttribute("value", value[config.value].toString());
            option.appendChild(document.createTextNode(value[config.text]));
            select.appendChild(option);
        }
    });

    return select;
}

DOMMgr.createCheckBoxes = function(arr, options) {
    JSUI.isArrayOrDie(arr);
    var config = { value: "value", text: "text", cssclass: "" };
    JSUI.assimilate(options, config);
    var ret = [];
    JSUI.forEach(arr, function(key, value) {
        var span = document.createElement('span');
        span.setAttribute("class", config.cssclass);
        span.id = JSUI.randomString(8);
        var chk = document.createElement('input');
        chk.id = JSUI.randomString(8);
        chk.setAttribute("type", "checkbox");
        chk.setAttribute("value", value[config.value]);

        span.appendChild(chk);
        var label = document.createElement('label');
        label.setAttribute('for', chk.id);
        label.appendChild(document.createTextNode(value[config.text]));

        span.appendChild(label);
        ret.push(span);
    });

    return ret;
}

JSUI.Assimilate(DOMMgr);