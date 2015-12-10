/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;
using Naizari.Extensions;
using System.IO;
using System.Xml.Serialization;
using Naizari.Encryption;
using System.Reflection;

namespace Naizari.Configuration
{
    [Serializable]
    public class EncryptedConfig
    {
        public EncryptedConfig()
            : this(true)
        {
        }

        public EncryptedConfig(bool autoLoadKey)
        {
            this.keyedValues = new Dictionary<string, string>();
            this.KeyFile = new KeyVectorPair();
            if (PasswordFile.Exists && autoLoadKey)
            {
                this.KeyFile = KeyVectorPair.Load(PasswordFile.FullName);
            }
        }

        public KeyVectorPair KeyFile
        {
            get;
            set;
        }

        public void Save()
        {         
            FileInfo file = ConfigFile;//new FileInfo(FsUtil.GetCurrentUserAppDataFolder() + typeof(EncryptedConfig).Name);
            if (file.Exists)
                file.Delete();

            KeyVectorPair key = RijndaelEncryptor.Encrypt(this, file.FullName, PasswordFile.FullName);
        }

        public static EncryptedConfig Current
        {
            get
            {
                EncryptedConfig config = SingletonHelper.GetApplicationProvider<EncryptedConfig>();
                if (config == null)
                {
                    config = SingletonHelper.GetApplicationProvider<EncryptedConfig>(EncryptedConfig.Load());
                }

                return config;
            }
        }

        public static FileInfo PasswordFile
        {
            get
            {
                return new FileInfo(FsUtil.GetCurrentUserAppDataFolder() + "Password");
            }
        }

        static FileInfo configFile = new FileInfo(FsUtil.GetCurrentUserAppDataFolder() + typeof(EncryptedConfig).Name); 
        public FileInfo ConfigFile
        {
            get
            {
                return configFile;
            }
        }

        public static EncryptedConfig Load()
        {
            FileInfo file = PasswordFile;
            if (!PasswordFile.Exists)
            {
                throw new InvalidOperationException(string.Format("The password file ({0}) was not found.", PasswordFile.FullName));
            }

            EncryptedConfig config = RijndaelEncryptor.Decrypt<EncryptedConfig>(configFile.FullName, PasswordFile.FullName);
            PasswordFile.Delete();
            return config;
        }

        public void SetProperties(object target)
        {
            Type targetType = target.GetType();
            PropertyInfo[] properties = targetType.GetProperties();
            foreach(PropertyInfo propertyInfo in properties)
            {
                string propertyName = propertyInfo.Name;
                if(keyedValues.ContainsKey(propertyName))
                {
                    propertyInfo.SetValue(target, keyedValues[propertyName], null);
                }
            }
        }

        Dictionary<string, string> keyedValues;

        public void SetValue(string name, string value)
        {
            keyedValues.Add(name, value);
            RefreshValues();
        }

        public string GetValue(string name)
        {
            return keyedValues[name];
        }

        KeyValuePair[] values;
        public KeyValuePair[] Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
                RefreshKeyedValues();
            }
        }

        private void RefreshValues()
        {
            List<KeyValuePair> values = new List<KeyValuePair>();
            foreach (string key in keyedValues.Keys)
            {
                values.Add(new KeyValuePair { Key = key, Value = keyedValues[key] });
            }
            this.Values = values.ToArray();
        }

        private void RefreshKeyedValues()
        {
            foreach (KeyValuePair pair in Values)
            {
                if (!keyedValues.ContainsKey(pair.Key))
                    keyedValues.Add(pair.Key, pair.Value);
                else
                    keyedValues[pair.Key] = pair.Value;
            }
        }

    }
}
