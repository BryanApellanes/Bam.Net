using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.MySql;
using Bam.Net.Data.Npgsql;
using Bam.Net.Data.Oracle;
using Bam.Net.Data.SQLite;

namespace Bam.Net.Data.Repositories
{
    public partial class DatabaseFactory
    {
        public DatabaseFactory()
        {
            _databaseConstructors = new Dictionary<SqlDialect, Func<string, Database>>();
            Func<string, Database> ms = (cs) => throw new NotImplementedException($"MsSqlDatabase not implemented on the current platform");
            Func<string, Database> my = (cs) => new MySqlDatabase(cs);
            Func<string, Database> lite = (cs) => new SQLiteDatabase(cs);
            Func<string, Database> oracle = (cs) => new OracleDatabase { ConnectionString = cs };
            Func<string, Database> postgres = (cs) => new NpgsqlDatabase(cs);
            _databaseConstructors.Add(SqlDialect.Ms, ms);
            _databaseConstructors.Add(SqlDialect.MsSql, ms);
            _databaseConstructors.Add(SqlDialect.My, my);
            _databaseConstructors.Add(SqlDialect.MySql, my);
            _databaseConstructors.Add(SqlDialect.SQLite, lite);
            _databaseConstructors.Add(SqlDialect.Oracle, oracle);
            _databaseConstructors.Add(SqlDialect.Postgres, postgres);
            _databaseConstructors.Add(SqlDialect.Npgsql, postgres);
        }
    }
}
