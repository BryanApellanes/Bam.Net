using Bam.Net.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class PerThreadDbConnectionManager : DbConnectionManager
    {
        ConcurrentDictionary<int, DbConnection> _connections;
        public PerThreadDbConnectionManager(Database database)
        {
            Database = database;
            MaxConnections = 10;
            LifetimeMilliseconds = 3100;
            _connections = new ConcurrentDictionary<int, DbConnection>();
        }
        
        public override DbConnection GetDbConnection()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;            
            if (_connections.ContainsKey(threadId))
            {
                GiveThreadAChanceToCompleteBeforeReleasingConnection(threadId);
            }

            if (_connections.Count >= MaxConnections)
            {
                if (!Exec.SleepUntil(() => _connections.Count < MaxConnections, out int slept, LifetimeMilliseconds))
                {
                    Log.TraceWarn("Waited {0} milliseconds for connection count to drop but they were never below {1}, releasing all connections", slept, MaxConnections);
                    ReleaseAllConnections();
                }
            }

            SetConnection(threadId, out DbConnection connection);
            return connection;
        }

        public override void ReleaseConnection(DbConnection connection)
        {
            try
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            catch (Exception ex)
            {
                Log.Trace("{0}: Exception releasing database connection: {1}", ex, nameof(PerThreadDbConnectionManager), ex.Message);
            }
        }

        private void ReleaseAllConnections()
        {
            try
            {
                foreach(int threadId in _connections.Keys)
                {
                    GiveThreadAChanceToCompleteBeforeReleasingConnection(threadId);
                }
            }
            catch (Exception ex)
            {
                Log.Trace("{0}: Exception releasing all connections: {1}", ex, nameof(PerThreadDbConnectionManager), ex.Message);
            }
        }

        private void GiveThreadAChanceToCompleteBeforeReleasingConnection(int threadId)
        {
            if (_connections.TryRemove(threadId, out DbConnection dbConnection))
            {
                Task.Run(() =>
                {
                    ProcessThread thread = Exec.GetThread(threadId);
                    if (thread != null)
                    {
                        int slept = Exec.SleepUntil(() => thread.ThreadState == System.Diagnostics.ThreadState.Terminated || thread.ThreadState == System.Diagnostics.ThreadState.Unknown, LifetimeMilliseconds * 2);
                        Exec.After(LifetimeMilliseconds - slept, () => ReleaseConnection(dbConnection));
                    }
                    else
                    {
                        Exec.After(LifetimeMilliseconds, () => ReleaseConnection(dbConnection));
                    }
                });
            }
        }

        private void SetConnection(int threadId, out DbConnection connection)
        {
            connection = CreateConnection();
            if(!_connections.TryAdd(threadId, connection))
            {
                DbConnection c = connection;                
                Log.TraceInfo("{0}: Failed to add DbConnection to inner tracking dictionary", nameof(PerThreadDbConnectionManager));
                Exec.After(LifetimeMilliseconds, () => ReleaseConnection(c));
            }
        }
    }
}
