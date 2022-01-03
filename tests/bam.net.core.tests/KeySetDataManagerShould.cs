using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Encryption;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Tests
{
    public class KeySetDataManagerShould
    {

        [UnitTest]
        public void CreateServerKeySet()
        {
            string testClientHostName = "test client hostname";
            KeySetDataManager keySetDataManager = new KeySetDataManager(CreateTestDatabase(nameof(CreateServerKeySet)));
            IServerKeySet serverKeySet = keySetDataManager.CreateServerKeySetAsync(testClientHostName).Result;

            Expect.IsNotNullOrEmpty(serverKeySet.RsaKey);
            Expect.IsNotNull(serverKeySet.Identifier);
            Expect.IsNullOrEmpty(serverKeySet.AesKey);
            Expect.IsNullOrEmpty(serverKeySet.AesIV);

            Expect.IsNotNullOrEmpty(serverKeySet.ServerHostName);
            Expect.IsNotNullOrEmpty(serverKeySet.ApplicationName);

            Expect.AreEqual(testClientHostName, serverKeySet.ClientHostName);            
        }

        [UnitTest]
        public void CreateClientKeySetForServerKeySet()
        {
            string testClientHostName = "test client hostname";
            KeySetDataManager keySetDataManager = new KeySetDataManager(CreateTestDatabase(nameof(CreateClientKeySetForServerKeySet)));
            IServerKeySet serverKeySet = keySetDataManager.CreateServerKeySetAsync(testClientHostName).Result;
            IClientKeySet clientKeySet = keySetDataManager.CreateClientKeySetForServerKeySetAsync(serverKeySet).Result;

            Expect.AreEqual(serverKeySet.Identifier, clientKeySet.Identifier);
            Expect.IsFalse(clientKeySet.GetIsInitialized());
            Expect.IsNullOrEmpty(clientKeySet.AesKey);
            Expect.IsNullOrEmpty(clientKeySet.AesIV);
        }

        [UnitTest]
        public void CreateAesKeyExchangeForClientKeySet()
        {
            string testClientHostName = "test client hostname";
            KeySetDataManager keySetDataManager = new KeySetDataManager(CreateTestDatabase(nameof(CreateAesKeyExchangeForClientKeySet)));
            IServerKeySet serverKeySet = keySetDataManager.CreateServerKeySetAsync(testClientHostName).Result;
            IClientKeySet clientKeySet = keySetDataManager.CreateClientKeySetForServerKeySetAsync(serverKeySet).Result;
            IAesKeyExchange aesKeyExchange = keySetDataManager.CreateAesKeyExchangeAsync(clientKeySet).Result;

            Expect.AreEqual(clientKeySet.PublicKey, aesKeyExchange.PublicKey);
            Expect.IsNotNullOrEmpty(aesKeyExchange.AesKeyCipher);
            Expect.IsNotNullOrEmpty(aesKeyExchange.AesIVCipher);
            Expect.AreEqual(clientKeySet.ClientHostName, aesKeyExchange.ClientHostName);
            Expect.AreEqual(clientKeySet.ServerHostName, aesKeyExchange.ServerHostName);
        }

        [UnitTest]
        public void SetServerAesKey()
        {
            string testClientHostName = "test client hostname";
            KeySetDataManager keySetDataManager = new KeySetDataManager(CreateTestDatabase(nameof(SetServerAesKey)));
            IServerKeySet serverKeySet = keySetDataManager.CreateServerKeySetAsync(testClientHostName).Result;
            IClientKeySet clientKeySet = keySetDataManager.CreateClientKeySetForServerKeySetAsync(serverKeySet).Result;
            IAesKeyExchange aesKeyExchange = keySetDataManager.CreateAesKeyExchangeAsync(clientKeySet).Result;
            serverKeySet = keySetDataManager.SetServerAesKeyAsync(aesKeyExchange).Result;

            Expect.IsNotNullOrEmpty(serverKeySet.RsaKey);
            Expect.IsNotNull(serverKeySet.Identifier);
            Expect.IsNotNullOrEmpty(serverKeySet.AesKey);
            Expect.IsNotNullOrEmpty(serverKeySet.AesIV);
            Expect.AreEqual(clientKeySet.AesKey, serverKeySet.AesKey);
            Expect.AreEqual(clientKeySet.AesIV, serverKeySet.AesIV);

            Expect.IsNotNullOrEmpty(serverKeySet.ServerHostName);
            Expect.IsNotNullOrEmpty(serverKeySet.ApplicationName);

            Expect.AreEqual(testClientHostName, serverKeySet.ClientHostName);
        }

        private Database CreateTestDatabase(string testName)
        {
            string fileName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetFileInfo().FullName);
            SQLiteDatabase db = new SQLiteDatabase(Path.Combine($"{BamHome.DataPath}", fileName), testName);
            Message.PrintLine("{0}: SQLite database: {1}", testName, db.DatabaseFile.FullName);
            return db;
        }
    }
}
