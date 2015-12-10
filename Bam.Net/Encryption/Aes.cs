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
    public static class Aes
    {
        /// <summary>
        /// Gets a Base64 encoded value representing the cypher of the specified
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            return Encrypt(value, AesKeyVectorPair.AppKey);
        }

        /// <summary>
        /// Gets a Base64 encoded value representing the cypher of the specified
        /// value using the specified key.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string value, AesKeyVectorPair key)
        {
            return Encrypt(value, key.Key, key.IV);
        }

        public static string Encrypt(string value, string base64EncodedKey, string base64EncodedIV)
        {
            AesManaged aes = new AesManaged();
            aes.IV = Convert.FromBase64String(base64EncodedIV);
            aes.Key = Convert.FromBase64String(base64EncodedKey);

            ICryptoTransform encryptor = aes.CreateEncryptor();

            return Encrypt(value, encryptor);
        }

        private static string Encrypt(string value, ICryptoTransform encryptor)
        {
            using(MemoryStream encryptBuffer = new MemoryStream())
            {
                using (CryptoStream encryptStream = new CryptoStream(encryptBuffer, encryptor, CryptoStreamMode.Write))
                {
                    byte[] data = Encoding.UTF8.GetBytes(value);
                    encryptStream.Write(data, 0, data.Length);
                    encryptStream.FlushFinalBlock();

                    return Convert.ToBase64String(encryptBuffer.ToArray());
                }
            }
        }

        public static string Decrypt(string base64EncodedValue)
        {
            return Decrypt(base64EncodedValue, AesKeyVectorPair.AppKey);
        }

        public static string Decrypt(string base64EncodedValue, AesKeyVectorPair key)
        {
            return Decrypt(base64EncodedValue, key.Key, key.IV);
        }

        public static string Decrypt(string base64EncodedValue, string base64EncodedKey, string base64EncodedIV)
        {
            AesManaged aes = new AesManaged();
            aes.IV = Convert.FromBase64String(base64EncodedIV);
            aes.Key = Convert.FromBase64String(base64EncodedKey);

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] encData = Convert.FromBase64String(base64EncodedValue);
            using (MemoryStream decryptBuffer = new MemoryStream(encData))
            {

                using(CryptoStream decryptStream = new CryptoStream(decryptBuffer, decryptor, CryptoStreamMode.Read))
                {
                    byte[] decrypted = new byte[encData.Length];

                    decryptStream.Read(decrypted, 0, encData.Length);

                    // This seems like a cheesy way to remove trailing 0 bytes
                    // but unless I know the expected length of the decrypted data
                    // I can't think of another way to do this effectively
                    List<byte> retBytes = new List<byte>();
                    foreach (byte b in decrypted)
                    {
                        if (b == 0)
                            break;
                        //{
                        retBytes.Add(b);
                        //}
                    }

                    return Encoding.UTF8.GetString(retBytes.ToArray());
                }
            }
        }

        public static AesKeyVectorPair Encrypt(this object target, string filePath)
        {
            return Encrypt(target, filePath, filePath + ".key", true);
        }

        public static AesKeyVectorPair Encrypt(this object target, string filePath, string keyFilePath)
        {
            return Encrypt(target, filePath, keyFilePath, true);
        }

        public static AesKeyVectorPair Encrypt(this object target, string filePath, string keyFilePath, bool writeKeyFile)
        {
            AesKeyVectorPair key;
            string text = ToBase64EncodedEncryptedXml(target, out key);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(text);
            }

            if (writeKeyFile)
            {
                key.Save(keyFilePath);
            }
            return key;
        }

        public static T Decrypt<T>(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            string keyFile = info.FullName + ".key";
            return Decrypt<T>(filePath, keyFile);
        }

        public static T Decrypt<T>(string filePath, string keyFile)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("The file specified to deserialize from {0} does not exist", filePath));

            if (!File.Exists(keyFile))
                throw new FileNotFoundException(string.Format("The key file specified {0} does not exist", keyFile));

            AesKeyVectorPair key = AesKeyVectorPair.Load(keyFile); //Serialization.Deserialize<Base64EncodedRijndaelKeyVectorPair>(keyFile);

            return Decrypt<T>(filePath, key);
        }

        public static T Decrypt<T>(string filePath, AesKeyVectorPair key)
        {
            string text;
            using (StreamReader sr = new StreamReader(filePath))
            {
                text = sr.ReadToEnd();                
            }
            return Deserialize<T>(text, key);
        }
        
        /// <summary>
        /// Get a base64 encoded encrypted xml serialization string representing the specified target object
        /// </summary>
        /// <param name="target">The object to serialize</param>
        /// <param name="key">The key used to encrypt and decrypt the resulting string</param>
        /// <returns>string</returns>
        public static string ToBase64EncodedEncryptedXml(this object target, out AesKeyVectorPair key)
        {
            string xml = SerializationExtensions.ToXml(target);
            AesManaged rm = new AesManaged();
            rm.GenerateIV();
            rm.GenerateKey();
            key = new AesKeyVectorPair();
            key.Key = Convert.ToBase64String(rm.Key);
            key.IV = Convert.ToBase64String(rm.IV);
            return Encrypt(xml, rm.CreateEncryptor());
        }

        public static T Deserialize<T>(string base64EncryptedXmlString, AesKeyVectorPair key)
        {
            AesManaged rKey = new AesManaged();
            rKey.IV = Convert.FromBase64String(key.IV);
            rKey.Key = Convert.FromBase64String(key.Key);

            string xml = Decrypt(base64EncryptedXmlString, key.Key, key.IV);
            return SerializationExtensions.FromXml<T>(xml);
        }
    }
}
