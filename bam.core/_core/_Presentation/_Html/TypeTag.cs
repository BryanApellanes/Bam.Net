using System;
using System.Collections.Generic;

namespace Bam.Net.Presentation.Html
{
    public class TypeTag: Tag
    {
        public TypeTag(Type type) : this(GetTagName(type), new {}, null)
        {
            Type = type;
            AddClass(GetClassName(type));
        }

        public TypeTag(string tagName, Func<Tag> content) : base(tagName, content)
        {
        }

        public TypeTag(string tagName, string content) : base(tagName, content)
        {
        }

        public TypeTag(string tagName, object attributes, Func<Tag> content) : base(tagName, attributes,
            content().Render())
        {
        }

        public TypeTag(string tagName, object attributes = null, object content = null) : base(tagName, attributes, content)
        {
        }

        public TypeTag(string tagName, Dictionary<string, object> attributes = null, object content = null) : base(tagName, attributes, content)
        {
        }

        private Type _type;

        public Type Type
        {
            get { return _type; }
            set
            {
                _type = value;
                Classes.Add(GetClassName(_type));
            }
        }

        public static string GetClassName(Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }

            if (type.HasCustomAttributeOfType<CssClassNameAttribute>(out CssClassNameAttribute classNameAttribute))
            {
                return classNameAttribute.ClassName;
            }

            return type.Name;
        }
        
        public static string GetTagName(Type type)
        {
            if (type.HasCustomAttributeOfType<TagAttribute>(out TagAttribute tagAttribute))
            {
                return tagAttribute.TagName;
            }

            return type.Name;
        }
    }
}