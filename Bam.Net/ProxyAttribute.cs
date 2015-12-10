/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net
{
    /// <summary>
    /// Used to specify the name of the client proxy instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ProxyAttribute : Attribute
    {
        public ProxyAttribute() { }
        public ProxyAttribute(string var)
        {
            this.VarName = var;
            this.MethodCase = MethodCase.CamelCase;
        }

        /// <summary>
        /// The name of the client variable
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// Describes the client side method case
        /// </summary>
        public MethodCase MethodCase { get; set; }
    }
}
