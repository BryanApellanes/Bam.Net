/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class DaoIdentity: IIdentity
    {
        public DaoIdentity() { }
        public DaoIdentity(User user, bool isAuthenticated)
            : this(user.UserName, isAuthenticated)
        { }

        public DaoIdentity(string name, bool isAuthenticated)
        {
            this.Name = name;
            this.IsAuthenticated = isAuthenticated;
        }

        public string AuthenticationType
        {
            get { return "DaoUsers"; }
        }

        public bool IsAuthenticated
        {
            get;
            internal protected set;
        }

        public string Name
        {
            get;
            internal protected set;
        }
    }
}
