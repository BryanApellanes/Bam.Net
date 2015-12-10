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
        public EmailComposer(string templateDirectory, string extension = ".txt")
        {
            this.TemplateDirectory = new DirectoryInfo(templateDirectory);
            this.FileExtension = extension;
            this.IsBodyHtml = true;
        }

        public EmailComposer()
            : this(".", ".txt")
        { }

        public DirectoryInfo TemplateDirectory
        {
            get;
            set;
        }
        
        string _fileExtension;
        public string FileExtension
        {
            get
            {
                return _fileExtension;
            }
            set
            {
                _fileExtension = value.StartsWith(".") ? value : ".{0}"._Format(value);
            }
        }

        public bool IsBodyHtml
        {
            get;
            set;
        }

        public void SetEmailTemplate(string emailName, System.IO.FileInfo file)
        {
            SetEmailTemplate(emailName, File.ReadAllText(file.FullName));
        }

        public void SetEmailTemplate(string emailName, string templateContent)
        {
            if (!TemplateDirectory.Exists)
            {
                TemplateDirectory.Create();
            }

            string templatePath = GetTemplatePath(emailName);
            using (StreamWriter sw = new StreamWriter(templatePath))
            {
                sw.Write(templateContent);
            }
        }

        private string GetTemplatePath(string emailName)
        {
            string fileName = "{0}{1}"._Format(emailName, FileExtension);
            string templatePath = Path.Combine(TemplateDirectory.FullName, fileName);
            return templatePath;
        }

        public bool TemplateExists(string emailName)
        {
            string templatePath = GetTemplatePath(emailName);
            return File.Exists(templatePath);
        }

        public Email Compose(string subject, string emailName, params object[] data)
        {
            return new Email().Subject(subject).IsBodyHtml(IsBodyHtml).Body(GetEmailBody(emailName, data));
        }

        public abstract string GetEmailBody(string emailName, params object[] data);

        protected internal virtual string GetTemplateContent(string emailName)
        {
            string path = Path.Combine(TemplateDirectory.FullName, "{0}{1}"._Format(emailName, FileExtension));
            Args.ThrowIf<FileNotFoundException>(!File.Exists(path), "Specified template doesn't exist: {0}", path);

            return File.ReadAllText(path);
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
    }
}
