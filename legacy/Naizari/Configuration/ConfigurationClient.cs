/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KLGates.Configuration;
using KLGates.Encryption;
using System.Reflection;

namespace KLGates.Configuration.Service.SoapClient
{
    public static class ConfigurationClient
    {
        static ConfigurationServiceClient client;
        static ClientConfig config;
        static Dictionary<string, string> appSettings;
        static ConfigurationResponse lastResponse;
        static RSAKeyPair serverKey;
        static RijndaelKeyVectorPair clientKey;

        //static string masterEncryptedAppHash;

        public static ConfigurationResponse GetLastResponse()
        {
            return lastResponse;
        }

        public static void SetProperties(object target)
        {
            SetProperties(target, false);
        }

        public static void AbandonSettings()
        {
            client = null;
            config = null;
            appSettings = null;
            lastResponse = null;
            serverKey = null;
            clientKey = null;
        }

        public static void SetProperties(object target, bool throwIfMissingRequiredProperties)
        {
            SetProperties(target, throwIfMissingRequiredProperties, false);
        }

        public static void SetProperties(object target, bool throwIfMissingRequiredProperties, bool reload)
        {
            Type targetType = target.GetType();
            PropertyInfo[] properties = targetType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    string propertyValueFromConfig = GetValue(targetType.Name + "." + property.Name, reload);
                    if (string.IsNullOrEmpty(propertyValueFromConfig))
                        propertyValueFromConfig = GetValue(property.Name);

                    if (!string.IsNullOrEmpty(propertyValueFromConfig))
                        property.SetValue(target, propertyValueFromConfig, null);
                }
            }

            if (throwIfMissingRequiredProperties)
            {
                if (targetType.GetInterface("IHasRequiredProperties") != null)
                {
                    DefaultConfiguration.CheckRequiredProperties((IHasRequiredProperties)target);
                }
            }
        }

        public static void CopyProperties(object fromObject, object toObject)
        {
            Type fromType = fromObject.GetType();
            Type toType = toObject.GetType();
            foreach (PropertyInfo property in fromType.GetProperties())
            {
                if (property.PropertyType == typeof(string) && property.CanRead)
                {
                    string propValue = (string)property.GetValue(fromObject, null);
                    PropertyInfo toProp = toType.GetProperty(property.Name);
                    if (toProp != null && toProp.CanWrite)
                        toProp.SetValue(toObject, propValue, null);
                }
            }
        }

        static string currentAppName;
        public static string GetCurrentAppName()
        {
            if (!string.IsNullOrEmpty(currentAppName))
                return currentAppName;

            Init();
            lastResponse = client.GetApplicationName(serverKey.EncryptWithPublicKey(config.ApplicationHash));
            currentAppName = lastResponse.ApplicationName;
            return currentAppName;
        }

        public static string GetValue(string key)
        {
            return GetValue(key, false);
        }

        public static string GetValue(string key, bool reload)
        {
            if (appSettings != null &&
                appSettings.ContainsKey(key) && 
                !reload)
            {
                return appSettings[key];
            }
            else
            {
                Init();

                string masterEncryptedAppHash = serverKey.EncryptWithPublicKey(config.ApplicationHash);
                //RSAKeyPair tempKey = new RSAKeyPair();
                //RijndaelKeyVectorPair clientKey = GetClientKey(masterEncryptedAppHash, tempKey);

                ConfigurationResponse valueResponse = client.GetValue(masterEncryptedAppHash, RijndaelEncryptor.Encrypt(key, clientKey.Base64Key, clientKey.Base64InitializationVector));

                string retVal = string.Empty;
                if( !string.IsNullOrEmpty(valueResponse.SettingValue) )
                    retVal = RijndaelEncryptor.Decrypt(valueResponse.SettingValue, clientKey.Base64Key, clientKey.Base64InitializationVector);

                if (appSettings.ContainsKey(key))
                {
                    appSettings[key] = retVal;
                }
                else
                {
                    appSettings.Add(key, retVal);
                }

                lastResponse = valueResponse;
                return retVal;
            }            
        }

        public static AppKeyValue[] GetAppSettings()
        {
            Init();
            string masterEncryptedAppHash = serverKey.EncryptWithPublicKey(config.ApplicationHash);

            ConfigurationResponse response = client.GetApplicationSettings(masterEncryptedAppHash);
            if (response.Result == ConfigurationResult.Error ||
                response.Result == ConfigurationResult.ApplicationNotRegistered)
                throw new Exception(response.Message);

            List<AppKeyValue> appSettings = new List<AppKeyValue>();
            foreach (AppKeyValue setting in response.AppSettings)
            {
                setting.Key = RijndaelEncryptor.Decrypt(setting.Key, clientKey.Base64Key, clientKey.Base64InitializationVector);
                setting.Value = RijndaelEncryptor.Decrypt(setting.Value, clientKey.Base64Key, clientKey.Base64InitializationVector);
                appSettings.Add(setting);
            }

            return appSettings.ToArray();
        }

        public static ConfigurationResponse SetValue(string key, string value)
        {
            Init();
            //RSAKeyPair serverKey = GetServerKey();
            string masterEncryptedAppHash = serverKey.EncryptWithPublicKey(config.ApplicationHash);
            //RSAKeyPair tempKey = new RSAKeyPair();
            //RijndaelKeyVectorPair clientKey = GetClientKey(masterEncryptedAppHash, tempKey);

            string encryptedKey = RijndaelEncryptor.Encrypt(key, clientKey.Base64Key, clientKey.Base64InitializationVector);
            string newValue = RijndaelEncryptor.Encrypt(value, clientKey.Base64Key, clientKey.Base64InitializationVector);

            lastResponse = client.SetValue(masterEncryptedAppHash, encryptedKey, newValue);
            return lastResponse;
        }

        private static RSAKeyPair GetServerKey()
        {
            if (serverKey == null)
            {
                serverKey = new RSAKeyPair();
                serverKey.PrivateKeyXml = string.Empty;
                ConfigurationResponse response = client.GetServerKey();
                if (response.Result == ConfigurationResult.Error)
                {
                    lastResponse = response;
                    return null;
                }
                serverKey.PublicKeyXml = response.ServerKey;
            }

            return serverKey;
        }

        private static RijndaelKeyVectorPair GetClientKey(string masterEncryptedAppHash, RSAKeyPair tempKey)
        {
            if (clientKey == null)
            {
                ConfigurationResponse clientKeyResponse = client.GetClientKey(masterEncryptedAppHash, tempKey.PublicKeyXml);
                clientKey = clientKeyResponse.ClientKey;
                if (clientKey != null)
                {
                    clientKey.Base64InitializationVector = tempKey.DecryptWithPrivateKey(clientKey.Base64InitializationVector);
                    clientKey.Base64Key = tempKey.DecryptWithPrivateKey(clientKey.Base64Key);
                }
            }
            return clientKey;
        }

        public static ClientConfig Config
        {
            get { return config; }
            set { config = value; }
        }

        public static void Init()
        {
            if (Config == null)
            {
                Config = new ClientConfig();
                DefaultConfiguration.SetProperties(Config);
            }
            Init(Config);
        }

        public static void Init(ClientConfig config)
        {
            if (appSettings == null)
                appSettings = new Dictionary<string, string>();
            
            client = ClientFactory.GetConfigurationServiceClient(config.ConfigurationServer, config.ConfigurationServerPort);
            if (GetServerKey() == null)
                throw new InvalidOperationException("Unable to retrieve server key");

            //masterEncryptedAppHash = serverKey.EncryptWithPublicKey(config.ApplicationHash);

            if (clientKey == null)
            {
                string masterEncryptedAppHash = serverKey.EncryptWithPublicKey(config.ApplicationHash);
                RSAKeyPair tempKey = new RSAKeyPair();
                clientKey = GetClientKey(masterEncryptedAppHash, tempKey);
            }

            // if the clientKey is still null
            // we were unable to get it from the service
            if (clientKey == null)
                throw new InvalidOperationException("Unable to retrieve client key.  Make sure that you are using the correct application hash");
        }

        /// <summary>
        /// Invokes methods declared in the ConfigurationServer for the current application.  
        /// </summary>
        /// <param name="target">The object instance to invoke methods on</param>
        /// <remarks>
        /// The keys 
        /// should be in the format &lt;Type.Name&gt;.InvokeMethod.&lt;MethodName&gt;.&lt;Order as int&gt;.  For
        /// example: PatternsDatabaseSetupPermissionsInfo.InvokeMethod.AddStandardDbOwnerGroup.0.  The trailing number
        /// determines the order of invocation relative to all other method invocations from configuration.
        /// The values should be a comma separated list of string parameters to be passed to the method upon invocation.
        /// Any additional spaces in the comma separated list of values will not be trimmed so the invoked methods
        /// should handle that possibility accordingly.
        /// </remarks>
        public static void InvokeConfigMethods(object target)
        {
            AppKeyValue[] appSettings = GetAppSettings();
            Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
            foreach (AppKeyValue keyValuePair in appSettings)
            {
                settingsDictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            DefaultConfiguration.InvokeConfigMethods(target, settingsDictionary);
        }

        /// <summary>
        /// Represents the base configuration settings required in the local default configuration file.
        /// The presence of these settings locally will enable the use of the ConfigurationClient by the
        /// current application.
        /// </summary>
        public class ClientConfig: IHasRequiredProperties
        {
            public string[] RequiredProperties
            {
                get { return new string[] { "ConfigurationServer", "ApplicationHash" }; }
            }

            public string ConfigurationServer { get; set; }
            public string ConfigurationServerPort { get; set; }
            public string ApplicationHash { get; set; }
        }
    }
}
