/*
	Copyright © Bryan Apellanes 2015  
*/
var edit = (function ($, _) {
    "use strict";

    function edit(el, opts) {
        var o = { // the edit object/configuration
            config: function () { },
            change: function (n, o) { } // new and old values
        },
            otxt = $(el).text(),
            first = otxt,
            the = $(el);
        $.extend(o, opts);
        if (_.isFunction(o.config)) {
            o.config(o);
        }
        o.el = the;

        o.input = $(document.createElement("input"))
            .attr("type", "text")
            .val(otxt)
            .on("blur", function () {
                var ntxt = $(this).val();
                the.text(ntxt).show();
                if (_.isFunction(o.config)) {
                    o.config(o);
                }
                $(this).hide();
                if (ntxt !== otxt) {
                    o.current = ntxt;
                    if (_.isString(o.change)) {
                        o.change = _.getFunction(o.change);
                        if (!_.isFunction(o.change)) {
                            o.change = function () { };
                        }
                    }
                    o.change(o);
                    otxt = ntxt;
                }
            });

        
        $(o.input).hide();
        
        o.original = otxt;
        o.current = "";

        $(el)
            .data("bam.edit", o)
            .after(o.input)
            .on("click", function () {
                $(this).hide();
                if (_.isFunction(o.config)) {
                    o.config(o);
                }
                $(o.input).show().select();
            });
    }

    $.fn.edit = function (options) {
        return $(this).each(function () {
            var the = $(this);
            edit(the, options);
        });
    };

    return {};
})(jQuery, _);

