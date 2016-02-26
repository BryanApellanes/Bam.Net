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
using Ionic.Zip;
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
			IsolateMethodCalls = false;

			Type type = typeof(Program);
			AddSwitches(typeof(Program));
            AddSwitches(typeof(VaultActions));
            AddSwitches(typeof(GlooActions));
			AddConfigurationSwitches();

			AddValidArgument("root", false, "The root directory to pack files from");
			AddValidArgument("saveTo", false, "The zip file to create when packing the toolkit");
			AddValidArgument("appName", false, "The name of the app to create when calling /ca (create app)");

            AddValidArgument("vp", false, "The path to a vault to read; used with /rv");
            AddValidArgument("vaultpath", false, "Same as /vp");
            AddValidArgument("vn", false, "The name of the vault to read; used with /rv");
            AddValidArgument("vaultname", false, "Same as /vn");
            
            AddValidArgument("host", false, "The hostname to download gloo client from; used with /gloo");
            AddValidArgument("port", false, "The port to download gloo client from; used with /gloo");

			DefaultMethod = type.GetMethod("Interactive");

			Initialize(args);

			if (Arguments.Length > 0 && !Arguments.Contains("i"))
			{
				ExecuteSwitches(Arguments, type);
                ExecuteSwitches(Arguments, typeof(VaultActions));
                ExecuteSwitches(Arguments, typeof(GlooActions));
			}
			else
			{
				Interactive();
			}
		}
    }
}
