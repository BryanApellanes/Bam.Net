using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using Bam.Net.CommandLine;
using Bam.Net.Presentation.Handlebars;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class UrlTests: CommandLineTool
    {
        [UnitTest]
        public void UrlShouldToStringCorrectly()
        {
            Db.For<Feature>(new SQLiteDatabase(nameof(UrlShouldToStringCorrectly)));
            Db.EnsureSchema<Feature>();

            Url test = Url.FromUri("http://twitter.github.com/bootstrap/base-css.html#tables");
            Message.Print(test.ToString());
            Expect.AreEqual("http://twitter.github.com/bootstrap/base-css.html#tables", test.ToString());
        }
        
        [UnitTest("Should not create dupe")]
        [TestGroup("AdHoc")]
        public void ShouldNotCreateDupeUrl()
        {
            SQLiteRegistrar.Register(Dao.ConnectionName(typeof(Url)));
            Db.For<Url>().ExecuteSql("DELETE FROM URL");
            Db.TryEnsureSchema<Url>();

            string uri = "http://www.funnycatpix.com/";
            Url funnycatpix = Url.FromUri(uri, true);
            Url check = Url.FromUri(uri, true);
            Expect.AreEqual(funnycatpix.ToString(), check.ToString());
            Expect.AreEqual(funnycatpix.Id, check.Id);
        }

        [UnitTest]
        public void TestUriPieces()
        {
            Uri uri = new Uri("http://www.monkey.com/this/is/the/path?and=thisTheQueryString&some=more");
            Message.PrintLine("Port: {0}", uri.Port);
            Message.PrintLine("Host (Domain): {0}", uri.Host);
            Message.PrintLine("Path: {0}", uri.PathAndQuery.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            Message.PrintLine("Query: {0}", uri.Query);

            Message.PrintLine("No Query: {0}", new Uri("http://test.com/path").Query);
            Message.PrintLine("No Query w/ Question Mark: {0}", new Uri("http://test.com/path?").Query);
        }

        [ConsoleAction("write", "write operators")]
        public void WriteOperators()
        {
            ConsoleLogger logger = new ConsoleLogger();
            Handlebars.HandlebarsDirectory = new HandlebarsDirectory("./Templates", logger);
            string fileName = "./tmp.txt";
            foreach(string dataType in new string[]{"int", "uint", "ulong", "long", "decimal", "int?", "uint?", "ulong?", "decimal?", "string", "DateTime", "DateTime?"})
            {
                string result = Handlebars.Render("operators", new {DataType = dataType});
                result.SafeAppendToFile("./operators.txt");
                result = Handlebars.Render("generic-operators", new {DataType = dataType});
                result.SafeAppendToFile("./generic-operators.txt");
                Message.PrintLine("Wrote file {0}", new FileInfo(fileName).FullName);
            }
        }
    }
}
