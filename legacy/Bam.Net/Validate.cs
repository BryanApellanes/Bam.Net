/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net
{
    /// <summary>
    /// Symantic convenience class
    /// </summary>
    public class Validate
    {
        /// <summary>
        /// Checks the properties named in the RequiredProperties property of the specified
        /// IHasRequiredProperties object ensuring that each has been set. This method will 
        /// throw an error if any required property is null or an empty string.
        /// </summary>
        /// <param name="toBeValidated">The IHasRequiredProperties implementation to check.</param>
        public static void RequiredProperties(IHasRequiredProperties toBeValidated)
        {
            DefaultConfiguration.CheckRequiredProperties(toBeValidated);
        }
    }
}
