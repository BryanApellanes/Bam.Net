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

namespace gloo
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            IsolateMethodCalls = false;
            BamResolver.Register();
            AddSwitches(typeof(ConsoleActions));
            GlooService.SetInfo(GlooService.ServiceInfo);
            if (!GlooService.ProcessCommandLineArgs(args))
            {
                Initialize(args, (a) =>
                {
                    OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                    Environment.Exit(1);
                });
                if (Arguments.Contains("i"))
                {
                    Interactive();
                }
                else if(!ExecuteSwitches(Arguments, new ConsoleActions()))
                {
                    GlooService.RunService<GlooService>();
                }
            }            
        }       
    }
}
