/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// The base class to be implemented by 
    /// any class that generates a proxy assembly and 
    /// emits events pertaining to the assembly
    /// generation process.
    /// </summary>
    public abstract class ProxyAssemblyGenerationEventSource
    {
        public event EventHandler<ProxyAssemblyGenerationEventArgs> AssemblyGenerating;
        public event EventHandler<ProxyAssemblyGenerationEventArgs> AssemblyGenerated;
        public event EventHandler<ProxyAssemblyGenerationEventArgs> MethodWarning;
        protected void OnAssemblyGenerating(ProxyAssemblyGenerationEventArgs args)
        {
            if (AssemblyGenerating != null)
            {
                AssemblyGenerating(this, args);
            }
        }
        protected void OnAssemblyGenerated(ProxyAssemblyGenerationEventArgs args)
        {
            if (AssemblyGenerated != null)
            {
                AssemblyGenerated(this, args);
            }
        }
        protected void OnMethodWarning(ProxyAssemblyGenerationEventArgs args)
        {
            if (MethodWarning != null)
            {
                MethodWarning(this, args);
            }
        }
    }
}
