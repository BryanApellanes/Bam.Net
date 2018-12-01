using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.Web.AppModules
{
    public class TestAppModule
    {
        public TestAppModule()
        {

        }

        public string GetTestString(string input)
        {
            return $"Test: {input}";
        }
    }
}
