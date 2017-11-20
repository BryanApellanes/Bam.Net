using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Net.Server.Binary;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Server.Tests
{
    public class TestBinaryServer: BinaryServer
    {
        public TestBinaryServer(ILogger logger, int port) : base(logger, port) { }

        public override void ProcessRequest(BinaryContext context)
        {
            WriteResponse(context, "Mine: yay!, Yours: " + context.Request.Message);
        }
    }

    [Serializable]
    public class BinaryServerTests: CommandLineTestInterface
    {
        [UnitTest]
        public void CanServe()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false };
            Encoding encoding = Encoding.UTF8;
            int port = 8080;
            logger.StartLoggingThread();
            TestBinaryServer server = new TestBinaryServer(logger, port);
            server.Start();

            BinaryClient client = new BinaryClient("localhost", port);
            BinaryResponse response = client.SendRequest((object)"this is a test");
            Console.WriteLine("Response: " + response.Data);
            response = client.SendRequest((object)"second message");
            Console.WriteLine("Second resposne: " + response.Data);
            Thread.Sleep(1000);
        }
        
    }
}
