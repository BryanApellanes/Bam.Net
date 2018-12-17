using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

namespace Bam.Net.Configuration
{
    public partial class ConfigurationResolver
    {
        public static ConfigurationResolver Current
        {
            get;
            set;
        }
        // TODO: add configurationService
    }
}
