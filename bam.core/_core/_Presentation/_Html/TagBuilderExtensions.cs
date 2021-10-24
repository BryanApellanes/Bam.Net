/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net.Html.Js;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Bam.Net.Presentation.Html
{
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Adds a style entry
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder Style(this TagBuilder tagBuilder, string name, string value)
        {
            return tagBuilder.Css(name, value);
        }

        /// <summary>
        /// Adds the specified class name to the tagbuilder if no value is specified.
        /// Adds a style entry if a value is specified.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="styleOrClassName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder Css(this TagBuilder tagBuilder, string styleOrClassName, string value = null)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (tagBuilder.Attributes.ContainsKey("style"))
                {
                    string current = tagBuilder.Attributes["style"];
                    tagBuilder.Attributes["style"] = string.Format("{0}{1}:{2};", current, styleOrClassName, value);
                }
                else
                {
                    tagBuilder.Attributes.Add("style", string.Format("{0}:{1};", styleOrClassName, value));
                }
            }
            else
            {
                tagBuilder.AddCssClass(styleOrClassName);
            }
            return tagBuilder;
        }

        public static TagBuilder AttrIf(this TagBuilder tagBuilder, bool condition, string name, string value)
        {
            if (condition)
            {
                return tagBuilder.Attr(name, value);
            }
            else
            {
                return tagBuilder;
            }
        }

        public static TagBuilder Attr(this TagBuilder tagBuilder, string name, string value, bool underscoreToDashes = true)
        {
            if (underscoreToDashes)
            {
                name = name.Replace("_", "-");
            }
            tagBuilder.MergeAttribute(name, value, true);
            return tagBuilder;
        }


        public static TagBuilder Html(this TagBuilder tagBuilder, string html)
        {
            tagBuilder.InnerHtml.Clear();
            tagBuilder.InnerHtml.AppendHtml(html);
            return tagBuilder;
        }

        public static TagBuilder Text(this TagBuilder tagBuilder, string text)
        {
            tagBuilder.InnerHtml.Clear();
            tagBuilder.InnerHtml.Append(text);
            return tagBuilder;
        }

        public static TagBuilder FirstChild(this TagBuilder tagBuilder, string html)
        {
            StringBuilder builder = new StringBuilder(html);
            builder.Append(tagBuilder.InnerHtml);
            tagBuilder.InnerHtml.Clear();
            tagBuilder.InnerHtml.AppendHtml(builder.ToString());
            return tagBuilder;
        }

        /// <summary>
        /// Adds the specified child as the first element in the current TagBuilder
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static TagBuilder FirstChild(this TagBuilder tagBuilder, TagBuilder child)
        {
            return tagBuilder.FirstChild(child.ToString());
        }

        /// <summary>
        /// Adds the specified child html as the first element in the current TagBuilder if 
        /// the specified condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static TagBuilder FirstChildIf(this TagBuilder tagBuilder, bool condition, string html)
        {
            if (condition)
            {
                return tagBuilder.FirstChild(html);
            }
            else
            {
                return tagBuilder;
            }
        }

        /// <summary>
        /// Adds the specified child as the first element in the current TagBuilder if
        /// the specified condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static TagBuilder FirstChildIf(this TagBuilder tagBuilder, bool condition, TagBuilder child)
        {
            if (condition)
            {
                return tagBuilder.FirstChild(child);
            }
            else
            {
                return tagBuilder;
            }
        }

        /// <summary>
        /// Adds the specified html to the end of the current TagBuilder html
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static TagBuilder Child(this TagBuilder tagBuilder, string html)
        {
            MemoryStream innerHtml = new MemoryStream();
            using (TextWriter tw = new StreamWriter(innerHtml))
            {
                tagBuilder.WriteTo(tw, HtmlEncoder.Default);
            }
            innerHtml.Seek(0, SeekOrigin.Begin);            
            StringBuilder builder = new StringBuilder(innerHtml.ReadToEnd());
            builder.Append(html);
            tagBuilder.InnerHtml.Clear();
            tagBuilder.InnerHtml.AppendHtml(builder.ToString());
            return tagBuilder;
        }

        /// <summary>
        /// Wraps the current TagBuilder in the specified tagName and returns
        /// the parent.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static TagBuilder Parent(this TagBuilder tagBuilder, string tagName)
        {
            return new TagBuilder(tagName).Child(tagBuilder);
        }

        /// <summary>
        /// Appends the specified child to the current TagBuilder
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static TagBuilder Child(this TagBuilder tagBuilder, TagBuilder child, TagRenderMode mode = TagRenderMode.Normal)
        {
            return tagBuilder.Child(child.Render());
        }

        public static string Render(this IHtmlContent content, HtmlEncoder encoder = null)
        {
            encoder = encoder ?? HtmlEncoder.Default;
            MemoryStream result = new MemoryStream();
            using (TextWriter tw = new StreamWriter(result))
            {
                content.WriteTo(tw, encoder);
            }

            result.Seek(0, SeekOrigin.Begin);
            return result.ReadToEnd();
        }

        public static TagBuilder ChildIf(this TagBuilder tagBuilder, bool condition, string child)
        {
            if (condition)
            {
                tagBuilder.Child(child);
            }

            return tagBuilder;
        }

        public static TagBuilder ChildIf(this TagBuilder tagBuilder, bool condition, TagBuilder child)
        {
            if (condition)
            {
                tagBuilder.Child(child);
            }

            return tagBuilder;
        }


        public static TagBuilder Select(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attr("selected", "selected");
        }

        /// <summary>
        /// Adds selected="selected" if the condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static TagBuilder SelectIf(this TagBuilder tagBuilder, bool condition)
        {
            return tagBuilder.AttrIf(condition, "selected", "selected");
        }

        public static TagBuilder CheckedIf(this TagBuilder tagBuilder, bool condition)
        {
            return tagBuilder.AttrIf(condition, "checked", "checked");
        }
        /// <summary>
        /// Adds attribute type="radio"
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <returns></returns>
        public static TagBuilder Radio(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attr("type", "radio");
        }

        /// <summary>
        /// Adds a label element as a child using the specified forName and text.
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="forName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TagBuilder Label(this TagBuilder tagBuilder, string forName, string text)
        {
            return tagBuilder.Child(new TagBuilder("label").Attr("for", forName).Text(text));
        }

        public static TagBuilder Value(this TagBuilder tagBuilder, string value)
        {
            return tagBuilder.Attr("value", value);
        }

        public static string Id(this TagBuilder tagBuilder)
        {
            return tagBuilder.Attributes["id"];
        }

        public static TagBuilder Id(this TagBuilder tagBuilder, string id)
        {
            return tagBuilder.Attr("id", id);
        }

        public static TagBuilder IdIf(this TagBuilder tagBuilder, bool condition, string id)
        {
            if (condition)
            {
                tagBuilder.Id(id);
            }

            return tagBuilder;
        }

        public static TagBuilder IdIfNone(this TagBuilder tagBuilder, string id)
        {
            string existing = tagBuilder.Attributes.ContainsKey("id") ? tagBuilder.Attributes["id"] : string.Empty;
            return tagBuilder.IdIf(string.IsNullOrEmpty(existing), id);
        }

        public static TagBuilder Name(this TagBuilder tagBuilder, string name)
        {
            return tagBuilder.Attr("name", name);
        }

        public static TagBuilder ValueIf(this TagBuilder tagBuilder, bool condition, string value)
        {
            return tagBuilder.AttrIf(condition, "value", value);
        }

        public static TagBuilder Type(this TagBuilder tagBuilder, string value)
        {
            return tagBuilder.Attr("type", value);
        }

        public static TagBuilder TextIf(this TagBuilder tagBuilder, bool condition, string text)
        {
            if (condition)
            {
                tagBuilder.Text(text);
            }

            return tagBuilder;
        }

        public static TagBuilder DropDown(this Type enumType, string selected = null, object htmlAttributes = null)
        {
            return Bam.Net.Presentation.Html.DropDownAttribute.DictionaryFromEnum(enumType).DropDown(selected, htmlAttributes);
        }

        /// <summary>
        /// Creates a TagBuilder representing a select element using the
        /// keys and values of the Dictionary being extended.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static TagBuilder DropDown(this Dictionary<string, string> options, string selected = null, object htmlAttributes = null)
        {
            TagBuilder select = new TagBuilder("select");
            foreach (string key in options.Keys)
            {
                string value = options[key];
                select.Child(new TagBuilder("option").Value(key)
                    .Text(value)
                    .SelectIf(value.Equals(selected) || key.Equals(selected))
                );
            }

            select.AttrsIf(htmlAttributes != null, htmlAttributes);

            return select;
        }

        public static TagBuilder DropDown(this TagBuilder tagBuilder, Dictionary<string, string> options, string selected = null)
        {
            return tagBuilder.Child(options.DropDown(selected));
        }

        public static TagBuilder DropDown(this TagBuilder tagBuilder, Type enumType, string selected = null)
        {
            return tagBuilder.Child(enumType.DropDown(selected));
        }

        /// <summary>
        /// Add a br element
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <returns></returns>
        public static TagBuilder Br(this TagBuilder tagBuilder)
        {
            return tagBuilder.Child("<br />");
        }

        /// <summary>
        /// Add a br element if the condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static TagBuilder BrIf(this TagBuilder tagBuilder, bool condition)
        {
            if (condition)
            {
                tagBuilder.Br();
            }

            return tagBuilder;
        }

        public static TagBuilder Button(this TagBuilder tagBuilder, object htmlAttributes = null)
        {
            return tagBuilder.Button("Submit", htmlAttributes);
        }

        public static TagBuilder Button(this TagBuilder tagBuilder, string text = null, object htmlAttributes = null)
        {
            string textAttr = text;
            if (htmlAttributes is string)
            {
                textAttr = (string)htmlAttributes;
                htmlAttributes = null;
            }
            else if (htmlAttributes != null)
            {
                PropertyInfo prop = htmlAttributes.GetType().GetProperty("text");
                textAttr = prop == null ? text : (string)prop.GetValue(htmlAttributes, null);
            }
            return tagBuilder
                .Attr("role", "button")
                .Text(textAttr ?? "Submit")
                .AttrsIf(htmlAttributes != null, htmlAttributes);
        }

        public static TagBuilder MethodButton(this TagBuilder tagBuilder, string className, string method, string parameterSource = null, object htmlAttributes = null)
        {
            if (tagBuilder == null)
            {
                tagBuilder = new TagBuilder("span");
            }
            tagBuilder.Child(
                new TagBuilder("span")
                .Button(htmlAttributes)
                .DataSet("plugin", "methodButton")
                .DataSet("method", method)
                .DataSet("class-name", className)
                .DataSetIf(parameterSource != null, "parameter-source", parameterSource)
            );
            return tagBuilder;
        }

        /// <summary>
        /// Applies all the attributes of the specified object
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static TagBuilder Attrs(this TagBuilder tagBuilder, object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return tagBuilder;
            }
            Type t = htmlAttributes.GetType();
            foreach (PropertyInfo prop in t.GetProperties())
            {
                tagBuilder.Attr(prop.Name.Replace("_", "-"), (string)prop.GetValue(htmlAttributes, null));
            }

            return tagBuilder;
        }

        /// <summary>
        /// Adds the specified htmlAttributes if the condition is true
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="condition"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static TagBuilder AttrsIf(this TagBuilder tagBuilder, bool condition, object htmlAttributes)
        {
            if (condition)
            {
                tagBuilder.Attrs(htmlAttributes);
            }

            return tagBuilder;
        }
        
        /// <summary>
        /// Shortcut for ToMvcHtml
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <returns></returns>
        public static string ToHtml(this TagBuilder tagBuilder)
        {
            return tagBuilder.Render();
        }
    }
}
