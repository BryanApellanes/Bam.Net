using System;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.Data.Repositories
{
    public class DefaultDataSettingsProvider : DatabaseProvider<SQLiteDatabase>, IDataDirectoryProvider
    {
        public DefaultDataSettingsProvider()
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
            AssemblyDirectory = "Assemblies";
            ProcessMode = ProcessMode.Current;
            Logger = Log.Default;            
        }

        public DefaultDataSettingsProvider(ProcessMode processMode, ILogger logger = null):this()
        {
            ProcessMode = processMode;
            Logger = logger ?? Log.Default;
        }

        public static SystemPaths GetPaths()
        {
            return SystemPaths.Get(Instance);
        }

        public static DataPaths GetDataPaths()
        {
            return GetDataPaths(ProcessMode.Current);
        }

        public static DataPaths GetDataPaths(ProcessModes mode)
        {
            return GetDataPaths(ProcessMode.FromEnum(mode));
        }

        public static DataPaths GetDataPaths(ProcessMode mode)
        {
            return DataPaths.Get(new DefaultDataSettingsProvider(mode));
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
        public string AssemblyDirectory { get; set; }

        static DefaultDataSettingsProvider _default;
        static object _defaultLock = new object();
        public static DefaultDataSettingsProvider Instance
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DefaultDataSettingsProvider());
            }
        }

        static DefaultDataSettingsProvider _fromConfig;
        static object _fromConfigLock = new object();
        public static DefaultDataSettingsProvider Current
        {
            get
            {
                return _fromConfigLock.DoubleCheckLock(ref _fromConfig, () => new DefaultDataSettingsProvider(ProcessMode.Current));
            }
        }

        public void Init(UserManager userManager)
        {
            Init(DefaultConfigurationApplicationNameProvider.Instance, userManager);
        }

        public void Init(IApplicationNameProvider appNameProvider, UserManager userManager)
        {            
            SetRuntimeAppDataDirectory(appNameProvider);
            User.UserDatabase = userManager.Database;
            Vault.SystemVaultDatabase = Current.GetSysDatabaseFor(typeof(Vault), "System");
            Vault.ApplicationVaultDatabase = Current.GetAppDatabaseFor(appNameProvider, typeof(Vault), appNameProvider.GetApplicationName());
        }

        public void SetRuntimeAppDataDirectory()
        {
            SetRuntimeAppDataDirectory(DefaultConfigurationApplicationNameProvider.Instance);
        }

        public void SetRuntimeAppDataDirectory(IApplicationNameProvider appNameProvider)
        {
            RuntimeSettings.AppDataFolder = GetAppDataDirectory(appNameProvider).FullName;
        }

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

        public DirectoryInfo GetSysAssemblyDirectory()
        {
            return GetSysDataDirectory(AssemblyDirectory);
        }

        public DirectoryInfo GetAppAssemblyDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, AssemblyDirectory);
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

        public DirectoryInfo GetSysDatabaseDirectory()
        {
            return GetSysDataDirectory(DatabaseDirectory);
        }
        
        public DirectoryInfo GetAppRepositoryDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, RepositoryDirectory);
        }

        public DirectoryInfo GetAppRepositoryDirectory(IApplicationNameProvider appNameProvider, string subDirectory)
        {
            return new DirectoryInfo(Path.Combine(GetAppDataDirectory(appNameProvider).FullName, subDirectory));
        }

        public IRepository GetSysRepository()
        {
            return new ObjectRepository(GetSysRepositoryDirectory().FullName);
        }

        public DirectoryInfo GetSysRepositoryDirectory()
        {
            return GetSysDataDirectory(RepositoryDirectory);
        }

        public DirectoryInfo GetSysRepositoryDirectory(string subDirectory)
        {
            return new DirectoryInfo(Path.Combine(GetSysRepositoryDirectory().FullName, subDirectory));
        }

        public T GetSysDaoRepository<T>() where T: DaoRepository, new()
        {
            T result = new T();
            result.Database = GetSysDatabaseFor(result);
            result.EnsureDaoAssemblyAndSchema();
            return result;
        }

        public T GetAppDaoRepository<T>(IApplicationNameProvider applicationNameProvider) where T : DaoRepository, new()
        {
            T result = new T
            {
                Database = GetAppDatabaseFor(applicationNameProvider, typeof(T))
            };
            result.EnsureDaoAssemblyAndSchema();
            return result;
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
            return GetAppWorkspaceDirectory(appNameProvider, type.Name, hash);
        }

        public DirectoryInfo GetAppWorkspaceDirectory(IApplicationNameProvider appNameProvider, string workspaceName, string hash)
        {
            return new DirectoryInfo(Path.Combine(GetAppDataDirectory(appNameProvider).FullName, WorkspacesDirectory, workspaceName, hash));
        }

        public DirectoryInfo GetWorkspaceDirectory(Type type)
        {
            string hash = type.ToInfoHash();
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, WorkspacesDirectory, type.Name, hash));
        }

        public DaoRepository GetSysDaoRepository(ILogger logger = null, string schemaName = null)
        {
            return new DaoRepository(GetSysDatabaseFor(typeof(DaoRepository)), logger, schemaName);
        }
        
        public DirectoryInfo GetAppEmailTemplatesDirectory(IApplicationNameProvider appNameProvider)
        {
            return GetAppDataDirectory(appNameProvider, EmailTemplatesDirectory);
        }

        public DirectoryInfo GetSysEmailTemplatesDirectory()
        {
            return GetSysDataDirectory(EmailTemplatesDirectory);
        }

        public void SetSysDatabaseFor(object instance)
        {
            instance.Property("Database", GetSysDatabaseFor(instance), false);
        }        

        /// <summary>
        /// Get a SQLiteDatabase for the specified object instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override SQLiteDatabase GetSysDatabaseFor(object instance)
        {
            string databaseName = instance.GetType().FullName;
            string schemaName = instance.Property<string>("SchemaName", false);
            if (!string.IsNullOrEmpty(schemaName))
            {
                databaseName = $"{databaseName}_{schemaName}";
            }
            return new SQLiteDatabase(GetSysDatabaseDirectory().FullName, databaseName);
        }

        /// <summary>
        /// Get the standard path for the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public override string GetSysDatabasePathFor(Type type, string info = null)
        {
            return GetSysDatabaseFor(type, info).DatabaseFile.FullName;
        }
        
        public override SQLiteDatabase GetSysDatabaseFor(Type objectType, string info = null)
        {
            return GetDatabaseFor(objectType, () => GetSysDatabaseDirectory().FullName, info);
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

        public override SQLiteDatabase GetAppDatabase(IApplicationNameProvider appNameProvider, string databaseName)
        {
            return new SQLiteDatabase(GetAppDatabaseDirectory(appNameProvider).FullName, databaseName);
        }

        public override SQLiteDatabase GetSysDatabase(string databaseName)
        {
            return new SQLiteDatabase(GetSysDatabaseDirectory().FullName, databaseName);
        }
    }
}
