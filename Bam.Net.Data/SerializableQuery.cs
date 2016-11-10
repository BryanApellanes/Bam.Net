using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class SerializableQuery
    {
        public SerializableQuery(SqlStringBuilder sql, Database db)
        {
            SqlStringBuilder = sql;
            Database = db;
            Sql = sql;
            Parameters = new Dictionary<string, string>();
            db.GetParameters(sql).Each(new { Parameters }, (ctx, p) =>
            {
                ctx.Parameters.Add(p.ParameterName, p.Value.ToString());
            });
        }

        protected SqlStringBuilder SqlStringBuilder { get; set; }
        protected Database Database { get; set; }

        public string Sql { get; set; }
        public Dictionary<string, string> Parameters { get; set; }        

        public IEnumerable<T> Execute<T>(Database db = null) where T : class, new()
        {
            return db.ExecuteReader<T>(Sql, Parameters.ToDbParameters(db));
        }
    }
}
