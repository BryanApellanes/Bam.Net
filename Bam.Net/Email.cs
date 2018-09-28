/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Bam.Net
{
    public class Email
    {        
        public class Configuration
        {
            public Configuration()
            {
                this.To = new MailAddress[] { };
                this.Cc = new MailAddress[] { };
                this.Bcc = new MailAddress[] { };
            }

            public MailAddress From { get; set; }
            public MailAddress[] To { get; set; }
            public MailAddress[] Cc { get; set; }
            public MailAddress[] Bcc { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public Attachment[] Attachments { get; set; }
            public bool IsBodyHtml { get; set; }

            public int Port { get; set; }

            public bool EnablSsl { get; set; }
            public string SmtpHost { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public Email()
        {
            Config = new Configuration();
        }

        public Configuration Config
        {
            get;
            private set;
        }

        public Email From(string address)
        {
            return From(address, "");
        }

        public Email From(string address, string displayName)
        {
            return From(new MailAddress(address, displayName));
        }

        public Email From(MailAddress address)
        {
            Config.From = address;
            return this;
        }
        
        public Email To(params string[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            foreach (string address in addressesToAdd)
            {
                addresses.Add(new MailAddress(address));
            }

            return To(addresses.ToArray());
        }
        
        public Email To(string address, string displayName)
        {
            return To(new MailAddress(address, displayName));
        }

        public Email To(params MailAddress[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            addresses.AddRange(Config.To);
            addresses.AddRange(addressesToAdd);
            Config.To = addresses.ToArray();
            return this;
        }

        public Email Cc(string address, string displayName)
        {
            return Cc(new MailAddress(address, displayName));
        }

        public Email Cc(params string[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            foreach (string address in addressesToAdd)
            {
                addresses.Add(new MailAddress(address));
            }

            return Cc(addresses.ToArray());
        }

        public Email Cc(params MailAddress[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            addresses.AddRange(Config.Cc);
            addresses.AddRange(addressesToAdd);
            Config.Cc = addresses.ToArray();
            return this;
        }


        public Email Bcc(string address, string displayName)
        {
            return Bcc(new MailAddress(address, displayName));
        }

        public Email Bcc(params string[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            foreach (string address in addressesToAdd)
            {
                addresses.Add(new MailAddress(address));
            }

            return Bcc(addresses.ToArray());
        }

        public Email Bcc(params MailAddress[] addressesToAdd)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            addresses.AddRange(Config.Bcc);
            addresses.AddRange(addressesToAdd);
            Config.Bcc = addresses.ToArray();
            return this;
        }

        public Email Subject(string subject)
        {
            Config.Subject = subject;
            return this;
        }

        public Email Body(string body)
        {
            Config.Body = body;
            return this;
        }

        public Email AppendBodyFormat(string format, params object[] args)
        {
            return AppendBody(string.Format(format, args));
        }

        public Email AppendBody(string append)
        {
            StringBuilder body = new StringBuilder(Config.Body);
            body.Append(append);
            Config.Body = body.ToString();
            return this;
        }

        public Email IsBodyHtml(bool isHtml)
        {
            Config.IsBodyHtml = isHtml;
            return this;
        }

        public Email Port(int port)
        {
            Config.Port = port;
            return this;
        }

        public Email Attach(params string[] files)
        {
            List<FileInfo> infos = new List<FileInfo>();
            foreach (string file in files)
            {
                infos.Add(new FileInfo(file));
            }

            return Attach(infos.ToArray());
        }

        public Email Attach(params FileInfo[] files)
        {
            List<Attachment> attachments = new List<Attachment>();
            attachments.AddRange(Config.Attachments);
            foreach (FileInfo file in files)
            {
                attachments.Add(new Attachment(file.FullName));
            }
            Config.Attachments = attachments.ToArray();
            return this;
        }

        public Email Attach(params Attachment[] attachments)
        {
            List<Attachment> current = new List<Attachment>();
            current.AddRange(Config.Attachments);
            current.AddRange(attachments);
            Config.Attachments = current.ToArray();
            return this;
        }

        public Email EnableSsl(bool enable)
        {
            Config.EnablSsl = enable;
            return this;
        }

        /// <summary>
        /// The same as SmtpHost
        /// </summary>
        /// <param name="serverHostName"></param>
        /// <returns></returns>
        public Email Server(string serverHostName)
        {
            return SmtpHost(serverHostName);
        }

        public Email SmtpHost(string smtpHost)
        {
            Config.SmtpHost = smtpHost;
            return this;
        }

        public Email UserName(string userName)
        {
            Config.UserName = userName;
            return this;
        }

        public Email Password(string password)
        {
            Config.Password = password;
            return this;
        }

        public Email Send()
        {
            MailMessage _message = new MailMessage();
            _message.From = Config.From;
            _message.Subject = Config.Subject;
            _message.IsBodyHtml = Config.IsBodyHtml;
            _message.Body = Config.Body;
            
            foreach (MailAddress address in Config.To)
            {
                _message.To.Add(address);
            }

            foreach (MailAddress address in Config.Cc)
            {
                _message.CC.Add(address);
            }

            foreach (MailAddress address in Config.Bcc)
            {
                _message.Bcc.Add(address);
            }
            _message.IsBodyHtml = Config.IsBodyHtml;

            SmtpClient _client = new SmtpClient(Config.SmtpHost, Config.Port)
            {
                EnableSsl = Config.EnablSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Config.UserName, Config.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            _client.Send(_message);
            return this;
        }


    }
}
