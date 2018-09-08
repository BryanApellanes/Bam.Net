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
    public class MaxCountDbConnectionManager : IDbConnectionManager
    {
        int _next;
        public MaxCountDbConnectionManager(Database database)
        {
            Database = database;
            MaxConnections = 20;
            LifetimeMilliseconds = 3100;
            _next = 0;
        }

        protected List<DbConnection> Connections { get; set; }

        public Database Database { get; set; }

        int _maxConnections;
        public int MaxConnections
        {
            get
            {
                return _maxConnections;
            }
            set
            {
                _maxConnections = value;
                Connections = new List<DbConnection>(_maxConnections);
            }
        }

        public int LifetimeMilliseconds { get; set; }

        public DbConnection GetDbConnection()
        {
            if(_next >= MaxConnections)
            {
                _next = 0;
            }

            if (Connections[_next] != null)
            {
                DbConnection c = Connections[_next];
                Task.Run(() =>
                {
                    Thread.Sleep(LifetimeMilliseconds);
                    ReleaseConnection(c);
                });
            }

            DbConnection conn = Database.ServiceProvider.Get<DbProviderFactory>().CreateConnection();
            conn.ConnectionString = Database.ConnectionString;            
            Connections[_next] = conn;

            return Connections[_next++];
        }

        public void ReleaseConnection(DbConnection dbConnection)
        {
            try
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Exception releasing database connection: {ex.Message}");
            }
        }
    }
}
