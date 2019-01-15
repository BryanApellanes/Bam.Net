/*
	Copyright © Bryan Apellanes 2015  
*/
/*

 * Copyright 2014, Bryan Apellanes
 * Available via the MIT or new BSD license.
 *
 * database schemafier for client side javascript objects
 *
 */

var dbs = (function($, _, b, d, w){
    var dataTypes = {
        Boolean: true,
        Int: 2147483647,
        Long: 9223372036854775807,
        Decimal: 10.00,
        String: "String",
        Byte: [],
        DateTime: new Date()
    };

    var databasePrototype = {
        nameSpace: "DefaultNamespace",
        schemaName: "DefaultSchemaName",
        xrefs: [],
        tables:[]
    };

    var tablePrototype = {
        name: "DefaultTableName",
        cols: [], // { ColumnName: "<DataType>", Null: <true||false> }
        fks: [] // { ColumnName: "<TableName>" }
    };

    function deriveTableName(obj){
        var catted = "",
            tableName = null;
        if(_.isUndefined(obj)){
            throw new Error("object not specified");
        }
        if (_.isNull(tableName)) {
            tableName = obj["name"] || null;
        }
        if(_.isNull(tableName)){
            tableName = obj["className"] || null;
        }
        if(_.isNull(tableName)){
            tableName = obj["tableName"] || null;
        }
        if(_.isNull(tableName)){
            _.each(obj, function (v, p) {
                catted += p.toString();
            });
            tableName = _.sha1(catted);
        }
        return tableName;
    }
    // Constructors
    /**
     * @constructor
     * @param name
     */
    function table(name){
        var the = this;
        _.extend(the, tablePrototype);

        this.name = name;
        this.addColumn = function(name, type, canNull){
            var col = {};
            if(!_.isUndefined(dataTypes[type]) || _.isNull(dataTypes[type])){
                throw new Error("Invalid type specified: " + type);
            }
            col[name] = type;
            col.Null = _.isBoolean(canNull) ? canNull: true;
            the.cols.push(col);
        };

        this.addFk = function(name, tableName){
            var fk = {};
            fk[name] = tableName;
            the.fks.push(fk);
        };

        this.getColumn = function(cn){
            var val = null;
            _.each(the.cols, function(v){
                if(!_.isNull(v[cn])){
                    val = v;
                }
            });
            return val;
        };

        this.raw = function(){
            var result = {};
            _.each(the.cols, function(v, i){
                _.each(v, function(val, key){
                    if(key != "Null"){
                        result[key] = val;
                    }
                })
            });
            return result;
        }
    }
    /**
     * @constructor
     * @param namespace
     * @param schemaName
     */
    function database(namespace, schemaName){
        var the = this;
        $.extend(the, databasePrototype);

        if(!_.isUndefined(namespace)){
            this.nameSpace = namespace;
        }

        if(!_.isUndefined(schemaName)){
            this.schemaName = schemaName;
        }

        function getTable(name){
            var val = null;
            if(!_.isUndefined(name)){
                _.each(the.tables, function(v){
                    if(v.name == name){
                        val = v;
                    }
                })
            }

            return val;
        }

        this.setNullable = function(tn, cn, val){
            var table = getTable(tn);
            if(!_.isNull(table)){
                var col = table.getColumn(cn);
                if(!_.isNull(col)){
                    col.Null = val;
                }
            }
        };

        this.addTable = function(name, example){
            if(_.isObject(name) && _.isUndefined(example)){
                example = name;
                name = deriveTableName(example);
            }
            var t = new table(name);
            _.each(example, function(value, key){
                var type = "string";
                if(_.isBoolean(value)){
                    type = "boolean";
                }else if(_.isNumber(value)){
                    type = "int";
                }else if(_.isDate(value)){
                    type = "dateTime";
                }

                if(_.isArray(value)){
                    var fkTable = value[0],
                        fkTableName = fkTable.name || _.randomString();
                    the.addTable(fkTableName, fkTable);
                    getTable(fkTableName).addFk(fkTableName + "Id", fkTableName);
                }else{
                    t.addColumn(key, type, true);
                }
            });
            the.tables.push(t);
        };

        this.addXref = function(leftTableName, rightTableName){
            the.xrefs.push([leftTableName, rightTableName]);
        };
    }

    // -- end contructors
    function schemafy(dbms, example){
        var db = dbms;
        if(_.isUndefined(example) && _.isObject(dbms)){
            example = dbms;
            db = null;
        }

        var tn = deriveTableName(example);

        if(db === null){
            db = new database();
        }

        db.addTable(tn, example);

        return db;
    }

    b.ctor.database = database;
    return {
        schemafy: schemafy,
        deriveTableName: deriveTableName
    }
})(jQuery, _, bam, dao, window || {});

