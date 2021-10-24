using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Net.Server.Streaming;
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
    public class TestBinaryServer : StreamingServer
    {
        public TestBinaryServer(ILogger logger, int port) : base(logger, port) { }

        public override void ProcessRequest(StreamingContext context)
        {
            WriteResponse(context, "Mine: yay!, Yours: " + context.Request.Body);
        }
    }

    [Serializable]
    public class StreamingServerTests : CommandLineTool
    {
        [UnitTest]
        public void CanServe()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false };
            Encoding encoding = Encoding.UTF8;
            int port = RandomNumber.Between(8081, 65535);
            logger.StartLoggingThread();
            TestBinaryServer server = new TestBinaryServer(logger, port);
            server.Start();

            StreamingClient client = new StreamingClient("localhost", port);
            StreamingResponse response = client.SendWrappedMessage((object)"this is a test");
            Console.WriteLine("Response: " + response.Body);
            response = client.SendWrappedMessage((object)"second message");
            Console.WriteLine("Second resposne: " + response.Body);
            Thread.Sleep(1000);
        }
    }
}
