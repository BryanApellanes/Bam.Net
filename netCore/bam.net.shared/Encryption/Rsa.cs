/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public static class Rsa
    {
        static Rsa()
        {
            DefaultKeySize = 1024;
        }

        public static int DefaultKeySize { get; set; }

        public static string Encrypt(string value)
        {
            return RsaKeyPair.Default.EncryptWithPublicKey(value);
        }

        public static string Decrypt(string cipherText)
        {
            return RsaKeyPair.Default.DecryptWithPrivateKey(cipherText);
        }

        public static string GetPublicKey()
        {
            return RsaKeyPair.Default.PublicKeyXml;
        }
    }
}
