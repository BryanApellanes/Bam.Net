using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    /// <summary>
    /// A serializable representation of a user.
    /// </summary>
    [Serializable]
    public partial class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Uuid { get; set; }

        /// <summary>
        /// Converts this UserInfo to a User instance. 
        /// This method will not load the User instance
        /// from a database, to do so use Load() instead.
        /// </summary>
        /// <returns></returns>
        public User ToUser()
        {
            return new User
            {
                UserName = UserName,
                Uuid = Uuid,
                Email = Email
            };
        }

        /// <summary>
        /// Loads the specified database.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <returns></returns>
        public User Load(UserAccountsDatabase database)
        {
            if (!string.IsNullOrEmpty(Uuid))
            {
                return User.FirstOneWhere(u => u.Uuid == Uuid, database);
            }
            if (!string.IsNullOrEmpty(Email))
            {
                return User.FirstOneWhere(u => u.Email == Email, database);
            }
            return User.FirstOneWhere(u => u.UserName == UserName, database);
        }
    }
}
