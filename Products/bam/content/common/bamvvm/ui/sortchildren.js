/*
	Copyright © Bryan Apellanes 2015  
*/
(function ($) {
    "use strict";

    function sortChildren(el, opts) {
        var arr = [],
            opts = $.extend({
                sortBy: "text",
                order: "asc",
                caseSensitive: true,
                sortFunction: "sortStrings",
                sortNumbers: function (av, bv) {
                    var asc = opts.order == "asc",
                        a = parseInt(av.value),
                        b = parseInt(bv.value);

                    return asc ? a - b : b - a;
                },
                sortStrings: function (a, b) {
                    var asc = opts.order == "asc";
                    if (a.value < b.value) { //sort string ascending
                        return asc ? -1 : 1;
                    }
                    if (a.value > b.value) {
                        return asc ? 1 : -1;
                    }
                    return 0; // equal
                }
            }, opts);

        function setElements(e, a) {
            $(e).empty();
            for (var i = 0; i < a.length; i++) {
                var obj = a[i];
                $(e).append(obj.element);
            }
        }

        $(el).children().each(function (i, v) {
            var txt;
            if ($.isFunction(opts.sortBy)) {
                txt = opts.sortBy(v);
            } else if (typeof opts.sortBy == "string" && $.isFunction($.fn[opts.sortBy])) {
                txt = $(v)[opts.sortBy]();
            }
            if (!opts.caseSensitive && _.isString(txt)) {
                txt = txt.toString().toLowerCase();
            }
            arr.push({
                value: txt,
                element: v
            });
        });

        arr.sort(opts[opts.sortFunction]);
        setElements(el, arr);
    }

    $.fn.sortChildren = function (opts) {
        return $(this).each(function () {
            sortChildren($(this), opts);
        });
    }
})(jQuery);