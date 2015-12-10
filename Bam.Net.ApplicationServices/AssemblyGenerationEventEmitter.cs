/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ApplicationServices
{
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
