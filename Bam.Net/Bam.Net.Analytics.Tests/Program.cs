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
			AddValidArgument("t", true, "run all tests");
	        DefaultMethod = typeof (Program).GetMethod("Start");
        }

	    public static void Start() 
		{
		    if (Arguments.Contains("t")) 
			{
			    RunAllTests(typeof(Program).Assembly);
		    } 
			else
			{
				Interactive();
		    }
	    }

        [ConsoleAction("Crawl For Images")]
        public void CrawlForImages()
        {
            ConnectionStringSettings s = new ConnectionStringSettings();
            s.ProviderName = SQLiteRegistrar.SQLiteFactoryAssemblyQualifiedName();

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

        [UnitTest]
        public void UrlShouldToStringCorrectly()
        {
            SQLiteRegistrar.Register<Url>();
            Db.TryEnsureSchema<Url>();

            Url test = Url.FromUri("http://twitter.github.com/bootstrap/base-css.html#tables");
            Out(test.ToString());
            Expect.AreEqual("http://twitter.github.com/bootstrap/base-css.html#tables", test.ToString());
        }

        [UnitTest]
        public void ImageCrawlerCreateMineShouldSetRootUrlAndName()
        {
            Dictionary<string, string> testSettings = new Dictionary<string, string>();
            string setAppNameTo = "TheMonkey";
            testSettings.Add("ApplicationName", setAppNameTo);
            DefaultConfiguration.SetAppSettings(testSettings);
            SQLiteRegistrar.Register(Dao.ConnectionName(typeof(Url)));
            Db.TryEnsureSchema<Url>();

            string appName = DefaultConfiguration.GetAppSetting("ApplicationName", "BAD");
            string rootUrl = "http://www.flickr.com/galleries/";

            Expect.IsFalse(appName.Equals("BAD"),"ApplicationName was not set in the config file");
            
            ImageCrawler c = (ImageCrawler)ImageCrawler.CreateMine(rootUrl);
            Expect.AreEqual(appName, setAppNameTo);
            Expect.AreEqual(appName, c.Name);
            Expect.AreEqual(rootUrl, c.Root);
        }

        [UnitTest("Should not create dupe")]
        public void ShouldNotCreateDupeUrl()
        {
            SQLiteRegistrar.Register(Dao.ConnectionName(typeof(Url)));
            Db.TryEnsureSchema<Url>();

	        string uri = "http://www.funnycatpix.com/";
            Url funnycatpix = Url.FromUri(uri, true);
	        Url check = Url.FromUri(uri, true);
			Expect.AreEqual(funnycatpix, check);
			Expect.AreEqual(funnycatpix.Id, check.Id);
        }

        [UnitTest]
        public void TestUriPieces()
        {
            Uri uri = new Uri("http://www.monkey.com/this/is/the/path?and=thisTheQueryString&some=more");
            OutLineFormat("Port: {0}", uri.Port);
			OutLineFormat("Host (Domain): {0}", uri.Host);
			OutLineFormat("Path: {0}", uri.PathAndQuery.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries)[0]);
			OutLineFormat("Query: {0}", uri.Query);

			OutLineFormat("No Query: {0}", new Uri("http://test.com/path").Query);
			OutLineFormat("No Query w/ Question Mark: {0}", new Uri("http://test.com/path?").Query);

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

        [UnitTest]
        public void ListUrls()
        {
            Init();
            UrlCollection all = Url.Where(c => c.Id != null);
            foreach (Url u in all)
            {
                OutLine(u.ToString());
            }
        }

		[UnitTest]
		public void ClassifierTest()
		{
			SQLiteRegistrar.Register<Feature>();
			Db.EnsureSchema<Feature>();

			When.A<NaiveBayesClassifier>("is trained", (classifer) =>
			{
				classifer.Train("The quick brown fox jumps over the lazy dog", "good");
				classifer.Train("make quick money in the online casino", "bad");
			})
			.TheTest
			.ShouldPass(because =>
			{
				Classifier classifier = because.ObjectUnderTest<Classifier>();
				long quickGood = classifier.FeatureCount("quick", "good");
				long quickBad = classifier.FeatureCount("quick", "bad");
				because.ItsTrue("The good count of 'quick' is " + quickGood, quickGood > 0);
				because.ItsTrue("The bad count of 'quick' is " + quickBad, quickBad > 0);
			})
			.SoBeHappy(c =>
			{
			})
			.UnlessItFailed();
		}

        //[UnitTest]
        //public void ClassifyTest()
        //{
        //    SqlClientRegistrar.Register<Feature>();
        //    _.TryEnsureSchema<Feature>();
        //    string resultOne = "";
        //    string resultTwo = "";
        //    NaiveBayesClassifier classifier = new NaiveBayesClassifier();

        //}

        private void SampleTrain(Classifier classifier)
        {
            classifier.Train("Nobody owns the water.", "good");
            classifier.Train("the quick rabbit jumps fences", "good");
            classifier.Train("buy phaarmaceuticals now", "bad");
            classifier.Train("make quick money at the online casino", "bad");
            classifier.Train("the quick brown fox jumps", "good");
        }

    }

}
