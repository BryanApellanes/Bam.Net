/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using System.Data.Common;

namespace Bam.Net.Data
{
    /// <summary>
    /// Resolves connection string requests to a sqlite database in the
    /// directory specified by the Directory property.
    /// </summary>
    public partial class SQLiteConnectionStringResolver: IConnectionStringResolver
    {
        public SQLiteConnectionStringResolver()
        {
            
        }

        static SQLiteConnectionStringResolver _instance;
        public static SQLiteConnectionStringResolver Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLiteConnectionStringResolver();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Register the default SQLiteConnectionStringResolver instance as 
        /// a ConnectionStringResolver with the ConnectionStringResolvers static
		/// class
        /// </summary>
        public static void Register()
        {
            ConnectionStringResolvers.AddResolver(Instance);
        }

        /// <summary>
        /// The diretory to create the database in
        /// </summary>
        public DirectoryInfo Directory
        {
            get;
            set;
        }

        #region IConnectionStringResolver Members

        public ConnectionStringSettings Resolve(string connectionName)
        {
			string dbFile = GetDatabaseFilePath(connectionName);

            ConnectionStringSettings s = new ConnectionStringSettings
            {
                Name = connectionName,
                ProviderName = SQLiteRegistrar.SQLiteFactoryAssemblyQualifiedName(),
                ConnectionString = string.Format("Data Source={0};Version=3;", dbFile)
            };
            FileInfo dbFileInfo = new FileInfo(dbFile);
            if (!dbFileInfo.Directory.Exists)
            {
                dbFileInfo.Directory.Create();
            }
            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new DbConnectionStringBuilder { ConnectionString = Resolve("Default")?.ConnectionString };
        }

		internal string GetDatabaseFilePath(string connectionName)
		{
			if (Directory == null)
			{
				Directory = DirectoryResolver();
			}
			string dbFile = Path.Combine(Directory.FullName, string.Format("{0}.sqlite", connectionName));
			return dbFile;
		}

        #endregion
    }
}
