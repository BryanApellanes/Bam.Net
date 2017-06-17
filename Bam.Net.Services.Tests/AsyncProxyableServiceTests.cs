using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.ServiceProxy.Tests;
using Bam.Net.Testing;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class AsyncProxyableServiceTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldBeAbleToUseInvokeAsync()
        {
            BamServer server;
            SecureServiceProxyClient<AsyncProxyableEcho> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetClient(out server, out sspc);
            ConsoleLogger logger = GetTestConsoleLogger();
            ProxyFactory serviceFactory = new ProxyFactory(".\\workspace_".RandomLetters(4), logger);
            string value = "this is a value: ".RandomString(8);
            bool? ran = false;
            AsyncCallbackService callbackService = new AsyncCallbackService(new AsyncCallback.Data.Dao.Repository.AsyncCallbackRepository(), new AppConf());
            AsyncProxyableEcho testObj = serviceFactory.GetProxy<AsyncProxyableEcho>("localhost", 8080);
            testObj.CallbackService = callbackService;
            AutoResetEvent blocker = new AutoResetEvent(false);
            testObj.InvokeAsync(r =>
            {
                ran = true;
                Expect.AreEqual(value, r.Result);
                blocker.Set();
            }, "Send", value);
            blocker.WaitOne();

            Expect.IsTrue(ran.Value);
        }

        [UnitTest]
        public void DnsHostnameTest()
        {
            string hostName = Dns.GetHostName();
            OutLine(hostName, ConsoleColor.Cyan);
            OutLine(Environment.MachineName, ConsoleColor.DarkCyan);
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            hostEntry.Aliases.Each(a => OutLineFormat("Alias: {0}", a));
            hostEntry.AddressList.Each(ip => OutLineFormat("Address: {0}, Family: {1}", ip.ToString(), ip.AddressFamily));
        }

        private static ConsoleLogger GetTestConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            return logger;
        }
    }
}
