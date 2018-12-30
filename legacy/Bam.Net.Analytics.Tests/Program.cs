/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.IO;
using System.Configuration;

using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Data;
using Bam.Net.Analytics;
using Bam.Net.Analytics.Classification;

namespace Bam.Net.Analytics.Crawlers.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
	        DefaultMethod = typeof (Program).GetMethod("Start");
        }

	    public static void Start() 
		{
		    if (Arguments.Contains("t")) 
			{
			    RunAllUnitTests(typeof(Program).Assembly);
		    } 
			else
			{
				Interactive();
		    }
	    }

        [ConsoleAction("Crawl For Images")]
        public void CrawlForImages()
        {
            ConnectionStringSettings s = new ConnectionStringSettings
            {
                ProviderName = SQLiteRegistrar.SQLiteFactoryAssemblyQualifiedName()
            };

            string path = Prompt("Please enter the path to the Analytics database");
            s.ConnectionString = string.Format("Data Source={0};Version=3;", path);

            DefaultConnectionStringResolver.Instance.Resolver = (cn) =>
            {
                if (cn.Equals(Image.ConnectionName(typeof(Image))))
                {
                    OutFormat("Resolved connectionString {0}", s.ConnectionString);
                    return s;
                }

                OutFormat("Couldn't resolve connectionString for connectionName {0}", cn);
                return null;
            };
            SQLiteRegistrar.Register<Image>();

            ImageCrawler crawler = new ImageCrawler();
            crawler.OnImageFound = (u, src) =>
            {
                string url = u.ToString();
                OutFormat("Url: {0}\r\n", ConsoleColor.Cyan, url);
                OutFormat("Src: {0}\r\n", ConsoleColor.Green, src);
            };

            crawler.ActionChanged += (cr, args) =>
            {
                OutFormat("{0}::{1}", args.OldAction.ToString(), args.NewAction.ToString());
            };

            string startUrl = Prompt("Enter the starting url");
            crawler.Name = startUrl;
            Crawler data = new Crawler();
            data.Name = startUrl;
            data.RootUrl = startUrl;
            data.Save();
            crawler.Crawl(startUrl);
        }

        [ConsoleAction("Get some images")]
        public void AddYourActionHere()
        {
            SQLiteRegistrar.Register("Analytics");
            ImageCollection images = Image.Top(10, c => c.Id >= 1, Order.By<ImageColumns>(c => c.Id, SortOrder.Descending));
            foreach (Image image in images)
            {
                Out(image.UrlOfUrlId.ToString());
            }
        }

        List<string> _srcs;
        [ConsoleAction]
        public void Crawl()
        {
            Init();
            _srcs = new List<string>();
            string connectionString = Db.For<Image>().ConnectionString;
			OutLineFormat("Connection String: {0}", ConsoleColor.Cyan, connectionString);

            try
            {
                connectionString.SafeWriteToFile(".\\Analytics.sqlite.connection.txt");
            }catch{}

            ImageCrawler crawler = new ImageCrawler();
            crawler.OnImageFound = (u, src) =>
            {
                if (!_srcs.Contains(src))
                {
                    _srcs.Add(src);
                    string url = u.ToString();
                    OutFormat("Url: {0}\r\n", ConsoleColor.Cyan, url);
                    OutFormat("Src: {0}\r\n", ConsoleColor.Green, src);

                }
            };
            crawler.Crawl("http://www.funnycatpix.com/");
        }

        private static void Init()
        {
            SQLiteRegistrar.Register<Url>();
            Db.TryEnsureSchema<Url>();
        }
    }

}
