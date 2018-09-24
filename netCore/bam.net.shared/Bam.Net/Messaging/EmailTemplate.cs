using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Messaging
{
    public class EmailTemplate
    {
        public string EmailName { get; set; }
        public bool IsHtml { get; set; }
        public string TemplateContent { get; set; }
    }
}
