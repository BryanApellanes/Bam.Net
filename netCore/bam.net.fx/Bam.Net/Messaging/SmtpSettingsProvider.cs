using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;

namespace Bam.Net.Messaging
{
    public class SmtpSettingsProvider : ISmtpSettingsProvider
    {
        public const string DefaultVaultName = "DefaultSmtpSettings";
        public const string SmtpHost = "SmtpHost";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string From = "From";
        public const string DisplayName = "DisplayName";
        public const string Port = "Port";
        public const string EnableSsl = "EnableSsl";

        public string[] SettingKeys
        {
            get
            {
                return new[] { SmtpHost, UserName, Password, From, DisplayName, Port, EnableSsl };
            }
        }

        public virtual Vault GetSmtpSettingsVault(string applicationName = null)
        {
            string vaultName = applicationName == null ? DefaultVaultName: $"{applicationName}-SmtpSettings";
            return Vault.Load(SmtpSettingsVaultPath, vaultName);
        }

        Vault _smtpSettingsVault;
        public virtual Vault SmtpSettingsVault
        {
            get
            {
                if (_smtpSettingsVault == null)
                {
                    _smtpSettingsVault = Vault.Load(SmtpSettingsVaultPath, DefaultVaultName);
                }
                return _smtpSettingsVault;
            }
            set
            {
                _smtpSettingsVault = value;
            }
        }

        string _smtpSettingsVaultPath;
        public virtual string SmtpSettingsVaultPath
        {
            get
            {
                return _smtpSettingsVaultPath;
            }
            set
            {
                _smtpSettingsVaultPath = value;
            }
        }

        public virtual Email CreateEmail(string fromAddress = null, string fromDisplayName = null)
        {
            Email result = new Email();
            SetSmtpHostSettings(SmtpSettingsVault, result, fromAddress, fromDisplayName);
            return result;
        }

        public static Email SetSmtpHostSettings(Vault smtpHostSettings, Email email, string fromAddress = null, string fromDisplayName = null)
        {
            string[] requiredSettingMessages;
            string[] extendedSettingMessages;

            bool requiredSettingsSet = Notify.ValidateRequiredSettings(smtpHostSettings, out requiredSettingMessages);
            bool extendedSettingsSet = Notify.ValidateExtendedSettings(smtpHostSettings, out extendedSettingMessages);

            Args.ThrowIf<RequiredSettingsNotSetException>(!requiredSettingsSet, "Required settings missing from specified vault: {0}", string.Join(", ", requiredSettingMessages));
            Args.ThrowIf<RequiredSettingsNotSetException>(!extendedSettingsSet, "Extended settings missing from specified vault: {0}", string.Join(", ", extendedSettingMessages));

            string smtpHost = smtpHostSettings[SmtpHost];
            string userName = smtpHostSettings[UserName];
            string password = smtpHostSettings[Password];
            string from = fromAddress ?? smtpHostSettings[From];
            string displayName = fromDisplayName ?? smtpHostSettings[DisplayName];
            int port = int.Parse(smtpHostSettings[Port]);
            bool enableSsl = bool.Parse(smtpHostSettings[EnableSsl]);
            return SetSmtpHostSettings(email, smtpHost, userName, password, from, displayName, port, enableSsl);
        }

        public static Email SetSmtpHostSettings(Email email, SmtpSettings settings)
        {
            return SetSmtpHostSettings(email, settings.SmtpHost, settings.UserName, settings.Password, settings.From, settings.DisplayName, settings.Port, settings.EnableSsl);
        }

        public static Email SetSmtpHostSettings(Email email, string smtpHost, string userName, string password, string from, string displayName, int port, bool enableSsl)
        {
            return email.SmtpHost(smtpHost).UserName(userName).Password(password).From(from, displayName).Port(port).EnableSsl(enableSsl);
        }
    }
}
