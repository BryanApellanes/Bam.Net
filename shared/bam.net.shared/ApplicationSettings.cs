using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class ApplicationSettings
    {
        public string OrganizationName { get; set; }
        public string ApplicationName { get; set; }
        public SystemPaths Paths { get; set; }
        public Dictionary<string, string> Configuration { get; set; }        
    }
}
