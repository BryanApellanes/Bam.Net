using Bam.Net.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation
{
    public interface IHasCompiledTemplates
    {
        // extract these into a separate interface and obsolete
        string CombinedCompiledLayoutTemplates { get; }
        string CombinedCompiledTemplates { get; }
        IEnumerable<ICompiledTemplate> CompiledTemplates { get; }
        // --
    }
}
