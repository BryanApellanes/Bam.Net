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
    public class DatabaseFactory
    {
        Dictionary<SqlDialect, Func<string, Database>> _databaseConstructors;
        public DatabaseFactory()
        {
            _databaseConstructors = new Dictionary<SqlDialect, Func<string, Database>>();
            Func<string, Database> ms = (cs) => new MsSqlDatabase(cs);
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

        static DatabaseFactory _factory;
        static object _factoryLock = new object();
        public static DatabaseFactory Instance
        {
            get
            {
                return _factoryLock.DoubleCheckLock(ref _factory, () => new DatabaseFactory());
            }
        }

        public Database GetDatabase(SqlDialect dialect, string connectionString)
        {
            Func<string, Database> ctor = _databaseConstructors[dialect];
            Args.ThrowIf(ctor == null, "Unsupported dialect specified: {0}", dialect.ToString());
            return ctor(connectionString);
        }
    }
}
