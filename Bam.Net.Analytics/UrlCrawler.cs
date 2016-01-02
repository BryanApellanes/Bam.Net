/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Net;
using Bam.Net.Analytics;

namespace Bam.Net.Analytics
{
    public abstract class UrlCrawler: BaseCrawler
    {
        public UrlCrawler()
        {
        }

        public override string[] ExtractTargets(string target)
        {
            List<string> results = new List<string>();
            Url url = Url.FromUri(target);
            CQ q = CQ.Create(url.Html);
            q["a"].Each((i, d) =>
            {
                string href = d.Attributes["href"];
                if (href != null && !href.Equals("#"))
                {
                    if (!Uri.IsWellFormedUriString(href, UriKind.Absolute))
                    {
                        try
                        {
                            href = System.IO.Path.Combine(Root, href);
                        }
                        catch //(Exception ex)
                        {
                            return;
                        }
                        if (!Uri.IsWellFormedUriString(href, UriKind.Absolute))
                        {
                            return;
                        }
                    }

                    results.Add(href);
                }
            });

            return results.ToArray();
        }

    }
}
