/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace Bam.Net.Data.Oracle
{
	public interface IPLSqlStringBuilder
	{
		OracleParameter IdParameter { get; set; }
		bool ReturnsId { get; set; }
	}
}
