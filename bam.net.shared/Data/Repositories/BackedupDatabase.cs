/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using System.Reflection;

namespace Bam.Net.Data.Repositories // shared
{
	public partial class BackedupDatabase: Database
	{

		public IRepository Repository { get; private set; }

		protected Database Database
		{
			get
			{
				return Backup.DatabaseToBackup;
			}
		}

		public DaoBackup Backup { get; set; }

		#region IDatabase Members

		public DaoTransaction BeginTransaction()
		{
			return Database.BeginTransaction();
		}

		public string ConnectionName
		{
			get
			{
				return Database.ConnectionName;
			}
			set
			{
				Database.ConnectionName = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				return Database.ConnectionString;
			}
			set
			{
				Database.ConnectionString = value;
			}
		}

		public System.Data.Common.DbCommand CreateCommand()
		{
			return Database.CreateCommand();
		}

		public System.Data.Common.DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return Database.CreateConnectionStringBuilder();
		}

		public void ExecuteSql(SqlStringBuilder builder)
		{
			Database.ExecuteSql(builder);
		}

		public void ExecuteSql(SqlStringBuilder builder, IParameterBuilder parameterBuilder)
		{
			Database.ExecuteSql(builder, parameterBuilder);
		}

		public void ExecuteSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParameters)
		{
			Database.ExecuteSql(sqlStatement, commandType, dbParameters);
		}

		public void ExecuteSql<T>(SqlStringBuilder builder) where T : Dao
		{
			Database.ExecuteSql<T>(builder);
		}

		public Dictionary<EnumType, T> FillEnumDictionary<EnumType, T>(Dictionary<EnumType, T> dictionary, string nameColumn) where T : Dao, new()
		{
			return Database.FillEnumDictionary<EnumType, T>(dictionary, nameColumn);
		}

		public System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParamaters)
		{
			return Database.GetDataSetFromSql(sqlStatement, commandType, dbParamaters);
		}

		public System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, params System.Data.Common.DbParameter[] dbParamaters)
		{
			return Database.GetDataSetFromSql(sqlStatement, commandType, releaseConnection, dbParamaters);
		}

		public System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, System.Data.Common.DbConnection conn, params System.Data.Common.DbParameter[] dbParamaters)
		{
			return Database.GetDataSetFromSql(sqlStatement, commandType, releaseConnection, dbParamaters);
		}

		public System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction tx, params System.Data.Common.DbParameter[] dbParamaters)
		{
			return Database.GetDataSetFromSql(sqlStatement, commandType, releaseConnection, dbParamaters);
		}

		public System.Data.DataTable GetDataTableFromSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParamaters)
		{
			return Database.GetDataTable(sqlStatement, commandType, dbParamaters);
		}

		public System.Data.Common.DbConnection GetDbConnection()
		{
			return Database.GetDbConnection();
		}

		public void TryEnsureSchema(Type type, ILogger logger = null)
		{
			Database.TryEnsureSchema(type, logger);
		}

		public int MaxConnections
		{
			get
			{
				return Database.MaxConnections;
			}
			set
			{
				Database.MaxConnections = value;
			}
		}

		public string Name
		{
			get { return Database.Name; }
		}

		public Incubator ServiceProvider
		{
			get
			{
				return Database.ServiceProvider;
			}
			set
			{
				Database.ServiceProvider = value;
			}
		}

		#endregion
	}
}
