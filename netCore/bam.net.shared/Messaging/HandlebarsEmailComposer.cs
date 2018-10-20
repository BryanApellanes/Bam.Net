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
    public partial class HandlebarsEmailComposer : EmailComposer
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
