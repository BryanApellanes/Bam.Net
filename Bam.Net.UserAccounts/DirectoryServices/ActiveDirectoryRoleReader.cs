using System;
using System.Collections.Generic;
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
            GroupToRoleMap = new Dictionary<string, string>();
        }

        public Dictionary<string, string> GroupToRoleMap { get; set; }
        protected ActiveDirectoryReader ActiveDirectoryReader { get; set; }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
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

        public void MapGroupToRole(string group, string role)
        {
            if (!GroupToRoleMap.ContainsKey(group))
            {
                GroupToRoleMap.Add(group, role);
            }
            else
            {
                GroupToRoleMap[group] = role;
            }
        } 
    }
}
