using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Server.WindowsServices
{
    public class PollingService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("PollingService", "Polling Service", "Polling service agent");
            }
        }

        bool _continuePolling;
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Start();
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);
                _continuePolling = true;
                Task.Run(() => TryPoll());
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting polling service: {0}", ex, ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                Log.AddEntry("{0} stopping", ServiceInfo.ServiceName);
                _continuePolling = false;
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error stopping {0}: {1}", LogEventType.Warning, ServiceInfo.ServiceName, ex.Message);
            }
        }

        protected void TryPoll()
        {
            while (_continuePolling)
            {
                try
                {
                    Log.AddEntry("polling");
                    Thread.Sleep(DefaultConfiguration.GetAppSetting("PollingIntervalMilliseconds", "3000").ToInt(3000));
                }
                catch (Exception ex)
                {
                    Log.AddEntry("Exception in polling service: {0}", ex, ex.Message);
                }
            }
        }
    }
}
