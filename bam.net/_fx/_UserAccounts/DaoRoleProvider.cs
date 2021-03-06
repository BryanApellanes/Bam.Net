/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.UserAccounts.Data;
using Bam.Net;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts
{
    [Proxy("roles")]
    [RoleRequired("Admin")]
    public class DaoRoleProvider : RoleProvider, IRoleProvider
    {
        public DaoRoleProvider() { }
        public DaoRoleProvider(Database source)
        {
            this.Database = source;
        }

        static DaoRoleProvider _default;
        static object _defaultLock = new object();
        public static DaoRoleProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DaoRoleProvider());
            }
            set
            {
                _default = value;
            }
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

        /// <summary>
        /// Initialize roles from the config file.  This will look for the 
        /// appSetting with the key "Roles" and assume that it is a
        /// semi-colon (;) separated list of key value pairs delimited by colons (:)
        /// where the key is the name of a role to initialize and the 
        /// value is a comma separated list of users to create and add to 
        /// the role.
        /// </summary>
        public static void InitializeFromConfig()
        {
            string[] roleNameToUserNames = DefaultConfiguration.GetAppSetting("Roles", "").DelimitSplit(";", true);
            DaoRoleProvider provider = new DaoRoleProvider();
            roleNameToUserNames.Each(keyValues =>
            {
                string[] split = keyValues.DelimitSplit(":", true);
                Expect.IsTrue(split.Length <= 2, "Unrecognized Role config value.");
                string role = split[0];
                string[] userNames = split.Length == 2 ? split[1].DelimitSplit(",", true) : new string[] { };
                provider.CreateRole(role);

                if (userNames.Length > 0)
                {
                    List<string> usersToAddToRole = new List<string>();
                    userNames.Each(userName =>
                    {
                        User.Ensure(userName);
                        usersToAddToRole.Add(userName);
                    });
                    if (usersToAddToRole.Count > 0)
                    {
                        provider.AddUsersToRoles(usersToAddToRole.ToArray(), new string[] { role });
                    }
                }
            });
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            UserCollection users = User.Where(c => c.UserName.In(usernames), Database);
            RoleCollection roles = Role.Where(c => c.Name.In(roleNames), Database);

            SqlStringBuilder sql = Database.GetSqlStringBuilder();
            for (int i = 0; i < users.Count; i++)
            {
                User currentUser = users[i];
                for (int ii = 0; ii < roles.Count; ii++)
                {
                    Role currentRole = roles[ii];
                    currentUser.Roles.Add(currentRole);
                }
                currentUser.Roles.WriteCommit(sql, Database);
            }

            sql.Execute(Database);
        }

        IApplicationNameProvider _applicationNameProvider;
        object _applicationNameProviderLock = new object();
        public IApplicationNameProvider ApplicationNameProvider
        {
            get
            {
                return _applicationNameProviderLock.DoubleCheckLock(ref _applicationNameProvider, () => DefaultConfigurationApplicationNameProvider.Instance);
            }
            set
            {
                _applicationNameProvider = value;
                _applicationName = _applicationNameProvider.GetApplicationName();
            }
        }

        string _applicationName;
        public override string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                {
                    _applicationName = ApplicationNameProvider.GetApplicationName();
                }

                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName, Database);
            if (role == null)
            {
                role = new Role();
                role.Name = roleName;
                role.Save(Database);
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            Role role = Role.OneWhere(c => c.Name == roleName, Database);
            bool result = false;
            if (role != null)
            {
                if (throwOnPopulatedRole && role.Users.Count > 0) 
				{
	                string[] userNames = role.Users.Select(u => u.UserName).ToArray();
                    throw new InvalidOperationException("({0}) Role is populated:\r\n{1}"._Format(roleName, userNames.ToDelimited(u => u, ", ")));
                }
                else
                {
                    SqlStringBuilder sql = Database.GetSqlStringBuilder();
                   
                    // deleting the role directly will cause the framework to attempt
                    // to delete the users as well since the relationship is an Xref.
                    // Doing it this way will prevent the deletion of the users.
                    UserRoleCollection xrefs = UserRole.Where(c => c.RoleId == role.Id, Database);
                    xrefs.WriteDelete(sql);
                    role.WriteDelete(sql);
                    sql.Execute(Database);
                    result = true;
                }
            }
            return result;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            Role role = Role.OneWhere(c => c.Name == roleName, Database);
            string[] results = new string[] { };
            if (role != null)
            {
                results = role.Users
                    .Where(u => u.UserName.ToLowerInvariant().Equals(usernameToMatch.ToLowerInvariant()))
                    .Select(u => u.UserName).ToArray();
            }

            return results;
        }

        public override string[] GetAllRoles()
        {
            return Role.Where(c => c.Id != null, Database).Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            User user = User.GetByUserName(username, Database);
            string[] results = new string[] { };
            if (user != null)
            {
                results = user.Roles.Select(r => r.Name).ToArray();
            }

            return results;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName, Database);
            string[] results = new string[] { };
            if (role != null)
            {
                results = role.Users.Select(u => u.UserName).ToArray();
            }

            return results;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            User user = User.GetByUserName(username, Database);
            bool result = false;
            if (user != null)
            {
                Role role = Role.OneWhere(c => c.Name == roleName, Database);
                if (role != null)
                {
                    UserRole xref = UserRole.OneWhere(c => c.UserId == user.Id && c.RoleId == role.Id, Database);
                    result = xref != null;
                }
            }

            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            RoleCollection roles = Role.Where(c => c.Name.In(roleNames), Database);
            UserCollection users = User.Where(c => c.UserName.In(usernames), Database);

            ulong[] roleIds = roles.Select(r => r.Id.Value).ToArray();
            ulong[] userIds = users.Select(u => u.Id.Value).ToArray();
            UserRoleCollection xrefs = UserRole.Where(c => c.RoleId.In(roleIds) && c.UserId.In(userIds), Database);
            xrefs.Delete(Database);            
        }

        public override bool RoleExists(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName, Database);
            return role != null;
        }
    }
}
