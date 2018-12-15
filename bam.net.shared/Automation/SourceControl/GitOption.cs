using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.SourceControl
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GitOption: Attribute
    {
        public GitOption() { }
        public GitOption(string value, string description)
        {
            this.Value = value;
            this.Description = description;
        }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
