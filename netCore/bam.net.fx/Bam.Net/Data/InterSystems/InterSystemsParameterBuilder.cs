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
using InterSystems.Data.CacheClient;

namespace Bam.Net.Data
{
    public class InterSystemsParameterBuilder : ParameterBuilder
    {
        public override DbParameter BuildParameter(string name, object value)
        {
            return new CacheParameter($"@{name}", value ?? DBNull.Value);
        }
        public override DbParameter BuildParameter(IParameterInfo c)
        {
            return new CacheParameter(string.Format("@{0}{1}", c.ColumnName, c.Number), c.Value ?? DBNull.Value);
        }
    }
}
