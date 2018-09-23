using Bam.Net.ServiceProxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    internal class Etags
    {
        static Etags()
        {
            Values = new ConcurrentDictionary<string, string>();
            LastModified = new ConcurrentDictionary<string, DateTime>();
        }

        internal static ConcurrentDictionary<string, string> Values { get; set; }
        internal static ConcurrentDictionary<string, DateTime> LastModified { get; set; }

        public static void Set(IResponse response, string path, byte[] content)
        {
            string etag = content.Sha1();
            response.AddHeader("ETag", etag);
            Etags.Values.AddOrUpdate(path, etag, (p, v) => etag);
        }

        public static void SetLastModified(IResponse response, string path, DateTime lastModified)
        {
            response.AddHeader("Last-Modified", lastModified.ToUniversalTime().ToString("r"));
            Etags.LastModified.AddOrUpdate(path, lastModified, (p, v) => lastModified);
        }

        public static bool CheckEtags(IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;
            string path = request.Url.ToString();
            string etag = request.Headers["If-None-Match"];
            if (!string.IsNullOrEmpty(etag))
            {
                if (Etags.Values.ContainsKey(path) && Etags.Values[path].Equals(etag))
                {
                    response.StatusCode = 304;
                    return true;
                }
            }
            string ifModifiedSinceString = request.Headers["If-Modified-Since"];
            if (!string.IsNullOrEmpty(ifModifiedSinceString) && Etags.LastModified.ContainsKey(path))
            {
                DateTime ifModifiedSince = DateTime.Parse(ifModifiedSinceString);
                if (Etags.LastModified[path] < ifModifiedSince) // if the content was last modified before the requested "If-Modified-Since" date then don't send content
                {
                    response.StatusCode = 304;
                    return true;
                }
            }
            return false;
        }
    }
}
