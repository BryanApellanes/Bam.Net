/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.ContinuousIntegration.Loggers
{
    public class ConsoleBuildLogger: BuildLogger<ConsoleLogger>
    {
        public ConsoleBuildLogger()
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props.Add("AddDetails", false);
            props.Add("UseColors", true);
            SetLoggerProperties(props);
        }
    }
}
