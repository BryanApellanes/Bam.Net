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
        Vault _smtpSettingsVault;
        public Vault SmtpSettingsVault
        {
            get
            {
                if (_smtpSettingsVault == null)
                {
                    _smtpSettingsVault = Vault.Load(SmtpSettingsVaultPath, "SmtpSettings");
                }
                return _smtpSettingsVault;
            }
            set
            {
                _smtpSettingsVault = value;
            }
        }

        string _smtpSettingsVaultPath;
        public string SmtpSettingsVaultPath
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

        public Email CreateEmail(string fromAddress = null, string fromDisplayName = null)
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

            string smtpHost = smtpHostSettings["SmtpHost"];
            string userName = smtpHostSettings["UserName"];
            string password = smtpHostSettings["Password"];
            string from = fromAddress ?? smtpHostSettings["From"];
            string displayName = fromDisplayName ?? smtpHostSettings["DisplayName"];
            int port = int.Parse(smtpHostSettings["Port"]);
            bool enableSsl = bool.Parse(smtpHostSettings["EnableSsl"]);

            return email.SmtpHost(smtpHost).UserName(userName).Password(password).From(from, displayName).Port(port).EnableSsl(enableSsl);
        }
    }
}
