using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class KeyValueStoreServerTests : CommandLineTool
    {
        [UnitTest]
        public void CanServeKeyValueStore()
        {
            int port = RandomNumber.Between(8081, 65535);
            string testKey = 16.RandomLetters();
            string testValue = 500.RandomLetters();
            Database sessionDb = DataProvider.Current.GetAppDatabase($"{nameof(KeyValueStoreServer)}_Sessions");
            sessionDb.TryEnsureSchema<SecureSession>();
            Db.For<SecureSession>(sessionDb);
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false };
            DirectoryInfo testStorage = new DirectoryInfo(Path.Combine(nameof(CanServeKeyValueStore), 8.RandomLetters()));
            KeyValueStoreServer server = new KeyValueStoreServer(testStorage, logger)
            {
                Port = port
            };
            server.Started += (o, a) => OutLine("Server started", ConsoleColor.Green);
            server.Start();
            KeyValueStoreClient client = new KeyValueStoreClient("localhost", port, logger);
            client.StartSession();
            Expect.IsTrue(client.Set(testKey, testValue));
            Console.WriteLine(testStorage.FullName);
            server.Stop();

        }
    }
}
