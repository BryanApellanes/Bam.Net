using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Presentation.Html;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation.Handlebars;
using Bam.Net.Encryption;

namespace Bam.Net.CoreServices
{
    [Serializable]
    [Proxy("notifySvc")]
    [ServiceSubdomain("notify")]
    public class NotificationService : ApplicationProxyableService, INotificationService
    {
        public const string SmtpSettingsFileName = "bam.smtp.settings.json";
        static NotificationService()
        {
            LoadSmtpSettings();
        }

        protected NotificationService() : base() { }

        public NotificationService(IUserManager userManager, SmtpSettingsProvider smtpSettingsProvider, ILogger logger)
        {
            UserManager = userManager;
            SmtpSettingsProvider = smtpSettingsProvider ?? DefaultSmtpSettingsProvider;
            Logger = logger ?? Log.Default;
            string emailTemplatesDirectory = DataSettings.Current.GetSysEmailTemplatesDirectory().FullName;
            NotificationTemplateDirectory = new DirectoryInfo(Path.Combine(DataSettings.Current.GetRootDataDirectory().FullName, "NotificationTemplates"));
            Templates = new HandlebarsDirectory(NotificationTemplateDirectory);
            Tld = "com";
            Templates.Reload();
        }

        private static void LoadSmtpSettings()
        {
            FileInfo smtpSettingsFile = new FileInfo(Path.Combine(DataSettings.Current.GetSysDataDirectory().FullName, SmtpSettingsFileName));
            Console.WriteLine("Trying to load smtp settings from file: {0}", smtpSettingsFile.FullName);
            if (smtpSettingsFile.Exists)
            {
                try
                {
                    SmtpSettings smtpSettings = smtpSettingsFile.FromJsonFile<SmtpSettings>();
                    DefaultSender = smtpSettings.From;
                    DefaultSmtpSettingsProvider = new DataSettingsSmtpSettingsProvider(smtpSettings);
                    //try { smtpSettingsFile.Delete(); } catch (Exception ex) { Log.Warn("Failed to delete smtp settings (attempting to delete for security reasons): {0}", ex.Message); }
                    //AppDomain.CurrentDomain.ProcessExit += (o, a) => smtpSettings.ToJsonFile(smtpSettingsFile);
                }
                catch (Exception ex)
                {
                    Log.Warn("Failed to load smtp settings file {0}: {1}", smtpSettingsFile.FullName, ex.Message);
                    Console.WriteLine("failed to load smtp settings: {0}", ex.Message);
                }
            }
            else
            {
                Log.Warn("The system smtp settings file was not present, notifications may not send correctly: {0}", smtpSettingsFile.FullName);
                Console.WriteLine("failed to load smtp settings, settings file not present: {0}", smtpSettingsFile.FullName);
            }
        }

        [Local]
        public static void SaveDefaultSmtpSettings(SmtpSettings settings)
        {
            FileInfo smtpSettingsFile = new FileInfo(Path.Combine(DataSettings.Current.GetSysDataDirectory().FullName, SmtpSettingsFileName));
            settings.ToJsonFile(smtpSettingsFile);
        }

        public static DataSettingsSmtpSettingsProvider DefaultSmtpSettingsProvider { get; set; }
        public SmtpSettingsProvider SmtpSettingsProvider { get; set; }
        public HandlebarsDirectory Templates { get; set; }
        public string Tld { get; set; }        
        public string DefaultSubject
        {
            get
            {
                return $"{ApplicationName} Notification";
            }
        }
        
        public static string DefaultSender
        {
            get;
            set;
        }

        public DirectoryInfo NotificationTemplateDirectory { get; set; }

        /// <summary>
        /// Saves the specified template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public virtual bool AddTemplate(NotificationTemplateInfo template)
        {
            return AddTemplate(template, out string ignore);
        }

        [Local]
        public bool AddTemplate(NotificationTemplateInfo template, out string filePath)
        {
            filePath = string.Empty;
            try
            {
                filePath = GetFilePath(template);
                NotificationTemplateInfo copy = template.CopyAs<NotificationTemplateInfo>();
                Templates.AddTemplate(copy.Name, copy.Content);
                copy.Content = null;
                copy.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error adding template {0}: {1}", ex, template?.PropertiesToLine(), ex.Message);
                return false;
            }
        }

        [Local]
        public bool TemplateNotify(User user, string templateName, object data = null, string subject = null)
        {
            Args.ThrowIfNull(user, "user");
            return TemplateNotify(user.Email, templateName, data, subject);
        }

        [RoleRequired("/CoreNotificationService/AccessDenied", "Admin")]
        public virtual bool TemplateNotify(string recipientIdendtifier, string templateName, string jsonData, string subject = null)
        {
            object data = string.IsNullOrEmpty(jsonData) ? new { } : JsonConvert.DeserializeObject(jsonData);
            return TemplateNotify(recipientIdendtifier, templateName, data, subject);
        }

        [Local]
        public bool TemplateNotify(string recipientIdentifier, string templateName, object data, string subject = null)
        {
            try
            {
                NotificationTemplateInfo template = GetTemplate(templateName);
                return Notify(
                    recipientIdentifier,
                    new EmailBody { IsHtml = template.IsHtml, Content = Templates.Render(template.Name, data) },
                    subject ?? template.Subject ?? DefaultSubject
                );
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error sending notification to ({0}) using template ({1}) with data ({2}): '{3}'", ex, recipientIdentifier, templateName, data?.PropertiesToLine() ?? "[null]", ex.Message);
                return false;
            }
        }

        [RoleRequired("/CoreNotificationService/AccessDenied", "Admin")]
        public virtual bool Notify(string recipientIdentifier, EmailBody emailBody, string subject = null, bool bypassRecipientValidation = false)
        {
            try
            {
                if (!bypassRecipientValidation)
                {
                    UserAccounts.Data.User user = ValidateRecipientOrDie(recipientIdentifier);
                    NotifyUser(user, emailBody, subject);
                }
                else
                {
                    NotifyRecipientEmail(recipientIdentifier, emailBody, subject);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error sending notification to ({0}): {1}", ex, recipientIdentifier, ex.Message);
                return false;
            }
        }

        [Local]
        public bool NotifyUser(UserAccounts.Data.User user, EmailBody emailBody, string subject, string from = null, string fromDisplayName = null)
        {
            return NotifyRecipientEmail(user.Email, emailBody, subject, from, fromDisplayName);
        }

        [Local]
        public bool NotifyRecipientEmail(string toEmail, EmailBody emailBody, string subject, string from = null, string fromDisplayName = null)
        {
            try
            {
                from = from ?? DefaultSender ?? $"no-reply@{ApplicationName}.{Tld}";
                fromDisplayName = fromDisplayName ?? from;
                Email email = DefaultSmtpSettingsProvider
                    .CreateEmail(from, fromDisplayName)
                    .To(toEmail)
                    .Subject(subject ?? DefaultSubject)
                    .IsBodyHtml(emailBody.IsHtml)
                    .Body(emailBody.Content);

                SendEmail(email);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error notifying email ({0}): {1}", ex, toEmail, ex.Message);
                return false;
            }
        }

        [Local]
        public override object Clone()
        {
            NotificationService clone = new NotificationService(UserManager, SmtpSettingsProvider, Logger);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);            
            return clone;
        }

        /// <summary>
        /// Sends the specified email.  Defined here for testing purposes
        /// </summary>
        /// <param name="email"></param>
        protected virtual void SendEmail(Email email)
        {
            email.Send();
        }

        private NotificationTemplateInfo GetTemplate(string templateName)
        {
            string filePath = Path.Combine(NotificationTemplateDirectory.FullName, $"{templateName}.json");
            if (File.Exists(filePath))
            {
                return filePath.FromJsonFile<NotificationTemplateInfo>();
            }
            throw new FileNotFoundException("Specified template was not found", filePath);
        }

        private string GetFilePath(NotificationTemplateInfo template)
        {
            string fileName = $"{template.Name}.json";
            return Path.Combine(NotificationTemplateDirectory.FullName, fileName);
        }

        private UserAccounts.Data.User ValidateRecipientOrDie(string identifier)
        {
            IUserManager userManager = GetUserManager();
            Database userDatabase = userManager.Property<DaoUserResolver>("DaoUserResolver", false)?.Database ?? Db.For<UserAccounts.Data.User>();
            if (!IsValidUserEmail(identifier, userDatabase, out UserAccounts.Data.User user))
            {
                if(!IsValidUserName(identifier, userDatabase, out user))
                {
                    if(!IsValidUserUuid(identifier, userDatabase, out user))
                    {
                        throw new ArgumentException($"Specified user not found: identifier={identifier}");
                    }
                }
            }

            return user;
        }

        protected internal bool IsValidUserEmail(string emailAddress, Database db, out UserAccounts.Data.User user)
        {
            user = UserAccounts.Data.User.FirstOneWhere(u => u.Email == emailAddress, db);
            return user != null;
        }

        protected internal bool IsValidUserName(string userName, Database db, out UserAccounts.Data.User user)
        {
            user = UserAccounts.Data.User.FirstOneWhere(u => u.UserName == userName, db);
            return user != null;
        }

        protected internal bool IsValidUserUuid(string uuid, Database db, out UserAccounts.Data.User user)
        {
            user = UserAccounts.Data.User.FirstOneWhere(u => u.Uuid == uuid, db);
            return user != null;
        }
    }
}
