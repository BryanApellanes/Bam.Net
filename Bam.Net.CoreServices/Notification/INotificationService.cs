using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Messaging;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.CoreServices
{
    public interface INotificationService
    {
        bool AddTemplate(NotificationTemplateInfo template);
        bool Notify(string recipientIdentifier, EmailBody emailBody, string subject = null, bool bypassRecipientValidation = false);
        bool NotifyRecipientEmail(string toEmail, EmailBody emailBody, string subject, string from = null, string fromDisplayName = null);
        bool NotifyUser(UserAccounts.Data.User user, EmailBody emailBody, string subject, string from = null, string fromDisplayName = null);
        bool TemplateNotify(string recipientIdentifier, string templateName, object data, string subject = null);
        bool TemplateNotify(string recipientIdendtifier, string templateName, string jsonData, string subject = null);
        bool TemplateNotify(ApplicationRegistration.Data.User user, string templateName, object data = null, string subject = null);
    }
}