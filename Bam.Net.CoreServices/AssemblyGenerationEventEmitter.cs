/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// The base class to be implemented by 
    /// any class that generates an assembly and 
    /// emits events pertaining to the assembly
    /// generation process.
    /// </summary>
    public abstract class AssemblyGenerationEventEmitter
    {
        public event EventHandler<AssemblyGenerationEventArgs> AssemblyGenerating;
        public event EventHandler<AssemblyGenerationEventArgs> AssemblyGenerated;
        public event EventHandler<AssemblyGenerationEventArgs> MethodWarning;
        protected void OnAssemblyGenerating(AssemblyGenerationEventArgs args)
        {
            if (AssemblyGenerating != null)
            {
                AssemblyGenerating(this, args);
            }
        }
        protected void OnAssemblyGenerated(AssemblyGenerationEventArgs args)
        {
            if (AssemblyGenerated != null)
            {
                AssemblyGenerated(this, args);
            }
        }
        protected void OnMethodWarning(AssemblyGenerationEventArgs args)
        {
            if (MethodWarning != null)
            {
                MethodWarning(this, args);
            }
        }
    }
}
