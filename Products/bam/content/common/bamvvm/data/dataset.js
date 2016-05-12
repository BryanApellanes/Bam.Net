/*
	Copyright © Bryan Apellanes 2015  
*/
/*

jQuery.dataSet plugin v0.1
Copyright 2012, Bryan Apellanes

$("body").dataSet(); // to initialize

var dataset = document.getElementById("myId").dataset;
    
    or

var dataset = $("#myId").data("dataset");

*/
/* dataset */
var dataset = {log: function(msg){}};

(function (ds, $) {
    "use strict";
    var events = ["abort", "blur", "change", "click", "dblclick", "error", "focus", "keydown", "keypress", "keyup", "load", "mousedown", "mousemove", "mouseout", "mouseover", "mouseup", "reset", "resize", "select", "submit", "touchstart", "touchend", "touchcancel", "touchmove", "unload"];
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
                    results[ele[i].nodeName] = ele[i].value;//ele[i].nodeValue
                }
            }
            return results;
        }
    }

    function format() {
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }

        return s;
    }

    function namedFormat(){
        var s = arguments[0];
        _.each(arguments[1], function(val, key){
            var reg = new RegExp("\\{" + key + "\\}", "gm");
            s = s.replace(reg, val);
        });

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
        if(_.isUndefined(fnName)){
            return null;
        }
        if (context === null || context === undefined) {
            context = window;
        }
        var namespaces = fnName.toString().split("."),
            func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            if(_.isUndefined(context) || _.isNull(context)){
                break;
            }
            context = context[namespaces[i]];
        }

        if (context !== null && context !== undefined) {
            return context[func];
        }

        return null;
    }

    function execute(fnName, context) {
        var sc = 2,
            args;
        if (context === null || context === undefined) {
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

    /**
     * Passes the value of the property op of object
     * oo to the function ofn and assigns the return value
     * to oo[op]
     * @param op
     * @param oo
     * @param ofn
     */
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

    ds.camelCase = camelCase;
    ds.execute = execute;
    ds.getFunction = getFunction;
    ds.hasPrefix = hasPrefix;
    ds.hasSuffix = hasSuffix;
    ds.startsWith = hasPrefix;
    ds.endsWith = hasSuffix;
    ds.hasDataPrefix = hasDataPrefix;
    ds.dateify = dateify;
    ds.munge = munge;
    ds.format = format;
    ds.namedFormat = namedFormat;

    function dataSet(el, options) {
        var config = {
            prefix: "", /* a company specific prefix intended to act like a namespace to prevent naming collisions */
            removePrefix: false,
            traverse: true /* traverse children and apply dataset */
        };

        var attrs = $.getAttributes(el);
        if (el.jquery) {
            el = el[0];
        }

        $.extend(config, options);
        if (el.dataset === null || el.dataset === undefined) {
            el.dataset = {};
        }
        for (var prop in attrs) {
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
    };

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
            _.each(result, function(v, k){
                var fn = getFunction(v);
                if(!_.isNull(fn) && _.isFunction(fn)){
                    result[k] = fn;
                }
            })
        } else {
            var fn = getFunction(str);
            if ($.isFunction(fn)) {
                result = fn;
            }

            // option is a viewModel method: data-opts-<name>='vm:<selector>:<methodName>'
            if (!$.isFunction(fn) && hasPrefix(str, "vm:")) {
                var segments = str.split(":");
                if(segments.length !== 3){
                    ds.log("invalid viewModel specification: " + str);
                }else{
                    var selector = segments[1],
                        methodName = segments[2],
                        vm;
                    
                    vm = $(selector).data("viewModel");
                    if(_.isObject(vm.model)){
                        vm = vm.model;
                    }
                    
                    if(!_.isFunction(vm[methodName])){
                        ds.log("invalid viewModel method specified: " + methodName);
                    }else{
                        result = vm[methodName];
                        fn = result;
                    }
                }
            }

            // option is a function that can be resolved from the global space
            if (!$.isFunction(fn) && hasPrefix(str, "f:")) {
                result = str.substr(2, str.length - 1);
                result = getFunction(result);
            }

            // option should resolve to an object: data-opts-<name>='o:prop1=value;prop2=value2;prop3=f:myFunctionName'
            if (!$.isFunction(fn) && hasPrefix(str, "o:")) {
                result = str.substr(2, str.length - 1);
                result = toObject(result);
            }

            // option should be treated as a boolean
            if (!$.isFunction(fn) && hasPrefix(str, "b:")) {
                result = str.substr(2, str.length - 1);
                result = !!(result == "1" || result == 1 || result == "true" || result == "True");
            }

            // option should be parsed as an array of strings that are comma separated
            if(!$.isFunction(fn) && hasPrefix(str, "a:")){
                result = str.substr(2, str.length - 1);
                var temp = [];
                _.each(result.split(","), function(v){
                    temp.push(v.trim());
                });
                result = temp;
            }

            if(!$.isFunction(fn) && hasPrefix(str, "d:")){
                result = str.substr(2, str.length - 1);
                var dateString = result;

                if (!$.isFunction(moment)) {
                    if(dateString !== ""){
                        result = new Date(dateString);
                    }else{
                        result = new Date();
                    }
                } else {
                    if(dateString !== ""){
                        result = moment(dateString).toDate();
                    }else{
                        result = moment().toDate();
                    }
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
            for (var propName in dataSet) {
                if (hasPrefix(propName, "opts")) {
                    var optName = camelCase(propName.substr(4, propName.length - 1)),
                        optVal = dataSet[propName];

                    options[optName] = resolveOption(optVal);
                }
            }
        }

        return options;
    }

    $.dataSetOptions = dataSetOptions;

    function dataSetPlugins(el, dataSetPluginOptions) {
        var config = {
                prefix: "",
                removePrefix: false,
                traverse: true
            },
            dataSet = $(el).data("dataset"),
            plugin,
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
    };

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
    };

    $(function () {
        if (_ !== undefined && _.mixin) {
            _.mixin(ds);
            _.mixin({dataSetOptions: dataSetOptions});
            _.mixin({options: dataSetOptions});
        }
    })
})(dataset, jQuery);
/* end dataset*/
