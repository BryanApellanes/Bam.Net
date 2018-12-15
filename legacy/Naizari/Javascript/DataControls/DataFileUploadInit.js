jQuery(document).ready(function() {
    JSUI.uploadMgr.$$JsonId$$ = {};
    JSUI.uploadMgr.$$JsonId$$.form = jQuery('[jsonid=$$JsonId$$]')[0];
    JSUI.uploadMgr.$$JsonId$$.acceptextensions = $$AcceptExtensionsJsRegEx$$;
    JSUI.uploadMgr.$$JsonId$$.invalidfilewarning = '$$InvalidFileWarning$$';

    var workingid = '$$WorkingDomId$$';
    JSUI.uploadMgr.$$JsonId$$.workingid = workingid;
 
// these can be inherited    
//    JSUI.uploadMgr.$$JsonId$$.hideMessage = function() {
//        if (!JSUI.isNullOrUndef(workingid)) {
//            jQuery('#' + workingid).hide();
//        }
//        jQuery('#$$JsonId$$_message').hide();
//    }

//    JSUI.uploadMgr.$$JsonId$$.showMessage = function(msg) {
//        if (!JSUI.isNullOrUndef(workingid)) {
//            jQuery('#' + workingid).hide();
//        }
//        jQuery('#$$JsonId$$_message').html(msg).show();
//    };

    if (workingid != null) {
        var workingel = JSUI.getElement(workingid);
        jQuery(workingel).hide();
        workingel.parentNode.removeChild(workingel);
        jQuery('#$$JsonId$$_message').before(workingel);
    }
}
);