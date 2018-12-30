/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Naizari.Testing;

namespace Naizari.Helpers
{
    public class HttpContextHelper
    {
        /// <summary>
        /// Takes the specified pageAndQueryString and returns the absolute
        /// webpath.  For example, if pageAndQueryString is "/page.htm?name=value"
        /// then RootPathFromRelative run from the page 'bar.aspx' in the folder 
        /// http://domain.com/foo will return "/foo/page.htm?name=value".
        /// This method will throw an ExpectFailedException if HttpContext.Current.Request
        /// is null, so it should only be called from within a web app.
        /// </summary>
        /// <param name="pageAndQueryString"></param>
        /// <returns></returns>
        public static string RootPathFromRelative(string pageAndQueryString)
        {
            pageAndQueryString = pageAndQueryString.Trim();
            if (pageAndQueryString.StartsWith("/"))
                pageAndQueryString = pageAndQueryString.Substring(1);

            HttpRequest request = HttpContext.Current.Request;
            Expect.IsNotNull(request, "HttpContext.Current.Request was null.");
            string appRoot = request.ApplicationPath;
            if (!appRoot.EndsWith("/"))
                appRoot += "/";

            return appRoot + pageAndQueryString;
        }

        public static string AbsolutePageUrl
        {
            get
            {                
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null)
                    return HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];

                return string.Empty;
            }
        }

        public static string PageUrl
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null)
                    return HttpContext.Current.Request.CurrentExecutionFilePath;

                return string.Empty;
            }
        }

        public static string BaseApplicationUrl
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null)
                {
                    string appPath = HttpContextHelper.ApplicationPath;
                    if (!appPath.EndsWith("/"))
                    {
                        appPath = appPath + "/";
                    }
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + appPath;
                }
                return "";
            }
        }

        public static string ApplicationPath
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null)
                    return HttpContext.Current.Request.ApplicationPath;

                return string.Empty;
            }
        }

        /// <summary>
        /// Redirects to the specified Url specifying that execution
        /// of the current page should terminate.
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect(string url)
        {
            Redirect(url, true);
        }

        /// <summary>
        /// Redirects to the specified url.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="endResponse"></param>
        public static void Redirect(string url, bool endResponse)
        {
            HttpContext.Current.Response.Redirect(url, endResponse);
        }

        /// <summary>
        /// Sends only the specified content to the client and calls HttpContext.Current.ApplicationInstance.CompleteRequest().
        /// </summary>
        /// <param name="content"></param>
        public static void SendOnly(string content)
        {
            Expect.IsNotNull(HttpContext.Current, "HttpContextHelper.SendOnly is valid only when HttpContext.Current is not null (inside a web app).");
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Write(content);
            response.Flush();
            response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void EndRequest()
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Flush();
            response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
