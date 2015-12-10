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
    public static class Http
    {
        static Http()
        {
            DefaultUserAgent = UserAgents.FF10;
        }

        public static Func<string, T> JsonParser<T>()
        {
            return (s) =>
            {
                return s.FromJson<T>();
            };
        }

        public static Func<string, T> XmlParser<T>()
        {
            return (s) =>
            {
                return s.FromXml<T>();
            };
        }

        /// <summary>
        /// Gets the specified url and parses the result as Json into the specified
        /// generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetJson<T>(string url, Dictionary<string, string> headers = null)
        {
            return Get(url, JsonParser<T>(), headers);
        }

		public static T GetXml<T>(string url, Dictionary<string, string> headers = null)
		{
			return Get(url, XmlParser<T>(), headers);
		}

        public static T Get<T>(string url, Func<string, T> parser, Dictionary<string, string> headers = null)
        {
            return Get<T>(new Uri(url), parser);
        }

        public static T Get<T>(Uri url, Func<string, T> parser, Dictionary<string, string> headers = null)
        {
            return parser(GetString(url.ToString(), headers));
        }
        public static string Get(Uri url, Dictionary<string, string> headers = null)
        {
            return Get(url.ToString(), headers);
        }
        public static string Get(string url, Dictionary<string, string>  headers = null)
        {
            return GetString(url, headers);
        }

        public static T Post<T>(string url, string postData, Dictionary<string, string> headers = null)
        {
            return Post(url, postData, JsonParser<T>(), headers);
        }

        public static T Post<T>(string url, string postData, Func<string, T> parser, Dictionary<string, string> headers = null)
        {
            return Post<T>(new Uri(url), postData, parser);
        }

        public static T Post<T>(Uri url, string postData, Func<string, T> parser, Dictionary<string, string> headers = null)
        {
            return parser(PostString(url.ToString(), postData, headers));
        }

        public static string Post(string url, string postData, Dictionary<string, string> headers = null)
        {
            return PostString(url, postData, headers);
        }

        public static string GetString(string url, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.DownloadString(url);
        }        
        
        public static string PostString(string url, string postData, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.UploadString(url, postData);
        }

        public static void Get(string url, string saveTo, Dictionary<string, string> headers = null)
        {
            Get(url, new FileInfo(saveTo), headers);
        }

        public static void Get(string url, FileInfo saveTo, Dictionary<string, string> headers = null)
        {
            byte[] data = GetData(url, headers);
            using (FileStream fs = new FileStream(saveTo.FullName, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public static void Post(string url, string postData, string saveTo, Dictionary<string, string> headers = null)
        {
            Post(url, postData, new FileInfo(saveTo), headers);
        }

        public static void Post(string url, string postData, FileInfo saveTo, Dictionary<string, string> headers = null)
        {
            Post(url, Encoding.UTF8.GetBytes(postData), saveTo, headers);
        }

        public static void Post(string url, byte[] postData, FileInfo saveTo, Dictionary<string, string> headers = null)
        {
            byte[] data = PostData(url, postData, headers);
            using (FileStream fs = new FileStream(saveTo.FullName, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public static byte[] GetData(string url, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.DownloadData(url);
        }        

        public static byte[] PostData(string url, byte[] postData, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.UploadData(url, postData);
        }

        private static WebClient GetClient(string agent = null)
        {
            if (string.IsNullOrEmpty(agent))
            {
                agent = DefaultUserAgent;
            }
            WebClient client = new CookieEnabledWebClient();
            client.Headers["User-Agent"] = agent;
            client.UseDefaultCredentials = true;

            return client;
        }

        public static string DefaultUserAgent
        {
            get;
            set;
        }

        private static void SetHeaders(Dictionary<string, string> headers, WebClient client)
        {
            if (headers != null)
            {
                headers.Keys.Each(key =>
                {
                    client.Headers[key] = headers[key];
                });
            }
        }
    }
}
