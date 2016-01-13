/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace Bryan.Apellanes.Data.Npgsql
{
	public interface IPLSqlStringBuilder
	{
		NpgsqlParameter IdParameter { get; set; }
		bool ReturnsId { get; set; }
	}
}
