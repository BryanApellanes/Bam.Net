using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public abstract class DbConnectionManager : IDbConnectionManager
    {
        public virtual Database Database { get; set; }
        public virtual int MaxConnections { get; set; }
        public virtual int LifetimeMilliseconds { get; set; }
        public virtual bool BlockOnRelease { get; set; }

        public abstract DbConnection GetDbConnection();

        public abstract void ReleaseConnection(DbConnection dbConnection);
    }
}
