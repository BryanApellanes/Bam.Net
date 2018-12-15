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
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.ServiceProxy.Tests;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class AsyncProxyableServiceTests : CommandLineTestInterface
    {
        static AsyncProxyableServiceTests()
        {
            AppDomain.CurrentDomain.DomainUnload += (o, a) => StopServers();
        }

        [UnitTest]
        public void ShouldBeAbleToUseGenericInvokeAsync()
        {
            AsyncProxyableEcho testObj = GetTestAsyncProxyable();

            AutoResetEvent blocker = new AutoResetEvent(false);
            string value = "this is a value: ".RandomString(8);
            Task<string> task = testObj.InvokeAsync<string>("Send", value);
            task.Wait(1000 * 60 * 3);

            StopServers();
            Expect.AreEqual(value, task.Result);
        }

        [UnitTest]
        public void ShouldUseCacheInvokeAsync()
        {
            AsyncProxyableEcho testObj = GetTestAsyncProxyable();

            AutoResetEvent blocker = new AutoResetEvent(false);
            string value = "this is a value";
            Task<string> task = testObj.InvokeAsync<string>("Send", value);
            task.Wait(1000 * 60 * 3);

            StopServers();
            Expect.AreEqual(value, task.Result);
            // TODO: add better assertions that validate that the remote call wasn't made and the result was retrieved from the local repo
        }

        [UnitTest]
        public void ShouldBeAbleToUseInvokeAsync()
        {
            AsyncProxyableEcho testObj = GetTestAsyncProxyable();

            AutoResetEvent blocker = new AutoResetEvent(false);
            string value = "this is a value: ".RandomString(8);
            bool? ran = false;
            testObj.InvokeAsync(r =>
            {
                ran = true;
                Expect.AreEqual(value, r.Result);
                blocker.Set();
            }, "Send", value);
            blocker.WaitOne(1000*60*3);

            StopServers();
            Expect.IsTrue(ran.Value);
        }

        [UnitTest]
        public void ShouldBeAbleToInstanciateExecutionRequest()
        {
            string className = "AsyncProxyableEcho";
            string methodName = "Send";
            string ext = "json";
            ExecutionRequest request = new ExecutionRequest(className, methodName, ext);
            Expect.AreEqual(className, request.ClassName);
            Expect.AreEqual(methodName, request.MethodName);
            Expect.AreEqual(ext, request.Ext);
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

        private static AsyncProxyableEcho GetTestAsyncProxyable()
        {
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetClient(out BamServer server, out SecureServiceProxyClient<AsyncProxyableEcho> sspc);
            ConsoleLogger logger = GetTestConsoleLogger();
            ProxyFactory serviceFactory = new ProxyFactory(".\\workspace_".RandomLetters(4), logger);
            AsyncCallbackService callbackService = new AsyncCallbackService(new AsyncCallback.Data.Dao.Repository.AsyncCallbackRepository(), new AppConf());
            AsyncProxyableEcho testObj = serviceFactory.GetProxy<AsyncProxyableEcho>(server.DefaultHostPrefix.HostName, server.DefaultHostPrefix.Port, logger); // the "server"
            testObj.CallbackService = callbackService;
            return testObj;
        }

        private static void StopServers()
        {
            ServiceProxyTestHelpers.Servers.Each(s => s.Stop());
        }
    }
}
