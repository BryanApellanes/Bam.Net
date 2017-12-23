using System.Collections.Generic;
using System.Linq;
using Bam.Net.Configuration;
using System.Collections.Specialized;
using Bam.Net.CoreServices.Configuration;

namespace Bam.Net.CoreServices
{
    public class ApplicationConfiguration
    {        
        public ApplicationConfiguration()
        {
            Settings = new List<SourcedConfigurationSetting>();
        }
        public string Name { get; set; }
        public List<SourcedConfigurationSetting> Settings { get; set; }
        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(SourcedConfigurationSetting setting in Settings)
            {
                result.AddMissing(setting.Key, setting.Value);
            }
            return result;
        }

        public SourcedConfigurationSetting this[string key]
        {
            get
            {
                return Settings.FirstOrDefault(c => c.Key.Equals(key));
            }
            set
            {
                SourcedConfigurationSetting setting = this[key];
                if(setting == null)
                {
                    Settings.Add(value);
                }
                else
                {
                    Settings.Remove(setting);
                    this[key] = value;
                }
            }
        }

        /// <summary>
        /// Adds the settings from the current ApplicationConfiguration instance
        /// to the DefaultConfiguration
        /// </summary>
        public void Inject()
        {
            NameValueCollection appSettings = DefaultConfiguration.GetAppSettings();
            Dictionary<string, string> settings = ToDictionary();
            foreach(string key in appSettings.AllKeys)
            {
                settings.AddMissing(key, appSettings[key]);
            }
            DefaultConfiguration.SetAppSettings(settings);
        }

        /// <summary>
        /// Undo the result of Inject and reset the inner appSettings to those 
        /// provided by ConfigurationManager.AppSettings
        /// </summary>
        public void UnInject()
        {
            DefaultConfiguration.SetAppSettings();
        }
    }
}
