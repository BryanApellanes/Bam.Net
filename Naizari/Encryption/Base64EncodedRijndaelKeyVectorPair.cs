/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Naizari.Configuration;
using System.IO;

namespace Naizari.Encryption
{
    [Serializable]
    public class KeyVectorPair
    {
        public KeyVectorPair()
        {
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            rm.GenerateIV();
            this.Key = Convert.ToBase64String(rm.Key);
            this.IV = Convert.ToBase64String(rm.IV);
        }

        public void Save(string filePath)
        {
            string xml = SerializationUtil.GetXml(this);
            byte[] xmlBytes = Encoding.ASCII.GetBytes(xml);
            string xmlBase64 = Convert.ToBase64String(xmlBytes);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(xmlBase64);
            }
        }

        public static KeyVectorPair Load(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string xmlBase64= sr.ReadToEnd();
                byte[] xmlBytes = Convert.FromBase64String(xmlBase64);
                string xml = Encoding.ASCII.GetString(xmlBytes);
                return SerializationUtil.FromXmlString<KeyVectorPair>(xml);
            }
        }

        public string Key { get; set; }
        public string IV { get; set; }
    }
}
