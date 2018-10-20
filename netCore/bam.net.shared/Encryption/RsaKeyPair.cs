/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Bam.Net.Configuration;

namespace Bam.Net.Encryption
{
    public class RsaKeyPair
    {
        public RsaKeyPair()
        {
            RSACryptoServiceProvider initial = CreateRSACryptoServiceProvider(Rsa.DefaultKeySize);
            PublicKeyXml = initial.ToXmlString(false);
            PrivateKeyXml = initial.ToXmlString(true);
        }
        
        static object _defaultLock = new object();
        static RsaKeyPair _rsaKeyPair;
        public static RsaKeyPair Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _rsaKeyPair, () =>
                {
                    string applicationName = DefaultConfiguration.GetAppSetting("ApplicationName", DefaultConfiguration.DefaultApplicationName);
                    RsaKeyPair result = new RsaKeyPair();
                    if (result.PubExists(applicationName, out string pubKeyPath) &&
                        result.PrivExists(applicationName, out string privKeyPath))
                    {
                        LoadPublic(result, pubKeyPath);
                        LoadPrivate(result, privKeyPath);
                    }
                    else
                    {
                        result.Save(applicationName);
                    }

                    return result;
                });
            }
        }

        protected bool PubExists(string fileName, out string filePath)
        {
            return Exists(fileName, "pub", out filePath);
        }

        protected bool PrivExists(string fileName, out string filePath)
        {
            return Exists(fileName, "priv", out filePath);
        }

        private bool Exists(string fileName, string ext, out string filePath)
        {
            filePath = Path.Combine(RuntimeSettings.AppDataFolder, string.Format("{0}.{1}", fileName, ext));
            return File.Exists(filePath);
        }

        public string PublicKeyXml { get; set; }
        public string PrivateKeyXml { get; set; }
        
        public void Save(string fileName)
        {
            Save(RuntimeSettings.AppDataFolder, fileName);
        }

        public void Save(string directory, string fileName)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string pubFile = Path.Combine(directory, string.Format("{0}.pub", fileName));
            using (StreamWriter sw = new StreamWriter(pubFile))
            {
                sw.Write(PublicKeyXml);
            }

            string privFile = Path.Combine(directory, string.Format("{0}.priv", fileName));
            using (StreamWriter sw = new StreamWriter(privFile))
            {
                sw.Write(PrivateKeyXml);
            }
        }

        public static RsaKeyPair Load(string fileName)
        {
            return Load(".", fileName);
        }

        public static RsaKeyPair Load(string directory, string fileName)
        {
            RsaKeyPair result = new RsaKeyPair();
            string pubFile = Path.Combine(directory, string.Format("{0}.pub", fileName));
            LoadPublic(result, pubFile);
            string privFile = Path.Combine(directory, string.Format("{0}.priv", fileName));
            LoadPrivate(result, privFile);
            return result;
        }

        protected static void LoadPublic(RsaKeyPair keys, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("The specified filePath was not found");
            }

            keys.PublicKeyXml = File.ReadAllText(filePath);
        }

        protected static void LoadPrivate(RsaKeyPair keys, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("The specified filePath was not found");
            }

            keys.PrivateKeyXml = File.ReadAllText(filePath);
        }

        public RSACryptoServiceProvider PublicKey
        {
            get
            {
                RSACryptoServiceProvider publicKey = CreateRSACryptoServiceProvider(Rsa.DefaultKeySize);
                publicKey.FromXmlString(PublicKeyXml);
                return publicKey;
            }
        }


        public RSACryptoServiceProvider PrivateKey
        {
            get
            {
                RSACryptoServiceProvider privateKey = CreateRSACryptoServiceProvider(Rsa.DefaultKeySize);
                privateKey.FromXmlString(PrivateKeyXml);
                return privateKey;
            }
        }

        public string EncryptWithPublicKey(string value)
        {
            return Encrypt(value, PublicKey, Encoding.UTF8);
        }

        public string DecryptWithPrivateKey(string base64EncodedCipher)
        {
            return Decrypt(base64EncodedCipher, PrivateKey, Encoding.UTF8);
        }

        private string Decrypt(string base64EncodedCipher, RSACryptoServiceProvider key, Encoding encoding)
        {
            byte[] enc = Convert.FromBase64String(base64EncodedCipher);
            byte[] denc = key.Decrypt(enc, false);
            return encoding.GetString(denc);
        }

        private string Encrypt(string value, RSACryptoServiceProvider key, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(value);
            byte[] enc = key.Encrypt(data, false);
            string base64Enc = Convert.ToBase64String(enc);
            return base64Enc;
        }

        private static RSACryptoServiceProvider CreateRSACryptoServiceProvider(int keySize = 1024)
        {
            CspParameters RSAParams = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider publicKey = new RSACryptoServiceProvider(keySize, RSAParams);
            return publicKey;
        }
    }
}
