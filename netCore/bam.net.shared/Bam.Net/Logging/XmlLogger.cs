/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Configuration;
using System.IO;

namespace Bam.Net.Logging
{
    /// <summary>
    /// A logger that logs to an xml file
    /// </summary>
    public class XmlLogger: TextFileLogger
    {
        LogEventCollection _xmlLog;
        public XmlLogger()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);
            this.Verbosity = VerbosityLevel.Information;

            this.FileNumber = 1;
            this.FileExtension = "xml";
            this.MaxEntriesPerFile = 20;

            this.Folder = new DirectoryInfo(GetAppDataFolder());

            if (File.Exists)
            {
                _xmlLog = File.FullName.FromXmlFile<LogEventCollection>();
            }
            else
            {
                _xmlLog = new LogEventCollection();
            }
        }

        public int MaxEntriesPerFile
        {
            get;
            set;
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            _xmlLog.Add(logEvent);
            _xmlLog.ToXmlFile(File.FullName);
            if (_xmlLog.Count >= MaxEntriesPerFile)
            {
                _xmlLog.Clear();
                SetNextFileInfo();                
            }
        }        
    }
}
