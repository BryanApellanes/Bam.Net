using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class SourcedConfigurationSetting
    {
        public SettingSource SettingSource { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
