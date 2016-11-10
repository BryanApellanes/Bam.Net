using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class StaticApplicationNameProvider : IApplicationNameProvider
    {
        public StaticApplicationNameProvider(string applicationName)
        {
            ApplicationName = applicationName;
        }
        public string ApplicationName { get; set; }
        public string GetApplicationName()
        {
            return ApplicationName;
        }
    }
}
