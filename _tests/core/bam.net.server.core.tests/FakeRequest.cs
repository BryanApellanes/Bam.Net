/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System.IO;

namespace Bam.Net.Server.Tests
{
    class FakeRequest : IRequest
    {
        public void SetUrl(string value)
        {
            this.Url = new Uri(value);
        }

        #region IRequest Members

        public string[] AcceptTypes
        {
            get;
            set;
        }

        public Encoding ContentEncoding
        {
            get;
            set;
        }

        public long ContentLength64
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public System.Net.CookieCollection Cookies
        {
            get;
            set;
        }

        public bool HasEntityBody
        {
            get;
            set;
        }

        public System.Collections.Specialized.NameValueCollection Headers
        {
            get;
            set;
        }

        public string HttpMethod
        {
            get;
            set;
        }

        public Stream InputStream
        {
            get;
            set;
        }

        public Uri Url
        {
            get;
            set;
        }

        public Uri UrlReferrer
        {
            get;
            set;
        }

        public string UserAgent
        {
            get;
            set;
        }

        public string UserHostAddress
        {
            get;
            set;
        }

        public string UserHostName
        {
            get;
            set;
        }

        public string[] UserLanguages
        {
            get;
            set;
        }

        public string RawUrl
        {
            get;
            set;
        }

        #endregion


        public int ContentLength
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Specialized.NameValueCollection QueryString
        {
            get { throw new NotImplementedException(); }
        }

    }

}
