if (JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded");

(function() {
    function filter(col, oper, val) {
        JSUI.throwIfNullOrUndef(col).throwIfNullOrUndef(oper).throwIfNullOrUndef(val);
        var validOpers = ["Equals", "NotEqualTo", "StartsWith", "EndsWith", "Contains", "DoesntStartWith", "DoesntEndWith", "DoesntContain", "GreaterThan", "LessThan"];
        if (oper == "=" || oper == "eq" || oper == "equals")
            oper = "Equals";
        if (oper == "!=" || oper == "ne" || oper == "neq" || oper == "notequalto")
            oper = "NotEqualTo";
        if (oper == ">" || oper == "gt" || oper == "after" || oper == "greterthan")
            oper = "GreaterThan";
        if (oper == "<" || oper == "lt" || oper == "before" || oper == "lessthan")
            oper = "LessThan";
        if (oper == "sw" || oper == "^=" || oper == "^" || oper == "startswith")
            oper = "StartsWith";
        if (oper == "!^=" || oper == "dnsw" || oper == "!^" || oper == "dsw" || oper == "doesntstartwith")
            oper = "DoesntStartWith";
        if (oper == "ew" || oper == "$=" || oper == "$" || oper == "endswith")
            oper = "EndsWith";
        if (oper == "dnew" || oper == "!$=" || oper == "!$" || oper == "dew" || oper == "doesntendwith")
            oper = "DoesntEndWith";
        if (oper == "c" || oper == "*=" || oper == "*" || oper == "contains")
            oper = "Contains";
        if (oper == "dnc" || oper == "!*=" || oper == "!*" || oper == "dc" || oper == "doesntcontain")
            oper = "DoesntContain";

        if (oper == "group") {
            this.group = true;
            oper = "Invalid";
        }
        else if (!JSUI.arrayContains(validOpers, oper))
            JSUI.throwException("Invalid operator specified for filter. Must be one of [" + validOpers.toString() + "].");

        this.column = col;
        this.oper = oper;
        this.value = val;
        this.appender = null;
        var the = this;

        this.isLast = function() {
            return JSUI.isNullOrUndef(the.appender);
        }

        this.toString = function() {
            return the.column + the.oper + the.value + (the.isLast() ? "" : the.appender);
        }
    }

    function filtersCollection(col, oper, val) {
        this.items = [];
        var the = this;
        this.add = function(f, appender) {
            if (the.items.length > 0) {
                the.items[the.items.length - 1].appender = appender;
            }
            the.items.push(f);
        }
        this.add(new filter(col, oper, val), "");
    }

    var and = function(col, oper, val) {
        this.filters.add(new filter(col, oper, val), "AND");
        return this;
    }

    var or = function(col, oper, val) {
        this.filters.add(new filter(col, oper, val), "OR");
        return this;
    }

    var top = function(count) {
        this.count = count;
        return this;
    }

//    var group = function() {
//        if (this.filters.items.length > 0) {
//            this.filters.items.push(new filter("", "group", ""));
//        }

//        return this;
//    }

    var select = function() {
        var the = this;
        JSUI.Conf.usingProxy("Gong", function(inf) {
            alert(JSON.stringify(the));
            inf.proxy.runQiQuery(the);
        });
    }

    var Qi = function(dbAlias, daoContext) {
        if (JSUI.isNullOrUndef(daoContext))
            daoContext = dbAlias, dbAlias = "default";

        return {
            dbAlias: dbAlias,
            daoContext: daoContext,
            top: function(count) {
                this.count = count;
            },

            
            from: function(table) {
                var fromReturn = {
                    table: table,
                    select: select,
                    where: function(col, oper, val) {
                        var whereReturn = {
                            filters: new filtersCollection(col, oper, val),
                            dbAlias: dbAlias,
                            daoContext: daoContext,
                            and: and,
//                            enclose: group,
//                            group: group,
                            or: or
                        };

                        jQuery.extend(whereReturn, fromReturn);

                        return whereReturn;
                    }
                }
                
                return fromReturn;
            }
             ,
            insert: function(obj) {
            }
        };
    }

    window.Qi = Qi;
})()