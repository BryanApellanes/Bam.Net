
if(JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file wasn't loaded");
    
var ArrowList = {};
ArrowList.ArrowListClass = function(strParentId, strInputElementId, strItemTagName) {
    if (strParentId == null || strParentId == 'undefined' || strInputElementId == null || strInputElementId == 'undefined')
        throw new JSUI.Exception("strParentId and strInputElementId are required parameters");

    this.ItemElements = [];
    this.ItemTagName = strItemTagName ? strItemTagName : "div";
    this.Parent = JSUI.GetElement(strParentId);
    this.InputElement = JSUI.GetElement(strInputElementId);
    this.InputElement.setAttribute("autocomplete", "off");
    this.HighlightBackgroundColor = "#0000FF";
    this.HighlightForeColor = "#FFFFFF";
    this.ForeColor = "#000000";
    this.BackgroundColor = "#FFFFFF";
    this.ResetOnSelect = false;

    var nullElement = {};
    nullElement.UnHighlight = function() { };
    nullElement.Highlight = function() { };
    this.CurrentElement = nullElement;
    this.SelectedIndex = -1;

    var thisObj = this;
    var selectListeners = [];
    var resetListeners = [];

    this.DownArrowKey = function() {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();

        thisObj.SelectedIndex++;
        if (thisObj.SelectedIndex > thisObj.ItemElements.length - 1)
            thisObj.SelectedIndex--;

        var selected = thisObj.ItemElements[thisObj.SelectedIndex];
        if (thisObj.SelectedIndex >= 0 && selected != null && selected != 'undefined')
            selected.Highlight();
    }

    this.UpArrowKey = function() {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();
        thisObj.SelectedIndex--;
        if (thisObj.SelectedIndex == -1)
            thisObj.SelectedIndex = 0;

        var selected = thisObj.ItemElements[thisObj.SelectedIndex];
        if (thisObj.SelectedIndex >= 0 && selected != null && selected != 'undefined')
            selected.Highlight();
    }

    this.OnSelected = function() {
        for (var i = 0; i < selectListeners.length; i++) {
            selectListeners[i](thisObj.CurrentElement);
        }
        if (thisObj.ResetOnSelect) {
            thisObj.Reset();
        }
    }

    this.OnReset = function() {
        for (var i = 0; i < resetListeners.length; i++) {
            resetListeners[i](thisObj);
        }
    }

    this.Reset = function() {
        if (thisObj.CurrentElement)
            thisObj.CurrentElement.UnHighlight();
        thisObj.OnReset();
    }

    this.EnterKey = function() {
        thisObj.OnSelected();
    }

    this.AddSelectListener = function(funcListener) {
        JSUI.IsFunctionOrDie(funcListener);
        selectListeners.push(funcListener);
    }

    this.AddResetListener = function(funcListener) {
        JSUI.IsFunctionOrDie(funcListener);
        resetListeners.push(funcListener);
    }


    var connectKeyDown = function() {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
                             function(evt) {
                                 var e = JSUI.GetEvent(evt);

                                 // enter key
                                 if (e.keyCode == 13) {
                                     thisObj.EnterKey();
                                     if (e.preventDefault)
                                         e.preventDefault();

                                     e.cancelBubble = true;
                                     e.returnValue = false;
                                     return false;
                                 } else if (e.keyCode == 40) //down arrow
                                 {
                                     thisObj.DownArrowKey();
                                     return false;

                                 } else if (e.keyCode == 38) // up arrow
                                 {
                                     thisObj.UpArrowKey();
                                     return false;
                                 } else if (e.keyCode == 27) // reset on escape key
                                 {
                                     thisObj.Reset();
                                     return false;
                                 }

                                 return true;
                             },
                             "keydown"
        )
    }

    var connectKeyPress = function() {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
           function(evt) {
               var e = JSUI.GetEvent(evt);
               if (e.keyCode == 13 || e.keyCode == "13")
                   return false;
           },
           "keypress"
       )

    }

    var connectBlur = function() {
        JSUI.AddEventHandler(thisObj.InputElement, function() {
            if (thisObj.CurrentElement === null || thisObj.CurrentElement === undefined || thisObj.CurrentElement === nullElement) {
                thisObj.Reset();
            }
        }, "blur");
    }

    var connectKeyUp = function() {
        JSUI.AddEventHandler(thisObj.InputElement, //textBox,
                function(evt) {
                    var e = evt;
                    if (window.event) {
                        e = window.event;
                    }

                    // enter key
                    if (e.keyCode == 13) {
                        thisObj.EnterKey();
                        return false;
                    } else if (e.keyCode == 40) //down arrow
                    {
                        return false;

                    } else if (e.keyCode == 38) // up arrow
                    {
                        return false;
                    } else if (e.keyCode == 27) // reset on escape key
                    {
                        thisObj.Reset();
                        return false;
                    }
                    return true;
                },
                "keyup"
        )
    }

    this.Initialize = function() {

        var itemElements = thisObj.Parent.getElementsByTagName(thisObj.ItemTagName);
        thisObj.CurrentElement = nullElement; //itemElements[0];
        for (var i = 0; i < itemElements.length; i++) {
            var itemElement = itemElements[i];
            itemElement.Index = i;

            itemElement.UnHighlight = function(evt) {
                target = this;

                target.style.backgroundColor = thisObj.BackgroundColor;
                target.style.color = thisObj.ForeColor;
                thisObj.CurrentElement = nullElement;
            }

            itemElement.Highlight = function(evt) {
                thisObj.CurrentElement.UnHighlight();

                target = this;

                target.style.backgroundColor = thisObj.HighlightBackgroundColor;
                target.style.color = thisObj.HighlightForeColor;
                thisObj.SelectedIndex = target.Index;
                thisObj.CurrentElement = target;

            }



            JSUI.AddEventHandler(itemElement, itemElement.Highlight, "mouseover");
            JSUI.AddEventHandler(itemElement, itemElement.UnHighlight, "mouseout");
            JSUI.AddEventHandler(itemElement, thisObj.OnSelected, "click");
            JSUI.SetHandCursor(itemElement);
            thisObj.ItemElements.push(itemElement);
        }

        connectKeyDown();
        connectBlur();
    }


}
