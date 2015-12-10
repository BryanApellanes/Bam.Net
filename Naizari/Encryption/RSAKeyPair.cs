/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
//using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Text;
using Naizari.Data;

namespace Naizari.Encryption
{
    public class RSAKeyPair
    {
        public RSAKeyPair()
        {
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
            RSACryptoServiceProvider initial = new RSACryptoServiceProvider(1024, RSAParams);
            PublicKeyXml = initial.ToXmlString(false);
            PrivateKeyXml = initial.ToXmlString(true);
        }

        public string PublicKeyXml { get; set; }
        public string PrivateKeyXml { get; set; }

        [DbPropertyIgnore]
        public RSACryptoServiceProvider PublicKey
        {
            get
            {
                RSACryptoServiceProvider publicKey = CreateRSACryptoServiceProvider();
                publicKey.FromXmlString(PublicKeyXml);
                return publicKey;
            }
        }

        private static RSACryptoServiceProvider CreateRSACryptoServiceProvider()
        {
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
            RSACryptoServiceProvider publicKey = new RSACryptoServiceProvider(RSAParams);
            return publicKey;
        }

        [DbPropertyIgnore]
        public RSACryptoServiceProvider PrivateKey
        {
            get
            {
                RSACryptoServiceProvider privateKey = CreateRSACryptoServiceProvider();
                privateKey.FromXmlString(PrivateKeyXml);
                return privateKey;
            }
        }

        public string EncryptWithPublicKey(string value)
        {
            return Encrypt(value, PublicKey);
        }

        public string DecryptWithPrivateKey(string base64EncodedCipher)
        {
            return Decrypt(base64EncodedCipher, PrivateKey);
        }

        private string Decrypt(string base64EncodedCipher, RSACryptoServiceProvider key)
        {
            byte[] enc = Convert.FromBase64String(base64EncodedCipher);
            byte[] denc = key.Decrypt(enc, false);
            return Encoding.ASCII.GetString(denc);
        }

        private string Encrypt(string value, RSACryptoServiceProvider key)
        {
            byte[] data = Encoding.ASCII.GetBytes(value);
            byte[] enc = key.Encrypt(data, false);
            string base64Enc = Convert.ToBase64String(enc);
            return base64Enc;
        }
    }
}
