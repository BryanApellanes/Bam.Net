using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net
{
    public static partial class RuntimeSettings
    {

        public static string AppDataFolder
        {
            get
            {
                return _appDataFolderLock.DoubleCheckLock(ref _appDataFolder, () =>
                {
                    StringBuilder path = new StringBuilder();
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
                    _appDataFolder = path.ToString();
                    return _appDataFolder;
                });
            }
            set
            {
                _appDataFolder = value;
            }
        }
    }
}
