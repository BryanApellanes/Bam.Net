/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using Bam.Net.Configuration;
using Bam.Net.Encryption;
using Bam.Net.ExceptionHandling;

namespace Bam.Net.Profiguration
{
    /// <summary>
    /// Represents encrypted configuration. (Pro - Con)figuration, get it ;) :b
    /// </summary>
    public class Profiguration
    {
        public const string EncryptAppSettingsKey = "EncryptAppSettings";
        public const string EncryptConnStringsKey = "EncryptConnStrings";

        public Profiguration()
        {
            this.AppSettings = new AppSettings();
            this.ConnectionStrings = new ConnectionStrings();
        }

        public AppSettings AppSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }

        NameValueCollection _preInjectAppSettings;
        ConnectionStringSettingsCollection _preInjectConnectionStrings;
        /// <summary>
        /// Inject the AppSettings and ConnectionStrings from this Profiguration into the
        /// DefaultConfiguration
        /// </summary>
        public void Inject()
        {
            SetAppSettings();

            SetConnectionStrings();
        }

        private void SetConnectionStrings()
        {
            _preInjectConnectionStrings = DefaultConfiguration.GetConnectionStrings();
            Dictionary<string, ConnectionStringSettings> connectionStrings = new Dictionary<string, ConnectionStringSettings>();
            foreach (ConnectionStringSettings conn in _preInjectConnectionStrings)
            {
                connectionStrings.Add(conn.Name, new ConnectionStringSettings(conn.Name, conn.ConnectionString, conn.ProviderName));
            }

            foreach (ConnectionStringValue conn in ConnectionStrings.Values)
            {
                ConnectionStringSettings val = new ConnectionStringSettings();
                val.Name = conn.ConnectionString.Key;
                val.ConnectionString = conn.ConnectionString.Value;
                val.ProviderName = conn.Provider;
                if (connectionStrings.ContainsKey(val.Name))
                {
                    connectionStrings[val.Name] = val;
                }
                else
                {
                    connectionStrings.Add(val.Name, val);
                }
            }

            DefaultConfiguration.SetConnectionStrings(connectionStrings.Values.ToArray());
        }

        private void SetAppSettings()
        {
            _preInjectAppSettings = DefaultConfiguration.GetAppSettings();
            NameValueCollection appSettings = new NameValueCollection();
            // copy the original values
            foreach (string key in _preInjectAppSettings.AllKeys)
            {
                appSettings[key] = _preInjectAppSettings[key];
            }
            // --

            // override with values from "this"
            foreach (string key in AppSettings.Keys)
            {
                appSettings[key] = AppSettings[key];
            }
            DefaultConfiguration.SetAppSettings(appSettings);
        }

        /// <summary>
        /// Revert the DefaultConfiguration back to the values from the default config file
        /// </summary>
        public void Revert()
        {
            DefaultConfiguration.SetAppSettings(_preInjectAppSettings);
            DefaultConfiguration.SetConnectionStrings(_preInjectConnectionStrings);
        }

        public void Save()
        {
            Save(DefaultFilePath);
        }

        public void Save(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            this.Encrypt(filePath);
        }

        public void CopyAppSetting(string key)
        {
            string value = DefaultConfiguration.GetAppSetting(key);
            if (!string.IsNullOrEmpty(value))
            {
                AppSettings[key] = value;                
            }
        }

        public void CopyConnectionString(string name)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
            {
                ConnectionStrings[name] = new ConnectionStringValue { ConnectionString = new KeyValuePair { Key = name, Value = settings.ConnectionString }, Provider = settings.ProviderName };
            }
        }

        /// <summary>
        /// Encrypts the appsetting values specified by the appsetting with the key EncryptAppSettings.
        /// Encrypts the connection strings specified by the appsetting with the key EncryptConnStrings.
        /// Values are expected to be comma and/or semi colon separated lists of keys or names.
        /// Saves the specified values to and injects values from Profiguration.Default
        /// </summary>
        public static void Initialize()
        {
            string appKeysString = DefaultConfiguration.GetAppSetting(EncryptAppSettingsKey);
            string connStringKeysString = DefaultConfiguration.GetAppSetting(EncryptConnStringsKey);

            Profiguration prof = Profiguration.Default;
            if (!string.IsNullOrEmpty(appKeysString))
            {
                string[] appSettingKeys = appKeysString.DelimitSplit(",", ";");
                foreach (string key in appSettingKeys)
                {
                    prof.CopyAppSetting(key);
                }
            }

            if (!string.IsNullOrEmpty(connStringKeysString))
            {
                string[] connStringNames = connStringKeysString.DelimitSplit(",", ";");
                foreach (string name in connStringNames)
                {
                    prof.CopyConnectionString(name);
                }
            }

            prof.Save();
            prof.Inject();
        }

        /// <summary>
        /// Load the default profiguration 
        /// </summary>
        /// <returns></returns>
        public static Profiguration Load()
        {
            return Load(DefaultFilePath);
        }

        public static Profiguration Load(string filePath)
        {
            return Aes.Decrypt<Profiguration>(filePath);
        }

        static Profiguration _default;
        static object _defaultLock = new object();
        public static Profiguration Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () =>
                {
                    Profiguration result = new Profiguration();
                    if (!File.Exists(DefaultFilePath))
                    {
                        result.Save();
                    }
                    else
                    {
                        result = Aes.Decrypt<Profiguration>(DefaultFilePath);
                    }
                    return result;
                });
            }
        }

        public static string DefaultFilePath
        {
            get
            {
                return Path.Combine(RuntimeSettings.AppDataFolder, ApplicationName, "App.prof");
            }
        }

        public static string ApplicationName
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            }
        }
    }
}
