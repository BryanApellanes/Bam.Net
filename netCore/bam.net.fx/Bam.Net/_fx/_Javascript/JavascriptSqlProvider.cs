using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Bam.Net.Data;
using System.Data;
using Bam.Net.Configuration;
using Bam.Net.Javascript.Sql;

namespace Bam.Net.Javascript
{
    public abstract partial class JavaScriptSqlProvider // fx
    {
        public SqlResponse Execute(string sql)
        {
            EnsureInitialized();
            SqlResponse result = new SqlResponse();
            try
            {
                DataTable results = Database.GetDataTable(sql, CommandType.Text);
                result.Results = results.ToDynamicList().ToArray();
                result.Count = results.Rows.Count;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Results = new object[] { };
            }

            return result;
        }
    }
}
