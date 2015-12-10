/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Naizari.Logging
{
    /// <summary>
    /// Represents a collection of LogEvents that can be persisted to a file.
    /// </summary>
    [Serializable]
    [XmlRoot("XmlLog")]
    public class LogEventCollection: List<LogEvent>
    {
        /// <summary>
        /// Gets or sets the LogEvent array that constitute
        /// this LogEventCollection
        /// </summary>
        public LogEvent[] EventLogEntries
        {
            get
            {
                return this.ToArray();
            }
            set
            {
                this.Clear();
                this.AddRange(value);
            }
        }
    }
}
