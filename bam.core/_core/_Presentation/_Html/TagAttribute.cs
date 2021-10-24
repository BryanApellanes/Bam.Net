using System;

namespace Bam.Net.Presentation.Html
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TagAttribute: Attribute
    {
        public TagAttribute(string tagName = "div")
        {
            TagName = tagName;
        }
        public string TagName { get; set; }
    }
}