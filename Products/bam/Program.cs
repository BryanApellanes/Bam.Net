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
using Bam.Net.Dust;
using Bam.Net.Web;
using Bam.Net.Data;
using dotless.Core;

namespace bam
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
            AddSwitches(typeof(UtilityActions));
            AddSwitches(typeof(BuildActions));
            AddSwitches(typeof(VaultActions));
            AddSwitches(typeof(GlooActions));
            AddSwitches(typeof(ManagementActions));
			AddConfigurationSwitches();
            ArgumentAdder.AddArguments();

			DefaultMethod = type.GetMethod("Interactive");

			Initialize(args);

			if (Arguments.Length > 0 && !Arguments.Contains("i"))
			{
				ExecuteSwitches(Arguments, type);
                ExecuteSwitches(Arguments, typeof(UtilityActions));
                ExecuteSwitches(Arguments, typeof(BuildActions));
                ExecuteSwitches(Arguments, typeof(VaultActions));
                ExecuteSwitches(Arguments, typeof(GlooActions));
                ExecuteSwitches(Arguments, typeof(ManagementActions));
			}
			else
			{
				Interactive();
			}
		}
    }
}
