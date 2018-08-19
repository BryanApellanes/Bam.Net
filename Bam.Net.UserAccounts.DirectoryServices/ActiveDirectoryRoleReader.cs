using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public class ActiveDirectoryRoleReader : IRoleReader
    {
        public ActiveDirectoryRoleReader(ActiveDirectoryReader reader)
        {
            ActiveDirectoryReader = reader;
        }

        protected ActiveDirectoryReader ActiveDirectoryReader { get; set; }

        protected Dictionary<string, string[]> UserGroups { get; set; }
        protected Dictionary<string, string[]> GroupUsers { get; set; }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (!GroupUsers.ContainsKey(roleName))
            {
                DirectoryEntry group = ActiveDirectoryReader.GetDirectoryEntry(roleName);
                if(group != null)
                {
                    GroupUsers.Add(roleName, ResultantGroupMemberResolver.ResolveMembers(group));
                }
                else
                {
                    GroupUsers.Add(roleName, new string[] { });
                }
            }
            return GroupUsers[roleName];
        }

        public string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
