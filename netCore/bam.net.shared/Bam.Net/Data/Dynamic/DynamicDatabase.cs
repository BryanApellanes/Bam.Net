/*
	Copyright © Bryan Apellanes 2015  
*/


namespace Bam.Net.Data.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Bam.Net;
    using Bam.Net.Data;
    using Bam.Net.Data.Schema;
    using Bam.Net.Data.MsSql;
    using Bam.Net.ExceptionHandling;
    using System.Data.Common;
    using System.Reflection;
    using System.Data;
    
    /// <summary>
    /// A dynamic crud interface to a database
    /// </summary>
    /// <typeparam name="Db"></typeparam>
    public partial class DynamicDatabase: IArbitrateExceptions
    {  
        public DynamicDatabase(){}
        public DynamicDatabase(Database database, SchemaNameMap schemaNameMap = null)
        {
            this.Database = database;
            this.ExceptionArbiter = new ExceptionArbiter();
            this.NameMap = schemaNameMap;
        }
                
        public SchemaNameMap NameMap
        {
            get;
            set;
        }

        public static implicit operator Database(DynamicDatabase dyn)
        {
            return dyn.Database;
        }

        public SqlStringBuilder CurrentSql { get; set; }

        public Database Database { get; private set; }

        public ExceptionArbiter ExceptionArbiter { get; set; }

        public void Create(params dynamic[] values)
        {
            Create(true, values);
        }
        public void Create(bool execute, params dynamic[] values)
        {
            CurrentSql = WriteInsert(values);
            if (execute)
            {
                Execute();
            }
        }

        public SqlStringBuilder WriteInsert(params dynamic[] values)
        {
            return WriteInsert(CurrentSql ?? Database.GetService<SqlStringBuilder>(), values);
        }

        public SqlStringBuilder WriteInsert(SqlStringBuilder sql, dynamic[] values)
        {            
            foreach (dynamic value in values)
            {
                string tableName = GetTableName(value);
                List<AssignValue> assignValues = GetValueAssignments(value, tableName);
                sql.Insert(tableName, assignValues.ToArray());
                sql.Go();
            }
            return sql;
        }

        /// <summary>
        /// Execute the specified querySpec and return the first 
        /// result or null
        /// </summary>
        /// <param name="querySpec"></param>
        /// <returns></returns>
        public dynamic RetrieveFirst(dynamic querySpec)
        {
            return Enumerable.FirstOrDefault(Retrieve(querySpec));
        }

        public dynamic RetrieveOne(dynamic querySpec)
        {
            dynamic[] results = Enumerable.ToArray(Retrieve(querySpec));
            if (results.Length > 1)
            {
                throw new MultipleEntriesFoundException();
            }
            return results.Length == 1 ? results[0] : null;
        }
        /*
         * new {
         *  Table = "Customer",
         *  Where = new {
         *      Name = "Monkey",
         *      And = new {
         *          LastName = "Tail"
         *      },
         *      Or = new {
         *          LastName = "Guy"
         *      }
         *  }
         * }
         * */
        public IEnumerable<dynamic> Retrieve(dynamic querySpec)
        {
            SqlStringBuilder sql = CurrentSql ?? Database.GetService<SqlStringBuilder>();
            string table = GetTableName(querySpec);
            WriteRetrieve(table, sql, querySpec);

            CurrentSql = sql;
            return Retrieve(table);
        }

        public SqlStringBuilder WriteRetrieve(dynamic querySpec)
        {
            string tableName = GetTableName(querySpec);
            SqlStringBuilder sql = CurrentSql ?? Database.GetService<SqlStringBuilder>();
            return WriteRetrieve(tableName, sql, querySpec);
        }

        public void Update(params dynamic[] values)
        {
            Update(true, values);
        }
        public void Update(bool execute, params dynamic[] values)
        {
            SqlStringBuilder sql = CurrentSql ?? Database.GetService<SqlStringBuilder>();
            foreach (dynamic value in values)
            {
                string tableName = GetTableName(value);
                List<AssignValue> assignValues = GetValueAssignments(value, tableName);
                sql.Update(tableName, assignValues.ToArray()).Where(ParseWhere(tableName, value));
            }
            CurrentSql = sql;
            if (execute)
            {
                Execute();
            }
        }

        /// <summary>
        /// Delete the entries that match the specified
        /// examples
        /// </summary>
        /// <param name="values"></param>
        public void Delete(params dynamic[] values)
        {
            Delete(true, values);
        }

        /// <summary>
        /// Write the sql to delete the entries that 
        /// match the specified examples and optionally
        /// executing
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="values"></param>
        public void Delete(bool execute, params dynamic[] values)
        {
            SqlStringBuilder sql = CurrentSql ?? Database.GetService<SqlStringBuilder>();
            foreach (dynamic value in values)
            {
                string tableName = GetTableName(value);
                sql.Delete(tableName).Where(ParseWhere(tableName, value));
            }
            CurrentSql = sql;
            if (execute)
            {
                Execute();
            }
        }

        /// <summary>
        /// Execute the current sql buffered in CurrentSql
        /// </summary>
        public void Execute()
        {
            if (CurrentSql != null)
            {
                CurrentSql.Execute(Database);
            }
        }

/*
* new {
*  Table = "Customer",
*  Where = new {
*      Name = "Monkey",
*      And = new {
*          LastName = "Tail"
*      },
*      Or = new {
*          LastName = "Guy"
*      }
*  }
* }
* */
        private QueryFilter ParseWhere(string tableName, dynamic where)
        {
            Type type = where.GetType();
            QueryFilter filter = new QueryFilter();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (TablePropertyNames.Contains(prop.Name))
                {
                    continue;
                }
                if (prop.Name.Equals("And"))
                {
                    object and = prop.GetValue(where);
                    filter = filter.IsEmpty ? ParseWhere(tableName, and) : filter && ParseWhere(tableName, and);
                }
                else if (prop.Name.Equals("Or"))
                {
                    object or = prop.GetValue(where);
                    filter = filter.IsEmpty ? ParseWhere(tableName, or) : filter || ParseWhere(tableName, or);
                }
                else if (prop.Name.Equals("Where"))
                {
                    filter = filter.IsEmpty ? ParseWhere(tableName, prop.GetValue(where)) : filter && ParseWhere(tableName, where);
                }
                else
                {
                    // TODO: enable multiple operators like QiQuery, contains, doesn't contain starts with etc
                    string columnName = NameMap.GetColumnName(tableName, prop.Name);
                    QueryFilter queryFilter = Bam.Net.Data.Query.Where(columnName) == prop.GetValue(where);
                    filter = filter.IsEmpty ? queryFilter : filter && queryFilter;
                }
            }
            return filter;
        }

        private string GetTableName(dynamic querySpec)
        {
            string table = ReflectionExtensions.Property<string>(querySpec, "from", false) ??
                ReflectionExtensions.Property<string>(querySpec, "From", false) ??
                ReflectionExtensions.Property<string>(querySpec, "Type", false) ??
                ReflectionExtensions.Property<string>(querySpec, "type", false) ?? 
                ReflectionExtensions.Property<string>(querySpec, "Table", false) ??
                ReflectionExtensions.Property<string>(querySpec, "TableName", false) ??
                ReflectionExtensions.Property<string>(querySpec, "table", false) ??
                ReflectionExtensions.Property<string>(querySpec, "tableName", false) ??
                ReflectionExtensions.Property<string>(querySpec, "Class", false) ??
                ReflectionExtensions.Property<string>(querySpec, "ClassName", false) ??
                ReflectionExtensions.Property<string>(querySpec, "className", false);
            Args.ThrowIfNullOrEmpty(table, "From/Table/Class not specified");
            table = NameMap.GetTableName(table);
            return table;
        }

        static List<string> _tablePropertyNames;
        private static List<string> TablePropertyNames
        {
            get
            {
                if (_tablePropertyNames == null)
                {
                    _tablePropertyNames = new List<string>(new string[] { "From", "from", "Type", "type", "Table", "TableName", "table", "tableName" });
                }
                return _tablePropertyNames;
            }
        }

        static List<string> _keywordProperties;
        private static List<string> KeywordProperties
        {
            get
            {
                if (_keywordProperties == null)
                {
                    List<string> tmp = new List<string>(TablePropertyNames);
                    tmp.Add("Where");
                    _keywordProperties = tmp;
                }
                return _keywordProperties;
            }
        }

        private List<AssignValue> GetValueAssignments(dynamic value, string tableName)
        {
            Type type = value.GetType();
            List<AssignValue> assignValues = new List<AssignValue>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (KeywordProperties.Contains(prop.Name))
                {
                    continue;
                }
                assignValues.Add(new AssignValue(NameMap.GetColumnName(tableName, prop.Name), prop.GetValue(value)));
            }
            return assignValues;
        }

        private DataTable MapDataTable(string tableName, DataTable dataTable)
        {
            DataTable result = new DataTable();
            foreach (DataColumn column in dataTable.Columns)
            {
                string property = NameMap.GetPropertyName(tableName, column.ColumnName);
                result.Columns.Add(new DataColumn(property));
            }
            foreach (DataRow row in dataTable.Rows)
            {
                DataRow newRow = result.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    string property = NameMap.GetPropertyName(tableName, column.ColumnName);
                    newRow[property] = row[column];
                }
                result.Rows.Add(newRow);
            }
            return result;
        }

        private SqlStringBuilder WriteRetrieve(string table, SqlStringBuilder sql, dynamic querySpec)
        {
            string columns = ReflectionExtensions.Property<string>(querySpec, "Columns", false) ?? "*";
            string[] columnNames = new string[] { columns };
            if (!columns.Equals("*"))
            {
                columnNames = columns.DelimitSplit(",").Select(s => NameMap.GetColumnName(table, s)).ToArray();
            }
            dynamic whereSpec = ReflectionExtensions.Property(querySpec, "Where") ?? ReflectionExtensions.Property(querySpec, "where");
            sql.Select(table);
            if (whereSpec != null)
            {
                sql.Where(ParseWhere(table, whereSpec));
            }
            return sql;
        }
    }
}
