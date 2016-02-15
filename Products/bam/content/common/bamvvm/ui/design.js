/*
	Copyright © Bryan Apellanes 2015  
*/
var design = (function ($, _, dst) {
    "use strict";    

    function contrast(hexcolor) {
        var hx = _.startsWith(hexcolor, "#") ? hexcolor.substr(1, hexcolor.length - 1) : hexcolor;
        var r = parseInt(hx.substr(0, 2), 16),
            g = parseInt(hx.substr(2, 2), 16),
            b = parseInt(hx.substr(4, 2), 16),
            yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
        return (yiq >= 128) ? 'black' : 'white';
    }

    function message(msg, classes) {
        var c = _.isArray(classes) ? classes : [classes],
            $msg = $("#messages");
        $msg.removeClass().text(msg);
        _.each(c, function (v) {
            $msg.addClass(v);
        });
    }

    function deriveColorPalette() {
        var url = $("#paletteUrl").val();
        if (_.isNull(url) || _.isUndefined(url) || url.trim() == "") {
            var prom = $.Deferred(function(){
                var txt = "please enter an url to extract a color palette from";
                message(txt, "text-warning");
                prom.reject(txt);
            }).promise();
            return prom;
        }
        var def = $.Deferred(function () {
            var prom = this;
            _.act("design", "derivecolorpalette", { url: url })
                .done(function (r) {
                    if (r.Success) {
                        var $palette = $("#palette");
                        $palette.empty();
                        for (var i = 0; i < r.Data.length; i++) {
                            var data = r.Data[i];

                            data.TxtColor = contrast(data.Hex);
                            dst.render("hexcolor", r.Data[i], function (e, h) {
                                if (e) {
                                    message(e, "text-error");
                                    prom.reject(e);
                                } else {
                                    $palette.append(h);
                                }
                            });
                        }
                        $palette.dataSetPlugins();
                        prom.resolve();
                    } else {
                        message(r.Message, "text-error");
                        prom.reject(r.Message);
                    }
                });
        });

        return def.promise();
    }

    var d = {
        contrast: contrast,
        deriveColorPalette: deriveColorPalette,
        setColorScheme: function(cs){
            if(_.isUndefined(cs)){
                cs = colorscheme;
            }
            var def = $.Deferred(function () {
                var prom = this;
                _.act("design", "lessbootstrapvariables", { scheme: cs }, { dataType: "html" })
                    .done(function (r) {
                        _.act("design", "setcolorscheme", { variablesDotLess: r })
                            .done(function (ri) {
                                if (ri.Success && ri.Data) {
                                    prom.resolve(ri.Data);
                                } else {
                                    prom.reject(ri.Message);
                                }
                            })
                            .fail(function (x, s, e) {

                            });
                    })
                    .fail(function (x, s, e) {
                        prom.reject(x, s, e);
                    });
            });
        },
        hexConfig: function () {
            
        },
        message: message,
        changeColor: function (opts) {
            var it = _.getItem("#" + opts.domId),
                sectionDotName = it.Name.split(".");

            $("#" + opts.domId).css("background-color", it.Hex).css("color", contrast(it.Hex));

            if (!_.isUndefined(colorscheme)) {
                colorscheme[sectionDotName[0]][sectionDotName[1]] = it.Hex;
                design.setColorScheme(colorscheme).done(function (r) {
                    if (!r.Success) {
                        design.message(r.Message, "text-warning");
                    }
                });
            } 
        }
    };

    _.mixin(d);

    return d;
})(jQuery, _, dust);