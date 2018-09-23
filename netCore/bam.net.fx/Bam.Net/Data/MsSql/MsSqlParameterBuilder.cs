/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data;
using System.Data;

namespace Bam.Net.Data
{
    public class MsSqlParameterBuilder: ParameterBuilder
    {
        public override DbParameter BuildParameter(string name, object value)
        {
            return new SqlParameter($"@{name}", value ?? DBNull.Value);
        }
        public override DbParameter BuildParameter(IParameterInfo c)
        {
            SqlParameter result = new SqlParameter(string.Format("@{0}{1}", c.ColumnName, c.Number), c.Value ?? DBNull.Value);
            if(c.Value is UInt32)
            {
                result.SqlDbType = SqlDbType.Int;
                result.SqlValue = Convert.ToInt32(c.Value);
            }
            if(c.Value is UInt64)
            {
                result.SqlDbType = SqlDbType.BigInt;
                result.SqlValue = Convert.ToInt64(c.Value);
            }
            return result;
        }
    }
}
