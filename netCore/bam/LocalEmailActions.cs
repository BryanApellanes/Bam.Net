using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Messaging;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class LocalEmailActions : CommandLineTestInterface
    {
        /// <summary>
        /// Sets the local SMTP settings.
        /// </summary>
        [ConsoleAction("localSetSmtpSettings", "set local smtp settings")]
        public void SetLocalSmtpSettings()
        {
            string smtpSettingsFile = GetArgument("setLocalSmtpSettings", "Please enter the path to the smtp settings json file");
            if (!File.Exists(smtpSettingsFile))
            {
                OutLineFormat("Specified smtp settings file doesn't exist: {0}", ConsoleColor.Magenta, smtpSettingsFile);
                return;
            }
            SmtpSettings settings = smtpSettingsFile.FromJsonFile<SmtpSettings>();
            if (string.IsNullOrEmpty(settings.Password))
            {
                settings.Password = PasswordPrompt("Please enter the smtp password", ConsoleColor.Yellow);
            }

            NotificationService.SetDefaultSmtpSettings(settings);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        [ConsoleAction("localSendEmail", "send an email using a local instance of NotificationService")]
        public void SendMessage()
        {
            string email = GetArgument("sendEmail", "Please enter the email address to send to");
            string subject = GetArgument("subject", "Please enter the message subject");
            string message = GetArgument("message", "Please enter the message body");
            string from = GetArgument("from", "Please enter the from display name");

            NotificationService svc = ServiceTools.GetService<NotificationService>();
            svc.NotifyRecipientEmail(email, message, subject, null, from);
        }
    }
}
