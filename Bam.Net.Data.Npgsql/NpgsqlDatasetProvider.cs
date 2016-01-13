/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Bryan.Apellanes.Incubation;
using Bryan.Apellanes;
using Bryan.Apellanes.Data;
using Bryan.Apellanes.Data.Npgsql;
using Npgsql;

namespace Bryan.Apellanes.Data.Npgsql
{
	/// <summary>
	/// Internal class used to provide a common implementation of
	/// GetDataSet for use by the NpgsqlQuerySet and the NpgsqlSqlStringBuilder
	/// each of which is a SqlStringBuilder.  This class is designed
	/// to prevent duplicate implementations in each of the afformentioned
	/// classes.
	/// </summary>
	internal class NpgsqlDatasetProvider
	{
		public NpgsqlDatasetProvider(NpgsqlSqlStringBuilder sqlStringBuilder)
		{
			this.SqlStringBuilder = sqlStringBuilder;
			this.PLSqlStringBuilder = sqlStringBuilder;
		}

		public NpgsqlDatasetProvider(NpgsqlQuerySet sqlStringBuilder)
		{
			this.SqlStringBuilder = sqlStringBuilder;
			this.PLSqlStringBuilder = sqlStringBuilder;
		}

		public SqlStringBuilder SqlStringBuilder { get; set; }
		public IPLSqlStringBuilder PLSqlStringBuilder { get; set; }

		public DataSet GetDataSet(Database db, bool releaseConnection = true, DbConnection conn = null, DbTransaction tx = null)
		{
			if (conn == null)
			{
				conn = db.GetDbConnection();
			}
			ParameterBuilder parameterBuilder = new NpgsqlParameterBuilder();
			List<DbParameter> parameters = new List<DbParameter>(parameterBuilder.GetParameters(SqlStringBuilder));
			if (PLSqlStringBuilder.ReturnsId)
			{
				NpgsqlParameter oracleParameter = new NpgsqlParameter(":Id", NpgsqlDbType.Int64, 0, "Id");
				oracleParameter.Direction = ParameterDirection.Output;
				parameters.Add(oracleParameter);
				PLSqlStringBuilder.IdParameter = oracleParameter;
			}

			DataSet ds = db.GetDataSetFromSql(SqlStringBuilder, CommandType.Text, releaseConnection, conn, tx, parameters.ToArray());
			SqlStringBuilder.OnExecuted(db);
			return ds;
		}
	}
}
