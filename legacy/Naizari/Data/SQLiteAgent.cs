/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data;
using Naizari.Helpers;
using Naizari.Logging;
using System.Web;
using System.IO;
using System.Data.SqlClient;

namespace Naizari.Data
{
    public class SQLiteAgent: DatabaseAgent
    {
        public SQLiteAgent()
            : base()
        {
            this.maxConnections = 100;
        }
                
        /// <summary>
        /// Instantiate a new SQLiteAgent using the specified connection string.
        /// Connection string should be in the format 
        /// "Data Source=&lt;full or relative path to new or existing db3 file&gt;;Version=3;"
        /// </summary>
        /// <param name="connectionString"></param>
        public SQLiteAgent(string connectionString): base(DaoDbType.SQLite, connectionString)
        {
        }

        /// <summary>
        /// Sets the database file name to the dbFileName specified.  It will 
        /// be either in the same folder as the currently executing assembly or
        /// the App_Data folder if we're in a web app.
        /// </summary>
        /// <param name="dbFileName"></param>
        public void Default(string dbFileName)
        {
            this.providerFactory = new DaoContextProviderFactory(DaoDbType.SQLite, "Data Source=" + DefaultPath(dbFileName) + ";Pooling=False;Max Pool Size=100;", false);
        }

        public static string DefaultPath(string dbFileName)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath("~/App_Data/" + dbFileName);

            return new FileInfo("./" + dbFileName).FullName;
        }

        public override DaoDbType DbType
        {
            get
            {
                return DaoDbType.SQLite;
            }
        }

        public override void Shrink()
        {
            try
            {
                this.ExecuteSql("VACUUM");
            }
            catch (Exception ex)
            {
                if (this.ShrinkException == null)
                {
                    Log.Default.AddEntry("SQLite: Error shrinking database: {0}", ex, this.ConnectionString);
                    this.ShrinkException = ex;
                }
            }
        }

        public override string SelectTopFormat
        {
            get
            {
                return SQLITESELECTTOPFORMAT;
            }
        }

        protected override string ParseTopAndWhere(ref string whereClause, int count)
        {
            if (!string.IsNullOrEmpty(whereClause) && !whereClause.Trim().ToUpperInvariant().StartsWith("WHERE"))
                whereClause = " WHERE " + whereClause;

            string topCount = "";
            if (count > 0)
                topCount = string.Format(" {0} ", count);
            return topCount;
        }

        protected override string GetSql(string whereClause, ref string orderBy, string topCount, string tableName)
        {
            string sql = string.Format(SELECTFORMAT, topCount, tableName, whereClause);

            if (!string.IsNullOrEmpty(orderBy) && !orderBy.Trim().ToUpperInvariant().StartsWith("ORDER BY"))
            {
                orderBy = new OrderBy(orderBy, SortOrder.Ascending).ToString();
            }

            if (!string.IsNullOrEmpty(orderBy))
                whereClause += orderBy;

            if (!string.IsNullOrEmpty(topCount))
            {
                sql = string.Format(this.SelectTopFormat, topCount, tableName, whereClause);
            }
            
            return sql;
        }
    }
}
