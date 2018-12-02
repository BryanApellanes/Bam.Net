using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.Web.AppModules
{
    [Transient]
    public class TestTransientAppModule
    {
        public TestTransientAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}
