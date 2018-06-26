using Bam.Net.Data;
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
    }
}
