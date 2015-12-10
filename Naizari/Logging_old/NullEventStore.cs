/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Logging.EventRegistration
{
    public class NullEventStore: IEventStore
    {
        #region IEventStore Members

        public EventStore.EventDefinitionInfo[] EventDefinitions
        {
            get
            {
                return null;
            }
            set
            {
                //
            }
        }

        public EventStore.EventDefinitionInfo GetEventDefinition(string applicationName, string messageSignature)
        {
            return new EventStore.EventDefinitionInfo();
        }

        public void Hydrate(string hydrateFrom)
        {
            
        }

        public void Save(string saveTo)
        {
            
        }

        #endregion
    }
}
