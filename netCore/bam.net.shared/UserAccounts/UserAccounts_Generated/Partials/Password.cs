/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Encryption;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Bam.Net.UserAccounts.Data
{
    public partial class Password
    {
        protected internal const string HashSaltFormat = "{0}::{1}";

        public static Password Set(string userName, string password, Database db = null)
        {
            User user = User.GetByUserNameOrDie(userName, db);
            return Set(user, password);
        }

        /// <summary>
        /// Saves the hash of the specified password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Password Set(User user, string password, Database db = null)
        {

            Password passwordEntry = user.PasswordsByUserId.FirstOrDefault();//Password.OneWhere(c => c.UserId == user.Id);
            if (passwordEntry == null)
            {
                passwordEntry = user.PasswordsByUserId.AddNew();
            }
            
            passwordEntry.Value = password.Sha1();
            passwordEntry.Save(db);
            return passwordEntry;
        }

        public static string Get(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);

            return Get(user);
        }

        public static string Get(User user)
        {
            Password password = user.PasswordsByUserId.FirstOrDefault();
            string result = string.Empty;
            if (password != null)
            {
                result = Aes.Decrypt(password.Value);
            }

            return result;
        }

        public static bool Validate(string userName, string password, bool updateFailure = true)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Validate(user, password, updateFailure: updateFailure);
        }

        public static bool Validate(string userName, string password, Database db = null, bool updateFailure = true)
        {
            User user = User.GetByUserName(userName, db);
            return Validate(user, password, db, updateFailure);
        }

        public static bool Validate(User user, string password, Database db = null, bool updateFailure = true)
        {
            Password passwordEntry = user.PasswordsByUserId.FirstOrDefault();
            bool result = false;
            if (passwordEntry != null)
            {
                result = passwordEntry.Value.Equals(password.Sha1());
            }

            if (!result && updateFailure)
            {
                PasswordFailure.Add(user.UserName, db);
            }
            return result;
        }
    }
}
