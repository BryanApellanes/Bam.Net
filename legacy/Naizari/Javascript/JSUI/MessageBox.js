if(JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded.")
    
var MessageBox = {};
MessageBox.Object = null;

var waitForObject;
var pendingMessage;
MessageBox.show = function(title, strMessage, onCloseFunction) {
    JSUI.isStringOrDie(title);
    if (JSUI.isFunction(strMessage) && JSUI.isNullOrUndef(onCloseFunction)) {
        onCloseFunction = strMessage;
        strMessage = title;
        title = "Message Box";
    }
    if (JSUI.isNullOrUndef(strMessage) && JSUI.isNullOrUndef(onCloseFunction)) {
        strMessage = title;
        title = "Message Box";
    }
    if (!MessageBox.Object) {
        pendingMessage = strMessage;
        waitForObject = setTimeout(MessageBox.ShowPending, 1500);
        return;
    }
    clearTimeout(waitForObject);
    var messageTextId = this.Object.MessageId;
    jQuery('#' + messageTextId).html(strMessage);
    jQuery('#' + this.Object.TitleId).html(title);
    this.Object.CenterScreen();

    if (JSUI.IsFunction(onCloseFunction)) {
        var closeListeners = this.Object.CloseListeners.clone();
        this.Object.AddCloseListener(onCloseFunction);
        var obj = this.Object;
        this.Object.AddCloseListener(function() {
            obj.CloseListeners = closeListeners;
        });
    }
    this.Object.ShowDialog();
}

MessageBox.Show = MessageBox.show;

MessageBox.ShowPending = function(){
    MessageBox.Show(pendingMessage);
}
MessageBox.RegisterMessageBox = function(boxIdOrEl, titleIdOrEl, info) {
    this.Object = BoxMgr.CreateDialog(boxIdOrEl, { dragHandle: titleIdOrEl });
    this.Object.OKOnly = true;
    this.Object.RenderDialogButtons();
    this.Object.MessageId = info.msgId;
    this.Object.TitleId = info.titleId;
}

MessageBox.init = function() {
    var msgid = JSUI.randomString(8);
    var box = document.createElement("div");
    jQuery(box).addClass("messagebox");
    var title = document.createElement("div");
    title.setAttribute("class", "messagebox-title");
    title.id = JSUI.randomString(8);
    box.appendChild(title);
    var body = document.createElement("div");
    body.setAttribute("class", "messagebox-body");
    body.id = msgid;
    box.appendChild(body);
    jQuery(box).css("display", "none");
    JSUI.getDocumentBody().appendChild(box);    
    MessageBox.RegisterMessageBox(box, title, { msgId: body.id, titleId: title.id });
}

MessageBox.init();