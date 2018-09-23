using Bam.Net.Logging;
using Bam.Net.Logging.Counters;
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
    public class MaxCountDbConnectionManager : DefaultDbConnectionManager
    {
        public MaxCountDbConnectionManager(Database database) : base(database) { }
    }

    public class DefaultDbConnectionManager : DbConnectionManager
    {
        int _next;
        public DefaultDbConnectionManager(Database database)
        {
            Database = database;
            MaxConnections = 10;
            LifetimeMilliseconds = 3100;
            _next = -1;
        }

        protected List<DbConnection> Connections { get; set; }
        
        int _maxConnections;
        public override int MaxConnections
        {
            get
            {
                return _maxConnections;
            }
            set
            {
                _maxConnections = value;
            }
        }

        public override DbConnection GetDbConnection()
        {
            if(Connections == null || Connections.Count == 0)
            {
                InitConnections();
            }

            int returnIndex = GetNext();

            if (Connections[returnIndex] != null)
            {
                Log.DebugInfo($"Releasing connection at index {returnIndex}.");
                DbConnection c = Connections[returnIndex];
                Task releaseTask = Task.Run(() =>
                {
                    Thread.Sleep(LifetimeMilliseconds); // give the consumer of the connection a chance to use it and complete
                    string timerName = $"{GetType().Name}.{Database.GetType().Name}.ConnectionReleaseTimer_{6.RandomLetters()}";
                    Bam.Net.Logging.Counters.Timer releaseTimer = Stats.Start(timerName);
                    Log.DebugInfo($"Waiting for connection to release");
                    ReleaseConnection(c);
                    Stats.End(releaseTimer, (timer) => Log.DebugInfo("{0}", timer));
                });
                if (BlockOnRelease)
                {                    
                    releaseTask.Wait();                    
                }
            }

            DbConnection conn = CreateConnection();
            Connections[returnIndex] = conn;

            return conn;
        }
        
        public override void ReleaseConnection(DbConnection dbConnection)
        {
            try
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            catch (Exception ex)
            {
                Log.Trace("{0}: Exception releasing database connection: {1}", ex, nameof(DefaultDbConnectionManager), ex.Message);
            }
        }

        object _nextLock = new object();
        protected int GetNext()
        {
            lock (_nextLock)
            {
                _next++;
                if(_next >= MaxConnections)
                {
                    _next = 0;
                }

                return _next;
            }
        }

        protected void InitConnections()
        {
            Connections = new List<DbConnection>(MaxConnections);
            foreach (DbConnection connection in Connections)
            {
                try
                {
                    connection.Close();
                    connection.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Trace("Exception disposing connection: {0}", ex, ex.Message);
                }
            }

            for(int i = 0; i < MaxConnections; i++)
            {
                Connections.Add(CreateConnection());
            }
        }
    }
}
