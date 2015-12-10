/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Naizari.Logging.EventRegistration
{
    public interface IEventStore
    {
        EventStore.EventDefinitionInfo[] EventDefinitions { get; set; }
        EventStore.EventDefinitionInfo GetEventDefinition(string applicationName, string messageSignature);
        void Hydrate(string hydrateFrom);
        void Save(string saveTo);
    }
}
