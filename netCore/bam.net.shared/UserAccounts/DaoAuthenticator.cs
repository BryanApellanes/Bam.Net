using Bam.Net.Data;
using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class DaoAuthenticator : IAuthenticator
    {
        public DaoAuthenticator(UserAccountsDatabase db)
        {
            UserAccountsDatabase = db;
        }

        public DaoAuthenticator() : this(Db.For<User>()) { }

        public UserAccountsDatabase UserAccountsDatabase { get; }

        public bool IsPasswordValid(string userName, string password)
        {
            return Password.Validate(userName, password, UserAccountsDatabase);
        }
    }
}
