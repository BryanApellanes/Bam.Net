/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Caching.File;
using Bam.Net.Javascript;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts.Data;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Bam.Net.Server.Meta;
using Bam.Net.Data.Repositories;
using Bam.Net.Configuration;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    /// <summary>
    /// The primary responder for all content files found in ~s:/ (defined as BamServer.ContentRoot)
    /// </summary>
    public partial class ContentResponder : Responder, IInitialize<ContentResponder>
    {
        public void RefreshLayouts()
        {
            Task.Run(() =>
            {
                IncludesCache.Clear();
                foreach (AppContentResponder appContent in AppContentResponders.Values.ToArray())
                {
                    appContent.LayoutModelsByPath.Clear();
                }
            });
        }

        protected internal Includes GetCommonIncludes()
        {
            return GetCommonIncludes(Root);
        }

        protected static internal Includes GetCommonIncludes(string root)
        {
            string includeJs = Path.Combine(root, "apps", IncludeFileName);
            return GetIncludesFromIncludeJs(includeJs);
        }

        /// <summary>
        /// Gets the Includes for the specified AppConf by reading the 
        /// include.js file in the application folder.
        /// </summary>
        /// <param name="appConf"></param>
        /// <returns></returns>
        protected static internal Includes GetAppIncludes(AppConf appConf)
        {
            string includeJs = Path.Combine(appConf.AppRoot.Root, IncludeFileName);
            string appRoot = Path.DirectorySeparatorChar.ToString();
            Includes includes = GetIncludesFromIncludeJs(includeJs);
            includes.Scripts.Each((scr, i) =>
            {
                includes.Scripts[i] = Path.Combine(appRoot, scr).Replace("\\", "/");
            });
            includes.Css.Each((css, i) =>
            {
                includes.Css[i] = Path.Combine(appRoot, css).Replace("\\", "/");
            });

            return includes;
        }

        static Dictionary<string, Includes> _includesCache;
        static object _includesCacheLock = new object();
        protected static internal Dictionary<string, Includes> IncludesCache
        {
            get
            {
                return _includesCacheLock.DoubleCheckLock(ref _includesCache, () => new Dictionary<string, Includes>());
            }
        }

        ConcurrentDictionary<string, byte[]> _pageMinCache;
        object _pageMinCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> MinCache
        {
            get
            {
                return _pageMinCacheLock.DoubleCheckLock(ref _pageMinCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }

        ConcurrentDictionary<string, byte[]> _zippedPageMinCache;
        object _zippedPageMinCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> ZippedMinCache
        {
            get
            {
                return _zippedPageMinCacheLock.DoubleCheckLock(ref _zippedPageMinCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }

        protected static internal Includes GetIncludesFromIncludeJs(string includeJs)
        {
            Includes returnValue = new Includes();
            string[] result = new string[] { };
            if (IncludesCache.ContainsKey(includeJs))
            {
                returnValue = IncludesCache[includeJs];
            }
            else if (File.Exists(includeJs))
            {
                lock (_includesCacheLock)
                {
                    dynamic include = includeJs.JsonFromJsLiteralFile("include").JsonToDynamic();
                    returnValue.Css = ((JArray)include["css"]).Select(v => (string)v).ToArray();
                    returnValue.Scripts = ((JArray)include["scripts"]).Select(v => (string)v).ToArray();
                    IncludesCache[includeJs] = returnValue;
                }
            }

            return returnValue;
        }
    }
}
