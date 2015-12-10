/*

jQuery.dataSet plugin v0.1
Copyright 2012, Bam DotNet

Enables html5 style data- attributes and the [element].dataset api.
Usage:

$("body").dataSet(); // to initialize

var dataset = document.getElementById("myId").dataset;
    
    or

var dataset = $("#myId").data("dataset");

*/
(function (bam, $) {
    var events = ["abort", "blur", "change", "click", "dblclick", "error", "focus", "keydown", "keypress", "keyup", "load", "mousedown", "mousemove", "mouseout", "mouseover", "mouseup", "reset", "resize", "select", "submit", "unload"];
    if ($.getAttributes === null || $.getAttributes === undefined) {
        $.getAttributes = function (ele) {
            var ele = ((typeof ele === "string") ? jQuery(ele)[0] : ele[0]),
                i = 0,
                ele = ele.attributes,
                count = ele.length,
                excl = events,
                results = {};

            for (i; i < count; i++) {
                if ($.inArray(ele[i].nodeName.replace(/^on/, ""), excl) == -1) {
                    results[ele[i].nodeName] = ele[i].nodeValue
                }
            }
            return results;
        }
    }

    String.format = function () {
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }

        return s;
    }

    function hasDataPrefix(s) {
        if (_.isString(s)) {
            return /^data\-(.*)$/.exec(s);
        } else {
            return false;
        }
    }

    function hasPrefix(s, p) {
        if (_.isString(s)) {
            return s.indexOf(p) === 0;
        } else {
            return false;
        }
    }

    function hasSuffix(s, p) {
        if (_.isString(s)) {
            return s.match(p + "$") == p;
        } else {
            return false;
        }
    }

    function getFunction(fnName, context) {
        if (context === null || context === undefined) {
            context = window;
        }
        var namespaces = fnName.split("."),
            func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }

        if (context !== null && context !== undefined) {
            return context[func];
        } else {
            return null;
        }
    }

    function execute(fnName, context) {
        var sc = 2,
            args;
        if (context === null | context === undefined) {
            context = window;
            sc = 1;
        }
        args = Array.prototype.slice.call(arguments).splice(sc);
        return getFunction(fnName, context).apply(this, args);
    }

    function camelCase(s, dlmtr) {
        if (dlmtr === undefined || dlmtr === null) {
            if (s.length == 1) {
                s = s.toLowerCase();
            } else if (s.length > 1) {
                s = s.charAt(0).toLowerCase() + s.substring(1, s.length);
            }
            return s;
        }
        var split = s.split(dlmtr),
            retVal = "";
        for (var i = 0; i < split.length; i++) {
            var word = split[i];
            if (i == 0) {
                word = word.toLowerCase();
            }
            for (var ii = 0; ii < word.length; ii++) {
                if (i != 0 && ii == 0) {
                    retVal += word.charAt(ii).toString().toUpperCase();
                } else {
                    retVal += word.charAt(ii);
                }
            }
        }

        return retVal.toString();
    }

    function dateify(ob) {
        function _dateify(o) {
            for (var p in o) {
                if (_.hasPrefix(o[p], "\/Date(") || _.hasPrefix(o[p], "/Date(")) {
                    o[p] = _.toLocal(o[p]);
                }
            }
        }
        if (_.isArray(ob)) {
            _.each(ob, function (v) {
                _dateify(v);
            });
        } else {
            _dateify(ob);
        }
    }

    function munge(op, oo, ofn) {
        function _munge(p, o, fn) {
            o[p] = fn(o[p]);
        }
        if (_.isArray(oo)) {
            _.each(oo, function (v) {
                _munge(op, v, ofn);
            });
        } else {
            _munge(op, oo, ofn);
        }
    }

    bam.camelCase = camelCase;
    bam.execute = execute;
    bam.getFunction = getFunction;
    bam.hasPrefix = hasPrefix;
    bam.hasSuffix = hasSuffix;
    bam.startsWith = hasPrefix;
    bam.endsWith = hasSuffix;
    bam.hasDataPrefix = hasDataPrefix;
    bam.dateify = dateify;
    bam.munge = munge;

    function dataSet(el, options) {
        var config = {
            prefix: "", /* a company specific prefix intended to act like a namespace to prevent naming collisions */
            removePrefix: false,
            traverse: true /* traverse children and apply dataset */
        }

        var attrs = $.getAttributes(el);
        if (el.jquery) {
            el = el[0];
        }

        $.extend(config, options);
        if (el.dataset === null || el.dataset === undefined) {
            el.dataset = {};
        }
        for (prop in attrs) {
            if (hasDataPrefix(prop)) {

                var propName = camelCase(prop.substr(5, prop.length - 1), "-");
                if (config.removePrefix && hasPrefix(propName, config.prefix)) {
                    propName = camelCase(propName.substr(config.prefix.length, propName.length - 1));
                }
                el.dataset[propName] = attrs[prop];
            }
        }


        $(el).data("dataset", el.dataset);
        if (config.traverse) {
            var children = $(el).children();
            if (children.length > 0) {
                children.dataSet(config);
            }
        }
    }

    $.fn.dataSet = function (options) {
        return this.each(function () {
            var the = $(this);
            dataSet(the, options);
        });
    };

    $.dataSet = function (el, name) {
        if (el.jQuery) {
            el = el[0];
        }
        return el.dataset[name];
    }

    function toObject(str) {
        var values = str.split(';'),
            result = {};
        $(values).each(function (i, v) {
            var keyVal = v.split("="),
                prop = $.trim(keyVal[0]),
                propVal = $.trim(keyVal[1]);

            result[prop] = resolveOption(propVal);
        });

        return result;
    }

    function resolveOption(str) {
        var result = str;
        if (hasPrefix(str, "{")) {
            result = JSON.parse(str);
        } else {
            var fn = getFunction(str);
            if ($.isFunction(fn)) {
                result = fn;
            }

            if (!$.isFunction(fn) && hasPrefix(str, "f:")) {
                result = str.substr(2, str.length - 1);
                result = getFunction(result);
            }

            if (!$.isFunction(fn) && hasPrefix(str, "o:")) {
                result = str.substr(2, str.length - 1);
                result = toObject(result);
            }

            if (!$.isFunction(fn) && hasPrefix(str, "b:")) {
                result = str.substr(2, str.length - 1);
                if (result == "1" || result == 1 || result == "true" || result == "True") {
                    result = true;
                } else {
                    result = false;
                }
            }
        }

        return result;
    }

    function dataSetOptions(el, dataSetOpts) {
        var dataSet = $(el).dataSet(dataSetOpts).data('dataset'),
            options = {};
        if (dataSet.opts) {
            options = resolveOption(dataSet.opts);
        } else {
            for (propName in dataSet) {
                if (hasPrefix(propName, "opts")) {
                    var optName = camelCase(propName.substr(4, propName.length - 1)),
                    optVal = dataSet[propName];

                    options[optName] = resolveOption(optVal);
                }
            }
        }

        return options;
    }

    $.dataSetOptions = function (el, dataSetOpts) {
        return dataSetOptions(el, dataSetOpts);
    }

    function dataSetPlugins(el, dataSetPluginOptions) {
        var config = {
            prefix: "",
            removePrefix: false,
            traverse: true
        },
            dataSet = $(el).data("dataset"),
            plugin = "",
            options = {};
        if (dataSet === null || dataSet === undefined) {
            $(el).dataSet();
            dataSet = $(el).data("dataset");
        }

        if (config.traverse) {
            var children = $(el).children();
            if (children.length > 0) {
                children.dataSetPlugins(config);
            }
        }

        plugin = dataSet.plugin;
        if (plugin && $(el)[plugin]) {
            options = dataSetOptions(el, dataSetPluginOptions);
            $(el)[plugin](options);
        }

    }

    $.fn.dataSetPlugins = function (options) {
        return $(this).each(function () {
            var the = $(this);
            dataSetPlugins(the, options);
        });
    }

    function dataSetEvents(el, dataSetEventsOptions) {
        var config = {
            traverse: true
        },
            dataSet = $(el).data("dataset"),
            handler = "",
            options = {};
        if (dataSet === null || dataSet === undefined) {
            $(el).dataSet();
            dataSet = $(el).data("dataset");
        }

        $.extend(config, dataSetEventsOptions);
        if (config.traverse) {
            var children = $(el).children();
            if (children.length > 0) {
                children.dataSetEvents(config);
            }
        }

        $.each(events, function (i, evName) {
            handler = dataSet[evName];
            if (handler && $.isFunction($(el)[evName])) {
                var setEvents = $(el).data("dataSetEvents") || {};
                if (!$.isFunction(setEvents[evName])) {
                    var func = getFunction(handler, window);
                    $(el)[evName](func);
                    setEvents[evName] = func;
                    $(el).data("dataSetEvents", setEvents);
                }
            }
        });
    }

    $.fn.dataSetEvents = function (options) {
        return $(this).each(function () {
            var the = $(this);
            dataSetEvents(the, options);
        });
    }

    $(function () {
        if (_ !== undefined && _.mixin) {
            _.mixin(bam);
        }
    })
})(bam, jQuery);