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
    public class MaxCountDbConnectionManager : DbConnectionManager
    {
        int _next;
        public MaxCountDbConnectionManager(Database database)
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
                SetConnections();
            }
        }

        public override DbConnection GetDbConnection()
        {
            int returnIndex = ++_next;
            if(returnIndex >= MaxConnections)
            {
                _next = -1;
                returnIndex = 0;
            }

            if (Connections[returnIndex] != null)
            {
                Log.Debug($"Releasing connection at index {returnIndex}.");
                DbConnection c = Connections[returnIndex];
                Task releaseTask = Task.Run(() =>
                {
                    Thread.Sleep(LifetimeMilliseconds); // give the consumer of the connection a chance to use it and complete
                    string timerName = $"{GetType().Name}.ConnectionReleaseTimer_{6.RandomLetters()}";
                    Bam.Net.Logging.Counters.Timer releaseTimer = Stats.Start(timerName);
                    Log.Debug($"Waiting for connection to release");
                    ReleaseConnection(c);
                    Stats.End(releaseTimer, (timer) => Log.Debug("{0}", timer));
                });
                if (BlockOnRelease)
                {                    
                    releaseTask.Wait();                    
                }
            }
            
            DbConnection conn = Database.CreateConnection();
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
                Log.Trace("Exception releasing database connection: {0}", ex, ex.Message);
            }
        }

        protected void SetConnections()
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
                Connections.Add(Database.CreateConnection());
            }
        }
    }
}
