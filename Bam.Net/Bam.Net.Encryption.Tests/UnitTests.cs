/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Messaging;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Bam.Net.Data;
using Bam.Net.UserAccounts;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace Bam.Net.Encryption.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void CryptoAesTest()
        {
            string encrypted = Crypto.EncryptStringAES("plainText", "password");
            string decrypted = Crypto.DecryptStringAES(encrypted, "password");
            Expect.AreEqual("plainText", decrypted);
            encrypted.SafeWriteToFile(".\\cipher.txt", true);
        }

        //EAAAAMMxdXbijDVbOh0k23+PPXUy47YzlJe+fZ9raJ5sWcEr
        [UnitTest]
        public void CryptoAesCipherTest()
        {
            string encrypted = "EAAAAMMxdXbijDVbOh0k23+PPXUy47YzlJe+fZ9raJ5sWcEr";
            string decrypted = Crypto.DecryptStringAES(encrypted, "password");

            Expect.AreEqual("plainText", decrypted);
        }

        [UnitTest]
        public void PasswordEncryptionTest()
        {
            string data = "monkey";
            PasswordEncrypted encrypted = new PasswordEncrypted(data, "password");
            OutLine(encrypted);

            PasswordDecrypted decrypted = new PasswordDecrypted(encrypted, "password");
            OutLine(decrypted);

            Expect.AreEqual(decrypted.Data, data);
        }

        [UnitTest]
        public void AesCipherTest()
        {
            string data = "Banana";
            string password = "monkey";
            PasswordEncrypted aes = new PasswordEncrypted(data, password);

            PasswordDecrypted aes2 = new PasswordDecrypted(aes.Value, password);
            Expect.AreEqual(data, aes2.Value);

            PasswordDecrypted aes3 = new PasswordDecrypted(aes, password);
            Expect.AreEqual(data, aes3.Value);
        }

        [UnitTest]
        public void EmptyStringShaOneCompare()
        {
            OutLine("".Sha1());
            OutLine("da39a3ee5e6b4b0d");
            Expect.IsTrue("".Sha1().StartsWith("da39a3ee5e6b4b0d"));
        }

        [UnitTest]
        public void CipherShouldntBeTheSame()
        {
            string data = "data".RandomLetters(16);
            string password = "baloney";
            PasswordEncrypted enc = new PasswordEncrypted(data, password);
            PasswordEncrypted enc2 = new PasswordEncrypted(data, password);

            Expect.IsFalse(enc.Value.Equals(enc2.Value));
            Expect.IsFalse(enc.Value == enc2.Value);

            PasswordDecrypted dec = new PasswordDecrypted(enc, password);
            PasswordDecrypted dec2 = new PasswordDecrypted(enc2, password);

            Expect.AreEqual(dec.Value, dec2.Value);
        }

        [UnitTest]
        public void EncryptTest()
        {
            string value = "This is a string: ".RandomLetters(16);
            AsymmetricCipherKeyPair keys = RsaKeyGen.GenerateKeyPair(RsaKeyLength._1024);
            string publicPem = keys.PublicKeyToPem();
            string pemString = keys.ToPem();

            string encrypted = value.EncryptWithPublicKey(keys);
            OutLine(encrypted, ConsoleColor.Cyan);
            string decrypted = encrypted.DecryptWithPrivateKey(pemString);
            OutLine(decrypted, ConsoleColor.Green);

            Expect.AreEqual(value, decrypted);
        }

        [UnitTest]
        public void ShouldBeAbleToCreateVault()
        {
            string p;
            Vault vault = CreateTestVault(out p);
            VaultKey key = vault.VaultKeysByVaultId.JustOne();
            
            Expect.IsNotNull(key.RsaKey, "RsaKey was null");
            AsymmetricCipherKeyPair keys = key.RsaKey.ToKeyPair(); // will throw an exception failing the test if invalid value
        }

        private static Vault CreateTestVault(out string password)
        {
            password = "Password_".RandomLetters(7);
            Vault vault = Vault.Create("test", password);
            Expect.IsNotNull(vault);            
            return vault;
        }

		private static void DeleteVault(string name)
		{
			Vault toDelete = Vault.Retrieve(name);
            if (toDelete != null)
            {
                toDelete.Delete();
            }
		}

        [UnitTest]
        public void PasswordShouldBeInTheVaultKey()
        {
			DeleteVault("test");
            string password;
            Vault vault = CreateTestVault(out password);
            VaultKey key = vault.VaultKeysByVaultId.JustOne();
            // input
            // password -> rsa public key encrypt -> VaultKey.Password

            // output
            // rsa private key decrypt -> compare to password
            string decrypted = key.PrivateKeyDecrypt(key.Password);
            Expect.AreEqual(password, decrypted);
        }

        [UnitTest]
        public void ShouldBeAbleToSetAndGetVaultValue()
        {
            string password;
            Vault vault = CreateTestVault(out password);
            string val1 = "value1";
            string val2 = "value2";
            vault["key1"] = val1;
            vault["key2"] = val2;

            Expect.AreEqual(val1, vault["key1"]);
            Expect.AreEqual(val2, vault["key2"]);

            Expect.AreEqual(2, vault.VaultItemsByVaultId.Count);

            vault.VaultItemsByVaultId.Each(item =>
            {
                // should be encrypted
                Expect.IsFalse(item.Key.Equals("key1"));
                Expect.IsFalse(item.Key.Equals("key2"));
                Expect.IsFalse(item.Value.Equals(val1));
                Expect.IsFalse(item.Value.Equals(val2));

                OutLineFormat("key: {0}\r\nvalue: {1}", item.Key, item.Value);
            });

            vault["key1"] = "monkey";

            Expect.AreEqual("monkey", vault["key1"]);  // make sure no dupes
        }

        [UnitTest]
        public void ShouldBeAbleToGetConnectionNameFromContext()
        {
            string name = Dao.ConnectionName(typeof(EncryptionContext));
            Expect.IsFalse(string.IsNullOrEmpty(name));
            OutLine(name);
        }

        [UnitTest]
        public void ShouldBeAbleToInitializeVaultDb()
        {
            VaultDatabaseInitializer initializer = new VaultDatabaseInitializer();
            initializer.VaultFile = new FileInfo(".\\{0}.vault"._Format(MethodBase.GetCurrentMethod().Name));
            DatabaseInitializationResult result = initializer.Initialize();
            Expect.IsTrue(result.Success);
            Expect.IsNotNull(result.Database);
            OutLine(result.Database.ConnectionString);
        }

        [UnitTest]
        public void ShouldBeAbleToHaveMultipleVaultsInDifferentFiles()
        {
            FileInfo fileOne = new FileInfo(".\\one.vault.sqlite");
            FileInfo fileTwo = new FileInfo(".\\two.vault.sqlite");
            if (fileOne.Exists)
            {
                fileOne.Delete();
            }

            if (fileTwo.Exists)
            {
                fileTwo.Delete();
            }

            Vault one = Vault.Load(fileOne, "one", "password");
            one["key1"] = "monkey";
            fileOne.Refresh();
            Expect.IsTrue(fileOne.Exists);

            Vault two = Vault.Load(fileTwo, "two", "password Two");
            two["key1"] = "banana";
            fileTwo.Refresh();
            Expect.IsTrue(fileTwo.Exists);

            Expect.IsFalse(one["key1"].Equals(two["key1"]));

            Expect.AreEqual("monkey", one["key1"]);
            Expect.AreEqual("banana", two["key1"]);

            one["key1"] = "changed";

            Expect.AreEqual("changed", one["key1"]);
        }

        [UnitTest]
        public void _1_ShouldBeAbleToCreateVault()
        {
            Vault testVault = Vault.Retrieve("TestVault", "sometandomtext");
            testVault["smtphost"] = "smtp.live.com";
            Expect.AreEqual("smtp.live.com", testVault["smtphost"]);
        }

        [UnitTest]
        public void _2_ShouldBeAbleToReadVault()
        {
            Vault testVault = Vault.Retrieve("TestVault");
            Expect.AreEqual("smtp.live.com", testVault["smtphost"]);
        }

        [UnitTest]
        public void ShouldBeAbleToSetNotifyCredentials()
        {
            OutLine(Notify.CredentialVault["smtphost"]);
            Notify.CredentialVault["smtphost"] = "smtp.live.com";
            OutLine(Notify.CredentialVault["smtphost"]);
        }

        [UnitTest]
        public void ShouldBeAbleToEnumerateVaultByKeys()
        {
            Notify.CredentialVault.Keys.Each(key =>
            {
                string value = Notify.CredentialVault[key];
                OutLineFormat("{0}={1}", key, value);
            });
        }

        [UnitTest]
        public void CreateShouldNotDuplicate()
        {
            FileInfo testFileInfo = new FileInfo(testFile);
            Vault test = Vault.Create(testFileInfo, "TestVault");
            Vault test2 = Vault.Create(testFileInfo, "TestVault");

            Expect.AreEqual(test.Id, test2.Id);

            VaultCollection retrieved = Vault.Where(c => c.Name == "TestVault", test.Database);
            Expect.AreEqual(1, retrieved.Count);
        }

        static string testFile = ".\\TestVaultFile.vault.sqlite";        
        [UnitTest]
        public void _3_ShouldBeAbleToCreateVaultInSpecificFile()
        {
            FileInfo testFileInfo = new FileInfo(testFile);
            Vault test = Vault.Create(testFileInfo, "TestVault");
            test["smtphost"] = "smtp.live.com";
        }

        [UnitTest]
        public void _4_ShouldBeAbleToReadVaultInSpecificFile()
        {
            FileInfo testFileInfo = new FileInfo(testFile);
            Vault test = Vault.Load(testFileInfo, "TestVault");
            OutLine(test["smtphost"]);
            Expect.AreEqual("smtp.live.com", test["smtphost"]);
        }

        [UnitTest]
        public void _5_ShouldBeAbleToLoadVaultInfo()
        {
            VaultInfo info = new VaultInfo { FilePath = testFile, Name = "TestVault" };
            Vault vault = info.Load();
            vault.Keys.Each(key =>
            {
                OutLineFormat("{0}={1}", key, vault[key]);
            });
        }
    }
}
