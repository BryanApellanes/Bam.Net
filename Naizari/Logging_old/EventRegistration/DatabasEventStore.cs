/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using Naizari.Helpers;
using System.Text;
using Naizari.Data;
using System.Data;
using System.Data.SqlClient;
using Naizari.Configuration;
using Naizari.Extensions;
using System.Web;
using Naizari.Data.Common;

namespace Naizari.Logging.EventRegistration
{
    public class DatabaseEventStore: EventStore
    {
        List<string> requiredProperties = new List<string>();
        XmlLogger logger;

        public DatabaseEventStore()
            : base()
        {
            this.logger = new XmlLogger();
            this.DaoDbType = DaoDbType.SQLite;
            this.logger.LogName = "EventStoreLog";
        }

        public DatabaseEventStore(DaoDbType type)
            : this()
        {
            this.DaoDbType = type;
        }

        private DatabaseAgent GetAgent()
        {
            return DatabaseAgent.GetAgent(EventDefinition.ContextName, this.DaoDbType); 
        }

        public DaoDbType DaoDbType
        {
            get;
            set;
        }

        public override void Save(string saveTo)
        {
            // This method should do nothing since it is used
            // by the Xml event store to perpetualize its'
            // event definitions.  Since the DatabaseEventStore (this class)
            // will be perpetualizing EventDefinitions using
            // the SaveDefinition method, doing work here
            // would be redundant and unecessary and would be
            // a detriment to performance.
        }

        /// <summary>
        /// This method has been modified to do nothing.  The internal Dictionary 
        /// of EventDefinitions is now poplated as necessary when requests for 
        /// EventDefinitions are made.
        /// </summary>
        /// <param name="hydrateFrom">This parameter is ignored.  It is only used
        /// by the base class EventStore which uses an Xml file as the event
        /// perpetualization mechanism.</param>
        public override void Hydrate(string hydrateFrom)
        {
            
            
        }

        object working = new object();
        public override EventStore.EventDefinitionInfo GetEventDefinition(string applicationName, string messageSignature)
        {
            //if (this.eventDefinitions == null)
            //    this.eventDefinitions = new Dictionary<EventDefinitionInfo, EventDefinitionInfo>();

            EventDefinitionInfo retVal = new EventDefinitionInfo(applicationName, messageSignature);

            //if( eventDefinitions.ContainsKey(retVal))
            //{
            //    return eventDefinitions[retVal];
            //}

            DatabaseAgent agent = GetAgent();
            lock (working)
            {
                EventDefinitionSearchFilter filter = new EventDefinitionSearchFilter();
                filter.AddParameter(EventDefinitionFields.ApplicationName, applicationName);
                filter.AddParameter(EventDefinitionFields.MessageSignature, messageSignature);
                EventDefinition[] definitions = EventDefinition.SelectListWhere(filter, agent);

                EventDefinition definition = null;
                if (definitions.Length == 0)
                {
                    definition = EventDefinition.New(agent);
                    definition.ApplicationName = applicationName;
                    definition.MessageSignature = messageSignature;

                    if (definition.Insert() != -1)
                    {
                        retVal.EventId = definition.EventId;
                        //this.eventDefinitions.Add(retVal, retVal);
                    }
                }
                else if (definitions.Length > 1)
                {
                    //LogManager.CurrentLog.AddEntry("The event data on {0}, in the database {1} is corrupt.  More than one definition was found with the same app name and signature", this.EventStoreLocation, this.EventStoreName);
                    Log(string.Format("The event data in the database {0} is corrupt.  More than one definition was found with the same app name ({1}) and signature ({2})", agent.ConnectionString, applicationName, messageSignature));
                }
                else if (definitions.Length == 1)
                {
                    retVal.EventId = definitions[0].EventId;
                    //if (!this.eventDefinitions.ContainsKey(retVal))
                    //    this.eventDefinitions.Add(retVal, retVal);
                }
            }
            return retVal;
        }

        private void Log(string message)
        {
            logger.AddEntry(message);
        }

        
    }
}
