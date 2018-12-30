/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Configuration;
using System.IO;

namespace Naizari.Logging
{
    public class XmlLogger: TextFileLogger
    {
        LogEventCollection _xmlLog;
        public XmlLogger()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);
            this.Verbosity = 4;

            this.Folder = new DirectoryInfo(GetAppDataFolder());
            this.FileNumber = 1;
            this.FileExtension = "xml";
            this.MaxEntriesPerFile = 20;
            this.SetNextFileInfo();

            if (File.Exists)
            {
                _xmlLog = File.FullName.FromXmlFile<LogEventCollection>();
            }
            else
            {
                _xmlLog = new LogEventCollection();
            }

            //this.RestartLoggingThread();
        }

        public int MaxEntriesPerFile
        {
            get;
            set;
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            _xmlLog.Add(logEvent);
            _xmlLog.ToXml(File.FullName);
            if (_xmlLog.Count >= MaxEntriesPerFile)
            {
                _xmlLog.Clear();
                SetNextFileInfo();                
            }
        }        
    }
}
