/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Configuration;
using System.IO;
using Bam.Net.Yaml;
using Bam.Net.Testing;
using System.Reflection;
using Bam.Net.Server;

namespace Bam.Net.Application
{
    class Program : ServiceExe
    {
        static void Main(string[] args)
        {
            CommandLineInterface.EnsureAdminRights();

            SetInfo(new ServiceInfo("BamDaemon", "Bam Daemon", "Bam http application server"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Program>();
            }
        }

        protected override void OnStart(string[] args)
        {
            Server.Start();
        }

        protected override void OnStop()
        {
            Server.Stop();
            Thread.Sleep(1000);
        }

        static BamServer _server;
        static object _serverLock = new object();
        public static BamServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    BamConf conf = BamConf.Load();
                    return new BamServer(conf);
                });
            }
        }

    }
}
