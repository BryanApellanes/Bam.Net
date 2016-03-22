using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class DaoRoleResolver : IRoleResolver
    {
        public IHttpContext HttpContext
        {
            get; set;
        }

        public string[] GetRoles(IUserResolver userResolver)
        {
            User user = User.OneWhere(c => c.UserName == userResolver.GetCurrentUser());
            List<string> results = new List<string>();
            if(user != null)
            {
                results.AddRange(user.Roles.Select(r => r.Name));
            }

            return results.ToArray();
        }

        public bool IsInRole(IUserResolver userResolver, string roleName)
        {
            return new List<string>(GetRoles(userResolver)).Contains(roleName);
        }
    }
}
