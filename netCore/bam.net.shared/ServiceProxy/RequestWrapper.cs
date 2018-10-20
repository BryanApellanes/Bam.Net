/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Reflection;
using Bam.Net.Configuration;
using System.Diagnostics;

namespace Bam.Net.ServiceProxy
{
    public partial class RequestWrapper: IRequest
    {

        public static implicit operator HttpListenerRequest(RequestWrapper wrapper)
        {
            return (HttpListenerRequest)wrapper.Wrapped;
        }

        public RequestWrapper(HttpListenerRequest request)
        {
            DefaultConfiguration.CopyProperties(request, this);
            this.Wrapped = request;
        }

        internal RequestWrapper(object requestToBeWrapped)
        {
            DefaultConfiguration.CopyProperties(requestToBeWrapped, this);
            this.Wrapped = requestToBeWrapped;
        }

        Type _wrappedType;
        protected Type WrappedType
        {
            get
            {
                if (_wrappedType == null)
                {
                    if (Wrapped != null)
                    {
                        _wrappedType = Wrapped.GetType();
                    }
                }

                return _wrappedType;
            }
        }

        public object Wrapped
        {
            get;
            private set;
        }

        private void Set(string name, object value)
        {
            if (Wrapped != null)
            {
                PropertyInfo property = WrappedType.GetProperty(name);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(Wrapped, value, null);
                }
            }
        }

        [DebuggerStepThrough]
        private object Get(string name)
        {
            if (Wrapped != null)
            {
                PropertyInfo prop = WrappedType.GetProperty(name);
                if(prop != null)
                {
                    try
                    {
                        return prop.GetValue(Wrapped, null);
                    }
                    catch// (Exception ex)
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        #region IRequest Members

        public string[] AcceptTypes
        {
            get
            {
                return (string[])Get("AcceptTypes");
            }
            set
            {
                Set("AcceptTypes", value);
            }
        }

        public Encoding ContentEncoding
        {
            get
            {
                return (Encoding)Get("ContentEncoding");
            }
            set
            {
                Set("ContentEncoding", value);
            }
        }

        public long ContentLength64
        {
            get
            {
                return (long)Get("ContentLength64");
            }
        }

        public int ContentLength
        {
            get
            {
                object val = Get("ContentLength");
                int result = 0;
                if (val != null)
                {
                    result = (int)val;
                }
                return result;
            }
        }

        public string ContentType
        {
            get
            {
                return (string)Get("ContentType");
            }
        }

        System.Net.CookieCollection _cookies;
        public System.Net.CookieCollection Cookies
        {
            get
            {
                if (_cookies == null)
                {
                    _cookies = (System.Net.CookieCollection)Get("Cookies");
                }
                return _cookies;
            }
            private set
            {
                _cookies = value;
            }
        }

        NameValueCollection _headers;
        public NameValueCollection Headers
        {
            get
            {
                if (_headers == null)
                {
                    _headers = (NameValueCollection)Get("Headers");
                }
                return _headers;
            }
            set
            {
                _headers = value;
            }
        }

        public string HttpMethod
        {
            get
            {
                return (string)Get("HttpMethod");
            }
        }

        public Stream InputStream
        {
            get
            {
                return (Stream)Get("InputStream");
            }
        }

        public Uri Url
        {
            get
            {
                return (Uri)Get("Url");
            }
        }

        public Uri UrlReferrer
        {
            get
            {
                return (Uri)Get("UrlReferrer");
            }
        }

        public NameValueCollection QueryString
        {
            get
            {
                return (NameValueCollection)Get("QueryString");
            }
        }
        public string UserAgent
        {
            get
            {
                return (string)Get("UserAgent");
            }
        }

        public string UserHostAddress
        {
            get
            {
                return (string)Get("UserHostAddress");
            }
        }

        public string UserHostName
        {
            get
            {
                return (string)Get("UserHostName");
            }
        }

        public string[] UserLanguages
        {
            get
            {
                return (string[])Get("UserLanguages");
            }
        }

        public string RawUrl
        {
            get
            {
                return (string)Get("RawUrl");
            }
        }

        #endregion
    }
}
