/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging.EventRegistration
{
    /// <summary>
    /// Thrown when an EventStore does not have write permission to the
    /// store file.
    /// </summary>
    public class EventStoreInitializationException: Exception
    {
        public EventStoreInitializationException(string storeFile)
            : base(string.Format("Unable to read store file: {0}", storeFile))
        { }
        
    }
}
