using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Bam.Net.Application
{
    public class HeartService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("HeartService", "Heart Service", "Provides core application functionality, the heart of the system.");
            }
        }
        
        protected override void OnStart(string[] args)
        {
            ConsoleActions.StartServer();
        }

        protected override void OnStop()
        {
            ConsoleActions.StopServer();
            Thread.Sleep(1000);
        }
        
    }
}
