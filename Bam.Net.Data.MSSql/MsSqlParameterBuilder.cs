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

namespace Bam.Net.Data
{
    public class MsSqlParameterBuilder: ParameterBuilder
    {
        public override DbParameter BuildParameter(IParameterInfo c)
        {
            return new SqlParameter(string.Format("@{0}{1}", c.ColumnName, c.Number), c.Value);
        }
    }
}
