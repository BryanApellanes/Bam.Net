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
using Bam.Net.Testing;
using System.Reflection;
using Bam.Net.Services.Chunking;

namespace Bam.Net.Server
{
    class Chunker : ServiceExe
    {
        static void Main(string[] args)
        {
            CommandLineInterface.EnsureAdminRights();

            SetInfo(new ServiceInfo("BamFileChunker", "Bam File Chunker", "Bam File Chunker: chunks files and serves chunks"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Chunker>();
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

        static ChunkServer _server;
        static object _serverLock = new object();
        public static ChunkServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () => new ChunkServer());
            }
        }

    }
}
