/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts.Data;
using System.Web.Security;

namespace Bam.Net.UserAccounts
{
    public class DaoPrincipal: IPrincipal
    {
        public DaoPrincipal(User user, bool isAuthenticated)
        {
            this.Identity = new DaoIdentity(user, isAuthenticated);
            this.RoleProvider = DaoRoleProvider.Default;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public RoleProvider RoleProvider
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            return RoleProvider.IsUserInRole(Identity.Name, role);
        }
    }
}
