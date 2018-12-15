using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class UrlTests: CommandLineTestInterface
    {
        [UnitTest]
        public void UrlShouldToStringCorrectly()
        {
            Db.For<Feature>(new SQLiteDatabase(nameof(UrlShouldToStringCorrectly)));
            Db.EnsureSchema<Feature>();

            Url test = Url.FromUri("http://twitter.github.com/bootstrap/base-css.html#tables");
            Out(test.ToString());
            Expect.AreEqual("http://twitter.github.com/bootstrap/base-css.html#tables", test.ToString());
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
    }
}
