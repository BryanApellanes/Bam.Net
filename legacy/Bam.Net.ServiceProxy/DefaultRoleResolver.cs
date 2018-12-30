/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Bam.Net.ServiceProxy
{
    public class DefaultRoleResolver: IRoleResolver
    {
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
            return Roles.IsUserInRole(userName, roleName);
        }

        public string[] GetRoles(IUserResolver userResolver)
        {
            return Roles.GetRolesForUser(userResolver.GetCurrentUser());
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
