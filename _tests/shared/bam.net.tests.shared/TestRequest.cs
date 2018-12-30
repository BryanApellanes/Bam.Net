/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts.Tests
{
    class TestRequest : IRequest
    {
        public TestRequest()
        {
            this.Cookies = new CookieCollection();
            Cookie sessionCookie = new Cookie(Session.CookieName, "0368c7fde0a40272d42e14e224d37761dbccef665116ccb063ae31aaa7708d72");
            this.Cookies.Add(sessionCookie);
        }

        #region IRequest
        public string[] AcceptTypes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Encoding ContentEncoding
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long ContentLength64
        {
            get { throw new NotImplementedException(); }
        }

        public int ContentLength
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Specialized.NameValueCollection QueryString
        {
            get { throw new NotImplementedException(); }
        }

        public string ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public CookieCollection Cookies
        {
            get;
            set;
        }

        public System.Collections.Specialized.NameValueCollection Headers
        {
            get { throw new NotImplementedException(); }
        }

        public string HttpMethod
        {
            get { throw new NotImplementedException(); }
        }

        public Stream InputStream
        {
            get { throw new NotImplementedException(); }
        }

        public Uri Url
        {
            get { return new Uri("http://localhost:8080/test"); }
        }

        public Uri UrlReferrer
        {
            get { throw new NotImplementedException(); }
        }

        public string UserAgent
        {
            get { throw new NotImplementedException(); }
        }

        public string UserHostAddress
        {
            get { throw new NotImplementedException(); }
        }

        public string UserHostName
        {
            get { throw new NotImplementedException(); }
        }

        public string[] UserLanguages
        {
            get { throw new NotImplementedException(); }
        }

        public string RawUrl
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

}
