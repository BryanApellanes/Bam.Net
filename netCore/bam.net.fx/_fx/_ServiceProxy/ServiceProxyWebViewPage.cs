/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.Incubation;
using System.Reflection;
using System.Xml.Serialization;

namespace Bam.Net.ServiceProxy
{
    public abstract class ServiceProxyWebViewPage<TModel> : WebViewPage<TModel>
    {
        public ServiceProxyWebViewPage()
            : base()
        {
            JQueryVersion = "1.7.1";
            JQueryUiVersion = "1.8.11";
            ModernizrVersion = "1.7";
            this.ServiceProxy = new ServiceProxyHelper();

            SetupCommonScripts();
        }

        private void SetupCommonScripts()
        {
            string dotMin = "";
#if !DEBUG
                dotMin = ".min";
#endif
            //scripts.Clear();
            //scripts.Add(string.Format("json2{0}.js", dotMin));
            //scripts.Add(string.Format("jquery-{0}{1}.js", JQueryVersion, dotMin));
            //scripts.Add(string.Format("jquery-ui-{0}{1}.js", JQueryUiVersion, dotMin));
            //scripts.Add("underscore.js");
            //scripts.Add(string.Format("modernizr-{0}{1}.js", ModernizrVersion, dotMin));

            //scripts.Add(string.Format("jquery.unobtrusive-ajax{0}.js", dotMin));
            //scripts.Add(string.Format("jquery.validate{0}.js", dotMin));
            //scripts.Add(string.Format("jquery.validate.unobtrusive{0}.js", dotMin));
        }

        public ServiceProxyHelper ServiceProxy { get; set; }

                
        string _modernizrVersion;
        public string ModernizrVersion
        {
            get
            {
                return _modernizrVersion;
            }
            protected set
            {
                _modernizrVersion = value;
                SetupCommonScripts();
            }
        }

        string _jQueryVersion;
        public string JQueryVersion
        {
            get
            {
                return _jQueryVersion;
            }
            protected set
            {
                _jQueryVersion = value;
                SetupCommonScripts();
            }
        }

        string _jQueryUIVersion;
        public string JQueryUiVersion
        {
            get
            {
                return _jQueryUIVersion;
            }
            protected set
            {
                _jQueryUIVersion = value;
                SetupCommonScripts();
            }
        }
    }

    public abstract class ServiceProxyWebViewPage : ServiceProxyWebViewPage<dynamic>
    {
        public ServiceProxyWebViewPage()
            : base()
        {
        }
    }
}
