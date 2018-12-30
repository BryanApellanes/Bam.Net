using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.IO;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class RequestData : RepoData
    {
        public string AcceptTypes { get; set; }
        public string ContentEncoding { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public virtual List<CookieData> Cookies { get; set; }
        public virtual List<HeaderData> Headers { get; set; }
        public string HttpMethod { get; set; }
        public byte[] InputStream { get; set; }

        public ulong UrlId { get; set; }
        public virtual UriData Url { get; set; }

        public ulong UrlReferrerId { get; set; }
        public virtual UriData UrlReferrer { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string UserHostName { get; set; }
        public string UserLanguages { get; set; } // defined on IRequest as a string[] array. comma delimit it for storage
        public string RawUrl { get; set; }

        public static RequestData FromRequest(IRequest request)
        {
            RequestData data = new RequestData
            {
                AcceptTypes = string.Join(",", request.AcceptTypes),
                ContentEncoding = request.ContentEncoding.ToString(),
                ContentLength = request.ContentLength64,
                ContentType = request.ContentType,
                HttpMethod = request.HttpMethod,
                Url = UriData.FromUri(request.Url),
                UrlReferrer = UriData.FromUri(request.UrlReferrer),
                UserAgent = request.UserAgent,
                UserHostAddress = request.UserHostAddress,
                UserHostName = request.UserHostName,
                UserLanguages = string.Join(",", request.UserLanguages),
                RawUrl = request.RawUrl
            };
            data.Cookies = new List<CookieData>();
            foreach(Cookie cookie in request?.Cookies)
            {
                data.Cookies.Add(CookieData.FromCookie(cookie));
            }
            data.Headers = new List<HeaderData>();
            foreach(string key in request?.Headers.AllKeys)
            {
                data.Headers.Add(new HeaderData { Name = key, Value = request.Headers[key] });
            }
            MemoryStream inputStream = new MemoryStream();
            request?.InputStream.CopyTo(inputStream);
            data.InputStream = inputStream.ToArray();
            return data;
        }
    }
}
