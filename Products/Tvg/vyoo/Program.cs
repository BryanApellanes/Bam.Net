using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using Bam.Net.Server;
using System.Threading;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            if (!VyooService.ProcessCommandLineArgs(args))
            {
                IsolateMethodCalls = false;
                Resolver.Register();
                AddSwitches(typeof(ConsoleActions));
                VyooService.SetInfo(VyooService.ServiceInfo);

                Initialize(args, (a) =>
                {
                    OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                    Environment.Exit(1);
                });
                if (Arguments.Contains("i"))
                {
                    Interactive();
                }
                else
                {
                    VyooService.RunService<VyooService>();
                }
            }
        }
    }
}
