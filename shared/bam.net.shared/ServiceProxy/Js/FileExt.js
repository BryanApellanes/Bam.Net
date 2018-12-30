/*

* Copyright 2011, Bam DotNet
* Available via the MIT or new BSD license.

*/

var FileExt = {};

(function (poco, $) {
    var appName = "";
    poco.setAppName = function (n) {
        var m = n.match("/$");
        if ((m === null || m === undefined) && !(n === null || n === undefined)) {
            n = n + "/";
        }
        appName = n;
    };

    poco.toDate = function(s) {
        return new Date(parseInt(s.substr(6)));
    }

    poco.toLocal = function(s) {
        return new Date(poco.toDate(s).toLocaleString() + " UTC");
    }

    poco.getAppRoot = function () {
        return window.location.protocol + "//" + window.location.host + "/" + appName;
    }  
    
    poco.invoke = function (className, method, args, format, options) {
        if (!$.isArray(args)) {
            var a = [];
            a.push(args);
            args = a;
        }
        var strings = [],
            orig = function () { }; // the original complete function if provided

        for (var i = 0; i < args.length; i++) {
            strings.push(JSON.stringify(args[i]));
        }
        var params = JSON.stringify(strings), /* stringifying twice */
            config = {
                url: poco.getAppRoot() + "fileext/" + className + "/" + method + "." + format + "?",
                dataType: format,
                data: JSON.stringify({ jsonParams: params }),
                global: false,
                success: function () { },
                error: function () { },
                type: "POST",
                contentType: "application/json; charset=utf-8"
            };

        $.extend(config, options);
        if (typeof config.view === "string") {
            var m = config.url.match("&$"),
                a = (m !== null && m.toString() !== config.url) ? "&" : "";
            config.url += a + "view=" + config.view;
        }

        function complete() {
            if ($.isFunction(orig)) {
                orig();
            }
        }

        if ($.isFunction(config.complete)) {
            orig = config.complete;
        }
        
        config.complete = complete;
        
        return $.ajax(config);
    }

    poco.action = function (ctrlr, actn, data, options) {
        // <summary>
        // Posts the specified data as json to the 
        // specified mvc action on the specified controller.
        // </summary>
        var config = {
            url: poco.getAppRoot() + ctrlr + "/" + actn,
            dataType: "json",
            data: JSON.stringify(data),
            global: false,
            success: function () { },
            error: function () { },
            type: "POST",
            contentType: "application/json; charset=utf-8"
        };

        if ($.isFunction(options)) {
            config.success = options;
        } else {
            $.extend(config, options);
        }
        if (config.update !== null && config.update !== undefined && config.update != "") {
            var succ = config.success;
            config.success = function (r) {
                succ(r);
                $(config.update).html(r);
            };
        }
        $.ajax(config);
    }

    $(function(){
        if(_ && _.mixin){
            _.mixin(poco);
        }
    })
})(FileExt, jQuery)