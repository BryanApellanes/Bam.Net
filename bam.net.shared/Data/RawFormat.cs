using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class RawFormat : FormatPart
    {
        public RawFormat(string sql, Dictionary<string, object> parameters)
        {
            Sql = sql;
            parameters.Each(kvp =>
            {
                AddParameter(new ParameterInfo { ColumnName = kvp.Key, Value = kvp.Value });
            });
        }
        public RawFormat(string sql, params ParameterInfo[] parameters)
        {
            Sql = sql;
            parameters.Each(pi=>
            {
                AddParameter(pi);
            });
        }
        public string Sql { get; set; }
        public override string Parse()
        {
            return Sql;
        }
    }
}
