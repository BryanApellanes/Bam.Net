/*
	Copyright © Bryan Apellanes 2015  
*/
/*

 * Copyright 2014, Bryan Apellanes
 * Available via the MIT or new BSD license.
 *
 * comma separated value outputter
 */
var csv = (function ($, _) {
    "use strict";

    function format() {
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }

        return s;
    }

    function csv(a, h) { // object[] array, boolean h => headers
        var def = $.Deferred(function () {
            var prom = this,
                result = "",
                properties = [];
            
            if (!_.isArray(a)) {
                throw { message: "The specified value must be an array" };
            }
            if (h) { // headers
                var first = true;
                for (var p in a[0]) {
                    if (!first) {
                        result += ",";
                    }

                    result += p;
                    first = false;
                    properties.push(p);
                }
                result += "\r\n";                
            }

            _.each(a, function (o) {
                var first = true;                
                _.each(properties, function(p){  
                    var val = o[p],
                        f = "{0}", // string format
                        replaceQuotes = false;

                    if (_.isString(val)) {
                        replaceQuotes = val.indexOf('"') !== -1;
                        f = val.indexOf(",") !== -1 ? '"{0}"' : "{0}";
                    }

                    if (replaceQuotes) {
                        var tmp = "";
                        for (var i = 0; i < val.length; i++) {
                            var curLetter = val[i];
                            if (curLetter == '"') {
                                tmp += '""';
                            } else {
                                tmp += curLetter;
                            }
                        }
                        val = tmp;
                    }

                    if (!first) {
                        result += ",";
                    }

                    result += format(f, val);
                    first = false;
                });
                result += "\r\n";
            });

            prom.resolve(result);
        });

        return def.promise();
    }

    var result = {
        csv: csv
    };

    _.mixin(result);

    return result;
})(jQuery, _);