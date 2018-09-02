/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using System.IO;
using System.Data.SQLite;
using System.Data.Common;
using System.Diagnostics;

namespace Bam.Net.Data.SQLite
{
    /// <summary>
    /// A SQLite database
    /// </summary>
    public class SQLiteDatabase : Database, IHasConnectionStringResolver
    {
        public const string DefaultFolder = ".\\SQLiteDbFiles";

        static SQLiteDatabase()
        {
        }

        public SQLiteDatabase() : this(DefaultFolder, "SQLiteDatabase")
        {
        }

        public SQLiteDatabase(string connectionName) : this(".\\SQLiteDbFiles", connectionName)
        {
        }

        /// <summary>
        /// Instantiate a new SQLiteDatabase instance where the database
        /// file will be placed into the specified directoryPath using the
        /// specified connectionName as the file name
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="connectionName"></param>
        public SQLiteDatabase(string directoryPath, string connectionName)
            : base()
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                directory.Create();
            }
            ConnectionStringResolver = new SQLiteConnectionStringResolver
            {
                Directory = directory
            };

            ConnectionName = connectionName;
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(SQLiteFactory.Instance);
            Register();
        }
        
        private void Register()
        {
            SQLiteRegistrar.Register(this);
            Infos.Add(new DatabaseInfo(this));
        }

        public IConnectionStringResolver ConnectionStringResolver
        {
            get;
            set;
        }

        string _connectionString;
        public override string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConnectionStringResolver.Resolve(ConnectionName).ConnectionString;
                }

                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        FileInfo _databaseFile;
        public FileInfo DatabaseFile
        {
            get
            {
                if (_databaseFile == null)
                {
                    ConnectionStringResolver.IsInstanceOfType<SQLiteConnectionStringResolver>("ConnectionStringResolver was not of the expected SQLiteConnectionStringResolver type");
                    _databaseFile = new FileInfo(((SQLiteConnectionStringResolver)ConnectionStringResolver).GetDatabaseFilePath(ConnectionName));
                }

                return _databaseFile;
            }
        }

        public bool GCOnRelease { get; set; }

        public override void ReleaseConnection(DbConnection conn)
        {
            ReleaseConnection(conn, GCOnRelease);
        }

        public void ReleaseConnection(DbConnection conn, bool gcCollect)
        {
            base.ReleaseConnection(conn);
            if (gcCollect)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public void ReleaseAllConnections()
        {
            lock (connectionLock)
            {
                foreach (DbConnection conn in Connections)
                {
                    try
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Exception releasing database connection: {ex.Message}");
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static SQLiteDatabase FromFile(string filePath)
        {
            return FromFile(new FileInfo(filePath));
        }

        public static SQLiteDatabase FromFile(FileInfo sqliteDatabaseFile)
        {
            string sqliteDatabaseFilePath = sqliteDatabaseFile.FullName;
            return new SQLiteDatabase
            {
                ConnectionString = string.Format("Data Source={0};Version=3;", sqliteDatabaseFilePath)
            };
        }
    }
}
