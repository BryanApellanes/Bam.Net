using System;

namespace Bam.Net.Presentation.Html
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CssClassNameAttribute:Attribute
    {
        public CssClassNameAttribute(string className)
        {
            ClassName = className;
        }
        
        public string ClassName { get; set; }
    }
}