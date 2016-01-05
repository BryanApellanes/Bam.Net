/*
	Copyright © Bryan Apellanes 2015  
*/
/*

 * Copyright 2014, Bryan Apellanes
 * Available via the MIT or new BSD license.
 *
 * Client side data access objects for use with bam
 */
/* dao */
var dao = (function ($, _) {
    "use strict";

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

    function blast(connectionName, typeName, action, rawDao, options){
        var config = {
            url: getRoot() + "dao/" + connectionName + "/" + action + "/" + typeName,
            dataType: "json",
            data: JSON.stringify(rawDao),
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
        return wr['delete']();
    }

    /**
     * @constructor
     * @param nameOrDao
     * @param idUndefOrDao
     */
    function wrapper(nameOrDao, idUndefOrDao, cxName) {
        this.tableName = null;
        this.cxName = cxName;
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
                                self.cxName = res.CxName;
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
                blast(qi.cxName, self.tableName, "Query", qi, {
                    success: function (r) {
                        if (r.Success) {
                            var results = r.Dao;
                            if (wrap) {
                                var wrappedResults = [];
                                $.each(r.Dao, function (i, v) {
                                    wrappedResults.push(new wrapper(self.tableName, v, r.CxName));
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
                blast(self.cxName, self.tableName, "OneWhere", qi, {
                    success: function (r) {
                        if (r.Success) {
                            var val = r.Dao;
                            if (wrap) {
                                val = new wrapper(self.tableName, r.Dao, r.CxName);
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
            });
        return prom.promise();
    };

    wrapper.prototype.retrieve = function (opts) {
        var self = this;
        blast(self.cxName, self.tableName, "Retrieve", self.pk(), opts);
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
                blast(self.cxName, self.tableName, "Create", self.Dao, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = r.Dao;
                            self.cxName = r.CxName;
                            prom.resolve(self);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(returnValue);
                    }
                });
            });
        return prom.promise();
    };

    wrapper.prototype.update = function () {
        var self = this, //dao.wrapper
            prom = $.Deferred(function () {
                blast(self.cxName, self.tableName, "Update", self.Dao, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = r.Dao;
                            self.cxName = r.CxName;
                            prom.resolve(self);
                        } else {
                            prom.reject(r.Message);
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(returnValue.Message);
                    }
                })
            });
        return prom.promise();
    };

    wrapper.prototype['delete'] = function () {
        var self = this,
            prom = $.Deferred(function () {
                blast(self.cxName, self.tableName, "Delete", { id: self.pk() }, {
                    success: function (r) {
                        if (r.Success) {
                            self.Dao = null;
                            prom.resolve(true, "");
                        }
                    },
                    error: function (x, s, e) {
                        prom.reject(false, returnValue.Message)
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

    /**
     * @constructor
     * @param o
     * @param pt
     * @param pk
     * @param ft
     * @param fk
     */
    function collection(o, pt, pk, ft, fk) {
        this.loaded = false;
        this.cxName = o.cxName;
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
                blast(self.cxName, self.ft, "SaveCollection", self.raw(), {
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
    };

    function missing() {
        alert("sdo.js is not included");
    }

    var returnValue = {
        setCtx: setCtx,
        getCtx: getCtx,
        setRoot: setRoot,
        getRoot: getRoot,
        collection: collection,
        getFks: getFks,
        wrapper: wrapper,
        load: load,
        query: query,
        wrap: wrap,
        act: act,
        blast: blast,
        construct: construct,
        deleteDao: deleteDao,
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
        returnValue.sdo = sdo; // schema dot org
        returnValue.dataBinder = sdo;
        returnValue.dataBind = function (selector, data) {
            this.dataBinder.dataBind(selector, data);
        }
    } else {
        returnValue.sdo = {
            item: missing,
            items: missing
        };
    }

    returnValue.getItem = returnValue.sdo.getItem;
    returnValue.getItems = returnValue.sdo.getItems;

    if (qi !== undefined) {
        returnValue.qi = qi;
    } else {
        returnValue.qi = {
            from: missing,
            where: missing
        }
    }

    _.mixin(returnValue);

    return returnValue;
})(jQuery, _);
/* dao */
