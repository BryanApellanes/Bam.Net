var qi = function (cxName) {
	"use strict";
	var operator = function (oper) {
		if (oper == "=" || oper == "eq" || oper == "equals" || oper == "==") {
			oper = "Equals";
		} else if (oper == "!=" || oper == "ne" || oper == "neq" || oper == "notequalto") {
			oper = "NotEqualTo";
		} else if (oper == ">" || oper == "gt" || oper == "after" || oper == "greaterthan") {
			oper = "GreaterThan";
		} else if (oper == "<" || oper == "lt" || oper == "before" || oper == "lessthan") {
			oper = "LessThan";
		} else if (oper == "sw" || oper == "^=" || oper == "^" || oper == "startswith") {
			oper = "StartsWith";
		} else if (oper == "!^=" || oper == "dnsw" || oper == "!^" || oper == "dsw" || oper == "doesntstartwith") {
			oper = "DoesntStartWith";
		} else if (oper == "ew" || oper == "$=" || oper == "$" || oper == "endswith") {
			oper = "EndsWith";
		} else if (oper == "dnew" || oper == "!$=" || oper == "!$" || oper == "dew" || oper == "doesntendwith") {
			oper = "DoesntEndWith";
		} else if (oper == "c" || oper == "*=" || oper == "*" || oper == "contains") {
			oper = "Contains";
		} else if (oper == "dnc" || oper == "!*=" || oper == "!*" || oper == "dc" || oper == "doesntcontain") {
			oper = "DoesntContain";
		} else if (oper == "(") {
			oper = "OpenParen";
		} else if (oper == ")") {
			oper = "CloseParen";
		} else if (oper == " AND ") {
			oper = "AND";
		} else if (oper == " OR ") {
			oper = "OR";
		}
		this.value = oper;
		this.toString = function () {
			return this.value;
		}
	};
	var clause = function (comp, opts) {
		this.hasValue = false;
		var the = this,
			split = comp.split(" "),
			prop = "",
			oper = "",
			val = "";

		if (split.length === 1) {
			oper = split[0];
		} else if (split.length === 3) {
			prop = split[0];
			oper = split[1];
			val = split[2];
		} else if (split.length === 2) {
			prop = split[0];
			oper = split[1];
			if (opts === null || opts === undefined) {
				throw new exception("value not specified");
			}
			val = opts;
		}else if(split.length > 3){
			prop = split[0];
			oper = split[1];
			for(var i = 2; i < split.length; i++){
				val += split[i] + " ";
			}
		} else {
			throw new exception("unrecognized clause argument");
		}

		this.setProperty = function (prop) {
			the.property = prop;
			if (prop !== null && prop !== undefined && prop !== "") {
				the.setParameterName("@" + prop);
			} else {
				the.setParameterName("");
			}
		};
		this.setOperator = function (oper) {
			the.operator_ = new operator(oper).toString();
		};
		this.setValue = function (val) {
			the.val = val;
			the.hasValue = true;
		};
		this.setParameterName = function (paramName) {
			the.parameterName = paramName;
		};
		this.toString = function () {
			var param = the.parameterName,
				num = the.num,
				val = the.property + " " + the.operator_ + " " + param + num;

			return val;
		};

		this.num = 0;
		this.setProperty(prop);
		this.setOperator(oper);
		this.setValue(val);

		$.extend(this, opts);
	};

	var tokenConfig = { hasValue: false, number: "" },
		AND = new clause(" AND ", tokenConfig),
		OR = new clause(" OR ", tokenConfig),
		OPENPAREN = new clause("(", tokenConfig),
		CLOSEPAREN = new clause(")", tokenConfig),
		GO = new clause(";GO;\r\n", tokenConfig);

	var query = {
		cxName: cxName,
		table: "",
		columns: "*",
		clauses: [],
		number: 0,
		select: function (props) {
			if (typeof props === "string") {
				this.columns = props;
			} else if ($.isArray(props)) {
				this.columns = "";
				for (var i = 0; i < props.length; i++) {
					this.columns += props[i];
					if (i != props.length - 1) {
						this.columns += ",";
					}
				}
			}
		},
		from: function (table) {
			this.table = table;
			return this;
		},
		where: function (prop) {
			if (this.clauses.length > 0) {
				this.clauses.push(AND);
			}
			var number = this.number++;
			this.clauses.push(new clause(prop, { num: number }));
			return this;
		},
		_and: function (prop) {
			this.clauses.splice(0, 0, OPENPAREN);
			this.clauses.splice(this.clauses.length, 0, CLOSEPAREN);
			this.where(prop);

			return this;
		},
		and: function (prop) {
			return this.where(prop);
		},
		_or: function (prop, oper, val) {
			this.clauses.splice(0, 0, OPENPAREN);
			this.clauses.splice(this.clauses.length, 0, CLOSEPAREN);
			this.or(prop);
			return this;
		},
		or: function (prop) {
			this.clauses.push(OR);
			var number = this.number++;
			this.clauses.push(new clause(prop, { num: number }));
			return this;
		},
		top: function(limit){
			this.limit = limit;
            return this;
		},
		/**
		 * load all matching records
		 * @param wrap
		 * @returns {*}
		 */
		load: function(wrap){
			return this.execute("where", wrap);
		},
		/**
		 * load the first matching record
		 * @param wrap
		 * @returns {*}
		 */
		loadOne: function (wrap) {
			return this.execute("oneWhere", wrap);
		},
		execute: function (mName, wrap) {
			var command = this.command(),
				self = this,
				prom = $.Deferred(function(){
					if(_.isNull(self.table) || _.isUndefined(self.table)){
						prom.reject("table name must be specified");
					}else if(_.isNull(self.cxName) || _.isUndefined(self.cxName)){
						prom.reject("cxName (aka contextName or connectionName) must be specified");
					}else if(!_.isNull(dao) && !_.isUndefined(dao)){
						dao.query(mName, command, wrap)
							.done(function(r){
								prom.resolve(r);
							})
							.fail(function(e){
							   prom.reject(e);
							})
					}
				})

			return prom;
		},
		command: function(){
			var result = this.clauses.parse();
			result.table = this.table;
			result.columns = this.columns;
			result.cxName = this.cxName;
			result.clauses = this.clauses;
			result.limit = this.limit;
		  return result;
		}
	};

	query.clauses.parse = function () {
		var txt = "",
			values = [];

		for (var i = 0; i < this.length; i++) {
			var clause = this[i];
			txt += clause.toString();
			if (clause.hasValue) {
				values.push(clause.val);
			}
		}

		return { parsed: txt, values: values };
	};

	return query;
};
