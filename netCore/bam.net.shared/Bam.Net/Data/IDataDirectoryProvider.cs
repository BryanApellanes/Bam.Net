using System;
using System.IO;

namespace Bam.Net.Data
{
    public interface IDataDirectoryProvider
    {
        DirectoryInfo GetAppDataDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppUsersDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppDatabaseDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppRepositoryDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppFilesDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetAppEmailTemplatesDirectory(IApplicationNameProvider applicationNameProvider);
        DirectoryInfo GetRootDataDirectory();
        DirectoryInfo GetSysDataDirectory();
        DirectoryInfo GetSysAssemblyDirectory();
        DirectoryInfo GetChunksDirectory();
        DirectoryInfo GetFilesDirectory();
    }
}