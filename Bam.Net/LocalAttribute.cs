using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    /// <summary>
    /// Used to denote a method that will not be 
    /// proxied and will execute locally.  The same 
    /// as Exclude, symantically different
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class LocalAttribute: ExcludeAttribute
    {
    }
}
