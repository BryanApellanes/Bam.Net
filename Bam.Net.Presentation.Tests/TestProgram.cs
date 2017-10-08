/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Bam.Net.Web;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using System.Threading;
using System.Web.Mvc;
using CsQuery;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Presentation.Html.Tests
{
    [Serializable]
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
			DefaultMethod = typeof(TestProgram).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(TestProgram).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          * 
          * All methods addorned with ConsoleAction and TestMethod attributes 
          * must be static for the purposes of extending CommandLineTestInterface
          * or an exception will be thrown.
          * 
          */

        [UnitTest]
        public void TestPathUtility()
        {
            string absPath = "/monkey/boat/island.txt";
            string extension = Path.GetExtension(absPath);
            string path = absPath.Truncate(extension.Length);

            Expect.AreEqual("/monkey/boat/island", path);
            OutLine(path);
        }

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

        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
