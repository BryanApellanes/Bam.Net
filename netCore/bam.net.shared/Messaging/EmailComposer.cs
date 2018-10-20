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
        public EmailComposer()
            : this(".")
        { }

        public EmailComposer(string templateDirectory)
        {
            FileExtension = "json";
            TemplateDirectory = new DirectoryInfo(templateDirectory);
        }

        public string FileExtension { get; protected set; }
        public DirectoryInfo TemplateDirectory
        {
            get;
            set;
        }
        public virtual void SetTemplateDirectory(string directory)
        {
            TemplateDirectory = new DirectoryInfo(directory);
        }
        public bool GetIsHtml(string emailName)
        {
            return GetTemplate(emailName).IsHtml;
        }

        public virtual void SetEmailTemplate(string emailName, FileInfo file)
        {
            SetEmailTemplate(emailName, File.ReadAllText(file.FullName));
        }

        public virtual void SetEmailTemplate(string emailName, string templateContent, bool isHtml = false)
        {
            if (!TemplateDirectory.Exists)
            {
                TemplateDirectory.Create();
            }

            string templatePath = GetTemplatePath(emailName);
            EmailTemplate template = new EmailTemplate { EmailName = emailName, IsHtml = isHtml, TemplateContent = templateContent };
            template.ToJsonFile(templatePath);
        }

        public virtual bool TemplateExists(string emailName)
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
        public virtual string[] GetTemplateNames()
        {
            return TemplateDirectory.GetFiles($"*.{FileExtension}").Select(fi => Path.GetFileNameWithoutExtension(fi.Name)).ToArray();
        }

        Dictionary<string, EmailTemplate> _templateCache; 
        object _cacheLock = new object();
        public virtual EmailTemplate GetTemplate(string templateName)
        {
            lock (_cacheLock)
            {
                if (_templateCache == null)
                {
                    _templateCache = new Dictionary<string, EmailTemplate>();
                }

                EmailTemplate template;
                if (_templateCache != null &&
                    _templateCache.ContainsKey(templateName))
                {
                    template = _templateCache[templateName];
                }
                else
                {
                    template = LoadTemplate(templateName);
                }
                _templateCache.AddMissing(templateName, template);

                return template;
            }
        }

        /// <summary>
        /// Loads the email template with the specified templateName
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        protected internal virtual EmailTemplate LoadTemplate(string templateName)
        {
            EmailTemplate template;
            string path = Path.Combine(TemplateDirectory.FullName, $"{templateName}.{FileExtension}");
            Args.ThrowIf<FileNotFoundException>(!File.Exists(path), "Specified email template doesn't exist: {0}", path);

            template = path.FromJsonFile<EmailTemplate>();
            return template;
        }

        public static Email SetSmtpHostSettings(Vault smtpHostSettings, Email email, string fromAddress = null, string fromDisplayName = null)
        {
            return SmtpSettingsProvider.SetSmtpHostSettings(smtpHostSettings, email, fromAddress, fromDisplayName);
        }
        
        /// <summary>
        /// Gets the file path for the specified templateName
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        protected virtual string GetTemplatePath(string templateName)
        {
            string fileName = $"{templateName}.{FileExtension}";
            string templatePath = Path.Combine(TemplateDirectory.FullName, fileName);
            return templatePath;
        }
    }
}
