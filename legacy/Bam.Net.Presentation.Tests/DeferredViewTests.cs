using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class DeferredViewTests: CommandLineTestInterface
    {
        [UnitTest]
        public static void DeferredContentShouldRender()
        {
            Tag done = new Tag("span").Text("done");
            int runCount = 0;
            DeferredView view = new DeferredView("Monkey", (dv) =>
            {
                // long processing
                Thread.Sleep(1000);
                runCount++;
                return done;
            },
            () =>
            {
                return new Tag("span").Text("initial");
            }, 300);

            Expect.AreEqual(0, runCount, "runcount mismatch");

            string initial = view.Render().ToHtmlString();
            OutLineFormat("Should be initial: \r\n{0}", initial);
            FileInfo compareToFile = new FileInfo(string.Format(".\\{0}_initial.txt", MethodBase.GetCurrentMethod().Name));
            Compare(view.Content, compareToFile);

            OutLine("waiting 2 seconds");
            Thread.Sleep(2000);
            Expect.AreEqual(1, runCount, "runcount mismatch");

            string doneHtml = view.Render().ToHtmlString();
            OutLineFormat("Should be done: \r\n{0}", doneHtml);
            compareToFile = new FileInfo(string.Format(".\\{0}_done.txt", MethodBase.GetCurrentMethod().Name));
            Compare(view.Content, compareToFile);

            string retreived = DeferredViewController.GetContentString(view.Name);
            Expect.AreEqual(1, runCount);
            string reretrieved = DeferredViewController.GetContentString(view.Name, true);
            Expect.AreEqual(retreived, reretrieved);
            Expect.AreEqual(2, runCount);
        }

        private static void Compare(MvcHtmlString tag, FileInfo compareToFile)
        {
            string compare = "";
            if (!compareToFile.Exists)
            {
                OutLine("The comparison file was not found, using result as comparison", ConsoleColor.Yellow);
                using (StreamWriter sw = new StreamWriter(compareToFile.FullName))
                {
                    sw.Write(tag.ToHtmlString());
                }
            }

            using (StreamReader sr = new StreamReader(compareToFile.FullName))
            {
                compare = sr.ReadToEnd();
            }

            Expect.IsNotNullOrEmpty(compare);
            Expect.AreEqual(compare, tag.ToHtmlString().ToString());
            OutLine(compare, ConsoleColor.Cyan);
        }
    }
}
