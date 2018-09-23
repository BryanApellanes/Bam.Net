(function ($, _) {
    function feather(el, o) {
        var data = $(el).dataSet().data("dataset"),
            tn = o.templateName,
            ctx = o.context,
            rin = o.renderIn,
            cb = o.renderCallback;

        if (!_.isFunction(cb) && !_.isNull(rin) && !_.isUndefined(rin)) {
            cb = function (e, o) {
                $("#" + rin).html(o);
            }
        }

        if (_.isFunction(ctx)) {
            ctx(function (v) {
                dust.render(tn, v, cb);
            });
        } else {
            dust.render(tn, ctx, cb);
        }
    }

    _.mixin({
        tickle: function (selector, complete) {
            if (_.isNull(selector) || _.isUndefined(selector)) {
                selector = "[data-plugin=feather]";
                complete = function () { };
            } else if (_.isFunction(selector) && (_.isUndefined(complete) || _.isNull(complete))) {
                complete = selector;
                selector = "[data-plugin=feather]";
            }

            $(selector).dataSetPlugins();
            complete(selector);
        }
    });

    $.fn.feather = function (o) {
        return $(this).each(function () {
            var the = $(this);
            feather(the, o);
        });
    }
})(jQuery, _)