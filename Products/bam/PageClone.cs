/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Web;
using System.IO;
using System.Reflection;
using CsQuery;

namespace Bam.Net.Server
{
	public class PageClone
	{
		public PageClone(Uri uri)
		{
			this.Uri = uri;
		}

		public Uri Uri { get; private set; }

		public string Html { get; set; }
		public Dictionary<string, string> CssFiles { get; set; }

		public void tmp()
		{
			//string html = Http.Get(url);
			//string fileName = Path.GetFileName(url);
			//CQ dollarSign = CQ.Create(html);
			//Dictionary<string, string> css = new Dictionary<string, string>();
			//OutLine("All css files: ");
			//dollarSign["link[type=text/css]"].Each(o =>
			//{
			//	string href = o.GetAttribute("href");
			//	OutLine(href, ConsoleColor.Cyan);
			//	if (!href.StartsWith("http://") && !href.StartsWith("https://"))
			//	{
			//		string cssUrl = "{0}{1}"._Format(url, href);
			//		string cssFileName = Path.GetFileName(href);
			//		OutLineFormat("Downloading: {0}", ConsoleColor.Yellow, cssUrl);
			//		css.Add(cssFileName, Http.Get(cssUrl));
			//	}
			//});
			//foreach (string key in css.Keys)
			//{
			//	OutLineFormat("{0}", ConsoleColor.Green, key);
			//	OutLineFormat("{0}", ConsoleColor.Cyan, css[key].First(500));
			//}

			//DirectoryInfo saveTo = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
			//if (!saveTo.Exists)
			//{
			//	saveTo.Create();
			//}

			//FileInfo htmlFile = new FileInfo(Path.Combine(saveTo.FullName, fileName));
			//html.SafeWriteToFile(htmlFile.FullName);
			//foreach (string key in css.Keys)
			//{
			//	FileInfo cssFile = new FileInfo(Path.Combine(saveTo.FullName, key));
			//	css[key].SafeWriteToFile(cssFile.FullName);
			//}
		}
	}
}
