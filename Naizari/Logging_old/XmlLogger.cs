/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using Naizari.Configuration;
using System.Web;

namespace Naizari.Logging
{
    public class XmlLogger: LoggerBase
    {
        XmlLog log;
        int nextLog = -1;
        public XmlLogger(): base()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(FlushLog);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(FlushLog);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(FlushLog);
        }

        object fileLock = new object();
        public void FlushLog(object sender, EventArgs e)
        {
            if (log != null)
            {
                string fileName = GetFileName();//this.LogLocation + "\\" + nextLog.ToString() + "_" + this.LogName;
                lock (fileLock)
                {
                    DefaultConfiguration.ToXml(log, fileName);
                }
            }
        }

        int entriesPerFile = 100;
        public int EntriesPerFile
        {
            get { return entriesPerFile; }
            set { entriesPerFile = value; }
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            if (nextLog == -1)
                GetNextLogNumber();

            string fileName = GetFileName();

            if (!File.Exists(fileName))
            {
                log = new XmlLog();
            }
            else if( log == null )
            {
                log = new XmlLog();
                DefaultConfiguration.TryFromXml<XmlLog>(log, fileName);
            }

            if (log.LogEvents.Length >= entriesPerFile)
            {
                DefaultConfiguration.ToXml(log, fileName);
                nextLog++;
                CommitLogEvent(logEvent);
                return;
            }

            log.AddEvent(logEvent);
            FileInfo info = new FileInfo(fileName);
            if (!info.Directory.Exists)
            {
                info.Directory.Create();
            }
            DefaultConfiguration.ToXml(log, fileName);
        }

        private string GetFileName()
        {
            string fileName = string.Empty;
            if (HttpContext.Current != null)
            {
                fileName = HttpContext.Current.Server.MapPath("~/App_Data/" + nextLog.ToString() + "_" + this.LogName + ".xml");
            }
            else
            {
                fileName = this.LogLocation + "\\" + nextLog.ToString() + "_" + this.LogName + ".xml";
            }
            return fileName;
        }

        private string GetCheckFile(int checkNumber)
        {
            string checkFileName = string.Empty;
            if (HttpContext.Current != null)
            {
                checkFileName = HttpContext.Current.Server.MapPath("~/App_Data/" + checkNumber.ToString() + "_" + this.LogName + ".xml");
            }
            else
            {
                checkFileName = string.Format("{0}\\{1}_{2}.xml", this.LogLocation, checkNumber, this.LogName);
            }

            return checkFileName;
        }

        private void GetNextLogNumber()
        {
            //string fileName = this.LogLocation + "\\" + this.LogName + "_";
            int check = 0;
            while (nextLog == -1)
            {
                string currentCheckFile = GetCheckFile(check);
                if (!File.Exists(currentCheckFile))
                    nextLog = check;
                else
                {
                    XmlLog checkLog = new XmlLog();
                    DefaultConfiguration.TryFromXml<XmlLog>(checkLog, currentCheckFile);
                    //checkLog.TryFromXml<XmlLog>(currentCheckFile);
                    if (checkLog.LogEvents.Length < entriesPerFile)
                        nextLog = check;
                }

                check++;
            }
        }
    }
}
