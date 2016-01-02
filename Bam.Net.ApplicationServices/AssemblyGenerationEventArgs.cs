/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ApplicationServices
{
    public class AssemblyGenerationEventArgs
    {
        /// <summary>
        /// The Type of the service being generated or null 
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// The settings used to generate or null
        /// </summary>
        public ServiceSettings ServiceSettings { get; set; }

        /// <summary>
        /// The MethodInfo being reported as non virtual or null
        /// </summary>
        public MethodInfo NonVirtualMethod { get; set; }
    }
}
