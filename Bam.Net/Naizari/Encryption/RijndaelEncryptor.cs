/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Naizari.Configuration;
using System.Xml.Serialization;
using System.Xml;

namespace Naizari.Encryption
{
    public static class RijndaelEncryptor
    {

        public static string Encrypt(string value, KeyVectorPair key)
        {
            return Encrypt(value, key.Key, key.IV);
        }

        public static string Encrypt(string value, string base64EncodedKey, string base64EncodedIV)
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.IV = Convert.FromBase64String(base64EncodedIV);
            rijndael.Key = Convert.FromBase64String(base64EncodedKey);

            ICryptoTransform encryptor = rijndael.CreateEncryptor();

            return Encrypt(value, encryptor);
        }

        public static string Encrypt(string value, ICryptoTransform encryptor)
        {
            MemoryStream encryptBuffer = new MemoryStream();
            CryptoStream encryptStream = new CryptoStream(encryptBuffer, encryptor, CryptoStreamMode.Write);
            byte[] data = Encoding.ASCII.GetBytes(value);
            encryptStream.Write(data, 0, data.Length);
            encryptStream.FlushFinalBlock();

            return Convert.ToBase64String(encryptBuffer.ToArray());
        }

        public static string Decrypt(string base64EncodedValue, KeyVectorPair key)
        {
            return Decrypt(base64EncodedValue, key.Key, key.IV);
        }

        public static string Decrypt(string base64EncodedValue, string base64EncodedKey, string base64EncodedIV)
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.IV = Convert.FromBase64String(base64EncodedIV);
            rijndael.Key = Convert.FromBase64String(base64EncodedKey);

            ICryptoTransform decryptor = rijndael.CreateDecryptor();

            byte[] encData = Convert.FromBase64String(base64EncodedValue);
            MemoryStream decryptBuffer = new MemoryStream(encData);
            CryptoStream decryptStream = new CryptoStream(decryptBuffer, decryptor, CryptoStreamMode.Read);

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

            return Encoding.ASCII.GetString(retBytes.ToArray());
        }

        public static KeyVectorPair Encrypt(this object target, string filePath)
        {
            return Encrypt(target, filePath, filePath + ".key", true);
        }

        public static KeyVectorPair Encrypt(this object target, string filePath, string keyFilePath)
        {
            return Encrypt(target, filePath, keyFilePath, true);
        }

        public static KeyVectorPair Encrypt(this object target, string filePath, string keyFilePath, bool writeKeyFile)
        {
            KeyVectorPair key;
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
                throw new FileNotFoundException(string.Format("The file specied to deserialize from {0} does not exist", filePath));

            if (!File.Exists(keyFile))
                throw new FileNotFoundException(string.Format("The key file specified {0} does not exist", keyFile));

            KeyVectorPair key = KeyVectorPair.Load(keyFile); //Serialization.Deserialize<Base64EncodedRijndaelKeyVectorPair>(keyFile);

            using (StreamReader sr = new StreamReader(filePath))
            {
                string text = sr.ReadToEnd();
                return Deserialize<T>(text, key);
            }
        }
        /// <summary>
        /// Get a base64 encoded encrypted xml serialization string representing the specified target object
        /// </summary>
        /// <param name="target">The object to serialize</param>
        /// <param name="key">The key used to encrypt and decrypt the resulting string</param>
        /// <returns>string</returns>
        public static string ToBase64EncodedEncryptedXml(this object target, out KeyVectorPair key)
        {
            string xml = SerializationUtil.GetXml(target);
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            rm.GenerateKey();
            key = new KeyVectorPair();
            key.Key = Convert.ToBase64String(rm.Key);
            key.IV = Convert.ToBase64String(rm.IV);
            return Encrypt(xml, rm.CreateEncryptor());
        }

        public static T Deserialize<T>(string base64EncryptedXmlString, KeyVectorPair key)
        {
            RijndaelManaged rKey = new RijndaelManaged();
            rKey.IV = Convert.FromBase64String(key.IV);
            rKey.Key = Convert.FromBase64String(key.Key);

            string xml = Decrypt(base64EncryptedXmlString, key.Key, key.IV);
            return SerializationUtil.FromXmlString<T>(xml);
        }
    }
}
