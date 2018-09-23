using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class DataPaths
    {
        public static DataPaths Get(IDataDirectoryProvider dataDirectoryProvider)
        {
            return new DataPaths
            {
                DataRoot = dataDirectoryProvider.GetRootDataDirectory().FullName,
                SysData = dataDirectoryProvider.GetSysDataDirectory().FullName,

                AppData = dataDirectoryProvider.GetAppDataDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName,
                UserData = dataDirectoryProvider.GetAppUsersDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName,
                AppDatabase = dataDirectoryProvider.GetAppDatabaseDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName,
                AppRepository = dataDirectoryProvider.GetAppRepositoryDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName,
                AppFiles = dataDirectoryProvider.GetAppFilesDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName,
                AppEmailTemplates = dataDirectoryProvider.GetAppEmailTemplatesDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName
            };
        }

        public string DataRoot { get; set; }
        public string SysData { get; set; }
        public string AppData { get; set; }
        public string UserData { get; set; }
        public string AppDatabase { get; set; }
        public string AppRepository { get; set; }
        public string AppFiles { get; set; }
        public string AppEmailTemplates { get; set; }
    }
}
