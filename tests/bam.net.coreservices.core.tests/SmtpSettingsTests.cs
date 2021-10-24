using Bam.Net.CommandLine;
using Bam.Net.Messaging;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class SmtpSettingsTests: CommandLineTool
    {
        [ConsoleAction]
        public void CreateEmailTest()
        {
            SmtpSettings settings = new SmtpSettings
            {
                SmtpHost = "mail.privateemail.com",
                Port = 587,
                UserName = "support@threeheadz.com",
                From = "support@threeheadz.com",
                DisplayName = "Three Headz",
                EnableSsl = true
            };

            settings.Password = PasswordPrompt("Please enter the smtp password", ConsoleColor.Yellow);

            DataProviderSmtpSettingsProvider provider = new DataProviderSmtpSettingsProvider(settings);
            Email email = provider.CreateEmail();
            email
                .Subject("SmtpSettings test")
                .Body("This is a test from CoreServices.Tests")
                .To("bryan.apellanes@gmail.com")
                .Send();            
        }

    }
}
