using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public class DataSettings: DatabaseProvider<SQLiteDatabase>
    {
        public DataSettings()
        {
            DataRootDirectory = "C:\\bam\\data";
            AppDataDirectory = "AppData";
            SysDataDirectory = "SysData";
            DatabaseDirectory = "Databases";
            RepositoryDirectory = "Repositories";
            FilesDirectory = "Files";
            ChunksDirectory = "Chunks";
            WorkspacesDirectory = "Workspaces";
            EmailTemplatesDirectory = "EmailTemplates";
            ProcessMode = ProcessMode.Default;
            Logger = Log.Default;
        }

        public DataSettings(ProcessMode processMode, ILogger logger = null):this()
        {
            ProcessMode = processMode;
            Logger = logger ?? Log.Default;
        }

        static DataSettings _default;
        static object _defaultLock = new object();
        public static DataSettings Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DataSettings());
            }
        }

        static DataSettings _fromConfig;
        static object _fromConfigLock = new object();
        public static DataSettings FromConfig
        {
            get
            {
                return _fromConfigLock.DoubleCheckLock(ref _fromConfig, () => new DataSettings(ProcessMode.FromConfig));
            }
        }

        public ProcessMode ProcessMode { get; set; }
        public string DataRootDirectory { get; set; }
        public string AppDataDirectory { get; set; }
        public string SysDataDirectory { get; set; }
        public string DatabaseDirectory { get; set; }
        public string RepositoryDirectory { get; set; }
        public string FilesDirectory { get; set; }
        public string ChunksDirectory { get; set; }
        public string WorkspacesDirectory { get; set; }
        public string EmailTemplatesDirectory { get; set; }

        public DirectoryInfo GetRootDataDirectory()
        {
            return new DirectoryInfo(Path.Combine(DataRootDirectory, ProcessMode.ToString()));
        }

        public DirectoryInfo GetRootDataDirectory(string directoryName)
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, directoryName));
        }

        public DirectoryInfo GetSysDataDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, SysDataDirectory));
        }

        public DirectoryInfo GetSysDataDirectory(string directoryName)
        {
            return new DirectoryInfo(Path.Combine(GetSysDataDirectory().FullName, directoryName));
        }

        public DirectoryInfo GetAppDataDirectory(IApplicationNameProvider appNameProvider)
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, AppDataDirectory, appNameProvider.GetApplicationName()));
        }

        public DirectoryInfo GetAppDataDirectory(IApplicationNameProvider appNameProvider, string directoryName)
        {
            return new DirectoryInfo(Path.Combine(GetAppDataDirectory(appNameProvider).FullName, directoryName));
        }

        public DirectoryInfo GetAppDatabaseDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, DatabaseDirectory);
        }

        public DirectoryInfo GetDatabaseDirectory()
        {
            return GetRootDataDirectory(DatabaseDirectory);
        }

        public DirectoryInfo GetAppRepositoryDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, RepositoryDirectory);
        }

        public DirectoryInfo GetRepositoryDirectory()
        {
            return GetRootDataDirectory(RepositoryDirectory);
        }

        public DirectoryInfo GetAppRepositoryWorkspaceDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, WorkspacesDirectory);
        }

        public DirectoryInfo GetRepositoryWorkspaceDirectory()
        {
            return GetRootDataDirectory(RepositoryDirectory);
        }

        public DirectoryInfo GetAppFilesDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, FilesDirectory);
        }

        public DirectoryInfo GetFilesDirectory()
        {
            return GetRootDataDirectory(FilesDirectory);
        }
        
        public DirectoryInfo GetChunksDirectory()
        {
            return GetRootDataDirectory(ChunksDirectory);
        }

        public DirectoryInfo GetAppWorkspaceDirectory(IApplicationNameProvider appNameProvider, Type type)
        {
            string hash = type.ToInfoHash();
            return new DirectoryInfo(Path.Combine(GetAppDataDirectory(appNameProvider).FullName, WorkspacesDirectory, type.Name, hash));
        }

        public DirectoryInfo GetWorkspaceDirectory(Type type)
        {
            string hash = type.ToInfoHash();
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, WorkspacesDirectory, type.Name, hash));
        }

        public DaoRepository GetGenericDaoRepository(ILogger logger = null, string schemaName = null)
        {
            return new DaoRepository(GetDatabaseFor(typeof(DaoRepository)), logger, schemaName);
        }
        
        public DirectoryInfo GetAppEmailTemplatesDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, EmailTemplatesDirectory);
        }

        public DirectoryInfo GetEmailTemplatesDirectory()
        {
            return GetRootDataDirectory(EmailTemplatesDirectory);
        }

        public void SetDatabaseFor(object instance)
        {
            instance.Property("Database", GetDatabaseFor(instance), false);
        }        

        public override SQLiteDatabase GetDatabaseFor(object instance)
        {
            string databaseName = instance.GetType().FullName;
            string schemaName = instance.Property<string>("SchemaName");
            if (!string.IsNullOrEmpty(schemaName))
            {
                databaseName = $"{databaseName}_{schemaName}";
            }
            return new SQLiteDatabase(GetDatabaseDirectory().FullName, databaseName);
        }

        public override string GetDatabasePathFor(Type type, string info = null)
        {
            return GetDatabaseFor(type, info).DatabaseFile.FullName;
        }
        
        public override SQLiteDatabase GetDatabaseFor(Type objectType, string info = null)
        {
            return GetDatabaseFor(objectType, () => GetDatabaseDirectory().FullName, info);
        }

        public override SQLiteDatabase GetAppDatabaseFor(IApplicationNameProvider appNameProvider, object instance)
        {
            return GetDatabaseFor(instance.GetType(), () => GetAppDatabaseDirectory(appNameProvider).FullName);
        }

        public override SQLiteDatabase GetAppDatabaseFor(IApplicationNameProvider appNameProvider, Type objectType, string info = null)
        {
            return GetDatabaseFor(objectType, () => GetAppDatabaseDirectory(appNameProvider).FullName, info);
        }

        /// <summary>
        /// Get the path to the application specific SQLite database file for the specified type
        /// </summary>
        /// <param name="appNameProvider"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public override string GetAppDatabasePathFor(IApplicationNameProvider appNameProvider, Type type, string info = null)
        {
            return GetAppDatabaseFor(appNameProvider, type, info).DatabaseFile.FullName;
        }

        protected SQLiteDatabase GetDatabaseFor(Type objectType, Func<string> databasePathProvider, string info = null)
        {
            string connectionName = Dao.ConnectionName(objectType);
            string fileName = string.IsNullOrEmpty(info) ? (string.IsNullOrEmpty(connectionName) ? objectType.FullName : connectionName) : $"{objectType.FullName}_{info}";
            string directoryPath = databasePathProvider();
            SQLiteDatabase db = new SQLiteDatabase(directoryPath, fileName);
            Logger.Info("Returned SQLiteDatabase with path {0} for type {1}\r\nFullPath: {2}\r\nName: {3}", db.DatabaseFile.FullName, objectType.Name, directoryPath, fileName);
            return db;
        }
    }
}
