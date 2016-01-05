/*
	Copyright © Bryan Apellanes 2015  
*/
;(function ($) {
    $.fn.enterkey = function (fn) {
        return $(this).keypress(function (ev) {
            if (ev.keyCode == 13 && $.isFunction(fn)) {
                if (ev.preventDefault) {
                    ev.preventDefault();
                }
                else {
                    ev.returnValue = false;
                }
                fn($(this));
            }
        });
    }
})(jQuery);