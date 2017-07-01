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
            DataDirectory = "C:\\BamData";
            DatabaseDirectory = "Databases";
            RepositoryDirectory = "Repository";
            FilesDirectory = "Files";
            Logger = Log.Default;
        }        
        public string DataDirectory { get; set; }
        public string DatabaseDirectory { get; set; }
        public string RepositoryDirectory { get; set; }
        public string FilesDirectory { get; set; }

        public DirectoryInfo GetDataDirectory()
        {
            return new DirectoryInfo(DataDirectory);
        }

        public DirectoryInfo GetDatabaseDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetDataDirectory().FullName, DatabaseDirectory));
        }

        public DirectoryInfo GetRepositoryDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetDataDirectory().FullName, RepositoryDirectory));
        }

        public DirectoryInfo GetRepositoryWorkspaceDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetRepositoryDirectory().FullName, "Workspaces"));
        }

        public DirectoryInfo GetFilesDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetDataDirectory().FullName, FilesDirectory));
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
    }
}
