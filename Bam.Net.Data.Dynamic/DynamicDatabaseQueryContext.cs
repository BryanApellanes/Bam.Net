/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Bam.Net.Data.Dynamic
{
    public class DynamicDatabaseQueryContext<Db> where Db: Database, new()
    {
        public DynamicDatabaseQueryContext(string table, DynamicDatabase<Db> dynamicDb)
        {
            this.DynamicDatabase = dynamicDb;
            this.TableName = table;
            this.Columns = "*";
            this.ParameterPrefix = "@";
        }

        public string TableName { get; private set; }
        public string Columns { get; private set; }
        public string WhereClauses { get; private set; }
        public string ParameterPrefix { get; set; }
        public DynamicDatabase<Db> DynamicDatabase { get; private set; }

        public DynamicDatabaseQueryContext<Db> Where(params dynamic[] whereClauses)
        {
            And(whereClauses);
            return this;
        }

        public DynamicDatabaseQueryContext<Db> And(params dynamic[] whereClauses)
        {
            StringBuilder whereString = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            AddClauses(whereClauses, whereString, parameters);
            Where(whereString.ToString(), parameters);
            return this;
        }
        public DynamicDatabaseQueryContext<Db> Or(params dynamic[] whereClauses)
        {
            StringBuilder whereString = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            AddClauses(whereClauses, whereString, parameters, "OR");
            Where(whereString.ToString(), parameters);
            return this;
        }

        public DynamicDatabaseQueryContext<Db> Where(string whereClauses, Dictionary<string, object> parameters)
        {
            try
            {
                foreach(string propertyName in DynamicDatabase.NameMap.PropertyNamesToColumnNames[TableName].Keys)
                {
                    whereClauses = whereClauses.Replace(propertyName, DynamicDatabase.NameMap.PropertyNamesToColumnNames[TableName][propertyName]);
                }
                WhereClauses = whereClauses;
                string sql = "SELECT {Columns} FROM {TableName} {WhereClauses}".NamedFormat(this);

                return this;
                //return DynamicDatabase.Execute(sql, parameters);
            }
            catch (Exception ex)
            {
                DynamicDatabase.ExceptionArbiter.Catch(this, ex);
            }

            return new List<dynamic>();
        }

        private void AddClauses(dynamic[] whereClauses, StringBuilder whereString, Dictionary<string, object> parameters, string appender = "AND")
        {
            foreach (dynamic whereClause in whereClauses)
            {
                Type type = whereClause.GetType();
                string oper = whereClause.Property("Oper") ?? whereClause.Property("Operator") ?? "=";
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    object value = prop.GetValue(whereClause);
                    string parameterName = "{ParameterPrefix}{ColumnName}".NamedFormat(new { ParameterPrefix = ParameterPrefix, ColumnName = prop.Name });
                    whereString.AppendFormat("{ColumnName} {Oper} {ParameterName} ".NamedFormat(new { ColumnName = prop.Name, Oper = oper, ParameterName = parameterName }));
                    parameters.Add(parameterName, value);
                }
                whereString.Append(appender);
            }
        }
    }
}
