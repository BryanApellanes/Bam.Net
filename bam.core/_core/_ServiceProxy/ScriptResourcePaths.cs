using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public class ScriptResourcePaths
    {
        /// <summary>
        /// Gets the resource prefix for any scripts that should be loaded be ResourceScripts.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public static string[] Value
        {
            get
            {
                return new string[]
                {
                    //"bam.net.fx._fx._Presentation._Dust."
                };
            }
        }
    }
}
