/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using Naizari.Configuration;

namespace Naizari.Logging.EventRegistration
{

    [Serializable]
    public class EventStore : IEventStore
    {
        protected Dictionary<EventDefinitionInfo, EventDefinitionInfo> eventDefinitions;
        protected int nextId = 1;

        public EventStore()
        {
            eventDefinitions = new Dictionary<EventDefinitionInfo, EventDefinitionInfo>();
        }

        public virtual EventDefinitionInfo GetEventDefinition(string applicationName, string messageSignature)
        {
            bool ignore;
            return GetEventDefinition(applicationName, messageSignature, out ignore);
        }

        public EventDefinitionInfo GetEventDefinition(string applicationName, string messageSignature, out bool added)
        {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName");

            added = false;

            EventDefinitionInfo key = new EventDefinitionInfo(applicationName, messageSignature);
            if (!this.eventDefinitions.ContainsKey(key))
            {
                eventDefinitions.Add(key, key);
                added = true;
            }
            
            if (eventDefinitions[key].EventId == -1)
            {
                eventDefinitions[key].EventId = nextId;
                nextId++;
                
            }

            return eventDefinitions[key];
        }

        [XmlElement]
        public EventDefinitionInfo[] EventDefinitions
        {
            get
            {
                EventDefinitionInfo[] ret = new EventDefinitionInfo[this.eventDefinitions.Values.Count];
                this.eventDefinitions.Values.CopyTo(ret, 0);
                return ret;//this.eventDefinitions.Values.ToArray<EventDefinition>();
            }
            set
            {
                SetDefinitions(value);
            }
        }

        public virtual void Hydrate(string hydrateFrom)
        {
            EventStore copy = SerializationUtil.DeserializeFromFile<EventStore>(hydrateFrom);
            DefaultConfiguration.CopyProperties(copy, this);
            //this.FromXml<EventStore>(hydrateFrom);
        }

        public virtual void Save(string saveTo)
        {
            DefaultConfiguration.ToXml(this, saveTo);
            //this.ToXml(saveTo);
        }

        protected virtual void SetDefinitions(EventDefinitionInfo[] value)
        {
            this.eventDefinitions.Clear();
            foreach (EventDefinitionInfo def in value)
            {
                if( !this.eventDefinitions.ContainsKey(def) )
                    this.eventDefinitions.Add(def, def);

                if (def.EventId >= nextId)
                {
                    nextId = def.EventId + 1;
                }
            }
        }

        [Serializable]
        public class EventDefinitionInfo
        {
            internal EventDefinitionInfo()
            { }

            public EventDefinitionInfo(string applicationName, string messageSignature)
            {
                this.ApplicationName = applicationName;
                this.MessageSignature = messageSignature;
            }

            int eventId = -1;
            public int EventId 
            {
                get { return eventId; }
                set { eventId = value; }
            }
            public string ApplicationName { get; set; }
            public string MessageSignature { get; set; }

            public override int GetHashCode()
            {
                return this.ApplicationName.GetHashCode() + this.MessageSignature.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                Type argType = obj.GetType();
                if (argType.Equals(this.GetType()))
                {
                    return argType.GetProperty("ApplicationName").GetValue(obj, null).Equals(this.ApplicationName) &&
                        argType.GetProperty("MessageSignature").GetValue(obj, null).Equals(this.MessageSignature);
                }

                return false;
            }
        }
    }
}
