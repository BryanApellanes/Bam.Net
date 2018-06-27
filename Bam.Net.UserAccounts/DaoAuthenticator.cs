using Bam.Net.Data;
using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class DaoAuthenticator : IAuthenticator, IPasswordHashValidator
    {
        public DaoAuthenticator(UserAccountsDatabase db)
        {
            UserAccountsDatabase = db;
        }

        public DaoAuthenticator() : this(Db.For<User>()) { }

        public UserAccountsDatabase UserAccountsDatabase { get; }

        public bool IsPasswordHashValid(User user, string passwordHash)
        {
            return Password.Validate(user, passwordHash, UserAccountsDatabase);
        }

        public bool IsPasswordValid(string userName, string password)
        {
            return IsPasswordHashValid(userName, password.Sha1());
        }

        public bool IsPasswordHashValid(string userName, string passwordHash)
        {
            return IsPasswordHashValid(GetUser(userName), passwordHash);
        }
        
        protected User GetUser(string userName)
        {
            User user = null;
            if (userName.Contains("@"))
            {
                user = User.GetByEmail(userName, UserAccountsDatabase);
            }
            else
            {
                user = User.GetByUserName(userName, UserAccountsDatabase);
            }

            return user;
        }

    }
}
