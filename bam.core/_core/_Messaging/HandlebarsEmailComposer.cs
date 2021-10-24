using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Presentation.Html;
using Bam.Net.Presentation.Handlebars;

namespace Bam.Net.Messaging
{
    public partial class HandlebarsEmailComposer
    {
        public override string GetEmailBody(string emailName, params object[] data)
        {
            Args.ThrowIfNullOrEmpty(emailName, "emailName");
            Args.ThrowIfNull(data);

            object combined = data;            
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
    }
}
