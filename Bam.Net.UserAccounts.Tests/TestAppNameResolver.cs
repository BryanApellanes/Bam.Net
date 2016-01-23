using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.Tests
{

    public class TestAppNameResolver : IApplicationNameProvider
    {
        public string GetApplicationName()
        {
            return "test";
        }
    }
}
