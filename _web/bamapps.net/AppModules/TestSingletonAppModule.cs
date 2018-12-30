using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.Web.AppModules
{
    [Singleton]
    public class TestSingletonAppModule
    {
        public TestSingletonAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}
