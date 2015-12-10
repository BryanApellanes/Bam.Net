

(function() {
    var uploadMgr = {};
    uploadMgr.setFileName = function(strJsonId) {
        jQuery("[jsonid=" + strJsonId + "]").each(function(k, el) {
            var fileElement = jQuery("[name=file]", el)[0];
            var fullName = fileElement.value;
            var shortName = fullName.match(/[^\/\\]+$/)[0];
            if (JSUI.uploadMgr[strJsonId].acceptextensions !== null && shortName.search(JSUI.uploadMgr[strJsonId].acceptextensions) == -1) {
                var msg = JSUI.uploadMgr[strJsonId].invalidfilewarning;
                JSUI.uploadMgr[strJsonId].showMessage(msg);
            } else {
                JSUI.uploadMgr.submit(strJsonId);
            }
        });
    }

    uploadMgr.submit = function(jsonId) {
        JSUI.uploadMgr.showWorking(jsonId);
        JSUI.uploadMgr.hideMessage(jsonId);
        JSUI.uploadMgr[jsonId].form.submit();
    }

    uploadMgr.showWorking = function(jsonId) {
        var workingid = JSUI.uploadMgr[jsonId].workingid;
        if (workingid != null) {
            jQuery('#' + workingid).show();
        }
    }

    uploadMgr.hideWorking = function(jsonId) {
        var workingid = JSUI.uploadMgr[jsonId].workingid;
        if (!JSUI.isNullOrUndef(workingid)) {
            jQuery('#' + workingid).hide();
        }
    }

    uploadMgr.hideMessage = function(jsonId) {
        jQuery("[id$=_message]", JSUI.getElement(jsonId)).hide();
    }

    uploadMgr.errorListeners = {};
    uploadMgr.completeListeners = {};

    uploadMgr.uploadComplete = function(jsonId, msg) {
        JSUI.uploadMgr.hideWorking(jsonId);
        if (JSUI.isNullOrUndef(msg))
            msg = "The upload completed successfully.";
        jQuery("[id$=_message]", JSUI.getElement(jsonId)).html(msg).show();
        JSUI.forEach(JSUI.uploadMgr.completeListeners[jsonId], function(index, value) {
            value(jsonId);
        });
        JSUI.uploadMgr.hideWorking(jsonId);
    }

    uploadMgr.uploadError = function(jsonId, msg) {
        JSUI.uploadMgr.hideWorking(jsonId);
        if (JSUI.isNullOrUndef(msg))
            msg = "An error occurred, we are working to correct the problem and apologize for the inconvenience.  Please try again in a few minutes...";
        jQuery("[id$=_message]", JSUI.getElement(jsonId)).html(msg).show();
        JSUI.forEach(JSUI.uploadMgr.errorListeners[jsonId], function(index, value) {
            value(jsonId);
        });
        JSUI.uploadMgr.hideWorking(jsonId);
    }

    uploadMgr.addErrorListener = function(jsonId, fn) {
        if (JSUI.isNullOrUndef(JSUI.uploadMgr.errorListeners[jsonId]))
            JSUI.uploadMgr.errorListeners[jsonId] = [];

        JSUI.uploadMgr.errorListeners[jsonId].push(fn);
    }

    uploadMgr.addCompleteListener = function(jsonId, fn) {
        if (JSUI.isNullOrUndef(JSUI.uploadMgr.completeListeners[jsonId]))
            JSUI.uploadMgr.completeListeners[jsonId] = [];

        JSUI.uploadMgr.completeListeners[jsonId].push(fn);
    }

    JSUI.uploadMgr = uploadMgr;
})()