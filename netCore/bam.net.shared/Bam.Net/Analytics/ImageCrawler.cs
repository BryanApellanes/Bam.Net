/*
	Copyright © Bryan Apellanes 2015  
*/
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Analytics;
using CsQuery;
using Bam.Net.Configuration;

namespace Bam.Net.Analytics
{
	public partial class ImageCrawler : UrlCrawler
    {
        public static ICrawler CreateMine(string url)
        {
            ICrawler result = null;
            string name = "{0}"._Format(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN"));
            if (Instances.ContainsKey(name))
            {
                result = Instances[name];
            }
            else
            {
                result = Create(name, url);
            }

            return result;
        }

        public static ICrawler Create(string url)
        {
            return Create(DefaultConfiguration.GetAppSetting("ApplicationName_{0}"._Format(url), url));
        }

        static object _createLock = new object();
        public static ICrawler Create(string name, string url)
        {
            lock (_createLock)
            {
                ImageCrawler crawler = new ImageCrawler();
                crawler.Name = name;
                crawler.Root = url;

                if (Instances.ContainsKey(name))
                {
                    Instances[name] = crawler;
                }
                else
                {
                    Instances.Add(crawler.Name, crawler);
                }

                ImageCrawler.EnsureOne(crawler);
                
                return crawler;
            }
        }

        public Action<Url, string> OnImageFound { get; set; }

        public override bool WasProcessed(string target = "")
        {
            Url url = Url.FromUri(target);
            if (url.Id == null)
            {
                return false;
            }
            return Image.OneWhere(c => c.UrlId == url.Id) != null;
        }

        /// <summary>
        /// Reads the target and saves the url of any img tag it finds
        /// </summary>
        /// <param name="target"></param>
        public override void ProcessTarget(string target)
        {
            Url url = Url.FromUri(target);//new Url(target);
            if (url.Id == null)
            {
                url.Save();
            }
            CQ q = CQ.Create(url.Html);

            q["img"].Each((i, d) =>
            {
                try
                {
                    string imgUrl = d.Attributes["src"];

                    if (Uri.IsWellFormedUriString(imgUrl, UriKind.Relative))
                    {
                        imgUrl = $"{url.ProtocolOfProtocolId.Value}://{url.DomainOfDomainId.Value}{url.PathOfPathId.Value}{imgUrl}";
                    }
                    Url image = Url.FromUri(new Uri(imgUrl), true);// new Url(imgUrl);                    
                    Image img = Image.OneWhere(c => c.UrlId == image.Id);
                    if (img == null)
                    {
                        Crawler cr = Crawler.OneWhere(c => c.Name == this.Name);
                        cr.RootUrl = target;
                        img = new Image();
                        img.UrlId = image.Id;
                        img.Date = DateTime.UtcNow;
                        img.CrawlerId = cr.Id;
                        img.Save();
                    }

                    if (OnImageFound != null)
                    {
                        OnImageFound(url, imgUrl);
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.AddEntry("Error occurred in image crawler: {0}", ex, ex.Message);
                }
            });
        }
    }
}
