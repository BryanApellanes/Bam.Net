/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
//using System.Linq;
using System.IO;
using Naizari.Data;
using Naizari.Helpers;

namespace Naizari.Logging.EventRegistration
{
    public class EventManager//: DaoContextConsumer, IHasRequiredProperties
    {
        string storeFile;        

        internal EventManager()
        {
            this.eventStoreType = EventStoreTypes.None;
        }

        public void InitStore()
        {
            InitStore(this.eventStoreType);
        }

        public void InitStore(EventStoreTypes type)
        {
            //this.EventStoreType = type.ToString();
            switch (type)
            {
                case EventStoreTypes.None:
                    store = new NullEventStore();
                    break;
                case EventStoreTypes.Xml:
                    storeFile = FsUtil.GetCurrentUserAppDataFolder() + "EventStore.xml";
                    store = new EventStore();
                    if (File.Exists(storeFile))
                    {
                        FileInfo info = new FileInfo(storeFile);
                        if (info.IsReadOnly)
                            throw new EventStoreInitializationException(storeFile);

                        store.Hydrate(storeFile);
                    }
                  
                    break;
                case EventStoreTypes.MSSql:
                    store = new DatabaseEventStore(DaoDbType.MSSql);
                    try
                    {
                        DatabaseAgent.GetAgent(LogEventData.ContextName, DaoDbType.MSSql).EnsureSchema<EventDefinition>();
                    }
                    catch
                    {
                        // we tried
                    }
                    break;
                case EventStoreTypes.SQLite:
                    store = new DatabaseEventStore(DaoDbType.SQLite);
                    try
                    {
                        DatabaseAgent.GetAgent(LogEventData.ContextName, DaoDbType.SQLite).EnsureSchema<EventDefinition>();
                    }
                    catch (UnableToDetermineConnectionStringException utdcse)
                    {
                        //AppDb.Current;
                    }
                    catch
                    {
                        //we tried
                    }
                    break;
            }


        }

        IEventStore store;
        public IEventStore Store
        {
            get { return store; }
            set { this.store = value; }
        }

        EventStoreTypes eventStoreType;
        public string EventStoreType 
        {
            get { return eventStoreType.ToString(); }
            set
            {
                eventStoreType = (EventStoreTypes)Enum.Parse(typeof(EventStoreTypes), value);
                this.InitStore(eventStoreType);
            }
        }

        private static EventManager GetEventManager()
        {
            if (current == null)
            {                
                current = SingletonHelper.GetApplicationProvider<EventManager>(new EventManager());
            }

            return current;
        }


        static EventManager current;
        public static EventManager Current
        {
            get
            {
                return GetEventManager();
            }
        }

        object getEventDefLock = new object();
        /// <summary>
        /// Gets a unique id for the specified applicationName and messageSignature.  If
        /// one does not exist one will be created.
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="messageSignature">The signature of the message</param>
        /// <returns>Event id as integer.</returns>
        public int GetEventId(string applicationName, string messageSignature)
        {
            if( string.IsNullOrEmpty(applicationName) )
                throw new ArgumentNullException("applicationName");

            lock (getEventDefLock)
            {
                if (store == null)
                    InitStore();

                if (store != null)
                {
                    int retVal = store.GetEventDefinition(applicationName, messageSignature).EventId;
                    store.Save(storeFile);
                    return retVal;
                }
            }
            return -1;
        }

        //public string TableName { get; set; }
        //public string IntegratedSecurity { get; set; }
        //public string UserId { get; set; }
        //public string Password { get; set; }
    }
}
