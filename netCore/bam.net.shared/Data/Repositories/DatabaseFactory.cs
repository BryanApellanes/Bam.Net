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
        Dictionary<SqlDialect, Func<string, Database>> _databaseConstructors;

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
