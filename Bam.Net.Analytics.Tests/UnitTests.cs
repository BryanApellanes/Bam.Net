using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Analytics.Classification;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {

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

            Expect.IsFalse(appName.Equals("BAD"), "ApplicationName was not set in the config file");

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

        private static void Init()
        {
            SQLiteRegistrar.Register<Url>();
            Db.TryEnsureSchema<Url>();
        }
    }
}
