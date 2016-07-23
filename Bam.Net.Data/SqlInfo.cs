using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class SqlInfo
    {
        public SqlInfo(string sqlStatement, params DbParameter[] dbParameters)
        {
            Sql = sqlStatement;
            DbParameters = dbParameters;
        }
        public string Sql { get; set; }
        public DbParameter[] DbParameters { get; set; }

        public string ToInfoString()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{DbParameters.ToInfoString()}\r\n{DbParameters.Sha1()}\r\n{Sql}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            SqlInfo compareTo = obj as SqlInfo;
            if(compareTo != null)
            {
                return compareTo.ToString().Equals(ToString());
            }
            return base.Equals(obj);
        }
    }
}
