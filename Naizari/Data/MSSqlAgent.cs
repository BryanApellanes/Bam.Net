/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Naizari.Logging;
using System.Data;
using System.Data.Common;

namespace Naizari.Data
{
    public class MSSqlAgent: DatabaseAgent
    {
        protected MSSqlAgent()
            : base()
        {
            this.UseUnicode = true;
        }

        /// <summary>
        /// Instantiate a new MSSqlAgent using the specified connection string.
        /// Connection string should be a valid MSSql string similar to the following
        /// "Data Source=labdev1;Initial Catalog=logging;Integrated Security=True"
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        public MSSqlAgent(string connectionString)
            : base(DaoDbType.MSSql, connectionString)
        {
            this.UseUnicode = true;
        }

        /// <summary>
        /// Gets an enum value indicating the type of database this agent represents.
        /// Always returns MSSql.
        /// </summary>
        public override DaoDbType DbType
        {
            get
            {
                return DaoDbType.MSSql;
            }
        }

        public override void Shrink()
        {
            try
            {
                this.ExecuteSql(string.Format("DBCC SHRINKDATABASE ({0})", GetDatabaseNameFromConnectionString(this.ConnectionString)));
            }
            catch (Exception ex)
            {
                if (this.ShrinkException == null)
                {
                    Log.Default.AddEntry("MSSql: Error shrinking database: {0}", ex, this.ConnectionString);
                    this.ShrinkException = ex;
                }
            }
        }

        public static string GetDatabaseNameFromConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder destinationConnBuilder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = destinationConnBuilder["Initial Catalog"] as string;

            if (string.IsNullOrEmpty(databaseName))
                databaseName = destinationConnBuilder["Database"] as string;

            if (string.IsNullOrEmpty(databaseName))
                throw new InvalidOperationException("Unable to determine database name from the connection string");
            return databaseName;
        }

        public override DataTable GetDataTableFromSql(string sqlStatement, CommandType commandType, int retryCount, params DbParameter[] dbParamaters)
        {
            DataTable value = new DataTable();
            try
            {
                value = base.GetDataTableFromSql(sqlStatement, commandType, retryCount, dbParamaters);
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 515) // Cannot insert the value NULL into column XXX
                {
                    throw new DaoNullColumnValueException(sqlEx);
                }
                else
                {
                    throw sqlEx;
                }
            }

            return value;
        }
    }
}
