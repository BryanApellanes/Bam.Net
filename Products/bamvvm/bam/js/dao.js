var dao;
dao = (function ($, _) {
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

    var daoName = "",
        daoRoot = window.location.protocol + "//" + window.location.host + "/" + daoName,
        ctx = "";

    /**
     * Gets the root of the current app by appending the appName to the http
     * protocol and host.
     *
     * @return {String}
     */
    function getRoot() {
        return daoRoot;
    }

    function setRoot(protoAndHost, daoName) {
        if (_.isUndefined(daoName)) {
            daoName = protoAndHost;
            protoAndHost = window.location.protocol + "//" + window.location.host + "/";
        }
        daoRoot = protoAndHost + daoName;
    }

    function getCtx() {
        return ctx;
    }

    function setCtx(c) {
        ctx = c;
        return this;
    }

    function getCtors(ctx) {
        if (_.isUndefined(ctx)) {
            ctx = getCtx();
        }
        return dao[ctx].ctors;
    }

    /**
     * Posts the specified data as json to the specified controller action
     */
    function act(ctrlr, actn, data, options) {
        var config = {
            url: getRoot() + ctrlr + "/" + actn,
            dataType: "json",
            data: JSON.stringify(data),
            global: false,
            type: "POST",
            contentType: "application/json; charset=utf-8"
        };

        if (_.isFunction(options)) {
            config.success = options;
        } else {
            $.extend(config, options);
        }

        return $.ajax(config).promise();
    }

    function query(mName, qi, wrap) {
        var wrapped = new wrapper(qi.table);
        return wrapped[mName](qi, wrap);
    }

    function wrap(tableNameOrDao, idUndefOrDao) {
        return new wrapper(tableNameOrDao, idUndefOrDao);
    }

    function construct(tableName, raw, ctx) {
        var ctx = _.isUndefined(ctx) ? getCtx() : ctx,
            ctors = dao[ctx].ctors,
            inst = new ctors[tableName]();

        if (_.isObject(raw)) {
            $.extend(inst.Dao, raw);
        } else {
            if (!_.isUndefined(raw)) {
                throw { message: "raw must be an object; " };
            }
        }

        return inst;
    }

    function load(tableNameOrDao, id) {
        var wrapped = new wrapper(tableNameOrDao, id);
        return wrapped.load();
    }

    function deleteDao(name, id) {
        var wr = new wrapper(name, id);
        return wr.delete();
    }

    function wrapper(nameOrDao, idUndefOrDao) {
        this.tableName = null;
        this.Dao = null;
        this.loaded = false;

        var daoInst = null,
            self = this;
        if (_.isString(nameOrDao) && _.isUndefined(idUndefOrDao)) {
            this.tableName = nameOrDao; // for queries
        } else if (_.isObject(nameOrDao)) {
            this.tableName = nameOrDao.tableName || nameOrDao;
            this.Dao = nameOrDao.Dao || nameOrDao;
            this.collections = nameOrDao.collections || {};
            this.loaded = true;
        } else if (_.isString(nameOrDao) && _.isObject(idUndefOrDao)) {
            this.tableName = nameOrDao;
            this.collections = idUndefOrDao.collections || {};
            this.Dao = idUndefOrDao;
            this.loaded = true;
        } else if (_.isString(nameOrDao) && _.isNumber(idUndefOrDao) && !_.isNaN(idUndefOrDao)) {
            this.tableName = nameOrDao;
            var ctors = dao[getCtx()].ctors;
            daoInst = new ctors[nameOrDao];
            this.collections = daoInst.collections;
            daoInst.pk(idUndefOrDao);
        }

        this.load = function () {
            var prom = $.Deferred(function () {
                var def = this; // the deferred object
                if (_.isNull(self.tableName) || _.isUndefined(self.tableName)) { // self is the wrapper object
                    def.reject("tableName not specified");
                } else if (self.loaded) {
                    def.resolve(self);
                } else {
                    daoInst.retrieve({
                        success: function (res) {
                            if (res.Success) {
                                self.Dao = res.Dao;
                                self.loaded = true;
                                def.resolve(self);
                            } else {
                                def.reject(res.Message);
                            }
                        },
                        error: function (x, s, e) {
                            def.reject(e);
                        }
                    });
                }
            });

            return prom.promise();
        }
    }

    wrapper.prototype.where = function (qi, wrap) {
        var self = this,
            prom = $.Deferred(function () {
                act(self.tableName, "Where", qi, {
                    success: function (r) {
                        if (r.Success) {
                            var results = r.Dao;
                            if (wrap) {
                                var wrappedResults = [];
                                $.each(r.Dao, function (i, v) {
                                    wrappedResults.push(new wrapper(self.tableName, v));
                                });
                                results = wrappedResults;
                            }
                            prom.resolve(results);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(e);
                    }
                })
            });
        return prom.promise();
    };

    wrapper.prototype.oneWhere = function (qi, wrap) {
        var self = this,
            prom = $.Deferred(function () {
                act(self.tableName, "OneWhere", qi, {
                    success: function (r) {
                        if (r.Success) {
                            var val = r.Dao;
                            if (wrap) {
                                val = new wrapper(self.tableName, r.Dao);
                            }

                            prom.resolve(val);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(e);
                    }
                })
            })
        return prom.promise();
    };

    wrapper.prototype.retrieve = function (opts) {
        act(this.tableName, "Retrieve", { id: this.pk() }, opts);
    };

    wrapper.prototype.pk = function (val) {
        var ctx = getCtx(),
            key = dao[ctx].tables[this.tableName].keyColumn;
        if (!_.isUndefined(val)) {
            this.Dao[key] = val;
        } else if (_.isUndefined(this.Dao) || _.isNull(this.Dao)) {
            throw { message: "Can't get primary key (" + key + "), Dao is not set" };
        }

        return this.Dao[key];
    };

    wrapper.prototype.raw = function () {
        return this.Dao;
    };

    wrapper.prototype.save = function () {
        if (_.isUndefined(this.pk())) {
            return this.insert();
        } else {
            return this.update();
        }
    };

    wrapper.prototype.insert = function () {
        var self = this, // dao.wrapper
            prom = $.Deferred(function () {
                act(self.tableName, "Create", self.Dao, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = r.Dao;
                            prom.resolve(self);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(r);
                    }
                });
            });
        return prom.promise();
    };

    wrapper.prototype.update = function () {
        var self = this, //dao.wrapper
            prom = $.Deferred(function () {
                act(self.tableName, "Update", self.Dao, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = r.Dao;
                            prom.resolve(self);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(r.Message);
                    }
                })
            });
        return prom.promise();
    };

    wrapper.prototype.delete = function () {
        var self = this,
            prom = $.Deferred(function () {
                act(self.tableName, "Delete", { id: self.pk() }, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = null;
                            prom.resolve(true, "");
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(false, r.Message)
                    }
                });
            });
        return prom.promise();
    };

    wrapper.prototype.get = function (prop) {
        return this.Dao[prop];
    };

    wrapper.prototype.set = function (prop, val) {
        this.Dao[prop] = val;
        return this;
    };

    function getFks(t) {
        var vals = [];
        _.each(dao.fks, function (v) {
            if (v.ft == t) {
                vals.push(v);
            }
        });

        return vals;
    }

    function collection(o, pt, pk, ft, fk) {
        this.loaded = false;
        this.owner = o;  // the primary key table wrapper
        this.pt = pt; // the primary table name
        this.pk = pk; // the primary key name
        this.ft = ft; // the foreign table name
        this.fk = fk; // the foreign key name

        this.values = [];

        var self = this;
        this.load = function (wrap, ctx) {
            var prom = $.Deferred(function () {
                var pr = this; // deferred
                if (self.loaded) {
                    pr.resolve(self);
                } else {
                    var ctx = _.isUndefined(ctx) ? getCtx() : ctx;
                    qi(ctx).from(self.ft).where(self.fk + " = " + self.owner.get(self.pk))
                        .load(wrap)
                        .done(function (d) {
                            self.values = d;
                            self.loaded = true;
                            pr.resolve(self);
                        })
                        .fail(function (e) {
                            pr.reject(e);
                        })
                }
            });

            return prom.promise();
        };
        this.reload = function () {
            self.loaded = false;
            return self.load();
        }
    }

    collection.prototype.raw = function () {
        var result = [];
        _.each(this.values, function (v) {
            if (_.isFunction(v.raw)) {
                result.push(v.raw());
            } else {
                result.push(v);
            }
        });

        return result;
    };

    collection.prototype.each = function (cb) {
        _.each(this.values, cb);
    };

    collection.prototype.add = function (data) {
        var wrapped = wrap(this.ft, data);
        wrapped.set(this.fk, this.owner.pk());
        this.values.push(wrapped);
        return this;
    };

    collection.prototype.addRange = function (arr) {
        if (!_.isArray(arr)) {
            throw { message: "specified argument must be an array" };
        }

        _.each(arr, function (v) {
            this.add(v);
        });

        return this;
    };

    collection.prototype.save = function (wrap) {
        var self = this,
            prom = $.Deferred(function () {
                act(self.ft, "Save", self.raw(), {
                    success: function (res) {
                        if (res.Success) {
                            self.values = [];
                            self.load(wrap)
                                .done(function () {
                                    prom.resolve(self);
                                })
                                .fail(function (e) {
                                    prom.reject(e);
                                })
                        } else {
                            prom.reject(res.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(e);
                    }
                })
            });

        return prom.promise();
    }

    function missing() {
        alert("sdo.js is not included");
    }

    var r = {
        setCtx: setCtx,
        getCtx: getCtx,
        setRoot: setRoot,
        getRoot: getRoot,
        value: value,
        collection: collection,
        getFks: getFks,
        wrapper: wrapper,
        load: load,
        query: query,
        wrap: wrap,
        act: act,
        construct: construct,
        deleteDao: deleteDao,
        observe: observe,
        observeArray: observeArray,
        dataBind: function (selector, data) {
            alert("dataBinder not set");
        },
        fks: [],
        sdo: { // schema dot org
            item: missing,
            items: missing,
            getItem: missing,
            getItems: missing
        }
    };

    if (sdo) {
        r.sdo = sdo; // schema dot org
        r.dataBinder = sdo;
        r.dataBind = function (selector, data) {
            this.dataBinder.dataBind(selector, data);
        }
    } else {
        r.sdo = {
            item: missing,
            items: missing
        };
    }

    r.getItem = r.sdo.getItem;
    r.getItems = r.sdo.getItems;

    if (qi !== undefined) {
        r.qi = qi;
    } else {
        r.qi = {
            from: missing,
            where: missing
        }
    }

    _.mixin(r);

    return r;
})(jQuery, _);