using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class UriApplicationNameResolver : IApplicationNameResolver, ICloneable
    {
        public UriApplicationNameResolver(BamConf bamConf)
        {
            BamConf = bamConf;
        }

        public object Clone()
        {
            UriApplicationNameResolver clone = new UriApplicationNameResolver(BamConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public BamConf BamConf { get; set; }
        object _httpContextLock = new object();
        IHttpContext _httpContext;
        public IHttpContext HttpContext
        {
            get
            {
                lock (_httpContextLock)
                {
                    return _httpContext;
                }
            }
            set
            {
                lock (_httpContextLock)
                {
                    _httpContext = value;
                }
            }
        }
        public string GetApplicationName()
        {
            return ResolveApplicationName(HttpContext);
        }

        public string ResolveApplicationName(IHttpContext context)
        {
            return ResolveApplicationName(context.Request.Url, BamConf.AppConfigs);
        }

        public static string ResolveApplicationName(Uri uri, AppConf[] appConfigs = null)
        {
            string result = AppNameFromBinding(uri, appConfigs);
            if (string.IsNullOrEmpty(result))
            {
                string fullDomainName = uri.Authority.DelimitSplit(":")[0].ToLowerInvariant();
                string[] splitOnDots = fullDomainName.DelimitSplit(".");
                result = fullDomainName;
                if (splitOnDots.Length == 2)
                {
                    result = splitOnDots[0];
                }
                else if (splitOnDots.Length == 3)
                {
                    result = splitOnDots[1];
                }
            }

            return result;
        }

        private static string AppNameFromBinding(Uri uri, AppConf[] configs)
        {
            AppConf conf = configs.Where(c => c.Bindings.Any(h => h.HostName.Equals(uri.Authority))).FirstOrDefault();
            if (conf != null)
            {
                return conf.Name;
            }
            return string.Empty;
        }
    }
}
