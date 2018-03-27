using Bam.Net.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Messaging
{
    public class SmtpSettings
    {
        public string SmtpHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }

        public static implicit operator SmtpSettings(Vault vault)
        {
            return FromVault(vault);
        }

        public static implicit operator Vault(SmtpSettings settings)
        {
            settings.Save(Vault.Application);
            return Vault.Application;
        }

        public static SmtpSettings FromVault(Vault smtpSettingsVault)
        {
            return new SmtpSettings
            {
                SmtpHost = smtpSettingsVault[nameof(SmtpHost)],
                UserName = smtpSettingsVault[nameof(UserName)],
                Password = smtpSettingsVault[nameof(Password)],
                From = smtpSettingsVault[nameof(From)],
                DisplayName = smtpSettingsVault[nameof(DisplayName)],
                Port = int.Parse(smtpSettingsVault[nameof(Port)]),
                EnableSsl = bool.Parse(smtpSettingsVault[nameof(EnableSsl)])
            };
        }

        public void Save()
        {
            Save(Vault.Application);
        }

        public void Save(Vault vault)
        {
            vault[nameof(SmtpHost)] = SmtpHost;
            vault[nameof(UserName)] = UserName;
            vault[nameof(Password)] = Password;
            vault[nameof(From)] = From;
            vault[nameof(DisplayName)] = DisplayName;
            vault[nameof(Port)] = Port.ToString();
            vault[nameof(EnableSsl)] = EnableSsl.ToString();
        }
    }
}
