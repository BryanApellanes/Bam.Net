var sdo = (function ($, _) {
    "use strict";

    function func(fnName, context) {
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

    function type() {
        var itype = this.itemtype,
                li = itype.lastIndexOf("/");
        return itype.substr(li + 1, itype.length - li);
    }

    function is(type) {
        var si = this.itemtype.lastIndexOf("/"),
            it = this.itemtype.substr(si + 1, this.itemtype.length - si);

        if (_.isUndefined(type)) {
            return it;
        } else {
            return it == type;
        }
    }

    function item(selector, data) {
        var it = $(selector).first(),
            val = {},
            itemtype = $(it).attr("itemtype");

        if (_.isUndefined($(it).attr("itemscope"))) {
            it = $("[itemscope]", it).first();
            itemtype = $(it).attr("itemtype");
        }

        if (_.isUndefined($(it).attr("itemscope"))) {
            throw new Error("itemscope attribute not found");
        }

        $("[itemprop]", $(it)).each(function (i, propElement) {
            var prop = $(propElement).attr("itemprop"),
                prType = $(propElement).attr("data-type"),
                setter = $(propElement).attr("data-set"), // Assumed to be the name of a function if provided.
                getter = $(propElement).attr("data-get"), // Assumed to be the name of a function if provided.
                updateOn = $(propElement).attr("data-update-on") || "change", // The name of a dom element event that causes
                                                               // a call to the setter on a new value.
                defaultGetterSetter = ($(propElement).is("input") || $(propElement).is("select")) ? "val" : "text",
                // -- dealing with observable properties
                propFn = !_.isUndefined(data) && !_.isUndefined(data[prop]) ? data[prop] : null,
                propVal = _.isFunction(propFn) ? propFn() : propFn,
                // -- end
                itempropGetterInfo = {
                    prop: prop,
                    type: prType,
                    setter: setter,
                    getter: getter,
                    getterFn: func(getter, data) || func(getter),
                    setterFn: func(setter, data) || func(setter),
                    data: data
                };

            if(!_.isNull(data) && !_.isUndefined(data)){
                $(propElement).data("itemprop", itempropGetterInfo);
            }

            // -- setting the prop value if provided
            if (!_.isUndefined(data) && $(propElement).data("sdo_set") !== true) {
                if (!_.isUndefined(setter) && !_.isUndefined(data) && !_.isNull(propVal)) {
                    var setterFn = func(setter, data);
                    if(_.isNull(setterFn) || _.isUndefined(setterFn)){
                        setterFn = func(setter);
                    }
                    if(_.isNull(setterFn) || _.isUndefined(setterFn)){
                        throw new Error(_.format("specified setter {0} not found", setter));
                    }else{
                        setterFn($(propElement), propVal);
                    }
                } else if (!_.isUndefined($(propElement).attr("itemscope")) && !_.isUndefined(data) && !_.isNull(propVal)) {
                    item(propElement, propVal);
                } else if (prType == "enum") {
                    var stor = "[value=" + propVal + "]";
                    if (isNaN(propVal)) {
                        stor = "[data-text=" + propVal + "]";
                    }
                    $("input[type=radio]" + stor, $(propElement)).attr("checked", "checked");
                    $("input[type=radio]", $(propElement)).change(function (ev) {
                        if ($(this).is(":checked")) {
                            propFn($(this).val());
                        }
                    });
                } else if (_.isBoolean(propVal) && propVal && $(propElement).attr("type") == "checkbox") {
                    $(propElement).attr("checked", "checked");
                } else if (_.isUndefined(setter) && !_.isUndefined(data) && !_.isUndefined(propVal) && !_.isNull(propVal)) {
                    setter = defaultGetterSetter;
                    $(propElement)[setter](propVal);
                }
                // prevents it from getting re/un-set; cleaned up on line 161 - 165
                $(propElement).data("sdo_set", true);
                // -- end setting the prop value

                // -- attach the setter on the specified update event
                if (_.isFunction(propFn) && prType != "enum") { // -
                    $(propElement).off(updateOn).on(updateOn, function (ev) {
                        if ($(propElement).attr("type") == "checkbox") {
                            propFn($(propElement).is(":checked"));
                        } else {
                            propFn($(propElement).val());
                        }
                    });
                }
                // -- end attach the setter on the specified update event
            } else {
                // -- getting the prop value
                var info = $(propElement).data("itemprop");
                if (!_.isUndefined(info) && _.isFunction(info.getterFn)) {
                    val[prop] = info.getterFn(propElement);
                } else if (!_.isUndefined($(propElement).attr("itemscope"))) {
                    val[prop] = item(propElement, data);
                } else if (prType == "enum") {
                    val[prop] = $("input[type=radio]:checked", $(propElement)).val();
                } else if ($(propElement).attr("type") == "checkbox") {
                    val[prop] = $(propElement).is(":checked");
                } else {
                    val[prop] = $(propElement)[defaultGetterSetter]();
                }
                // -- end getting the prop value
            }
        });

        // -- attach actions
        if(!_.isNull(data) && !_.isUndefined(data)){
            $("[data-item-action],[data-action]", $(it)).each(function (i, e) {
                var action = $(e).attr("data-item-action") || $(e).attr("data-action"),
                            actOn = $(e).attr("data-action-on") || "click",
                            aFn = data[action],
                            aFn = _.isUndefined(aFn) && !_.isUndefined(data.actions) ? data.actions[action] : aFn,
                            d = data;

                if (_.isFunction(aFn)) {
                    $(e).off(actOn).on(actOn, function (ev) {
                        aFn(getItem(it), d, ev); // new item, old item
                    });
                }
            });
        }
        // -- end attach actions

        val.raw = _.clone(val);
        val.itemtype = _.isUndefined(itemtype) ? "http://schema.org/Thing" : itemtype;
        val.is = is;
        val.type = type;
        return val;
    }

    function getItem(selector) {
        return item(selector);
    }

    function getItems(selector) {
        var vals = [];
        $("[itemscope]", selector).each(function (i, v) {
            vals.push(getItem(v));
        });

        return vals;
    }

    function setItem(selector, data) {
        item(selector, data);
        // -- clean up 'visited' markers
        $("[itemprop]", $(selector)).each(function (i, v) {
            $(v).data("sdo_set", null);
        });
        // -- end clean up 'visited' markers
    }
    
    var sdo = {
        getItem: getItem,
        getItems: getItems,
        setItem: setItem,
        dataBind: setItem,
        dataScrape: getItem,
        bindData: setItem,
        scrapeData: getItem
    };

    _.mixin(sdo);

    return sdo;
})(jQuery, _);