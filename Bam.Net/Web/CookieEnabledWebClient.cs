/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace Bam.Net.Web
{
    public class CookieEnabledWebClient: WebClient
    {
        public CookieEnabledWebClient()
        {
            Cookies = new CookieContainer();
        }

        protected CookieContainer Cookies
        {
            get;
            private set;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            HttpWebRequest httpRequest = request as HttpWebRequest;
            if (httpRequest != null)
            {
                if (httpRequest.CookieContainer == null)
                {
                    httpRequest.CookieContainer = Cookies;
                }

                if (!string.IsNullOrEmpty(Referer))
                {
                    httpRequest.Referer = Referer;
                }
            }

            Referer = address.ToString();
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            HttpWebResponse httpResponse = response as HttpWebResponse;
            if (httpResponse != null)
            {
                foreach (Cookie cookie in httpResponse.Cookies)
                {
                    Cookies.Add(cookie);
                }
            }

            return response;
        }

        protected string Referer
        {
            get;
            set;
        }
    }
}
