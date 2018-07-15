using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class UserAccountsDatabase
    {
        public static implicit operator Database(UserAccountsDatabase db)
        {
            return db.Database;
        }

        public static implicit operator UserAccountsDatabase(Database db)
        {
            return new UserAccountsDatabase(db);
        }

        public UserAccountsDatabase(Database db)
        {
            Database = db;
        }

        public Database Database { get; }

        static UserAccountsDatabase _default;
        static object _defaultLock = new object();           
        public static UserAccountsDatabase Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, 
                    () => 
                    {
                        UserAccountsDatabase db = new UserAccountsDatabase(new SQLiteDatabase(RuntimeSettings.AppDataFolder, DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName()));
                        db.Database.TryEnsureSchema<UserAccounts.Data.User>();
                        return db;
                    });
            }
        }
    }
}
