/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Web;
using Bam.Net.Configuration;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    public partial class ResponseWrapper: IResponse
    {
        public ResponseWrapper(HttpListenerResponse response)
        {
            DefaultConfiguration.CopyProperties(response, this);
            this.Wrapped = response;            
        }

        internal ResponseWrapper(object responseToBeWrapped)
        {
            DefaultConfiguration.CopyProperties(responseToBeWrapped, this);
            this.Wrapped = responseToBeWrapped;
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

        private object Get(string name)
        {
            if (Wrapped != null)
            {
                PropertyInfo prop = WrappedType.GetProperty(name);
                if (prop != null)
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

        #region IResponse Members

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
            set
            {
                Set("ContentLength64", value);
            }
        }

        public string ContentType
        {
            get
            {
                return (string)Get("ContentType");
            }
            set
            {
                Set("ContentType", value);
            }
        }

        public CookieCollection Cookies
        {
            get
            {
                return (CookieCollection)Get("Cookies");
            }
            set
            {
                Set("Cookies", value);
            }
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return (WebHeaderCollection)Get("Headers");
            }
            set
            {
                Set("Headers", value);
            }
        }

        public bool KeepAlive
        {
            get
            {
                return (bool)Get("KeepAlive");
            }
            set
            {
                Set("KeepAlive", value);
            }
        }

        public Stream OutputStream
        {
            get
            {
                return (Stream)Get("OutputStream");
            }
            private set
            {
                Set("OutputStream", value);
            }
        }

        public string RedirectLocation
        {
            get
            {
                return (string)Get("RedirectLocation");
            }
            set
            {
                Set("RedirectLocation", value);
            }
        }

        public bool SendChunked
        {
            get
            {
                return (bool)Get("SendChunked");
            }
            set
            {
                Set("SendChunked", value);
            }
        }

        public int StatusCode
        {
            get
            {
                return (int)Get("StatusCode");
            }
            set
            {
                Set("StatusCode", value);
            }
        }

        public string StatusDescription
        {
            get
            {
                return (string)Get("StatusDescription");
            }
            set
            {
                Set("StatusDescription", value);
            }
        }

        public void Abort()
        {
            Wrapped.GetType().GetMethod("Abort", Type.EmptyTypes).Invoke(Wrapped, null);
        }

        public void AddHeader(string name, string value)
        {
            Wrapped.GetType().GetMethod("AddHeader", new Type[] { typeof(string), typeof(string) }).Invoke(Wrapped, new object[] { name, value });
        }

        public void AppendCookie(Cookie cookie)
        {
            Wrapped.GetType().GetMethod("AppendCookie", new Type[] { typeof(Cookie) }).Invoke(Wrapped, new object[] { cookie });
        }

        public void AppendHeader(string name, string value)
        {
            Wrapped.GetType().GetMethod("AppendHeader", new Type[] { typeof(string), typeof(string) }).Invoke(Wrapped, new object[] { name, value });
        }

        public void Close()
        {
            Wrapped.GetType().GetMethod("Close", Type.EmptyTypes).Invoke(Wrapped, null);
        }

        public void Close(byte[] responseEntity, bool willBlock)
        {
            Wrapped.GetType().GetMethod("Close", new Type[] { typeof(byte[]), typeof(bool) }).Invoke(Wrapped, new object[] { responseEntity, willBlock });
        }

        public void Redirect(string url)
        {
            Wrapped.GetType().GetMethod("Redirect", new Type[] { typeof(string) }).Invoke(Wrapped, new object[] { url });
        }

        public void SetCookie(Cookie cookie)
        {
            Wrapped.GetType().GetMethod("SetCookie", new Type[] { typeof(Cookie) }).Invoke(Wrapped, new object[] { cookie });
        }

        #endregion
    }
}
