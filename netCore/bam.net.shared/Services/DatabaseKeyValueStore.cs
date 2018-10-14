using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class DatabaseKeyValueStore : IKeyValueStore
    {
        public const string DatabaseName = "KeyValueStore";

        public DatabaseKeyValueStore(Database db = null, ILogger logger = null)
        {
            SystemPaths = SystemPaths.Get(DefaultDataDirectoryProvider.Current);
            Logger = logger ?? Log.Default;
            Database = db ?? SQLiteDatabase.FromFile(Path.Combine(SystemPaths.Data.UserData, DatabaseName));
        }

        public DatabaseKeyValueStore(SystemPaths paths, ILogger logger = null)
        {
            SystemPaths = paths;
            Logger = logger ?? Log.Default;
            Database = SQLiteDatabase.FromFile(Path.Combine(SystemPaths.Data.UserData, DatabaseName));
        }

        public static DatabaseKeyValueStore ForApplication(ILogger logger = null)
        {
            SystemPaths paths = SystemPaths.Get(DefaultDataDirectoryProvider.Current);
            string dbPath = Path.Combine(paths.Data.AppData, DatabaseName);
            return new DatabaseKeyValueStore(SQLiteDatabase.FromFile(dbPath), logger);
        }

        public static DatabaseKeyValueStore ForUser(string userName, ILogger logger = null)
        {
            SystemPaths paths = SystemPaths.Get(DefaultDataDirectoryProvider.Current);
            string dbPath = Path.Combine(paths.Data.UserData, userName, DatabaseName);
            return new DatabaseKeyValueStore(SQLiteDatabase.FromFile(dbPath), logger);
        }

        public SystemPaths SystemPaths { get; }
        public Database Database { get; set; }
        public ILogger Logger { get; }

        public string Get(string key)
        {
            try
            {
                Data.KeyValuePair kvp = Data.KeyValuePair.FirstOneWhere(c => c.Key == key, Database);
                if (kvp != null)
                {
                    return kvp.Value;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception retrieving KeyValuePair from database, (db={0}): {1}", ex, Database.ToString(), ex.Message);
                return string.Empty;
            }
        }

        public bool Set(string key, string value)
        {
            try
            {
                Data.KeyValuePair kvp = new Data.KeyValuePair { Key = key, Value = value };
                kvp.Save(Database);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception setting KeyValuePair in database, (db={0}): {1}", ex, Database.ToString(), ex.Message);
                return false;
            }
        }
    }
}
