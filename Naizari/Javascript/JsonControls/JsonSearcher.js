if(JSUI == null || JSUI == 'undefined')
    alert("The core JSUI.js file was not loaded");
    
var JsonSearchers = {}
JsonSearchers.Searcherfy = function(strJsonId, strTextBoxId, strResultsId, intMinChars, funcOnSelected){
    JSUI.IsFunctionOrDie(funcOnSelected);
    var searcher = new JsonSearchers.JsonSearcherClass(strJsonId, intMinChars);
    searcher.Initialize(strTextBoxId, strResultsId);
    searcher.OnResultSelected = funcOnSelected;
}

JsonSearchers.JsonSearcherClass = function(strJsonId, intMinChars) {
    if (strJsonId == null || strJsonId == 'undefined' || strJsonId == "")
        throw new JSUI.Exception("JsonId was not specified");

    this.JsonId = strJsonId;
    this.MinChars = 3;
    this.TextBoxId = null;
    this.ResultsId = null;
    this.HighlightColor = "#0092B5";
    this.OriginalText = '';

    JsonSearchers[strJsonId] = this;
    var thisObj = this;

    if (intMinChars != null && intMinChars != 'undefined')
        this.MinChars = parseInt(intMinChars);

    this.CurrentInput = "";

    this.OnResultSelected = function(selectedElement) { } // override this

    var preSearch = function(e) {
        e = JSUI.GetEvent(e);
        if (e.keyCode == '36' || e.keyCode == '35' || e.keyCode == '32' || e.keyCode == '13')
            return false;
        var textBox = JSUI.GetElement(thisObj.TextBoxId);
        if (textBox.value.length < thisObj.MinChars || textBox.value == thisObj.OriginalText) {
            return false;
        } else {
            if (thisObj.CurrentInput != textBox.value) {
                thisObj.CurrentInput = textBox.value;
                JSUI.FireEvent(thisObj.JsonId + "_startsearchevent", thisObj.JsonId + "_startsearch");
            }
        }
    }

    this.SearchComplete = function() {
        var el = JSUI.getElement(thisObj.TextBoxId);
        JSUI.clearEventHandlers(el, "blur");
        var searchResultsList = new ArrowList.ArrowListClass(thisObj.ResultsId, thisObj.TextBoxId, "div");
        searchResultsList.HighlightBackgroundColor = thisObj.HighlightColor;
        searchResultsList.Initialize();
        searchResultsList.AddResetListener(function() {
            JSUI.HideElement(thisObj.ResultsId);
            var textbox = JSUI.GetElement(thisObj.TextBoxId);
            //                textbox.value = thisObj.OriginalText;
            textbox.focus();
        }
        );

        searchResultsList.AddSelectListener(
                thisObj.OnResultSelected

        );
    }


    this.Initialize = function(strTextBoxId, strResultsId) {
        thisObj.TextBoxId = strTextBoxId;
        thisObj.ResultsId = strResultsId;
        thisObj.OriginalText = JSUI.GetElement(strTextBoxId).value;
        JSUI.RegisterEventSource(thisObj.JsonId + "_startsearchevent", new Object());
        JSUI.AddEventHandler(strTextBoxId, preSearch, "keyup");
        //JSUI.SetPosition(strResultsId, JSUI.Orientation.BottomLeft(strTextBoxId));
    }
}

