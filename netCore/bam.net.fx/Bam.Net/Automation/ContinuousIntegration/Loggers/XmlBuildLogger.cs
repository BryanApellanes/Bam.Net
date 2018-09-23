/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Automation.ContinuousIntegration;
using System.IO;

namespace Bam.Net.Automation.ContinuousIntegration.Loggers
{
    public class XmlBuildLogger: BuildLogger<XmlLogger>
    {
        public XmlBuildLogger(string folderPath)
            : base()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("Folder", new DirectoryInfo(folderPath));
            SetLoggerProperties(properties);
        }

        public XmlBuildLogger()
            : this(".\\XmlBuildLogs")
        { }
    }
}
