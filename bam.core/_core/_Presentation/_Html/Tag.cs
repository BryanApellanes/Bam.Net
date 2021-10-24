/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using Bam.Net;
using Bam.Net.ServiceProxy;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;
using Newtonsoft.Json.Schema;

namespace Bam.Net.Presentation.Html
{

    public partial class Tag
    {
        public Tag(string tagName) : this(tagName, new Dictionary<string, object>(), new object())
        {
        }

        public Tag(string tagName, Func<Tag> content) : this(tagName, content().Render())
        {
        }

        public Tag(string tagName, object attributes, Func<Tag> content = null) : this(tagName, attributes, content().Render())
        {
        }
        
        public Tag(string tagName, string content) : this(tagName, null, content)
        {
        }

        public Tag(string tagName, object attributes = null, object content = null) : this(tagName,
            attributes is Dictionary<string, object> ? (Dictionary<string, object>)attributes: attributes?.ToDictionary(), content)
        {
        }

        public Tag(string tagName, Dictionary<string, object> attributes = null, object content = null)
        {
            TagName = tagName;
            Attributes = attributes ?? new Dictionary<string, object>();
            Styles = new Dictionary<string, object>();
            Classes = new HashSet<string>();
            Content = content?.ToString();
        }
        
        public Tag(string tagName, params Func<Tag>[] contents)
        {
            TagName = tagName;
            Attributes = new Dictionary<string, object>();
            Styles = new Dictionary<string, object>();
            Classes = new HashSet<string>();
            StringBuilder contentBuilder = new StringBuilder();
            foreach (Func<Tag> content in contents)
            {
                contentBuilder.Append(content().Render());
            }

            Content = contentBuilder.ToString();
        }

        public Tag(string tagName, params Tag[] contents)
        {
            TagName = tagName;
            Attributes = new Dictionary<string, object>();
            Styles = new Dictionary<string, object>();
            Classes = new HashSet<string>();
            StringBuilder contentBuilder = new StringBuilder();
            foreach (Tag tag in contents)
            {
                contentBuilder.Append(tag.Render());
            }

            Content = contentBuilder.ToString();
        }

        public Tag FirstChild(Tag tag)
        {
            return BeforeContent(tag.Render());
        }

        public Tag BeforeContent(string content)
        {
            Content = content + Content;
            return this;
        }
        
        public Tag AddChildren(params Func<Tag>[] children)
        {
            return AddChildren(children.Select(f => f()).ToArray());
        }

        public Tag AddChildren(params Tag[] children)
        {
            StringBuilder childBuilder =  new StringBuilder();
            childBuilder.Append(Content);
            foreach(Tag tag in children)
            {
                childBuilder.Append(tag.Render());
            }

            Content = childBuilder.ToString();
            return this;
        }
        
        public static Tag For(Type type)
        {
            return new TypeTag(type);
        }

        public static Tag RadioList(Type enumType, object selected, string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = 6.RandomLetters();
            }

            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            Tag list = Tags.Ul(new {id}).Css("list-style", "none");
            List<Tag> items = new List<Tag>();
            bool first = true;
            foreach (FieldInfo field in fields)
            {
                object enumValue = field.GetRawConstantValue();
                string enumString = field.Name.PascalSplit(" ");
                string radioId = new StringBuilder(field.Name).Append("_".RandomString(4)).ToString();

                bool selectedCondition = selected != null ? field.Name.Equals(selected.ToString()) : first;
                items.Add(Tags.Li(() => Tags.Input(new {type = "radio", name = enumType.Name, value = enumValue.ToString()})
                    .SetAttribute("data-text", field.Name)
                    .CheckedIf(selectedCondition))
                );
            }

            list.AddChildren(items.ToArray());
            return list;
        }
        
        public string TagName { get; set; }
        public Dictionary<string, object> Attributes { get; private set; }
        public Dictionary<string, object> Styles { get; private set; }
        public HashSet<string> Classes { get; set; }
        protected internal string Content { get; set; }
        
        public string InnerText => Content.HtmlEncode();

        public string InnerHtml => Content;
        
        public Tag Id(string id)
        {
            SetAttribute("id", id);
            return this;
        }
        
        public Tag SetStyles(object styles)
        {
            return SetStyles(styles.ToDictionary());
        }

        public Tag SetStyles(Dictionary<string, object> styles)
        {
            Styles = styles;
            return this;
        }

        public Tag Css(string className)
        {
            return AddClass(className);
        }
        
        public Tag Css(string key, object value)
        {
            return AddStyle(key, value);
        }
        
        /// <summary>
        /// Adds the specified style if it has not already been added.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Tag AddStyle(string key, object value)
        {
            Styles.AddMissing(key, value);
            return this;
        }
        
        public Tag AddStyles(object styles)
        {
            return AddStyles(styles.ToDictionary());
        }

        /// <summary>
        /// Adds the specified styles if they have not already been set.
        /// </summary>
        /// <param name="styles"></param>
        /// <returns></returns>
        public Tag AddStyles(Dictionary<string, object> styles)
        {
            foreach (string key in styles.Keys)
            {
                Styles.AddMissing(key, styles[key]);
            }

            return this;
        }

        public Tag AddClass(string className)
        {
            return AddClasses(className);
        }

        public Tag AddClasses(params string[] classNames)
        {
            foreach (string className in classNames)
            {
                Classes.Add(className);
            }

            return this;
        }

        public Tag AddAttributes(object attributes)
        {
            Args.ThrowIfNull(attributes, "attributes");
            foreach (KeyValuePair kvp in attributes.ToKeyValuePairs())
            {
                AddAttribute(kvp.Key, kvp.Value);
            }

            return this;
        }

        public Tag AddAttribute(string name, object value)
        {
            Attributes.AddMissing(name, value);
            return this;
        }
        
        public Tag SetAttribute(string name, object value)
        {
            if (!Attributes.AddMissing(name, value))
            {
                Attributes[name] = value;
            }

            return this;
        }

        protected bool SelfClosing { get; set; }

        public static Tag Of(string tagName)
        {
            return Of(tagName, new { }, string.Empty);
        }
        
        public static Tag Of(string tagName, object attributes = null, Func<Tag> content = null)
        {
            return new Tag(tagName, attributes, content());
        }
        public static Tag Of(string tagName, object attributes = null, string content = null)
        {
            return new Tag(tagName, attributes, content);
        }

        public Tag DataSet(string dataName, object value)
        {
            return SetAttribute($"data-{dataName}", value);
        }
        
        public virtual string Render(bool indented = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(RenderStartOpenTag());
            stringBuilder.Append(RenderTagAttributes());

            if (Content != null)
            {
                stringBuilder.Append(RenderEndOfTag());
                stringBuilder.Append(Content);
                stringBuilder.Append(RenderEndTag());
            }
            else
            {
                if (SelfClosing)
                {
                    stringBuilder.Append(RenderEndOfSelfClosingTag());
                }
                else
                {
                    stringBuilder.Append(RenderEndOfTag());
                    stringBuilder.Append(RenderEndTag());
                }
            }

            return indented ? stringBuilder.ToString().XmlToHumanReadable() : stringBuilder.ToString();
        }

        protected string RenderTagAttributes()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Attributes != null && Attributes.Count > 0)
            {
                stringBuilder.Append(RenderAttributes());
            }

            if (Styles != null && Styles.Count > 0)
            {
                stringBuilder.Append(RenderStyles());
            }

            if (Classes != null && Classes.Count > 0)
            {
                stringBuilder.Append(RenderClasses());
            }

            return stringBuilder.ToString();
        }

        protected string RenderStartTag()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(RenderStartOpenTag());
            stringBuilder.Append(RenderTagAttributes());
            stringBuilder.Append(RenderEndOfTag());
            return stringBuilder.ToString();
        }
        
        private string RenderStartOpenTag()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<{TagName}");
            
            return stringBuilder.ToString();
        }

        private string RenderEndTag()
        {
            return $"</{TagName}>";
        }

        private string RenderEndOfSelfClosingTag()
        {
            return "/>";
        }

        private string RenderEndOfTag()
        {
            return ">";
        }

        private string RenderAttributes()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" ");
            string[] keys = Attributes.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                stringBuilder.Append($"{key}=\"{Attributes[key]}\"");
                if (i != keys.Length - 1)
                {
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();
        }

        private string RenderStyles()
        {
            if (Styles.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" style=\"");
                string[] keys = Styles.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    string key = keys[i];
                    stringBuilder.Append($"{key}: {Styles[key].ToString()}");
                    if (i != keys.Length - 1)
                    {
                        stringBuilder.Append("; ");
                    }
                }

                stringBuilder.Append(";\"");
                return stringBuilder.ToString();
            }

            return string.Empty;
        }

        private string RenderClasses()
        {
            if (Classes != null && Classes.Count > 0)
            {
                return $" class=\"{string.Join(" ", Classes.ToArray())}\"";
            }

            return string.Empty;
        }
    }
}
