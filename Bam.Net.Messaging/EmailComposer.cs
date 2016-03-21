/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Bam.Net.Encryption;

namespace Bam.Net.Messaging
{
    public abstract class EmailComposer: IEmailComposer
    {
        public EmailComposer(string templateDirectory)
        {
            this.TemplateDirectory = new DirectoryInfo(templateDirectory);
        }

        public EmailComposer()
            : this(".")
        { }

        public DirectoryInfo TemplateDirectory
        {
            get;
            set;
        }

        public bool GetIsHtml(string emailName)
        {
            return GetTemplate(emailName).IsHtml;
        }

        public void SetEmailTemplate(string emailName, FileInfo file)
        {
            SetEmailTemplate(emailName, File.ReadAllText(file.FullName));
        }

        public void SetEmailTemplate(string emailName, string templateContent, bool isHtml = false)
        {
            if (!TemplateDirectory.Exists)
            {
                TemplateDirectory.Create();
            }

            string templatePath = GetTemplatePath(emailName);
            EmailTemplate template = new EmailTemplate { EmailName = emailName, IsHtml = isHtml, TemplateContent = templateContent };
            template.ToJsonFile(templatePath);
        }

        public bool TemplateExists(string emailName)
        {
            string templatePath = GetTemplatePath(emailName);
            return File.Exists(templatePath);
        }

        public Email Compose(string subject, string emailName, params object[] data)
        {
            return new Email().Subject(subject).IsBodyHtml(GetIsHtml(emailName)).Body(GetEmailBody(emailName, data));
        }

        public abstract string GetEmailBody(string emailName, params object[] data);

        protected internal virtual string GetTemplateContent(string emailName)
        {
            EmailTemplate template = GetTemplate(emailName);
            return template.TemplateContent;
        }

        Dictionary<string, EmailTemplate> _templateCache;
        private EmailTemplate GetTemplate(string emailName)
        {
            if (_templateCache == null)
            {
                _templateCache = new Dictionary<string, EmailTemplate>();
            }

            EmailTemplate template;
            if(_templateCache != null &&
                _templateCache.ContainsKey(emailName))
            {
                template = _templateCache[emailName];
            }
            else
            {
                string path = Path.Combine(TemplateDirectory.FullName, "{0}.json"._Format(emailName));
                Args.ThrowIf<FileNotFoundException>(!File.Exists(path), "Specified template doesn't exist: {0}", path);

                template = path.FromJsonFile<EmailTemplate>();
            }
            _templateCache.AddMissing(emailName, template);

            return template;
        }

        public static Email SetEmailSettings(Vault settingContainer, Email email)
        {
            string[] requiredSettingMessages;
            string[] extendedSettingMessages;

            bool requiredSettingsSet = Notify.ValidateRequiredSettings(settingContainer, out requiredSettingMessages);
            bool extendedSettingsSet = Notify.ValidateExtendedSettings(settingContainer, out extendedSettingMessages);

            Args.ThrowIf<RequiredSettingsNotSetException>(!requiredSettingsSet, "Required settings missing from specified vault: {0}", string.Join(", ", requiredSettingMessages));
            Args.ThrowIf<RequiredSettingsNotSetException>(!extendedSettingsSet, "Extended settings missing from specified vault: {0}", string.Join(", ", extendedSettingMessages));

            string smtpHost = settingContainer["SmtpHost"];
            string userName = settingContainer["UserName"];
            string password = settingContainer["Password"];
            string from = settingContainer["From"];
            string displayName = settingContainer["DisplayName"];
            int port = int.Parse(settingContainer["Port"]);
            bool enableSsl = bool.Parse(settingContainer["EnableSsl"]);

            return email.SmtpHost(smtpHost).UserName(userName).Password(password).From(from, displayName).Port(port).EnableSsl(enableSsl);
        }
        
        private string GetTemplatePath(string emailName)
        {
            string fileName = "{0}.json"._Format(emailName);
            string templatePath = Path.Combine(TemplateDirectory.FullName, fileName);
            return templatePath;
        }

    }
}
