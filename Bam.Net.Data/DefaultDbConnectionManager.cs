using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class DefaultDbConnectionManager : IDbConnectionManager
    {
        HashSet<DbConnection> _connections;
        AutoResetEvent _resetEvent;
        public DefaultDbConnectionManager(Database database)
        {
            Database = database;
            MaxConnections = 10;
            LifetimeMilliseconds = 3100;
            _connections = new HashSet<DbConnection>();
            _resetEvent = new AutoResetEvent(false);
        }

        public Database Database { get; set; }

        public int MaxConnections { get; set; }
        public int LifetimeMilliseconds { get; set; }

        object _connectionLock = new object();
        public DbConnection GetDbConnection()
        {
            if (_connections.Count >= MaxConnections)
            {
                if (!_resetEvent.WaitOne(LifetimeMilliseconds))
                {
                    _connections.BackwardsEach(connection => ReleaseConnection(connection));
                }
            }

            DbConnection conn = Database.ServiceProvider.Get<DbProviderFactory>().CreateConnection();
            conn.ConnectionString = Database.ConnectionString;
            lock (_connectionLock)
            {
                _connections.Add(conn);
            }
            return conn;
        }

        public void ReleaseConnection(DbConnection conn)
        {
            try
            {
                lock (_connectionLock)
                {
                    if (_connections.Contains(conn))
                    {
                        _connections.Remove(conn);
                    }

                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            catch (Exception ex)
            {
                Log.Trace("{0}: Exception releasing database connection: {1}", ex, nameof(DefaultDbConnectionManager), ex.Message);
            }

            _resetEvent.Set();
        }
    }
}