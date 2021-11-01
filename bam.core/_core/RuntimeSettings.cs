using System;
using System.IO;
using System.Text;
using Bam.Net.Configuration;

namespace Bam.Net
{
    public static partial class RuntimeSettings
    {

        public static string ProcessDataFolder
        {
            get
            {
                return _appDataFolderLock.DoubleCheckLock(ref _appDataFolder, () =>
                {
                    if (!OSInfo.IsUnix)
                    {
                        StringBuilder path = new StringBuilder();
                        path.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                        if (!path.ToString().EndsWith(Path.PathSeparator.ToString()))
                        {
                            path.Append(Path.PathSeparator);
                        }

                        path.Append(DefaultConfiguration.GetAppSetting("ApplicationName", ApplicationNameProvider.Default.GetApplicationName()) + Path.PathSeparator);
                        FileInfo fileInfo = new FileInfo(path.ToString());
                        if (!Directory.Exists(fileInfo.Directory.FullName))
                        {
                            Directory.CreateDirectory(fileInfo.Directory.FullName);
                        }
                        _appDataFolder = path.ToString();
                        return _appDataFolder;
                    }
                    else
                    {
                        return Path.Combine(BamHome.DataPath, Config.Current?.ApplicationName ?? CoreServices.ApplicationRegistration.Data.Application.Unknown.Name);
                    }
                });
            }
            set => _appDataFolder = value;
        }
    }
}
