using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class TemplateAttribute: Attribute
    {
        public TemplateAttribute()
        {
            Name = DefaultTemplateName;
        }
        public static string DefaultTemplateName => "Default";
        public string DirectoryPath { get; set; }

        public string Name { get; set; }
    }
}
