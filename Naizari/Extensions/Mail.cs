/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Naizari.Configuration;

namespace Naizari.Extensions
{
    public static class Mail
    {
        public static MailMessage Merge(this MailMessage messageOne, MailMessage message, bool mergeBody = false)
        {
            return Merge(messageOne, message, mergeBody, false);
        }

        public static MailMessage Merge(this MailMessage messageOne, MailMessage message, string bodySeparator)
        {
            return Merge(messageOne, message, true, bodySeparator, false, "");
        }
        
        public static MailMessage Merge(this MailMessage messageOne, MailMessage message, bool mergeBody, bool mergeSubject)
        {
            string bodySeparator = messageOne.IsBodyHtml ? "<br /><br />": "\r\n\r\n";
            return Merge(messageOne, message, mergeBody, bodySeparator, mergeSubject, " :: ");
        }

        public static MailMessage Merge(this MailMessage messageOne, MailMessage message, bool mergeBody, string bodySeparator, bool mergeSubject, string subjectSeparator)
        {
            MailMessage mergedMessage = new MailMessage();
            DefaultConfiguration.CopyProperties(messageOne, mergedMessage);

            foreach (MailAddress address in messageOne.To)
            {
                mergedMessage.To.Add(address);
            }

            foreach (MailAddress address in message.To)
            {
                mergedMessage.To.Add(address);
            }

            foreach (MailAddress address in message.CC)
            {
                mergedMessage.CC.Add(address);
            }

            foreach (MailAddress address in message.Bcc)
            {
                mergedMessage.Bcc.Add(address);
            }

            if (mergeBody)
            {
                StringBuilder newBody = new StringBuilder(mergedMessage.Body);
                newBody.AppendFormat("{0}{1}", bodySeparator, message.Body);
                mergedMessage.Body = newBody.ToString();
            }

            if (mergeSubject)
            {
                StringBuilder newSubject = new StringBuilder(mergedMessage.Subject);
                newSubject.AppendFormat("{0}{1}", subjectSeparator, message.Subject);
                mergedMessage.Subject = newSubject.ToString();
            }

            return mergedMessage;
        }
    }
}
