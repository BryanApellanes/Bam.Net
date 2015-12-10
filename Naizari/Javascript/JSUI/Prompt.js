if (JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded.");

JSUI.prompt = {};

JSUI.prompt.show = function(title, text, cb) {
    if (JSUI.isNullOrUndef(JSUI.prompt.dialog)) {
        var el = document.createElement("div");
        var titleEl = document.createElement("div");
        var body = document.createElement("div");
        var textBox = document.createElement("input");
        textBox.setAttribute("type", "text");
        jQuery(textBox).addClass("prompt-textbox");
        titleEl.appendChild(document.createTextNode(title));
        jQuery(titleEl).addClass("prompt-title");
        jQuery(body).addClass("prompt-body");
        jQuery(el).addClass("prompt");
        el.appendChild(titleEl);
        el.appendChild(body);
        el.appendChild(textBox);
        el.id = JSUI.randomString(8);

        JSUI.setStyleNum(el, "width", 400);
        JSUI.setStyleNum(el, "height", 250);
        JSUI.getDocumentBody().appendChild(el);
        JSUI.prompt.dialog = BoxMgr.createDialog(el, { dragHandle: titleEl });
        JSUI.prompt.dialog.RenderDialogButtons();
        JSUI.prompt.dialog.message = body;
        JSUI.prompt.dialog.textBox = textBox;
        JSUI.prompt.el = el;
        JSUI.prompt.title = titleEl;
    }

    var onClose = function() {
        var dialog = JSUI.prompt.dialog;
        dialog.RemoveCloseListener(onClose);
        cb(dialog.DialogResult, dialog.textBox.value);
        dialog.textBox.value = "";
    }
    JSUI.prompt.dialog.AddCloseListener(onClose);
    jQuery(JSUI.prompt.dialog.message).html(text);
    jQuery(JSUI.prompt.title).html(title);
    JSUI.prompt.dialog.textBox.focus();
    JSUI.orient(JSUI.prompt.el, "CenterScreen");
    JSUI.prompt.dialog.Show(true);
}