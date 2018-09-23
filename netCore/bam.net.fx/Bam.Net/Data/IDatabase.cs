/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.Logging;
namespace Bam.Net.Data
{
	public interface IDatabase
	{
		DaoTransaction BeginTransaction();
		string ConnectionName { get; set; }
		string ConnectionString { get; set; }
		System.Data.Common.DbCommand CreateCommand();
		System.Data.Common.DbConnectionStringBuilder CreateConnectionStringBuilder();
		void ExecuteSql(SqlStringBuilder builder);
		void ExecuteSql(SqlStringBuilder builder, IParameterBuilder parameterBuilder);
		void ExecuteSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParameters);
		void ExecuteSql<T>(SqlStringBuilder builder) where T : Dao;
		System.Collections.Generic.Dictionary<EnumType, T> FillEnumDictionary<EnumType, T>(System.Collections.Generic.Dictionary<EnumType, T> dictionary, string nameColumn) where T : Dao, new();
		System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParamaters);
		System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, params System.Data.Common.DbParameter[] dbParamaters);
		System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, System.Data.Common.DbConnection conn, params System.Data.Common.DbParameter[] dbParamaters);
		System.Data.DataSet GetDataSetFromSql(string sqlStatement, System.Data.CommandType commandType, bool releaseConnection, System.Data.Common.DbConnection conn, System.Data.Common.DbTransaction tx, params System.Data.Common.DbParameter[] dbParamaters);
		System.Data.DataTable GetDataTableFromSql(string sqlStatement, System.Data.CommandType commandType, params System.Data.Common.DbParameter[] dbParamaters);
		System.Data.Common.DbConnection GetDbConnection();
		void TryEnsureSchema(Type type, ILogger logger = null);
		int MaxConnections { get; set; }
		string Name { get; }
		Bam.Net.Incubation.Incubator ServiceProvider { get; set; }
	}
}
