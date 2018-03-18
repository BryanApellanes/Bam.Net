using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Encryption;
using Bam.Net.Messaging;

namespace Bam.Net.CoreServices
{
    public class DataSettingsSmtpSettingsProvider: SmtpSettingsProvider
    {
        public DataSettingsSmtpSettingsProvider()
        {
            DataSettings = DataSettings.Current;
        }

        public DataSettingsSmtpSettingsProvider(SmtpSettings smtpSettings, DataSettings dataSettings = null)
        {
            DataSettings = dataSettings ?? DataSettings.Current;
            SmtpSettings = smtpSettings;
            SmtpSettings.Save(SmtpSettingsVault);            
        }
        
        public DataSettings DataSettings { get; set; }

        SmtpSettings _smtpSettings;
        public SmtpSettings SmtpSettings
        {
            get
            {
                return _smtpSettings;
            }
            set
            {
                _smtpSettings = value;
                _smtpSettings.Save(SmtpSettingsVault);
            }
        }

        public override string SmtpSettingsVaultPath
        {
            get
            {
                return DataSettings.GetSysDatabasePathFor(typeof(SmtpSettings), "CoreServices");                
            }
            set
            {
                // dissallow overwriting
            }
        }
    }
}
