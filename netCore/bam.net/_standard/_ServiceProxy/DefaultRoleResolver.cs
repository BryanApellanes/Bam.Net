/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public partial class DefaultRoleResolver : IRoleResolver
    {
        static DefaultRoleResolver()
        {
        }

        public static IRoleProvider RoleProvider { get; set; }

        [Exclude]
        public object Clone()
        {
            DefaultRoleResolver clone = new DefaultRoleResolver();
            clone.CopyProperties(this);
            return clone;
        }

        public bool IsInRole(IUserResolver userResolver, string roleName)
        {
            string userName = userResolver.GetCurrentUser();
            return RoleProvider.IsUserInRole(userName, roleName);
        }

        public string[] GetRoles(IUserResolver userResolver)
        {
            Args.ThrowIfNull(RoleProvider, $"{nameof(DefaultRoleResolver)}.{nameof(RoleProvider)}");

            return RoleProvider.GetRolesForUser(userResolver.GetCurrentUser());
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
