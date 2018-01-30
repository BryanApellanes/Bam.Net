using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.Example
{
    class DocumentationExampleClass
    {
        /// <summary>
        /// A method that takes a string and returns a string.
        /// </summary>
        /// <param name="aValue">Any string value</param>
        /// <returns>The value passed in.</returns>
        public string MyDocumentedMethod(string aValue)
        {
            return aValue;
        }
    }
}
