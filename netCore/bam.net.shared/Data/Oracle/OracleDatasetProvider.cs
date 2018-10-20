/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Oracle;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Bam.Net.Data.Oracle
{
	/// <summary>
	/// Internal class used to provide a common implementation of
	/// GetDataSet for use by the OracleQuerySet and the OracleSqlStringBuilder
	/// each of which is a SqlStringBuilder.  This class is designed
	/// to prevent duplicate implementations in each of the afformentioned
	/// classes.
	/// </summary>
	internal class OracleDatasetProvider
	{
		public OracleDatasetProvider(OracleSqlStringBuilder sqlStringBuilder)
		{
			this.SqlStringBuilder = sqlStringBuilder;
			this.PLSqlStringBuilder = sqlStringBuilder;
		}

		public OracleDatasetProvider(OracleQuerySet sqlStringBuilder)
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
			ParameterBuilder parameterBuilder = new OracleParameterBuilder();
			List<DbParameter> parameters = new List<DbParameter>(parameterBuilder.GetParameters(SqlStringBuilder));
			if (PLSqlStringBuilder.ReturnsId)
			{
				OracleParameter oracleParameter = new OracleParameter(":Id", OracleDbType.Int64, 0, "Id");
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
