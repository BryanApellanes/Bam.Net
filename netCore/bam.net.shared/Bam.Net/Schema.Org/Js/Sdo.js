var sdo = (function ($, _) {
    "use strict";

    function func(fnName, context) {
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
            throw {message: "itemscope attribute not found"};
        }

        $("[itemprop]", $(it)).each(function (i, v) {
            var prop = $(v).attr("itemprop"),
                prType = $(v).attr("data-type"),
                set = $(v).attr("data-set"), // Assumed to be the name of a function if provided.
                upd = $(v).attr("data-update-on"), // The name of a dom element event that causes  
                upd = _.isUndefined(upd) ? "change" : upd, // a call to the setter on a new value.
                get = ($(v).is("input") || $(v).is("select")) ? "val" : "text",
                setFn = !_.isUndefined(data) && !_.isUndefined(data[prop]) ? data[prop] : null,
                propVal = _.isFunction(setFn) ? setFn() : setFn;


            // -- setting the prop value if provided
            if (!_.isUndefined(data) && $(v).data("sdo_set") !== true) {
                if (!_.isUndefined(set) && !_.isUndefined(data) && !_.isNull(propVal)) {
                    set = func(set);
                    set($(v), propVal);
                } else if (!_.isUndefined($(v).attr("itemscope")) && !_.isUndefined(data) && !_.isNull(propVal)) {
                    item(v, propVal);
                } else if (prType == "enum") {
                    var stor = "[value=" + propVal + "]";
                    if (isNaN(propVal)) {
                        stor = "[data-text=" + propVal + "]";
                    }
                    $("input[type=radio]" + stor, $(v)).attr("checked", "checked");
                    $("input[type=radio]", $(v)).change(function (ev) {
                        if ($(this).is(":checked")) {
                            setFn($(this).val());
                        }
                    });
                } else if (_.isBoolean(propVal) && propVal && $(v).attr("type") == "checkbox") {
                    $(v).attr("checked", "checked");
                } else if (_.isUndefined(set) && !_.isUndefined(data) && !_.isUndefined(propVal)) {
                    set = get;
                    $(v)[set](propVal);
                }
                // prevents it from getting re/un-set; cleaned up on line 151 - 155
                $(v).data("sdo_set", true);
                // -- end setting the prop value

                // -- attach the setter on the specified update event
                if (_.isFunction(setFn) && prType != "enum") { // - setFn -> see line 53
                    $(v).on(upd, function (ev) {
                        if ($(v).attr("type") == "checkbox") {
                            setFn($(v).is(":checked"));
                        } else {
                            setFn($(v).val());
                        }
                    });
                }
                // -- end attach the setter on the specified update event
            } else {
                // -- getting the prop value
                if (!_.isUndefined($(v).attr("data-get"))) {
                    get = func($(v).attr("data-get"));
                    val[prop] = get(v);
                } else if (!_.isUndefined($(v).attr("itemscope"))) {
                    val[prop] = item(v, data);
                } else if (prType == "enum") {
                    val[prop] = $("input[type=radio]:checked", $(v)).val();
                } else if ($(v).attr("type") == "checkbox") {
                    val[prop] = $(v).is(":checked");
                } else {
                    val[prop] = $(v)[get]();
                }
                // -- end getting the prop value
            }
        });

        // -- attach actions
        $("[data-item-action]", $(it)).each(function (i, e) {
            var action = $(e).attr("data-item-action"),
                        aFn = data[action],
                        aFn = _.isUndefined(aFn) && !_.isUndefined(data.actions) ? data.actions[action] : aFn,
                        d = data;

            if (_.isFunction(aFn)) {
                $(e).click(function (ev) {
                    aFn(d, ev);
                });
            }
        });
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
        dataBind: setItem
    }

    _.mixin(sdo);

    return sdo;
})(jQuery, _)