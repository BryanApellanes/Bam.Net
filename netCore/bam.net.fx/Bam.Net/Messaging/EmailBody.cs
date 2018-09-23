using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Messaging
{
    public class EmailBody
    {
        public bool IsHtml { get; set; }
        public string Content { get; set; }
        public Email ToEmail()
        {
            return new Email().IsBodyHtml(IsHtml).Body(Content);
        }
        public static EmailBody FromEmail(Email email)
        {
            return new EmailBody { IsHtml = email.Config.IsBodyHtml, Content = email.Config.Body };
        }

        public static implicit operator EmailBody(string content)
        {
            return new EmailBody { Content = content };
        }
    }
}
