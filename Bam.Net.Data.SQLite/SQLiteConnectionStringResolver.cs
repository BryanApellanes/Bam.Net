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
    public class SQLiteConnectionStringResolver: IConnectionStringResolver
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

        Func<DirectoryInfo> _directoryResolver;
        object _directoryResolverLock = new object();
        public Func<DirectoryInfo> DirectoryResolver
        {
            get
            {
                return _directoryResolverLock.DoubleCheckLock(ref _directoryResolver, () =>
                {
                    return () =>
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(".");
                        if (HttpContext.Current != null)
                        {
                            dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/App_Data"));
                        }

                        return dirInfo;
                    };
                });
            }
        }

        #region IConnectionStringResolver Members

        public ConnectionStringSettings Resolve(string connectionName)
        {
			string dbFile = GetDatabaseFilePath(connectionName);

            ConnectionStringSettings s = new ConnectionStringSettings();
			s.Name = connectionName;
            s.ProviderName = SQLiteRegistrar.SQLiteFactoryAssemblyQualifiedName();            
            s.ConnectionString = string.Format("Data Source={0};Version=3;", dbFile);

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
