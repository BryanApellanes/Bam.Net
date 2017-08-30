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
            DataRootDirectory = "C:\\BamData";
            DatabaseDirectory = "Databases";
            RepositoryDirectory = "Repositories";
            FilesDirectory = "Files";
            WorkspacesDirectory = "Workspaces";
            EmailTemplatesDirectory = "EmailTemplates";
            Logger = Log.Default;
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

        public string DataRootDirectory { get; set; }
        public string DatabaseDirectory { get; set; }
        public string RepositoryDirectory { get; set; }
        public string FilesDirectory { get; set; }
        public string WorkspacesDirectory { get; set; }
        public string EmailTemplatesDirectory { get; set; }

        public DirectoryInfo GetRootDataDirectory()
        {
            return new DirectoryInfo(DataRootDirectory);
        }

        public DirectoryInfo GetDatabaseDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, DatabaseDirectory));
        }

        public DirectoryInfo GetRepositoryDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, RepositoryDirectory));
        }

        public DirectoryInfo GetRepositoryWorkspaceDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRepositoryDirectory().FullName, "RepoWorkspaces"));
        }

        public DirectoryInfo GetFilesDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, FilesDirectory));
        }

        public DirectoryInfo GetWorkspaceDirectory(Type type)
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, WorkspacesDirectory, type.Name));
        }

        public DaoRepository GetGenericDaoRepository(ILogger logger = null, string schemaName = null)
        {
            return new DaoRepository(GetDatabaseFor(typeof(DaoRepository)), logger, schemaName);
        }

        public DirectoryInfo GetEmailTemplatesDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRootDataDirectory().FullName, EmailTemplatesDirectory));
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
            string name = string.IsNullOrEmpty(info) ? objectType.FullName : $"{objectType.FullName}_{info}";
            string fullPath = GetDatabaseDirectory().FullName;
            SQLiteDatabase db = new SQLiteDatabase(fullPath, name);
            Logger.Info("Returned SQLiteDatabase with path {0} for type {1}\r\nFullPath: {2}\r\nName: {3}", db.DatabaseFile.FullName, objectType.Name, fullPath, name);
            return db;
        }
    }
}
