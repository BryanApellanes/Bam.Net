using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.UserAccounts.Data;
using Bam.Net;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts
{
    [Proxy("roles")]
    [RoleRequired("Admin")]
    public class DaoRoleProvider : IRoleProvider
    {
        public DaoRoleProvider(Database userManagerDatabase)
        {
            Database = userManagerDatabase;
        }

        Database _database;
        protected internal Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Db.For<User>();
                }

                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public const string ErrorMessage = "DaoRoleProvider is not implemented on this platform (yet).  If you need this functionality please email bryan.apellanes@gmail.com to let him know.";

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public void CreateRole(string roleName)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public string[] GetAllRoles()
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException(ErrorMessage);
        }

        public bool RoleExists(string roleName)
        {
            throw new NotImplementedException(ErrorMessage);
        }
    }
}
