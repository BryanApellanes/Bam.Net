/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Naizari.Logging;
using Naizari.Data;
using System.Data;
using System.Data.SqlClient;
using Naizari.Configuration;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;
using System.Reflection;
using System.Threading;
using System.Security.Principal;
using System.Web;

namespace Naizari.Logging
{
    public abstract class DbLogger: LoggerBase
    {
       
        Timer maintenance;

        public DbLogger()
            : base()
        {
            int dueTime = 1000 * 60 * 60;
            this.maintenance = new Timer(new TimerCallback(Clean), null, 0, dueTime);
        }

        public DbLogger(DatabaseAgent agent): this()
        {
            this.DatabaseAgent = agent;
        }

        public void Initialize(DaoDbType type)
        {
            this.DatabaseAgent = GetAgent(type);            
        }

        private DatabaseAgent GetAgent(DaoDbType type)
        {
            return DatabaseAgent.GetAgent(LogEventData.ContextName, type);
        }

        protected DatabaseAgent agent;
        public DatabaseAgent DatabaseAgent
        {
            get
            {
                if (agent == null)
                    return DaoContext.Get(LogEventData.ContextName).DatabaseAgent;

                return agent;
            }
            set
            {
                this.agent = value;
                this.agent.EnsureSchema<LogEventData>();
                this.RestartLoggingThread();
            }
        }

        public void Clean()
        {
            this.Clean(null);
        }

        private void Clean(object state)
        {
            if (this.History > 0)
            {
                LogEventDataSearchFilter filter = new LogEventDataSearchFilter();
                filter.AddParameter(LogEventDataFields.TimeOccurred, DateTime.UtcNow.Subtract(new TimeSpan(this.History, 0, 0, 0)), Comparison.LessThan);

                this.DatabaseAgent.Delete<LogEventData>(filter);
                this.DatabaseAgent.Shrink();
            }
        }
        
        public override void CommitLogEvent(LogEvent logEvent)
        {
            LogEventData toInsert = LogEventData.FromLogEvent(logEvent, this.DatabaseAgent);
            if (toInsert.Insert() == -1)
                throw toInsert.LastException;
        }


        /// <summary>
        ///  Gets or sets the number of days to keep entries before deleting.
        /// </summary>
        public int History
        {
            get;
            set;
        }


        
    }
}
