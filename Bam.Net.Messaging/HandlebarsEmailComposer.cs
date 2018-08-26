using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HandlebarsDotNet;
using Bam.Net.Presentation.Html;
using Bam.Net.Presentation.Handlebars;

namespace Bam.Net.Messaging
{
    public class HandlebarsEmailComposer : EmailComposer
    {
        public HandlebarsEmailComposer()
        {
            FileExtension = "hbs";
            SetTemplateDirectory("HandlebarsEmailTemplates");
        }

        public HandlebarsDirectory Templates { get; set; }
        
        public override string[] GetTemplateNames()
        {
            return Templates.Directory.GetFiles($"*.{FileExtension}").Select(fi => Path.GetFileNameWithoutExtension(fi.Name)).ToArray();
        }

        public override bool TemplateExists(string emailName)
        {
            return new FileInfo(GetFileName(emailName)).Exists;
        }

        public override string GetEmailBody(string emailName, params object[] data)
        {
            Args.ThrowIfNullOrEmpty(emailName, "emailName");
            Args.ThrowIfNull(data);

            object combined = data;
            if(data.Length > 0)
            {
                combined = data[0].Combine(data);
            }
            if (!Templates.Templates.ContainsKey(emailName))
            {
                Templates.Reload();
            }
            if (!Templates.Templates.ContainsKey(emailName))
            {
                Args.Throw<InvalidOperationException>("Specified email not found: {0}", emailName);
            }
            return Templates.Templates[emailName](combined);
        }

        protected internal override string GetTemplateContent(string emailName)
        {
            string fileName = GetFileName(emailName);
            FileInfo file = new FileInfo(Path.Combine(Templates.Directory.FullName, fileName));
            if (file.Exists)
            {
                return file.ReadAllText();
            }
            return string.Empty;
        }

        private string GetFileName(string emailName)
        {
            return emailName.EndsWith($".{FileExtension}") ? emailName : $"{emailName}.{FileExtension}";
        }
    }
}
