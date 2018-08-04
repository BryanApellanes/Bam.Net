using System;
using System.IO;

namespace Bam.Net.Data
{
    public interface IDataDirectoryProvider
    {
        DirectoryInfo GetAppDataDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppDatabaseDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppRepositoryDirectory(IApplicationNameProvider appNameProvider);
        DirectoryInfo GetAppFilesDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppEmailTemplatesDirectory(IApplicationNameProvider appNameProvider);

        DirectoryInfo GetRootDataDirectory();
        DirectoryInfo GetSysDataDirectory();
    }
}