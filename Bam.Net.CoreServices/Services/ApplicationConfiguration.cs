using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

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

        public void Inject()
        {
            DefaultConfiguration.SetAppSettings(ToDictionary());
        }

        public void UnInject()
        {
            DefaultConfiguration.SetAppSettings();
        }
    }
}
