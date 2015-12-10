/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using System.Data.Common;
using System.Data;
using Bam.Net.Incubation;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace Bam.Net.Data
{
	public class OracleParameterBuilder : ParameterBuilder
    {
        public override DbParameter BuildParameter(IParameterInfo c)
        {
            string parameterName = string.Format(":{0}{1}", c.ColumnName, c.Number);
			object value = c.Value;
			if (c.Value is bool)
			{
				bool b = (bool)c.Value;
				char val = b ? '1' : '0';
				value = val;
			}
			OracleParameter result = new OracleParameter(parameterName, value);
			if (c.Value is string)
			{				
				result.DbType = DbType.String;
			}
            return result;
        }

    }
}
