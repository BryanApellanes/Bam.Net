/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Security;

namespace Bam.Net
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleRequiredAttribute
    {
        public RoleRequiredAttribute(string redirectTo, params string[] roles)
        {
            this.Roles = roles;
            this.RedirectTo = redirectTo;
        }

        public string[] Roles { get; set; }
        public string RedirectTo { get; set; }
    }
}
