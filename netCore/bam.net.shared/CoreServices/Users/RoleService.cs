using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;
using Bam.Net.UserAccounts;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A service wrapper for any IRoleProvider implementation.
    /// Exposes the specified IRoleProvider as a service
    /// </summary>
    [Proxy("roleSvc")]
    [Encrypt]
    [ServiceSubdomain("role")]
    public class RoleService : ApplicationProxyableService, IRoleProvider
    {
        protected RoleService() { } // required for proxy gen
        public RoleService(IRoleProvider wrapped, AppConf conf)
        {
            AppConf = conf;
        }

        protected IRoleProvider RoleProvider { get; set; }

        public override object Clone()
        {
            RoleService clone = new RoleService(RoleProvider, AppConf);
            clone.CloneProperties(this);
            return clone;
        }

        public virtual void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            RoleProvider.AddUsersToRoles(usernames, roleNames);
        }

        public virtual void CreateRole(string roleName)
        {
            RoleProvider.CreateRole(roleName);
        }

        public virtual bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return RoleProvider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public virtual string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return RoleProvider.FindUsersInRole(roleName, usernameToMatch);
        }

        public virtual string[] GetAllRoles()
        {
            return RoleProvider.GetAllRoles();
        }

        public virtual string[] GetRolesForUser(string username)
        {
            return RoleProvider.GetRolesForUser(username);
        }

        public virtual string[] GetUsersInRole(string roleName)
        {
            return RoleProvider.GetUsersInRole(roleName);
        }

        public virtual bool IsUserInRole(string username, string roleName)
        {
            return RoleProvider.IsUserInRole(username, roleName);
        }

        public virtual void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            RoleProvider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public virtual bool RoleExists(string roleName)
        {
            return RoleProvider.RoleExists(roleName);
        }
    }
}
