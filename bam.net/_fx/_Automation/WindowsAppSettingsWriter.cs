using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Sys;

namespace Bam.Net.Automation
{
    public class WindowsAppSettingsWriter : IAppSettingsWriter
    {
        public void SetAppSettings(string host, string configPathOnRemote, Dictionary<string, string> appSettings)
        {
            FileInfo configFile = new FileInfo(configPathOnRemote);
            string adminSharePath = configFile.GetAdminSharePath(host);

            DefaultConfiguration.SetAppSettings(adminSharePath, appSettings);
        }
    }
}
