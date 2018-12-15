/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;

namespace Bam.Net.Messaging
{
    public interface IEmailComposer
    {        
        Email Compose(string subject, string emailName, params object[] data);
        void SetEmailTemplate(string emailName, FileInfo file);
        void SetEmailTemplate(string emailName, string templateContent, bool isHtml = false);
        string GetEmailBody(string emailName, params object[] data);
    }
}
