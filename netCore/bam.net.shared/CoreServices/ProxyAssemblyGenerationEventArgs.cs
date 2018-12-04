/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Reflection;

namespace Bam.Net.CoreServices
{
    public class ProxyAssemblyGenerationEventArgs
    {
        /// <summary>
        /// The Type of the service being generated or null 
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// The settings used to generate or null
        /// </summary>
        public ProxySettings ServiceSettings { get; set; }

        /// <summary>
        /// The MethodInfo being reported as non virtual or null
        /// </summary>
        public MethodInfo NonVirtualMethod { get; set; }

        public Assembly Assembly { get; set; }
    }
}
