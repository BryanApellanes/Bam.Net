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
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Encryption.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {
        [UnitTest]
        public void CredentialManagerCanSetAndGetCredentialInfo()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            CredentialInfo info = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password"
            };
            mgr.SetCredentials(info);

            CredentialInfo retrieved = mgr.GetCredentials();

            Expect.AreEqual(info.UserName, retrieved.UserName);
            Expect.AreEqual(info.Password, retrieved.Password);
            Expect.IsNull(info.TargetService);
            Expect.IsNull(info.MachineName);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetCredentialInfoServiceTarget()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            CredentialInfo info1 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password1",
                TargetService = "MyAwesomeService1"
            };
            CredentialInfo info2 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password2",
                TargetService = "MyAwesomeService2"
            };
            mgr.SetCredentials(info1);
            mgr.SetCredentials(info2);

            CredentialInfo check1 = mgr.GetCredentials(info1.TargetService);
            CredentialInfo check2 = mgr.GetCredentials(info2.TargetService);

            Expect.AreEqual(info1.UserName, check1.UserName);
            Expect.AreEqual(info1.Password, check1.Password);
            Expect.AreEqual(info1.TargetService, check1.TargetService);
            Expect.IsNull(info1.MachineName);
            Expect.IsFalse(info1.Password.Equals(check2.Password));
            Expect.AreEqual(info2.UserName, check2.UserName);
            Expect.AreEqual(info2.Password, check2.Password);
            Expect.AreEqual(info2.TargetService, check2.TargetService);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetCredentialInfoMachine()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            CredentialInfo info1 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password1",
                TargetService = "MyAwesomeService1",
                MachineName = "Machine1"
            };
            CredentialInfo info2 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password2",
                TargetService = "MyAwesomeService2"
            };
            mgr.SetCredentials(info1);
            mgr.SetCredentials(info2);

            CredentialInfo check1 = mgr.GetCredentials(info1.MachineName, info1.TargetService);
            CredentialInfo check2 = mgr.GetCredentials(info2.TargetService);

            Expect.AreEqual(info1.UserName, check1.UserName);
            Expect.AreEqual(info1.Password, check1.Password);
            Expect.AreEqual(info1.TargetService, check1.TargetService);
            Expect.IsNull(info2.MachineName);
            Expect.IsFalse(info1.Password.Equals(check2.Password));
            Expect.AreEqual(info2.UserName, check2.UserName);
            Expect.AreEqual(info2.Password, check2.Password);
            Expect.AreEqual(info2.TargetService, check2.TargetService);
        }

        [UnitTest]
        public void CredentialManagerReturnsNullForUnknownUser()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            CredentialInfo info1 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password1",
                TargetService = "MyAwesomeService1",
                MachineName = "Machine1"
            };
            CredentialInfo info2 = new CredentialInfo
            {
                UserName = "Monkey",
                Password = "Password2",
                TargetService = "MyAwesomeService2"
            };
            mgr.SetCredentials(info1);
            mgr.SetCredentials(info2);

            CredentialInfo info = mgr.GetCredentials(9.RandomLetters());
            Expect.IsNullOrEmpty(info.UserName);
            Expect.IsNullOrEmpty(info.Password);
            Expect.IsTrue(info.IsNull);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetUserName()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string userName = "Monkey";
            mgr.SetUserName(userName);

            string retrieved = mgr.CredentialProvider.GetUserName();
            Expect.AreEqual(userName, retrieved);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetUserNameFor()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string userName = "Monkey";
            string someApplication = "The name of an application or service";
            mgr.SetUserNameFor(someApplication, userName);

            string retrieved = mgr.GetUserNameFor(someApplication);
            Expect.AreEqual(userName, retrieved);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetUserNameForMachineName()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string userName = "Monkey";
            string someApplication = "The name of an application or service";
            string machineName = "my awesome machine";
            mgr.SetUserNameFor(machineName, someApplication, userName);

            string retrieved = mgr.GetUserNameFor(machineName, someApplication);
            Expect.AreEqual(userName, retrieved);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetPassword()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string password = "My awesome password";
            mgr.SetPassword(password);

            string retrieved = mgr.GetPassword();
            Expect.AreEqual(password, retrieved);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetPasswordFor()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string password = "My awesome password";
            string someApplicationName = "My awesome application";
            mgr.SetPasswordFor(someApplicationName, password);

            string retrieved = mgr.GetPasswordFor(someApplicationName);
            Expect.AreEqual(password, retrieved);
        }

        [UnitTest]
        public void CredentialManagerCanSetAndGetPasswordForMachineName()
        {
            CredentialManager mgr = new CredentialManager(GetTestCredentialVault());
            string password = "My awesome password";
            string someApplicationName = "My awesome application";
            string machineName = "the computer";
            mgr.SetPasswordFor(machineName, someApplicationName, password);

            string retrieved = mgr.GetPasswordFor(machineName, someApplicationName);
            Expect.AreEqual(password, retrieved);
        }

        [UnitTest]
        public void CanLoadVault()
        {
            SQLiteDatabase db = GetVaultDatabase();
            string password = "This is my awesome password";
            string sensitiveValue = "Sensitive Value";
            string keyName = "SensitiveInformation";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            v.Set(keyName, sensitiveValue);

            VaultItemCollection items = VaultItem.LoadAll(db);
            Expect.AreEqual(1, items.Count);
            foreach (VaultItem item in items)
            {
                Message.PrintLine("Should be gibberish: Key={0}, Value={1}", item.Key, item.Value);
                Expect.IsFalse(item.Value.Equals(sensitiveValue));
                Expect.AreEqual(sensitiveValue, v[keyName]);
            }
        }

        [UnitTest]
        public void SetShouldCreateVaultItem()
        {
            SQLiteDatabase db = GetVaultDatabase();
            VaultItemCollection items = VaultItem.LoadAll(db);
            Expect.AreEqual(0, items.Count);
            
            string password = "This is my awesome password";
            string sensitiveValue = "Sensitive Value";
            string keyName = "SensitiveInformation";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            v.Set(keyName, sensitiveValue);

            items = VaultItem.LoadAll(db);
            Expect.AreEqual(1, items.Count);
            Message.PrintLine(items.ToJsonSafe().ToJson());
        }

        [UnitTest]
        public void VaultShouldHaveOneKey()
        {
            SQLiteDatabase db = GetVaultDatabase();
            string password = "My secret password";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            Expect.IsGreaterThan(v.VaultKeysByVaultId.Count, 0);
            Expect.AreEqual(1, v.VaultKeysByVaultId.Count);
        }

        [UnitTest]
        public void ExportShouldNullifyKey()
        {
            SQLiteDatabase db = GetVaultDatabase();
            string password = "My secret password";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            VaultKeyInfo keyInfo = v.ExportKey();
            v.VaultKey.ShouldBeNull();
        }
        
        [UnitTest]
        public void CanExportVaultKey()
        {
            SQLiteDatabase db = GetVaultDatabase();
            
            string password = "This is my awesome password";
            string sensitiveValue = "Sensitive Value";
            string keyName = "SensitiveInformation";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            v.Set(keyName, sensitiveValue);

            VaultKeyInfo keyInfo = v.ExportKey();

            string data = v[keyName];
            Expect.IsNull(data, "data should have been null");

            Message.PrintLine(keyInfo.ToJson());

            v.ImportKey(keyInfo);
            data = v[keyName];
            Expect.AreEqual(sensitiveValue, data);
        }


        [UnitTest]
        public void CanUpdateByKeyName()
        {
            SQLiteDatabase db = GetVaultDatabase();
            string password = "This is my awesome password";
            string sensitiveValue = "Sensitive Value";
            string keyName = "SensitiveInformation";
            Vault v = Vault.Retrieve(db, "EncryptedData", password);
            v.Set(keyName, sensitiveValue);
            v[keyName] = "updated";
            VaultKeyInfo keyInfo = v.ExportKey();

            string data = v[keyName];
            Expect.IsNull(data);

            Message.PrintLine(keyInfo.ToJson());

            v.ImportKey(keyInfo);

            data = v[keyName];
            Expect.AreEqual("updated", data);
        }

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

                Message.PrintLine("key: {0}\r\nvalue: {1}", item.Key, item.Value);
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
        public void _1_ShouldBeAbleToCreateByRetrievingVault()
        {
            Vault testVault = Vault.Retrieve(nameof(_1_ShouldBeAbleToCreateByRetrievingVault), "somerandomtext_".RandomLetters(8));
            Expect.IsNotNull(testVault);
        }

        [UnitTest]
        public void _1_2_VaultKeyShouldNotBeNullOnRetrieve()
        {
            Vault testVault = Vault.Retrieve(nameof(_1_2_VaultKeyShouldNotBeNullOnRetrieve), "somerandomtext_".RandomLetters(8));
            Expect.IsNotNull(testVault?.VaultKey);
        }

        [UnitTest]
        public void _1_1_ShouldThrowIfVaultKeyNotSet()
        {
            Expect.Throws(() =>
            {
                Vault testVault = Vault.Retrieve(nameof(_1_1_ShouldThrowIfVaultKeyNotSet),
                    $"{nameof(_1_1_ShouldThrowIfVaultKeyNotSet)}_password".RandomLetters(8));
                VaultKeyInfo keyInfo = testVault.ExportKey();
                testVault["should fail"] = "this shouldn't make it into the database";
            }, ex =>
            {
                ex.IsObjectOfType<VaultKeyNotSetException>();
            });
        }

        [UnitTest]
        public void ShouldBeAbleToSetNotifyCredentials()
        {
            Vault testVault = Vault.Create(nameof(ShouldBeAbleToSetNotifyCredentials), Vault.GeneratePassword(), RsaKeyLength._1024);
            Notify.Credentials = testVault;
            Message.PrintLine(Notify.Credentials["smtphost"]);
            Notify.Credentials["smtphost"] = "smtp.bamapps.com";
            Message.PrintLine(Notify.Credentials["smtphost"]);
        }

        [UnitTest]
        public void ShouldBeAbleToEnumerateVaultByKeys()
        {
            Notify.Credentials.Keys.Each(key =>
            {
                string value = Notify.Credentials[key];
                Message.PrintLine("{0}={1}", key, value);
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

        static string testFile = "./TestVaultFile.vault.sqlite";
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
            Vault test = Vault.Load(testFileInfo, nameof(_4_ShouldBeAbleToReadVaultInSpecificFile));
            test["smtphost"] = "smtp.bamapps.com";
            Message.PrintLine(test["smtphost"]);
            Expect.AreEqual("smtp.bamapps.com", test["smtphost"]);
        }

        [UnitTest]
        public void _5_ShouldBeAbleToLoadVaultInfo()
        {
            VaultInfo info = new VaultInfo { FilePath = testFile, Name = "TestVault" };
            Vault vault = info.Load();
            vault.Keys.Each(key =>
            {
                Message.PrintLine("{0}={1}", key, vault[key]);
            });
        }

        static string vaultDataDb = Path.Combine(BamHome.DataPath, "TestVaultData.sqlite");
        static string testVaults = Path.Combine(BamHome.DataPath, "TestVaults.vault.sqlite");
        [AfterUnitTests]
        public static void CleanUp()
        {
            OutLine("Cleaning up test vaults...", ConsoleColor.Yellow);
            GetTestCredentialVault(out VaultDatabase db);
            Vault.LoadAll(db).Delete(db);
            if (File.Exists(vaultDataDb))
            {
                File.Delete(vaultDataDb);
            }

            if (File.Exists(testVaults))
            {
                File.Delete(testVaults);
            }
            
            OutLine("Clean up test vaults complete", ConsoleColor.Green);
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
            toDelete?.Delete();
        }

        private static Vault GetTestCredentialVault()
        {
            return GetTestCredentialVault(out VaultDatabase ignore);
        }

        private static Vault GetTestCredentialVault(out VaultDatabase db)
        {
            string filePath = Path.Combine(BamHome.DataPath, "TestVaults.vault.sqlite");
            return Vault.Load(new FileInfo(filePath), 8.RandomLetters(), out db);
        }

        private static SQLiteDatabase GetVaultDatabase(string name = "TestVaultData")
        {
            SQLiteDatabase db = new SQLiteDatabase(BamHome.DataPath, name);
            if (db.DatabaseFile.Exists)
            {
                File.Delete(db.DatabaseFile.FullName);
            }
            db.TryEnsureSchema<Vault>();
            return db;
        }
    }
}
