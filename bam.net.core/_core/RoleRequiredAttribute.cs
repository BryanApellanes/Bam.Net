/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleRequiredAttribute: Attribute
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
