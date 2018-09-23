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
    [Serializable]
    public class AesKeyVectorPair
    {
        public AesKeyVectorPair()
        {
            SetKeyAndIv();
        }

        static object _aesLock = new object();
        static volatile AesKeyVectorPair _key;
        public static AesKeyVectorPair AppKey
        {
            get
            {
                if (_key == null)
                {
                    string fileName = Path.Combine(Paths.Local, "appkey.aes");
                    if (File.Exists(fileName))
                    {
                        _key = AesKeyVectorPair.Load(fileName);
                    }
                    else
                    {
                        _key = new AesKeyVectorPair();
                        _key.Save(fileName);
                    }
                }

                return _key;
            }
        }

        private void SetKeyAndIv()
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();
            this.Key = Convert.ToBase64String(aes.Key);
            this.IV = Convert.ToBase64String(aes.IV);
        }

        public void Save(string filePath)
        {
            string xml = SerializationExtensions.ToXml(this);
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
            string xmlBase64 = Convert.ToBase64String(xmlBytes);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(xmlBase64);
            }
        }

        public static AesKeyVectorPair Load(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string xmlBase64 = sr.ReadToEnd();
                byte[] xmlBytes = Convert.FromBase64String(xmlBase64);
                string xml = Encoding.UTF8.GetString(xmlBytes);
                return SerializationExtensions.FromXml<AesKeyVectorPair>(xml);
            }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the initialization vector.
        /// </summary>
        /// <value>
        /// The iv.
        /// </value>
        public string IV { get; set; }

        /// <summary>
        /// Gets a Base64 encoded value representing the cypher of the specified
        /// value using the specified key.
        /// </summary>
        public string Encrypt(string value)
        {
            return Aes.Encrypt(value, this);
        }

        /// <summary>
        /// Decrypts the specified base64 encoded value.
        /// </summary>
        /// <param name="base64EncodedValue">The base64 encoded value.</param>
        /// <returns></returns>
        public string Decrypt(string base64EncodedValue)
        {
            return Aes.Decrypt(base64EncodedValue, this);
        }
    }
}
