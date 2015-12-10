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
    public class Encrypted
    {
        protected static string DefaultIV = Convert.ToBase64String(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        public Encrypted()
        {
            SecureRandom random = new SecureRandom();
            this.Key = random.GenerateSeed(16);
            this.IV = random.GenerateSeed(16);
            this.Plain = string.Empty;
        }

        public Encrypted(string data)
            : this()
        {
            this.Plain = data;
        }

        public Encrypted(string data, string b64Key)
            : this(data, b64Key, DefaultIV)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The plain text data to be encrypted</param>
        /// <param name="b64Key">A plaintext value to derive a key from</param>
        /// <param name="b64IV">A plain text vlaue to derive 
        /// the initialization vector from</param>
        public Encrypted(string data, string b64Key, string b64IV)
        {
            this.Plain = data;

            this.Base64Key = b64Key;
            this.Base64IV = b64IV;

            this.Cipher = Encrypt();
        }

        //protected void SetKeyAndIV(string keySeed, string initializationVectorSeed)
        //{
        //    string passwordHash = keySeed.Sha1();
        //    string ivHash = initializationVectorSeed.Sha1();
        //    this.Key = Encoding.UTF8.GetBytes(passwordHash.First(16));
        //    this.IV = Encoding.UTF8.GetBytes(ivHash.First(16));
        //}

        public Encrypted(byte[] cipher, byte[] key, byte[] iv)
        {
            this.Key = key;
            this.Cipher = cipher;
            this.IV = iv;
        }

        //public Encrypted(byte[] cipher, byte[] key, string iv)
        //{
        //    this.Key = key;
        //    this.IV = Encoding.UTF8.GetBytes(iv);
        //    this.Cipher = cipher;
        //}

        public static implicit operator string(Encrypted enc)
        {
            return enc.Value;
        }

        public string Value
        {
            get
            {
                return Base64Cipher;
            }
        }

        public byte[] Key
        {
            get;
            set;
        }

        public byte[] IV
        {
            get;
            set;
        }

        public string Salt
        {
            get
            {
                return ";".RandomLetters(1);
            }
        }

        public string Plain
        {
            get;
            set;
        }

        public byte[] Cipher
        {
            get;
            set;
        }

        public string Base64Cipher
        {
            get
            {
                if (Cipher == null)
                {
                    Encrypt();
                }

                return Convert.ToBase64String(Cipher);
            }
            set
            {
                Cipher = Convert.FromBase64String(value);
            }
        }

        public string Base64Key
        {
            get
            {
                if (Key != null)
                {
                    return Convert.ToBase64String(Key);
                }

                return string.Empty;
            }
            set
            {
                Key = Convert.FromBase64String(value);
            }
        }

        public string Base64IV
        {
            get
            {
                if (IV != null)
                {
                    return Convert.ToBase64String(IV);
                }

                return string.Empty;
            }
            set
            {
                IV = Convert.FromBase64String(value);
            }
        }

        protected byte[] Encrypt()
        {
            Base64Cipher = Aes.Encrypt(string.Concat(Plain, Salt), Base64Key, Base64IV);
            //byte[] data = Encoding.UTF8.GetBytes(string.Concat(Plain, Salt));
            //Cipher = GetBytes(data, Key, IV);
            return Cipher;
        }

        //public static byte[] GetBytes(byte[] data, byte[] key, byte[] iv)
        //{
        //    byte[] encryptedData;

        //    using (SymmetricAlgorithm aes = SymmetricAlgorithm.Create())
        //    {
        //        aes.Mode = CipherMode.CBC;
        //        aes.Key = key;
        //        aes.IV = iv;
        //        aes.Padding = PaddingMode.PKCS7;
        //        ICryptoTransform encryptor = aes.CreateEncryptor();
        //        using (MemoryStream mStream = new MemoryStream())
        //        {
        //            using (CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
        //            {
        //                cStream.Write(data, 0, data.Length);
        //                cStream.FlushFinalBlock();
        //                encryptedData = mStream.ToArray();
        //            }
        //        }
        //    }

        //    return encryptedData;
        //}

    }
}
