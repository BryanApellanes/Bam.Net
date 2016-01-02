/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Naizari.Testing;
using Naizari.Logging;
using System.IO;
using System.Net;

namespace Naizari.Helpers
{
    public class EmailHelper
    {
        public static string From = "noreply@domain.cxm";// fake domain to prevent the bounce of death
        public static string SmtpHost = string.Empty;

        public static void SendEmail(string subject, string messageBody, params string[] toRecipients)
        {
            SendEmail(subject, messageBody, toRecipients, new string[] { }, new string[] { });
        }

        public static void SendEmail(string subject, string messageBody, string[] toRecipients, FileInfo[] attachments)
        {
            SendEmail(subject, messageBody, toRecipients, new string[] { }, new string[] { }, attachments);
        }

        public static void SendEmail(string subject, string messageBody, string[] toRecipients, string[] ccRecipients)
        {
            SendEmail(subject, messageBody, toRecipients, ccRecipients, new string[] { });
        }

        public static void SendEmail(string subject, string messageBody, string[] toRecipients, string[] ccRecipients, FileInfo[] attachments)
        {
            SendEmail(subject, messageBody, toRecipients, ccRecipients, new string[] { }, attachments);
        }

        public static void SendEmail(string subject, string messageBody, string[] toRecipients, string[] ccRecipients, string[] bccRecipients)
        {
            SendEmail(subject, messageBody, toRecipients, ccRecipients, bccRecipients, null);
        }

        public static void SendEmail(string subject, string messageBody, string[] toRecipients, string[] ccRecipients, string[] bccRecipients, FileInfo[] attachments)
        {
            Expect.IsNotNull(toRecipients);
            if (toRecipients.Length == 0)
                return;

            MailMessage message = new MailMessage();
            foreach (string recipient in toRecipients)
            {
                Validate(recipient);
                message.To.Add(new MailAddress(recipient));
            }

            foreach (string recipient in ccRecipients)
            {
                Validate(recipient);
                message.CC.Add(new MailAddress(recipient));
            }

            foreach (string recipient in bccRecipients)
            {
                Validate(recipient);
                message.Bcc.Add(new MailAddress(recipient));
            }

            message.From = new MailAddress(From);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = string.Format("<font face='arial'>{0}</font>", messageBody.Replace("\r\n", "<br />"));

            if (attachments != null)
            {
                foreach (FileInfo file in attachments)
                {
                    message.Attachments.Add(new Attachment(file.FullName));
                }
            }

            Expect.IsNotNullOrEmpty(SmtpHost, "The EmailHelper.SmtpHost property was not set.");

            SmtpClient client = new SmtpClient(SmtpHost);
            //client.EnableSsl = true;
            try
            {
                if (From.Equals("noreply@domain.cxm"))
                    Log.Default.AddEntry("The EmailHelper.From property should be set to an application specific address.", LogEventType.Warning);

                client.Send(message);
            }
            catch (Exception ex)
            {
                Log.Default.AddEntry("An error occurred sending email.", ex);
            }

        }

        private static void Validate(string recipient)
        {
            if (!recipient.Contains("@"))
                throw ExceptionHelper.CreateException<InvalidOperationException>("Email address was not in the correct format: {0}", recipient);            
            
        }
    }
}
