/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Bam.Net.Analytics
{
    public partial class ImageCrawler
    {
        public static void EnsureOne(Bam.Net.Analytics.ImageCrawler crawler)
        {
            Crawler value = Crawler.OneWhere(c => c.Name == crawler.Name);
            if (value == null)
            {
                value = new Crawler();
                value.Name = crawler.Name;
                value.RootUrl = crawler.Root;
                value.Save();
            }
            else
            {
                crawler.Root = value.RootUrl;
            }
        }
    }
}
