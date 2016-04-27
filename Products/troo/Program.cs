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
using Bam.Net.Server.Tvg;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using Bam.Net.Server;
using System.Threading;

namespace troo
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            TrooService.SetInfo(TrooService.ServiceInfo);
            if (!TrooService.ProcessCommandLineArgs(args))
            {
                IsolateMethodCalls = false;             
                AddSwitches(typeof(UtilityActions));
                AddConfigurationSwitches();
                ArgumentAdder.AddArguments(args);

                Initialize(args, (a) =>
                {
                    OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                    Environment.Exit(1);
                });

                if (Arguments.Length > 0 && !Arguments.Contains("i"))
                {
                    ExecuteSwitches(Arguments, typeof(UtilityActions));
                }
                else if (Arguments.Contains("i"))
                {
                    Interactive();
                }
                else
                {
                    TrooService.RunService<TrooService>();
                }
            }
        }
    }
}
