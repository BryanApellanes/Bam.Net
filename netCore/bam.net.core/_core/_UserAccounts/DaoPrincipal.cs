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

namespace Bam.Net.UserAccounts
{
    public class DaoPrincipal: IPrincipal
    {
        public DaoPrincipal(User user, bool isAuthenticated)
        {
            Identity = new DaoIdentity(user, isAuthenticated);
            
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public IRoleReader RoleReader { get; set; }

        public bool IsInRole(string role)
        {
            return RoleReader.IsUserInRole(Identity.Name, role);
        }
    }
}
