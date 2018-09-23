/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation.Html
{
    public class Tag : IHtmlString
    {
        TagBuilder _html;
        public Tag(string tagName, object attributes = null)
        {
            _html = new TagBuilder(tagName);
            Attrs(attributes);
        }

        public static implicit operator string(Tag e)
        {
            return e.ToString();
        }

        public static implicit operator MvcHtmlString(Tag e)
        {
            return e.TagBuilder.ToMvcHtml();
        }

        protected TagBuilder TagBuilder
        {
            get
            {
                return _html;
            }
        }

        /// <summary>
        /// Sets the html result of the specified Func to the html of the
        /// current tag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vals"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public Tag Each<T>(T[] vals, Func<T, MvcHtmlString> func)
        {
            if (vals == null)
            {
                return this;
            }

            StringBuilder result = new StringBuilder(_html.InnerHtml);
            foreach (T val in vals)
            {
                result.AppendLine(func(val).ToHtmlString());
            }

            return Html(result.ToString());
        }

        public Tag AttrFormat(string name, string valueFormat, params object[] values)
        {
            return Attr(name, string.Format(valueFormat, values));
        }

        public Tag Attr(string name, string value)
        {
            _html.Attr(name, value);
            return this;
        }

        public Tag AttrIf(bool condition, string name, string value)
        {
            _html.AttrIf(condition, name, value);
            return this;
        }

        public Tag Attrs(object attributes)
        {
            if (attributes != null)
            {
                Type attrType = attributes.GetType();
                PropertyInfo[] props = attrType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object val = prop.GetValue(attributes, null);
                    _html.AttrIf(val != null, prop.Name, (string)val);
                }
            }

            return this;
        }

        public Tag Attrs(Dictionary<string, string> attrs)
        {
            foreach (string key in attrs.Keys)
            {
                _html.AttrIf(!string.IsNullOrWhiteSpace(attrs[key]), key, attrs[key]);
            }

            return this;
        }

        public Tag Id(string id)
        {
            _html.Id(id);
            return this;
        }

        public string Id()
        {
            return _html.Id();
        }

        public Tag DropDown(Dictionary<string, string> options, string selected = null)
        {
            _html.DropDown(options, selected);
            return this;
        }

        public Tag Radio(bool chked, object attributes = null)
        {
            Tag radio = new Tag("input", attributes).Type("radio");
            if (chked)
            {
                radio.Attrs(new { Checked = "checked" });
            }

            return SubTag(radio);
        }

        public Tag CheckBox(bool chked, object attributes = null)
        {
            Tag checkBox = new Tag("input", attributes).Type("checkbox");
            if (chked)
            {
                checkBox.Attrs(new { Checked = "checked" });
            }
            return SubTag(checkBox);
        }

        public Tag TextArea(string value = null, int rows = 10, int cols = 40, object attributes = null)
        {
            Tag textArea = new Tag("textarea", new { rows = rows.ToString(), cols = cols.ToString() });
            textArea.Attrs(attributes);
            return SubTag(textArea);
        }

        public Tag TextBox(string value = null, object attributes = null)
        {
            Tag textBox = new Tag("input", attributes).Type("text").Value(value);
            return SubTag(textBox);
        }

        public Tag Input(string type, object attributes = null)
        {
            return SubTag(new Tag("input", attributes).Type(type));
        }

        public Tag Name(string name)
        {
            return Attrs(new { name = name });
        }

        public Tag Value(string value)
        {
            return Attrs(new { value = value });
        }

        public Tag Type(string type)
        {
            return Attrs(new { type = type });
        }

        public Tag ChildIf(bool condition, Tag tag)
        {
            if (condition)
            {
                Child(tag);
            }

            return this;
        }
        /// <summary>
        /// Same as SubTag.  Adds a subtag to the current Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Tag Child(string tagName, string text)
        {
            return SubTag(tagName, text);
        }

        /// <summary>
        /// Same as Child.  Adds a subtag to the current
        /// Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Tag SubTag(string tagName, string text)
        {
            _html.Child(new TagBuilder(tagName).Text(text));
            return this;
        }

        public Tag Child(Tag tag)
        {
            return SubTag(tag);
        }

        public Tag SubTag(Tag tag)
        {
            _html.Child(tag.TagBuilder);
            return this;
        }

        public Tag Class(string className)
        {
            return Css(className);
        }

        public Tag CssIf(bool condition, string classOrStyle, string value = null)
        {
            if (condition)
            {
                Css(classOrStyle, value);
            }

            return this;
        }

        public Tag Css(string classOrStyle, string value = null)
        {
            _html.Css(classOrStyle, value);
            return this;
        }

        public Tag Text(string text)
        {
            _html.Text(text);
            return this;
        }

        public Tag Text(IHtmlString text)
        {
            return Text(text.ToString());
        }

        public Tag Text(MvcHtmlString text)
        {
            return Text(text.ToString());
        }

        public Tag Html(IHtmlString html)
        {
            return Html(html.ToHtmlString());
        }

        public Tag Html(MvcHtmlString html)
        {
            return Html(html.ToHtmlString());
        }

        public Tag Html(Tag tag)
        {
            return Html(tag.TagBuilder);
        }

        public Tag Html(TagBuilder tagBuilder)
        {
            _html.Child(tagBuilder);
            return this;
        }

        public Tag Html(string html)
        {
            _html.Html(html);
            return this;
        }

        public Tag DataClick(string dataClick)
        {
            _html.AttrIf(!string.IsNullOrWhiteSpace(dataClick), "data-click", dataClick);
            return this;
        }

        public Tag Data(string name, string value)
        {
            _html.AttrIf(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value), string.Format("data-{0}", name), value);
            return this;
        }

        /// <summary>
        /// Adds an onabort attribute with the specified value
        /// </summary>
        /// <param name="abort">The value</param>
        /// <returns>The Tag</returns>
        public Tag Abort(string abort)
        {
            return this.On("abort", abort);
        }

        /// <summary>
        /// Short for Blur
        /// </summary>
        /// <param name="blur"></param>
        /// <returns></returns>
        public Tag B(string blur)
        {
            return Blur(blur);
        }

        /// <summary>
        /// Adds an onblur attribute with the specified value
        /// </summary>
        /// <param name="blur">The value</param>
        /// <returns>The Tag</returns>
        public Tag Blur(string blur)
        {
            return this.On("blur", blur);
        }

        /// <summary>
        /// Short for Change
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public Tag Ch(string change)
        {
            return Change(change);
        }
        /// <summary>
        /// Adds an onchange attribute with the specified value
        /// </summary>
        /// <param name="change">The value</param>
        /// <returns>The Tag</returns>
        public Tag Change(string change)
        {
            return this.On("change", change);
        }

        /// <summary>
        /// Short for Click
        /// </summary>
        /// <param name="click"></param>
        /// <returns></returns>
        public Tag Cl(string click)
        {
            return Click(click);
        }

        /// <summary>
        /// Adds an onclick attribute with the specified value
        /// </summary>
        /// <param name="click">The value</param>
        /// <returns>The Tag</returns>
        public Tag Click(string click)
        {
            return this.On("click", click);
        }

        /// <summary>
        /// Short for DblClick
        /// </summary>
        /// <param name="dblclick"></param>
        /// <returns></returns>
        public Tag Dc(string dblclick)
        {
            return DblClick(dblclick);
        }
        /// <summary>
        /// Adds an ondblclick attribute with the specified value
        /// </summary>
        /// <param name="dblclick">The value</param>
        /// <returns>The Tag</returns>
        public Tag DblClick(string dblclick)
        {
            return this.On("dblclick", dblclick);
        }
        /// <summary>
        /// Adds an onerror attribute with the specified value
        /// </summary>
        /// <param name="error">The value</param>
        /// <returns>The Tag</returns>
        public Tag Error(string error)
        {
            return this.On("error", error);
        }

        /// <summary>
        /// Short for Focus
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public Tag F(string focus)
        {
            return Focus(focus);
        }

        /// <summary>
        /// Adds an onfocus attribute with the specified value
        /// </summary>
        /// <param name="focus">The value</param>
        /// <returns>The Tag</returns>
        public Tag Focus(string focus)
        {
            return this.On("focus", focus);
        }

        /// <summary>
        /// Short for Keydown
        /// </summary>
        /// <param name="keydown"></param>
        /// <returns></returns>
        public Tag Kd(string keydown)
        {
            return Keydown(keydown);
        }
        /// <summary>
        /// Adds an onkeydown attribute with the specified value
        /// </summary>
        /// <param name="keydown">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keydown(string keydown)
        {
            return this.On("keydown", keydown);
        }

        public Tag Kp(string keypress)
        {
            return Keypress(keypress);
        }

        /// <summary>
        /// Adds an onkeypress attribute with the specified value
        /// </summary>
        /// <param name="keypress">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keypress(string keypress)
        {
            return this.On("keypress", keypress);
        }

        /// <summary>
        /// Short for keyup
        /// </summary>
        /// <param name="keyup"></param>
        /// <returns></returns>
        public Tag Ku(string keyup)
        {
            return Keyup(keyup);
        }
        /// <summary>
        /// Adds an onkeyup attribute with the specified value
        /// </summary>
        /// <param name="keyup">The value</param>
        /// <returns>The Tag</returns>
        public Tag Keyup(string keyup)
        {
            return this.On("keyup", keyup);
        }

        /// <summary>
        /// Short for Load
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public Tag L(string load)
        {
            return Load(load);
        }
        /// <summary>
        /// Adds an onload attribute with the specified value
        /// </summary>
        /// <param name="load">The value</param>
        /// <returns>The Tag</returns>
        public Tag Load(string load)
        {
            return this.On("load", load);
        }

        public Tag Md(string mousedown)
        {
            return Mousedown(mousedown);
        }

        /// <summary>
        /// Adds an onmousedown attribute with the specified value
        /// </summary>
        /// <param name="mousedown">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mousedown(string mousedown)
        {
            return this.On("mousedown", mousedown);
        }

        public Tag Mm(string mousemove)
        {
            return Mousemove(mousemove);
        }

        /// <summary>
        /// Adds an onmousemove attribute with the specified value
        /// </summary>
        /// <param name="mousemove">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mousemove(string mousemove)
        {
            return this.On("mousemove", mousemove);
        }

        /// <summary>
        /// Short for Mouseout
        /// </summary>
        /// <param name="mouseout"></param>
        /// <returns></returns>
        public Tag Mout(string mouseout)
        {
            return Mouseout(mouseout);
        }
        /// <summary>
        /// Adds an onmouseout attribute with the specified value
        /// </summary>
        /// <param name="mouseout">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseout(string mouseout)
        {
            return this.On("mouseout", mouseout);
        }

        /// <summary>
        /// Short for Mouseover
        /// </summary>
        /// <param name="mouseover"></param>
        /// <returns></returns>
        public Tag Mo(string mouseover)
        {
            return Mouseover(mouseover);
        }

        /// <summary>
        /// Adds an onmouseover attribute with the specified value
        /// </summary>
        /// <param name="mouseover">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseover(string mouseover)
        {
            return this.On("mouseover", mouseover);
        }

        /// <summary>
        /// Short for mouseup
        /// </summary>
        /// <param name="mouseUp"></param>
        /// <returns></returns>
        public Tag Mu(string mouseUp)
        {
            return Mouseup(mouseUp);
        }

        /// <summary>
        /// Adds an onmouseup attribute with the specified value
        /// </summary>
        /// <param name="mouseup">The value</param>
        /// <returns>The Tag</returns>
        public Tag Mouseup(string mouseup)
        {
            return this.On("mouseup", mouseup);
        }
        /// <summary>
        /// Adds an onreset attribute with the specified value
        /// </summary>
        /// <param name="reset">The value</param>
        /// <returns>The Tag</returns>
        public Tag Reset(string reset)
        {
            return this.On("reset", reset);
        }
        /// <summary>
        /// Adds an onresize attribute with the specified value
        /// </summary>
        /// <param name="resize">The value</param>
        /// <returns>The Tag</returns>
        public Tag Resize(string resize)
        {
            return this.On("resize", resize);
        }

        /// <summary>
        /// Short for Unload
        /// </summary>
        /// <param name="unload"></param>
        /// <returns></returns>
        public Tag U(string unload)
        {
            return Unload(unload);
        }
        /// <summary>
        /// Adds an onunload attribute with the specified value
        /// </summary>
        /// <param name="unload">The value</param>
        /// <returns>The Tag</returns>
        public Tag Unload(string unload)
        {
            return this.On("unload", unload);
        }

        /// <summary>
        /// Wraps the current tag in a tag of the specified type and returns
        /// the wrapper tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Tag Wrap(string tagName)
        {
            return new Tag(tagName).Html(this);
        }

        static internal List<string> events = new List<string>(new string[] { "abort", "blur", "change", "click", 
                                   "dblclick", "error", "focus", "keydown", 
                                   "keypress", "keyup", "load", "mousedown", 
                                   "mousemove", "mouseout", "mouseover", "mouseup", 
                                   "reset", "resize", "select", "submit", "unload"});

        public Tag On(string eventName, string value)
        {
            if (!events.Contains(eventName))
            {
                throw Args.Exception<InvalidOperationException>("The specified eventName is invalid: {0}", eventName);
            }

            _html.AttrIf(!string.IsNullOrWhiteSpace(value), string.Format("on{0}", eventName), value);
            return this;
        }

        #region IHtmlString Members

        public string ToHtmlString()
        {
            return TagBuilder.ToMvcHtml().ToHtmlString();
        }

        #endregion

        public string ToHtmlString(TagRenderMode mode)
        {
            return TagBuilder.ToMvcHtml(mode).ToHtmlString();
        }
    }
}
