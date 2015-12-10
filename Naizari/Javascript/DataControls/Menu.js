(function() {
    if (JSUI === null || JSUI === undefined)
        alert("The core JSUI.js file was not loaded.");

    var contextMenu = {};
    contextMenu.contextMenu = function(jsonid, options) {
        jQuery("div[dataid]", JSUI.getElement(jsonid)).mouseover(function() {
            jQuery(this).toggleClass("highlight");
        }).mouseout(function() {
            jQuery(this).toggleClass("highlight");
        });
    };

    JSUI.assimilate(contextMenu);

})()