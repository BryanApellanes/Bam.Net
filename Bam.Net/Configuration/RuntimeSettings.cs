using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.Configuration
{
    public static class RuntimeSettings
    {
        public static string AppDataFolder
        {
            get
            {
                StringBuilder path = new StringBuilder();
                if (HttpContext.Current == null)
                {
                    path.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    if (!path.ToString().EndsWith("\\"))
                    {
                        path.Append("\\");
                    }

                    path.Append(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN") + "\\");
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
}
