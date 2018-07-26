/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net;
using Bam.Net.Javascript;
using Bam.Net.ServiceProxy;
using Bam.Net.Server;
//using Bam.Net.Presentation.Dust;
using Bam.Net.Web;
using Bam.Net.Data;
using Bam.Net.Logging;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
		static void Main(string[] args)
		{
            Resolver.Register();
			IsolateMethodCalls = false;

			Type type = typeof(Program);
			AddSwitches(typeof(Program));
            AddSwitches(typeof(BuildClient));
            AddSwitches(typeof(UserAdministrationActions));
            AddSwitches(typeof(UtilityActions));
			AddConfigurationSwitches();
            ArgumentAdder.AddArguments();
            
			Initialize(args);

			if (Arguments.Length > 0 && !Arguments.Contains("i"))
			{
                ExecuteSwitches(Arguments, type, false, Log.Default);
                ExecuteSwitches(Arguments, typeof(BuildClient), false, Log.Default);
                ExecuteSwitches(Arguments, typeof(UserAdministrationActions), false, Log.Default);
                ExecuteSwitches(Arguments, typeof(UtilityActions), false, Log.Default);
			}
			else
			{
				Interactive();
			}
		}
    }
}
