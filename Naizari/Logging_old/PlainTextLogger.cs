/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;

namespace Naizari.Logging
{
    public class PlainTextLogger: LoggerBase
    {
        //string filePath;
        public PlainTextLogger(): base()
        {
            //this.filePath = string.Format("{0}\\{1}", this.LogLocation, this.LogName);
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            string filePath = string.Format("{0}\\{1}", this.LogLocation, this.LogName);
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(logEvent.Severity.ToString() + ", " +
                    logEvent.TimeOccurred.ToString() + ", " +
                    logEvent.EventID + ", " +
                    logEvent.User + ", " +
                    logEvent.Message.Replace(",", "&comma&") + ", " +
                    logEvent.Source + "," +
                    logEvent.Computer + ", " +
                    logEvent.Category); 
            }
        }
    }
}
