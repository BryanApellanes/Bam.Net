using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestSuiteAttribute: Attribute
    {
        public string Title { get; set; }
    }
}
