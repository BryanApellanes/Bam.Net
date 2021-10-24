using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Bam.Net.Presentation.Html
{
    public static class TagExtensions
    {
        public static Tag Attr(this Tag tag, string name, object value)
        {
            return tag.SetAttribute(name, value);
        }
        
        public static Tag ValueIf(this Tag tag, bool condition, object value)
        {
            return AttributeIf(tag, condition, "value", value);
        }
        
        public static Tag CheckedIf(this Tag tag, bool condition)
        {
            return AttributeIf(tag, condition, "checked", "checked");
        }

        public static Tag AttributeIf(this Tag tag, bool condition, string attributeName, object value)
        {
            if (condition)
            {
                tag.SetAttribute(attributeName, value);
            }

            return tag;
        }

        public static Tag Child(this Tag tag, Tag child)
        {
            return Child(tag, child.Render());
        }
        
        public static Tag Child(this Tag tag, string child)
        {
            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append(tag.Content);
            contentBuilder.Append(child);
            tag.Content = contentBuilder.ToString();
            return tag;
        }
        
        public static Tag ChildIf(this Tag tag, bool condition, Tag child)
        {
            if (condition)
            {
                StringBuilder contentBuilder = new StringBuilder();
                contentBuilder.Append(tag.Content);
                contentBuilder.Append(child.Render());
            }

            return tag;
        }

        public static Tag TextIf(this Tag tag, bool condition, string text)
        {
            if (condition)
            {
                tag.Content = text;
            }

            return tag;
        }

        public static Tag Text(this Tag tag, string text)
        {
            tag.Content = text;
            return tag;
        }

        public static Tag DataSetIf(this Tag tag, bool condition, string dataName, object value)
        {
            if (condition)
            {
                tag.DataSet(dataName, value);
            }

            return tag;
        }

        public static Tag BreakIf(this Tag tag, bool condition)
        {
            if (condition)
            {
                tag.Child("<br />");
            }

            return tag;
        }

        public static Tag Value(this Tag tag, object value)
        {
            return tag.SetAttribute("value", value);
        }
        
        public static Tag FirstChildIf(this Tag tag, bool condition, Tag child)
        {
            StringBuilder content = new StringBuilder();
            content.Append(child.Render());
            content.Append(tag.Content);
            tag.Content = content.ToString();
            return tag;
        }
    }
}