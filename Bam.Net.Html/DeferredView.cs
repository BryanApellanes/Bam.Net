/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using Bam.Net;
using System.Threading;

namespace Bam.Net.Html
{
    public class DeferredView
    {
        public DeferredView()
        {
            Register();
        }

        public DeferredView(string name)
        {
            this.Name = name;
            Register();
        }

        public DeferredView(string name, int millisecondsToRender)
            : this(name)
        {
            this.MillisecondsToRender = millisecondsToRender;
        }

        public DeferredView(string name, Func<object, MvcHtmlString> content, int millisecondsToRender = 300)
            :this(name, millisecondsToRender)
        {
            this.ContentProvider = content;
        }

        public DeferredView(Func<object, MvcHtmlString> content, int millisecondsToRender = 300)
        {
            this.ContentProvider = content;
            this.MillisecondsToRender = millisecondsToRender;
            Register();
        }

        public DeferredView(string name, Func<object, MvcHtmlString> content, Func<MvcHtmlString> initial, int millisecondsToRender = 300)
            : this(name)
        {
            this.ContentProvider = content;
            this.MillisecondsToRender = millisecondsToRender;
            MvcHtmlString initialHtml = initial();
            if (initialHtml != null)
            {
                this.Initial = initialHtml;
            }
        }

        public DeferredView(string name, Func<object, MvcHtmlString> content, MvcHtmlString initial, int millisecondsToRender = 300)
            : this(name)
        {
            this.ContentProvider = content;
            this.MillisecondsToRender = millisecondsToRender;
            if (initial != null)
            {
                this.Initial = initial;
            }
        }

        public DeferredView(string name, Func<object, MvcHtmlString> content, MvcHtmlString initial, object attributes, int millisecondsToRender = 300)
            : this(name, content, initial, millisecondsToRender)
        {
            this.Attributes = attributes;
        }

        private void Register()
        {
            this.Created = DateTime.Now;
            this.Initial = new Tag("span").Text("Working...");
            DeferredViewController.SetView(this);
        }

        DateTime _created;
        internal DateTime Created
        {
            get
            {
                if (_created == null)
                {
                    _created = DateTime.Now;
                }

                return _created;
            }
            set
            {
                _created = value;
            }
        }

        public object Attributes
        {
            get;
            set;
        }

        string _tagName = "div";
        protected string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                _tagName = value;
            }
        }

        public Func<object, MvcHtmlString> ContentProvider
        {
            get;
            set;
        }

        MvcHtmlString _state;
        public MvcHtmlString State
        {
            get
            {
                if (_state == null)
                {
                    _state = Initial;
                }

                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public MvcHtmlString Initial
        {
            get;
            set;
        }

        MvcHtmlString _content;
        internal protected MvcHtmlString Content
        {
            get
            {
                if (_content == null)
                {
                    NamedThread thread = Exec.GetThread(this.Name);
                    if (thread != null)
                    {
                        _content = State;
                    }
                    else
                    {
                        _content = Initial;
                    }
                }

                return _content;
            }

            set
            {
                _content = value;
            }
        }

        /// <summary>
        /// The amount of time to allow Render to run
        /// on the server
        /// </summary>
        public int MillisecondsToRender
        {
            get;
            set;
        }

        string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = "deferredViewName_".RandomLetters(4);
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        bool _contentReady;

        public bool ContentReady
        {
            get
            {
                return _contentReady;
            }
        }

        public MvcHtmlString Render()
        {
            Tag result = new Tag(this.TagName, this.Attributes);

            if (!_contentReady)
            {
                if (ContentProvider.TakesTooLong(
                    (mvchtml) =>
                    {
                        Content = mvchtml;
                        _contentReady = true;
                        return Content;
                    }, this.Name, this, this.MillisecondsToRender)) // will allow ContentProvider MillisecondsToRender before unblocking
                {
                    result.Data("view-name", this.Name)
                        .Data("plugin", "deferredView")
                        .Html(Content);
                }
            }

            result.Html(Content);
            return MvcHtmlString.Create(result.ToHtmlString());
        }

        public override string ToString()
        {
            return Render().ToHtmlString();
        }
    }
}
