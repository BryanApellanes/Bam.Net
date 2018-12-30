/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Security;

namespace Bam.Net.Encryption
{
    public class Decrypted: Encrypted
    {
        public Decrypted(byte[] cipher, byte[] key, byte[] iv)
            : base(cipher, key, iv)
        {
        }

        public Decrypted(Encrypted value)
            : base(value.Cipher, value.Key, value.IV)
        {
            Decrypt();
        }

        public Decrypted(string base64Cipher, string b64Key, string iv = null)
        {
            iv = iv ?? DefaultIV;
            this.Base64Cipher = base64Cipher;
            this.Base64Key = b64Key;
            this.Base64IV = iv;
        }

        public static implicit operator string(Decrypted dec)
        {
            return dec.Value;
        }

        public string Value
        {
            get
            {
                if (string.IsNullOrEmpty(Plain))
                {
                    Decrypt();
                }

                return Plain;
            }
        }

        protected string Decrypt()
        {
            Plain = Decrypt(Cipher, Key, IV).Truncate(2);// truncate desalinates the value
            return Plain;
        }

        public static string Decrypt(byte[] cipher, byte[] key, byte[] iv)
        {
            return Aes.Decrypt(Convert.ToBase64String(cipher), Convert.ToBase64String(key), Convert.ToBase64String(iv));
        }
    }
}
