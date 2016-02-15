/**
 * Created by bryan on 10/1/2015.
 */
var observer = (function(){
    "use strict";

    function value(n, v, own) {
        var listeners = [];

        this.name = n;
        this.value = v;
        this.oldvalue = v;
        this.owner = own;

        this.get = function () {
            return this.value;
        };

        this.set = function (v) {
            this.oldvalue = this.value;
            this.value = v;
            if (listeners.length > 0 && this.oldvalue != this.value) {
                for (var i = 0; i < listeners.length; i++) {
                    listeners[i](this);
                }
            }
        };

        this.change = function (name, fn) {
            if (_.isFunction(name) && _.isUndefined(fn)) {
                fn = name;
                name = this.name;
            }
            if (name == this.name) {
                if (_.isFunction(fn)) {
                    listeners.push(fn);
                }
            }
        };
    }

    function observeArray(a) {
        var result = [];
        result.listeners = [];
        result.toData = function () {
            var dataArr = [];
            _.each(this, function (d) {
                dataArr.push(d.toData());
            });
            return dataArr;
        };
        result.toJson = function () {
            return JSON.stringify(this.toData());
        };

        result.changeAny = function (name, fn) {
            _.each(this, function (d) {
                d.change(name, fn);
            });
        };

        result.change = result.changeAny;

        result.changeAt = function (i, p, fn) {
            if (_.isUndefined(fn) && _.isFunction(p)) {
                this[i].change(p);
            } else {
                this[i].change(p, fn);
            }
        };

        if (!_.isArray(a)) {
            result.push(observe(a));
        } else {
            _.each(a, function (o) {
                result.push(observe(o));
            });
        }
        return result;
    }

    function observe(o) {
        if (_.isArray(o)) {
            return observeArray(o);
        }
        var result = {};
        result.listeners = [];
        result.properties = [];
        result.toData = function () {
            var l = this.properties.length,
                the = this,
                result = {};

            _.each(this.properties, function (prop) {
                result[prop] = the[prop]();
            });
            return result;
        };

        result.toJson = function () {
            return JSON.stringify(this.toData());
        };

        function accessor(pName, val) {
            if (!_.isNull(val) && !_.isUndefined(val)) {
                this[pName].value.set(val);
            }
            return this[pName].value;
        }

        for (var prop in o) {
            if (!_.isFunction(o[prop])) {
                var setVal = o[prop];

                result[prop] = function f(val) {
                    var p = f.value.name;
                    return accessor.apply(result, [p, val]).value;
                };

                result[prop].value = new value(prop, setVal, result);

                result[prop].toString = function ts() {
                    return ts.value.value;
                };

                result.properties.push(prop);
            }else{
                result[prop] = o[prop];
            }
        }
        result.change = function (pName, fn) {
            if (_.isUndefined(fn) && _.isFunction(pName)) {
                for (var i = 0; i < result.properties.length; i++) {
                    var p = result.properties[i];
                    result[p].value.change(p, pName); // pName is a function here
                }
            } else {
                result[pName].value.change(pName, fn);
            }
        };
        return result;
    }


    var o = {
        observe: observe,
        observeArray: observeArray
    };

    _.mixin(o);
    return o;
})();

