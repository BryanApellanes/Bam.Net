if (JSUI === null || JSUI === undefined)
    alert("The Core JSUI.js file was not loaded.");

JSUI.Orientation = {};
JSUI.Orientation.normalize = function(retVal) {
    if (retVal.Top < 0) {
        retVal.Top = 0;
    }

    if (retVal.Left < 0) {
        retVal.Left = 0;
    }

    retVal.top = retVal.Top;
    retVal.left = retVal.Left;
}

JSUI.Orientation.TopLeftMouse = function(elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height)) + body.scrollTop, Left: evt.clientX - (width) };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left += overlap;
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.TopRightMouse = function(elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height)) + body.scrollTop, Left: evt.clientX };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left -= overlap;
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomRightMouse = function(elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY + (height)) + body.scrollTop, Left: evt.clientX };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left -= overlap;
        retVal.Top -= overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomLeftMouse = function(elementOrId, evt, overlap) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY + (height)) + body.scrollTop, Left: evt.clientX - (width) };
    if (overlap !== undefined && overlap !== null) {
        retVal.Left += overlap;
        retVal.Top -= overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.CenterMouse = function(elementOrId, evt) {
    evt = JSUI.GetEvent(evt);
    var height = JSUI.getElementHeight(elementOrId);
    var width = JSUI.getElementWidth(elementOrId);

    var body = JSUI.GetDocumentBody();
    var retVal = { Top: (evt.clientY - (height / 2)) + body.scrollTop, Left: evt.clientX - (width / 2) };
    
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.TopLeft = function(toPosition, refEl, overlap) {
    var moving = JSUI.GetElement(toPosition);
    var movingDims = JSUI.getDimensions(moving);
    var position = JSUI.GetElementPosition(refEl);

    var retVal = {};
    retVal.Top = position.Top - movingDims.height;
    retVal.Left = position.Left - movingDims.width;
    if (overlap !== undefined && overlap !== null) {
        retVal.Left = retVal.Left + overlap;
        retVal.Top = retVal.Top + overlap;
    }
    JSUI.Orientation.normalize(retVal, overlap);
    return retVal;
}

JSUI.Orientation.TopRight = function(toPosition, refEl, overlap) {
    var moving = JSUI.GetElement(toPosition);
    var movingDims = JSUI.getDimensions(moving);
    var position = JSUI.GetElementPosition(refEl);

    var retVal = {};
    retVal.Top = position.Top - movingDims.height;
    retVal.Left = position.Left + movingDims.width;
    if (overlap !== null && overlap !== undefined) {
        retVal.Left = retVal.Left - overlap;
        retVal.Top = retVal.Top + overlap;
    }
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.BottomCenter = function (toPosition, refEl, overlap) {
    var height = jQuery(refEl).height();
    var refOffset = jQuery(refEl).offset();
    var elToPositionDims = JSUI.getDimensions(toPosition);
    var refDims = JSUI.getDimensions(refEl);
    var refCenter = refOffset.left + (refDims.width / 2);
    var retVal = {};
    retVal.Top = refOffset.top + height;
    retVal.Left = refCenter - (elToPositionDims.width / 2);

    if (JSUI.isNumber(overlap)) {
        if (overlap !== null && overlap !== undefined) {
            retVal.Top -= overlap;
        }
    } else if (JSUI.isObject(overlap)) {
        if (overlap.left !== null && overlap.left !== undefined) {
            retVal.Left -= overlap.left;
        }
        if (overlap.top !== null && overlap.top !== undefined) {
            retVal.Top -= overlap.top;
        }
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.TopCenter = function(toPosition, refEl, overlap) {
    var refPosition = JSUI.GetElementPosition(refEl);
    var movingDims = JSUI.getDimensions(toPosition);
    var refCenter = refPosition.Left - refDims.width;
    var retVal = {};
    retVal.Top = refPosition.Top - movingDims.height;
    retVal.Left = refCenter - (movingDims.width / 2);

    if (overlap !== null && overlap !== undefined) {
        retVal.Top += overlap;
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.BottomLeft = function (toPosition, refEl, overlap) {
    var height = JSUI.GetElementHeight(refEl);
    var position = JSUI.GetElementPosition(refEl);
    var elToPositionDims = JSUI.getDimensions(toPosition);
    var retVal = {};
    retVal.Top = position.Top + height;
    retVal.Left = position.Left - elToPositionDims.width;

    if (JSUI.isNumber(overlap)) {
        retVal.Left = retVal.Left + overlap;
        retVal.Top = retVal.Top - overlap;
    } else if (JSUI.isObject(overlap)) {
        if (!JSUI.isNullOrUndef(overlap.left)) {
            retVal.Left += overlap.left;
        }
        if (!JSUI.isNullOrUndef(overlap.top)) {
            retVal.Top -= overlap.top;
        }
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.BottomRight = function(toPosition, refEl, overlap) {
    var height = JSUI.GetElementHeight(refEl);
    var position = JSUI.GetElementPosition(refEl);
    var refElDims = JSUI.getDimensions(refEl);
    var retVal = {};
    retVal.Top = position.Top + height;
    retVal.Left = position.Left + refElDims.width;

    if (overlap !== null && overlap !== undefined) {
        retVal.Left = retVal.Left - overlap;
        retVal.Top = retVal.Top - overlap;
    }
    JSUI.Orientation.normalize(retVal);

    return retVal;
}

JSUI.Orientation.CenterScreen = function (elementOrId, intPlusTop, intPlusLeft) {
    var retVal = {};
    var height = JSUI.GetElementHeight(elementOrId);
    var width = JSUI.GetElementWidth(elementOrId);

    var docBody = JSUI.GetDocumentBody();
    retVal.Top = ((docBody.clientHeight / 2) - (height / 2)) + docBody.scrollTop;
    retVal.Left = (docBody.clientWidth / 2) - (width / 2);

    if (JSUI.isNumber(intPlusTop))
        retVal.Top = retVal.Top + intPlusTop;

    if (JSUI.isNumber(intPlusLeft))
        retVal.Left = retVal.Left + intPlusLeft;

    if (retVal.Top <= 0) {
        retVal.Top = 0;
    }

    if (retVal.Left <= 0) {
        retVal.Left = 0;
    }
    retVal.top = retVal.Top;
    retVal.left = retVal.Left;
    JSUI.Orientation.normalize(retVal);
    return retVal;
}

JSUI.Orientation.CenterPage = function(elementOrId, intPlusTop, intPlusLeft) {
    var retVal = {};
    var height = JSUI.GetElementHeight(elementOrId);
    var width = JSUI.GetElementWidth(elementOrId);

    var docBody = JSUI.GetDocumentBody();
    retVal.Top = (docBody.clientHeight / 2) - (height / 2);
    retVal.Left = (docBody.clientWidth / 2) - (width / 2);

    if (intPlusTop != null && intPlusTop != 'undefined')
        retVal.Top = retVal.Top + intPlusTop;

    if (intPlusLeft != null && intPlusLeft != 'undefined')
        retVal.Left = retVal.Left + intPlusLeft;

    if (retVal.Top <= 0) {
        retVal.Top = 0;
    }

    if (retVal.Left <= 0) {
        retVal.Left = 0;
    }

    retVal.top = retVal.Top;
    retVal.left = retVal.Left;
    return retVal;
}

JSUI.orient = function (toPosition, options) {
    var config = { toBePositioned: JSUI.GetElement(toPosition), reference: "", orientation: "CenterPage", event: "", overlap: 0 };
    JSUI.copyProperties(options, config);
    if (JSUI.isString(config.reference) && config.reference != "")
        config.reference = JSUI.GetElement(config.reference);
    var func = JSUI.Orientation[config.orientation];
    JSUI.isFunctionOrDie(func);
    var position = {};
    if (config.event !== null && config.event != "") {
        position = func(config.toBePositioned, config.event, config.overlap);
    } else {
        position = func(config.toBePositioned, config.reference, config.overlap, config.event);
    }
    config.toBePositioned.parentNode.removeChild(config.toBePositioned);
    if (config.reference && config.reference.parentNode) {
        JSUI.insertAfter(config.reference, config.toBePositioned);
    } else {
        document.body.appendChild(config.toBePositioned);
    }
    config.toBePositioned.style.position = 'absolute';
    config.toBePositioned.style.top = position.top + "px";
    config.toBePositioned.style.left = position.left + "px";
};

JSUI.Assimilate(JSUI.Orientation); 

var BoxMgr = {};
BoxMgr.Images = {};

BoxMgr.createImageElement = function(strImageName) {
    if (!BoxMgr.Images[strImageName])
        BoxMgr.ThrowException(strImageName + " was not preloaded, use JSUI.PreLoadImage('" + strImageName + "', <src>) to load the image");

    var img = document.createElement("img");
    img.setAttribute("src", BoxMgr.Images[strImageName].src);
    return img;
}

BoxMgr.CreateImageElement = function(strImageName) {
    return BoxMgr.createImageElement(strImageName);
}

BoxMgr.preLoadImage = function(strName, strImageSrc, boolThrowIfLoaded) {
    if (BoxMgr.Images[strName]) {
        if (boolThrowIfLoaded == true) {
            JSUI.ThrowException("Image named " + strName + " has already been loaded, specify a different name to load a different image");
        } else {
            return BoxMgr.Images[strName];
        }
    }
    var image = new Image();
    image.src = strImageSrc;
    BoxMgr.Images[strName] = image;
    return image;
}

BoxMgr.PreLoadImage = function(name, src, throwIfLoaded) {
    return BoxMgr.preLoadImage(name, src, throwIfLoaded);
}


BoxMgr.Dialogs = {};
BoxMgr.IEBuggyControls = [];

BoxMgr.PreLoadImage("closebutton", "images/closebutton.png");
BoxMgr.PreLoadImage("closebuttonover", "images/closebuttonover.png");
BoxMgr.PreLoadImage("defaultbutton", "images/defaultbutton.gif");
BoxMgr.PreLoadImage("defaultbuttonover", "images/defaultbuttonover.gif");



// create a div that covers the whole screen to catch
// the mouse during a drag
BoxMgr.CreateScreenDiv = function(){
    var returnElement = document.createElement("div");
    returnElement.style.position = "absolute";
    returnElement.style.left = "0px";
    returnElement.style.top = "0px";
    returnElement.style.width = "100%";
    returnElement.style.height = "100%";
    returnElement.style.zIndex = -1;
    return returnElement;
}

BoxMgr.ModalScreen = BoxMgr.CreateScreenDiv();

BoxMgr.ModalScreen.Modalize = function(color) {
    if (!JSUI.isString(color))
        color = "#000000";
    BoxMgr.ModalScreen.style.backgroundColor = color;
    JSUI.SetOpacity(BoxMgr.ModalScreen, 5);
    JSUI.toFront(BoxMgr.ModalScreen);
}

BoxMgr.ModalScreen.DropBack = function(color) {
    if (!JSUI.isString(color))
        color = "#FFFFFF";
    BoxMgr.ModalScreen.style.backgroundColor = color;
    BoxMgr.ModalScreen.style.zIndex = -2;
    JSUI.SetOpacity(BoxMgr.ModalScreen, 0);

}

BoxMgr.AppendScreen = function() {
    document.body.appendChild(BoxMgr.ModalScreen);
    BoxMgr.SetScreenSize();
}

BoxMgr.ScreenSetter = null;
BoxMgr.SetScreenSize = function() {
    var ieBody = (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
    BoxMgr.ModalScreen.style.top = document.all ? ieBody.scrollTop + "px" : pageYOffset + "px";
    BoxMgr.ModalScreen.style.left = document.all ? ieBody.scrollLeft + "px" : pageXOffset + "px";
}

JSUI.addOnWindowScroll(BoxMgr.SetScreenSize);
JSUI.addOnWindowResize(BoxMgr.SetScreenSize);
JSUI.addOnWindowLoad(BoxMgr.AppendScreen);

BoxMgr.RegisterIEBuggyControl = function(idOrElement){
    BoxMgr.IEBuggyControls.push(JSUI.GetElement(idOrElement));
}

BoxMgr.HideBuggyControls = function(){
    for(var i = 0 ; i < BoxMgr.IEBuggyControls.length; i++){
        JSUI.HideElement(BoxMgr.IEBuggyControls[i]);
    }
}

BoxMgr.ShowBuggyControls = function(){
    for(var i = 0; i < BoxMgr.IEBuggyControls.length; i++){
        JSUI.ShowElementInline(BoxMgr.IEBuggyControls[i]);
    }
}

BoxMgr.topZIndex = 99;
BoxMgr.getTopZIndex = function(){
    BoxMgr.topZIndex++;
    return BoxMgr.topZIndex;
}

BoxMgr.toFront = function(el) {
    el = JSUI.GetElement(el);
    el.style.zIndex = BoxMgr.getTopZIndex();
    return JSUI;
}

BoxMgr.toBack = function(el) {
    el = JSUI.GetElement(el);
    el.style.zIndex = -99;
    return JSUI;
}

BoxMgr.SetDimensions = function(strTargetElementOrId, objDimensions){
    if( objDimensions.Width && objDimensions.Height){
        var targetElement = JSUI.GetElement(strTargetElementOrId);
        targetElement.style.width = objDimensions.Width + "px";
        targetElement.style.height = objDimensions.Height + "px";
    }
}

BoxMgr.dropListeners = {};

BoxMgr.addDropListener = function(droppable, func) {
    JSUI.isFunctionOrDie(func);
    JSUI.isStringOrDie(droppable.id);
    BoxMgr.dropListeners[droppable.id] = { func: func, droppable: droppable};
}

BoxMgr.onDropped = function(dropEvent) {
    JSUI.forEach(BoxMgr.dropListeners, function(key, value) {
        if (BoxMgr.dragging === null || BoxMgr.dragging === undefined)
            return;
        var fn = value.func;
        var el = value.droppable;
        if (JSUI.mouseIsOverElement(el, dropEvent)) {
            try {
                fn(BoxMgr.dragging);
            } catch (e) {
                JSUI.logError(e, true);
            }
        }
    });

    
}

BoxMgr.draggable = function(el, options) {
    var element = JSUI.GetElement(el);
    var clone = jQuery(element).clone().appendTo(element.parentNode)[0]; //element.cloneNode(true);

    jQuery('img', clone).show();
    JSUI.handCursor(element).handCursor(clone);
    var config = { startDrag: function(el) { }, endDrag: function(el) { }, copy: clone, clonestyles: {} };
    JSUI.copyProperties(options, config);
    if (JSUI.isNullOrUndef(config.clonestyles.height))
        config.clonestyles.height = 15;

    if (JSUI.isNullOrUndef(config.clonestyles.width))
        config.clonestyles.width = 125;
        
    config.copy.style.display = 'none';
    config.copy.style.position = 'absolute';

    for (styleName in config.clonestyles) {
        if (styleName != "width" && styleName != "height" && styleName != "opacity") {
            config.copy.style[styleName] = config.clonestyles[styleName];
        }
        else if (styleName == "opacity") {
            JSUI.isNumberOrDie(config.clonestyles[styleName]);
            JSUI.setOpacity(config.copy, config.clonestyles[styleName]);
        }
        else if (styleName == "width" || styleName == "height") {
            JSUI.setStyleNum(config.copy, styleName, config.clonestyles[styleName]);
        }
    }

    // -- start mousedown
    JSUI.AddEventHandler(element,
        function(event) {
            BoxMgr.dragging = config.copy;
            config.startDrag(config.copy);
            JSUI.show(config.copy);
            BoxMgr.toFront(BoxMgr.ModalScreen); //.style.zIndex = BoxMgr.getTopZIndex();
            BoxMgr.toFront(config.copy); //.style.zIndex = BoxMgr.getTopZIndex();
            JSUI.orient(config.copy, { orientation: "CenterMouse", event: event });

            JSUI.DisableTextSelect(); //config.copy);
            var e = JSUI.GetEvent(event);
            var offsetX = (parseInt(e.clientX) - parseInt(config.copy.offsetLeft));
            var offsetY = (parseInt(e.clientY) - parseInt(config.copy.offsetTop));

            var moveFunction = function(moveEvent) {
                var moveOffsetX = (parseInt(e.clientX) - parseInt(config.copy.offsetLeft));
                var moveOffsetY = (parseInt(e.clientY) - parseInt(config.copy.offsetTop));
                //BoxMgr.MouseMove(moveEvent, targetElement, offsetX, offsetY);
                moveEvent = JSUI.GetEvent(moveEvent);
                var newLeft = moveEvent.clientX - offsetX;
                var newTop = moveEvent.clientY - offsetY;
                if (newTop <= 0)
                    newTop = 0;

                config.copy.style.left = (newLeft) + "px";
                config.copy.style.top = (newTop) + "px";
            }

            var upFunction = function(upEvent) {
                JSUI.RemoveEventHandler(config.copy, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
                BoxMgr.onDropped(upEvent);
                BoxMgr.dragging = null;
                BoxMgr.toBack(BoxMgr.ModalScreen);
                JSUI.hide(config.copy);
                config.endDrag(config.copy);
            }

            // -- start mousemove ---
            JSUI.AddEventHandler(config.copy, moveFunction, "mousemove");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");

            // -- end mousemove

            // -- start mouseup ---
            JSUI.AddEventHandler(config.copy, upFunction, "mouseup");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, upFunction, "mouseup");

            // -- end mouseup


        },
        "mousedown"
    );
    // -- end mousedown

}

BoxMgr.droppable = function(elementOrId, options) {
    var element = JSUI.GetElement(elementOrId);
    var config = { ondrop: function(ele) { } }; 
    if (JSUI.isFunction(options))
        config.ondrop = options;
    else if (JSUI.isObject(options))
        JSUI.copyProperties(options, config);

    config.self = element;
    BoxMgr.addDropListener(config.self, config.ondrop);
}

BoxMgr.createDialog = function(draggableElementOrId, options) {
    var title = null;
    var body = null;
    if (JSUI.isNullOrUndef(draggableElementOrId) && JSUI.isNullOrUndef(options)) {
        draggableElementOrId = document.createElement("div");
        draggableElementOrId.id = JSUI.randomString(8);
        jQuery(draggableElementOrId).addClass("dialog");
        dragHandle = document.createElement("div");
        jQuery(dragHandle).addClass("dialog-title");
        draggableElementOrId.appendChild(dragHandle);
        var dialogbody = document.createElement("div");
        jQuery(dialogbody).addClass("dialog-body");
        draggableElementOrId.appendChild(dialogbody);
        title = dragHandle;
        body = dialogbody;
        JSUI.getDocumentBody().appendChild(draggableElementOrId);
        options = { dragHandle: dragHandle };
    }
    var config = { dragHandle: draggableElementOrId, modal: false };
    if (JSUI.isString(options)) {
        config.dragHandle = JSUI.GetElement(draggableElementOrId);
    } else if (JSUI.isObject(options)) {
        JSUI.copyProperties(options, config);
    }
    var dragHandleElementOrId = config.dragHandle;
    var element = JSUI.GetElement(draggableElementOrId);
    var draggableId = element.id;
    var dialog = new BoxMgr.Dialog(draggableElementOrId, dragHandleElementOrId);
    dialog.IsModal = config.modal;
    BoxMgr.Dialogs[draggableId] = dialog;
    dialog.id = draggableId;
    dialog.title = title;
    dialog.body = body;
    element.dialog = dialog;
    return dialog;
}

BoxMgr.CreateDialog = function(el, options) {
    return BoxMgr.createDialog(el, options);
}

BoxMgr.SetPosition = function(elementOrId, objPosition) {
    objPosition.Top = objPosition.top;
    objPosition.Left = objPosition.left;
    var element = JSUI.GetElement(elementOrId);
    element.style.top = objPosition.Top + "px";
    element.style.left = objPosition.Left + "px";
}

BoxMgr.Dialog = function(targetElementOrId, dragByElementOrId) {
    var targetElement = JSUI.GetElement(targetElementOrId);
    var dragByElement = null;
    var thisObj = this;
    var dialogButtonsRendered = false;

    this.IsModal = false;
    this.OKOnly = false;
    this.DialogResult = "Cancel";
    this.OKButton = null;
    this.CancelButton = null;
    this.DefaultFocus = null;

    this.CloseListeners = [];

    if (dragByElementOrId !== null && dragByElementOrId !== undefined) {
        dragByElement = JSUI.GetElement(dragByElementOrId);
    } else {
        dragByElement = targetElement;
    }

    targetElement.style.position = "absolute";

    JSUI.SetMoveCursor(dragByElement);

    this.SetPosition = function(objPosition) {
        BoxMgr.SetPosition(targetElement, objPosition);
    }

    this.CenterScreen = function(intPlusTop, intPlusLeft) {
        var centerPosition = JSUI.Orientation.CenterScreen(targetElement, intPlusTop, intPlusLeft);
        BoxMgr.SetPosition(targetElement, centerPosition);
    }

    this.CenterPage = function(intPlusTop, intPlusLeft) {
        var centerPagePosition = JSUI.Orientation.CenterPage(targetElement, intPlusTop, intPlusLeft);
        BoxMgr.SetPosition(targetElement, centerPagePosition);
    }

    this.SetDimensions = function(objDimensions) {
        BoxMgr.SetDimensions(targetElement, objDimensions);
    }

    this.Show = function(boolModal) {
        if (boolModal !== null && boolModal != 'undefined') {
            thisObj.IsModal = boolModal;
        }
        BoxMgr.HideBuggyControls();
        JSUI.ShowElement(targetElement);
    }

    this.Activate = function(boolModal) {
        if (boolModal != null && boolModal != 'undefined') {
            thisObj.IsModal = boolModal;
        }
        BoxMgr.ModalScreen.style.zIndex = BoxMgr.getTopZIndex();
        targetElement.style.zIndex = BoxMgr.getTopZIndex();
        dragByElement.style.zIndex = BoxMgr.getTopZIndex();
        thisObj.Show(boolModal);
        if (thisObj.DefaultFocus && thisObj.DefaultFocus.focus) {
            thisObj.DefaultFocus.focus();
        }
    }

    this.Hide = function() {
        JSUI.HideElement(targetElement);
        BoxMgr.ModalScreen.DropBack();
        BoxMgr.ShowBuggyControls();
    }

    this.OnClose = function() {
        thisObj.Hide();
        BoxMgr.ModalScreen.DropBack();
        for (var i = 0; i < thisObj.CloseListeners.length; i++) {
            thisObj.CloseListeners[i](thisObj.DialogResult);
        }
    }

    var okayClicked = function(evt) {
        thisObj.DialogResult = "OK";
        thisObj.OnClose();
    }

    var cancelClicked = function(evt) {
        thisObj.DialogResult = "Cancel";
        thisObj.OnClose();
    }

    this.RenderDialogButtons = function() {
        if (!dialogButtonsRendered) {
            thisObj.IsModal = true;

            var ok = new BoxMgr.ButtonClass("OK");
            ok.AddClickListener(okayClicked);
            //            ok.RenderIn(targetElement);

            var cancel = new BoxMgr.ButtonClass("Cancel");
            cancel.AddClickListener(cancelClicked);
            //cancel.RenderIn(targetElement);

            var buttons = new BoxMgr.ButtonStrip();
            buttons.AddButton(ok);
            if (!thisObj.OKOnly)
                buttons.AddButton(cancel);
            buttons.RenderIn(targetElement);
            thisObj.OKButton = ok;
            thisObj.CancelButton = cancel;
            dialogButtonsRendered = true;
        }
    }

    this.SetOkId = function(strId) {
        var okElement = JSUI.GetElement(strId);
        JSUI.SetHandCursor(okElement);
        JSUI.AddEventHandler(okElement, okayClicked, "click");
    }

    this.SetCancelId = function(strId) {
        var cancelElement = JSUI.GetElement(strId);
        JSUI.SetHandCursor(cancelElement);
        JSUI.AddEventHandler(cancelElement, cancelClicked, "click");
    }

    this.ShowDialog = function(boolWithButtons) {
        if (!dialogButtonsRendered && boolWithButtons) {
            thisObj.RenderDialogButtons();
        }
        BoxMgr.ModalScreen.Modalize();
        thisObj.IsModal = true;
        thisObj.Activate(true);
    }

    this.AddCloseListener = function(funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (!JSUI.ArrayContains(thisObj.CloseListeners, funcPointer)) {
            thisObj.CloseListeners.push(funcPointer);
        }
    }

    this.RemoveCloseListener = function(funcPointer) {
        if (JSUI.ArrayContains(thisObj.CloseListeners, funcPointer)) {
            thisObj.CloseListeners.splice(thisObj.CloseListeners.indexOf(funcPointer), 1);
        }
    }

    // -- start mousedown
    JSUI.AddEventHandler(dragByElement,
        function(event) {
            thisObj.Activate();
            JSUI.DisableTextSelect(); //dragByElement);
            var e = JSUI.GetEvent(event);
            var offsetX = (parseInt(e.clientX) - parseInt(targetElement.offsetLeft));
            var offsetY = (parseInt(e.clientY) - parseInt(targetElement.offsetTop));

            var moveFunction = function(moveEvent) {
                var moveOffsetX = (parseInt(e.clientX) - parseInt(targetElement.offsetLeft));
                var moveOffsetY = (parseInt(e.clientY) - parseInt(targetElement.offsetTop));
                //BoxMaster.MouseMove(moveEvent, targetElement, offsetX, offsetY);
                moveEvent = JSUI.GetEvent(moveEvent);
                targetElement.style.left = (moveEvent.clientX - offsetX) + "px";
                targetElement.style.top = (moveEvent.clientY - offsetY) + "px";
            }

            var upFunction = function(upEvent) {
                JSUI.RemoveEventHandler(dragByElement, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
                JSUI.RemoveEventHandler(targetElement, moveFunction, "mousemove");
                //                if (!thisObj.IsModal)
                //                    BoxMgr.ModalScreen.DropBack();
            }

            // -- start mousemove ---
            JSUI.AddEventHandler(dragByElement, moveFunction, "mousemove");
            JSUI.AddEventHandler(targetElement, moveFunction, "mousemove");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, moveFunction, "mousemove");
            // -- end mousemove 

            // -- start mouseup ---
            JSUI.AddEventHandler(dragByElement, upFunction, "mouseup");
            JSUI.AddEventHandler(targetElement, upFunction, "mouseup");
            JSUI.AddEventHandler(BoxMgr.ModalScreen, upFunction, "mouseup");
            // -- end mouseup


        },
        "mousedown"
    );
    // -- end mousedown
}

BoxMgr.CreateButton = function(strText){
    var button = new BoxMgr.ButtonClass(strText);
    return button;
}

BoxMgr.ButtonStrip = function(){
    var div = document.createElement("div");
    div.style.width = "100%";
    var table = document.createElement("table");
    div.appendChild(table);    
    table.setAttribute("align", "right");    
    
    if(document.all){
        var thead = document.createElement("thead");
        table.appendChild(thead);
        var tbody = document.createElement("tbody");
        table.appendChild(tbody);
        table = tbody;
    }
    
    
    var tableRow = document.createElement("tr");
    var isRendered = false;
    table.appendChild(tableRow);
    

    var dockTop = false;
    var dockBottom = true;
    this.setDockTop = function(bool){
        dockTop = bool;
        dockBottom = !bool;
    }
    
    this.setDockBottom = function(bool){
        dockBottom = bool;
        dockTop = !bool;
    } 
        
    this.Buttons = [];
    this.Buttons.items = [];
    var thisObj = this;
    this.AddButton = function(objButtonClass){
        if(!JSUI.ArrayContains(thisObj.Buttons, objButtonClass)){
            thisObj.Buttons.push(objButtonClass);
            thisObj.Buttons.items[objButtonClass.Text] = objButtonClass;
            var cell = document.createElement("td");
            tableRow.appendChild(cell);
            objButtonClass.RenderIn(cell);
        }
    }
    
    this.RenderIn = function(elementOrId){
        if(!isRendered){
            var element = JSUI.GetElement(elementOrId);
            if( dockTop ){
                if(element.hasChildNodes){
                    element.insertBefore(div, element.firstChild);                    
                }else{
                    element.appendChild(div);
                }
            }else{
                element.appendChild(div);
                
                //div.style.top = ((JSUI.GetElementHeight(element) * 1) - 75) + "px";
            }
            
            isRendered = true;
        }
    }
}


BoxMgr.ButtonClass = function(strText){
    this.Text = strText;
    this.IsRendered = false;
    this.DisableOnClick = false;
    this.Enabled = false;
    this.ButtonElement = null;
    this.ImageName = "defaultbutton";
    var thisObj = this;
    
    this.ClickListeners = [];
    
    this.AddClickListener = function(funcPointer){
        JSUI.IsFunctionOrDie(funcPointer);
        if(!JSUI.ArrayContains(thisObj.ClickListeners, funcPointer)){
            thisObj.ClickListeners.push(funcPointer);
        }
    }
    
    this.OnClick = function(evt){
        for(var i = 0 ; i < thisObj.ClickListeners.length; i++){
            thisObj.ClickListeners[i](evt);
        }
    }
    
    var element;// = JSUI.GetElement(elementOrId);
    var buttonDiv;// = document.createElement("div");
    var text = document.createElement("div");
    text.style.position = "relative";
    text.style.top = "-23px";
    text.style.textAlign = "center";
    text.style.width = "100%";
    text.style.height = "100%";
    text.appendChild(document.createTextNode(thisObj.Text));
            
    var click = function(evt){
        if(thisObj.DisableOnClick){
            thisObj.Disable();
        }
        thisObj.OnClick(evt);
    }
    
    this.Disable = function(){
        if(thisObj.Enabled){
            JSUI.RemoveEventHandler(text, click, "click");
            thisObj.Enabled = false;
        }
    }
    
    this.Enable = function(){
        if(!thisObj.Enabled){
            JSUI.AddEventHandler(buttonDiv, click, "click");
            thisObj.Enabled = true;
        }
    }
    
    this.RenderIn = function(elementOrId){
        if(!thisObj.IsRendered){
            thisObj.IsRendered = true;
            element = JSUI.GetElement(elementOrId);
            buttonDiv = document.createElement("div");
       
            BoxMgr.SetDimensions(buttonDiv, {Width: 100, Height: 25});
            var img = JSUI.CreateImageElement(thisObj.ImageName);//"defaultbutton"); 
            JSUI.ImageSwapify(img, thisObj.ImageName);       
            img.style.width = "100%";
            img.style.height = "100%";
            buttonDiv.appendChild(img);
            
            buttonDiv.appendChild(text);
            
            element.appendChild(buttonDiv);
            
            var mouseOver = function(){
                img.src = JSUI.Images[thisObj.ImageName + "over"].src;
            }
            
            var mouseOut = function(){
                img.src = JSUI.Images[thisObj.ImageName].src;
            }
            
            JSUI.AddEventHandler(text, mouseOver, "mouseover");
            JSUI.AddEventHandler(text, mouseOut, "mouseout");
            //JSUI.AddEventHandler(text, click, "click");
            thisObj.Enable();
            
            JSUI.SetHandCursor(buttonDiv);
            JSUI.SetHandCursor(text);
            JSUI.SetHandCursor(img);
            thisObj.ButtonElement = element;
        }
        return thisObj.ButtonElement;
    }  
    
}


JSUI.Assimilate(BoxMgr);//, JSUI);

