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
        }

        protected SqlStringBuilder SqlStringBuilder { get; set; }
        protected Database Database { get; set; }

        public string Sql { get; set; }
        Dictionary<string, object> _parameters;
        public Dictionary<string, object> Parameters
        {
            get
            {
                if(_parameters == null)
                {
                    Args.ThrowIfNull(Database, "Database");
                    _parameters = new Dictionary<string, object>();
                    Database.GetParameters(SqlStringBuilder).Each(new { Parameters }, (ctx, p) =>
                    {
                        ctx.Parameters.Add(p.ParameterName, p.Value.ToString());
                    });
                }
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        public IEnumerable<T> Execute<T>(Database db = null) where T : class, new()
        {
            Database = db;
            return db.ExecuteReader<T>(Sql, Parameters.ToDbParameters(Database));
        }
    }
}
