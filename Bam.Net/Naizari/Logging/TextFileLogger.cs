/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Naizari;
using Naizari.Configuration;
using Naizari.Extensions;
using System.IO;

namespace Naizari.Logging
{
    /// <summary>
    /// A logger used to log events to a text file
    /// </summary>
    public class TextFileLogger: Logger
    {
        const string unknownAppName = "UNKNOWN";
        int _fileNumber;

        public TextFileLogger()
            : base()
        {
            this.Folder = new DirectoryInfo(GetAppDataFolder());
            this._fileNumber = 1;
            this.MaxBytes = 102400;
            this.FileExtension = "log";
            
            this.SetNextFileInfo();
        }

        /// <summary>
        /// Gets or sets the maximum size of any single log file created by this logger.
        /// No effect if XmlLogger, use MaxEntries instead.
        /// </summary>
        public long MaxBytes
        {
            get;
            set;
        }

        protected string FileExtension
        {
            get;
            set;
        }

        protected int FileNumber
        {
            get
            {
                return _fileNumber;
            }
            set
            {
                _fileNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the directory where logs are written
        /// </summary>
        public DirectoryInfo Folder
        {
            get;
            set;
        }

        FileInfo _file;
        /// <summary>
        /// Gets the FileInfo representing the current log.  This will change as the file reaches 
        /// the max size or entries per file for XmlLogger.
        /// </summary>
        public FileInfo File
        {
            get
            {
                return _file;
            }
        }

        /// <summary>
        /// Writes the specified logEvent to the file referenced by the File property
        /// of the current TextFileLogger.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void CommitLogEvent(LogEvent logEvent)
        {
            string logText = string.Format("{0}\r\n****************\r\n\r\n", logEvent.PropertiesToString());

            logText.SafeAppendToFile(this._file.FullName);
            _file.Refresh();
            if (_file.Length >= this.MaxBytes)
            {
                SetNextFileInfo();
            }
        }

        /// <summary>
        /// Increments the file number if the current file number already exists.
        /// </summary>
        protected void SetNextFileInfo()
        {
            string appName = DefaultConfiguration.GetAppSetting("ApplicationName", unknownAppName);
            string fileName = string.Format("{0}_{1}.{2}", appName, _fileNumber, FileExtension);

            _file = new FileInfo(Folder.FullName + fileName);
            while (_file.Exists)
            {
                _fileNumber += 1;
                fileName = string.Format("{0}_{1}.{2}", appName, _fileNumber, FileExtension);
                _file = new FileInfo(Folder.FullName + fileName);
            }
        }

        /// <summary>
        /// Gets the path to the current user's AppData folder. If
        /// this is run in a Web app (HttpContext.Current isn't null)
        /// then the full path to ~/AppData/ is returned. 
        /// </summary>
        protected static string GetAppDataFolder()
        {
            StringBuilder path = new StringBuilder();
            if (HttpContext.Current == null)
            {
                path.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                if (!path.ToString().EndsWith("\\"))
                {
                    path.Append("\\");
                }

                path.Append("KLGates\\Logs\\" + DefaultConfiguration.GetAppSetting("ApplicationName", unknownAppName) + "\\");
                FileInfo fileInfo = new FileInfo(path.ToString());
                if (!Directory.Exists(fileInfo.Directory.FullName))
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }
            }
            else
            {
                path.Append(HttpContext.Current.Server.MapPath("~/App_Data/"));                
            }

            return path.ToString();
        }
    }
}
