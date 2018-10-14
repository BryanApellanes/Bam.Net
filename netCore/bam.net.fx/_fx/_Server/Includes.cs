/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;
using Bam.Net.Presentation.Html;
using System.Web.Mvc;

namespace Bam.Net.Server
{
    public class Includes
    {        
        public Includes()
        {
            this._scripts = new HashSet<string>();
            this._css = new List<string>();
        }

        HashSet<string> _scripts;
        public string[] Scripts
        {
            get
            {
                return _scripts.ToArray();
            }
            set
            {
                _scripts = new HashSet<string>();
                value.Each(s => _scripts.Add(s));
            }
        }

        List<string> _css;
        public string[] Css
        {
            get
            {
                return _css.ToArray();
            }
            set
            {
                _css = new List<string>();
                _css.AddRange(value);
            }
        }

        public void AddScript(string path)
        {
            _scripts.Add(path);
        }

        public void AddCss(string path)
        {
            _css.Add(path);
        }

        public Includes Combine(Includes includes)
        {
            return Combine(this, includes);
        }

        public static Includes Combine(Includes includes1, Includes includes2)
        {
            Includes result = new Includes();
            List<string> css = new List<string>();
            includes1.Css.Each(c =>
            {
                css.Add(c);
            });
            includes2.Css.Each(c =>
            {
                css.Add(c);
            });
            List<string> scripts = new List<string>();
            includes1.Scripts.Each(scr =>
            {
                scripts.Add(scr);
            });
            includes2.Scripts.Each(scr =>
            {
                scripts.Add(scr);
            });
            
            result.Css = css.ToArray();
            result.Scripts = scripts.ToArray();

            return result;
        }
        /// <summary>
        /// Renders the Scripts as a series of html script tags
        /// with the src attributes set to the value of each 
        /// Script string and the type attribute set to 
        /// "text/javascript"
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString GetScriptTags()
        {
            StringBuilder result = new StringBuilder();
            Scripts.Each(script =>
            {
                Tag scr = new Tag("script").Attr("src", script).Attr("type", "text/javascript");
                result.Append(scr.ToHtmlString(TagRenderMode.Normal));
            });

            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// Renders the Css as a series of link tags
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString GetStyleSheetLinkTags()
        {
            StringBuilder result = new StringBuilder();
            Css.Each(css =>
            {
                Tag link = new Tag("link").Attr("rel", "stylesheet").Attr("href", css);
                result.Append(link.ToHtmlString(TagRenderMode.SelfClosing));
            });

            return MvcHtmlString.Create(result.ToString());
        }

    }
}
