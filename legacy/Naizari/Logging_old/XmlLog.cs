/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Naizari.Configuration;

namespace Naizari.Logging
{
    [Serializable]
    public class XmlLog
    {
        public XmlLog()
        {
            logEvents = new List<LogEvent>();
        }

        public void AddEvent(LogEvent logEvent)
        {
            logEvents.Add(logEvent);
        }

        List<LogEvent> logEvents;
        [XmlElement]
        public LogEvent[] LogEvents
        {
            get { return logEvents.ToArray(); }
            set
            {
                this.logEvents.Clear();
                foreach (LogEvent ev in value)
                {
                    this.logEvents.Add(ev);
                }
            }
        }

        public static bool TryFromXml(string filePath, out XmlLog log)
        {
            log = new XmlLog();
            return DefaultConfiguration.TryFromXml<XmlLog>(log, filePath);
        }
    }
}
