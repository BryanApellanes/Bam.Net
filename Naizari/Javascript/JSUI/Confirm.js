if (JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded.");

JSUI.confirm = {};
JSUI.confirm.show = function(title, message, cb) {
    if (JSUI.isNullOrUndef(cb) && JSUI.isFunction(message)) {
        cb = message;
        message = title;
        title = "Confirm";
    }
    if (JSUI.isNullOrUndef(JSUI.confirm.dialog)) {
        JSUI.Conf.usingResource(["naizari.javascript.jsui.boxmgr.js"], function(s) {
            JSUI.confirm.dialog = BoxMgr.createDialog();
            JSUI.confirm.dialog.RenderDialogButtons();
        });
    }

    var onClose = function() {
        JSUI.confirm.dialog.RemoveCloseListener(onClose);
        cb(JSUI.confirm.dialog.DialogResult);
    }

    var titleEl = JSUI.confirm.dialog.title;
    var body = JSUI.confirm.dialog.body;
    jQuery(titleEl).html(title);
    jQuery(body).html(message);
    JSUI.orient(JSUI.confirm.dialog.id, { orientation: "CenterScreen" });
    JSUI.confirm.dialog.AddCloseListener(onClose);
    JSUI.confirm.dialog.ShowDialog();
}