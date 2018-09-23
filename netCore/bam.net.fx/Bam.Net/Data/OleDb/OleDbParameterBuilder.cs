/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class OleDbParameterBuilder: ParameterBuilder
    {
        public override DbParameter BuildParameter(string name, object value)
        {
            OleDbParameter p = new OleDbParameter($"@{name}", value ?? DBNull.Value);
            SetOleDbTypeForDateTime(value, p);
            return p;
        }

        public override DbParameter BuildParameter(IParameterInfo c)
        {
            object value = c.Value;
            OleDbParameter p = new OleDbParameter(string.Format("@{0}{1}", c.ColumnName, c.Number), value ?? DBNull.Value);
            SetOleDbTypeForDateTime(value, p);
            return p;
        }

        private static void SetOleDbTypeForDateTime(object value, OleDbParameter p)
        {
            if (value is DateTime || value is DateTime?)
            {
                p.OleDbType = OleDbType.Date;
            }
        }
    }
}
