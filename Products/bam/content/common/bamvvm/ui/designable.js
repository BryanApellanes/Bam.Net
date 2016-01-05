/*
	Copyright © Bryan Apellanes 2015  
*/
/*

 * Copyright 2013, Bryan Apellanes
 * Available via the MIT or new BSD license.
 *
 * A context menu exposing classes, styles and attributes
 * allowing one to post the html for saving as a dust template.
 *  ***** This is incomplete. *****
 */

var designable = (function($, _, b, d, q, w){
    "use strict";

    function designable(el, opts){
        $(el).on("contextmenu", function(e){
            e.preventDefault();
            alert("contextMenu");
        })
    }

    $.fn.designable = function (options) {
        return $(this).each(function () {
            var the = $(this);
            designable(the, options);
        });
    };

    return {

    }
})(jQuery, _, bam, dao, qi, window || {});
