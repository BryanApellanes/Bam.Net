/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Bam.Net.Testing;
using System.IO;
using Bam.Net.Dust;
using Bam.Net.CommandLine;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.ComponentModel;
using Bam.Net;

namespace CommandLineTests
{
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
			AddValidArgument("t", true, "run all tests");
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

        // To run ConsoleAction methods use the command line argument /i.        
        
        [ConsoleAction("Long max")]
        public static void LongMax()
        {
            OutLineFormat("Int: {0}", Int32.MaxValue);
            OutLineFormat("Logn: {0}", Int64.MaxValue);
        }

        [UnitTest]
        public static void ShouldCompileDust()
        {
            DustTemplate dc = new DustTemplate("Hello {name}!", "test");
            dc.Compile();
            OutLine();
            OutLine(dc.CompiledScript, ConsoleColor.Green);
        }

        [UnitTest]
        public static void ShouldRenderDust()
        {
            DustTemplate dc = new DustTemplate("Hello {Name}!", "test");
            object value = new { Name = "Guy" };
            string result = dc.Render(value);
            Expect.AreEqual("Hello Guy!", result);
            OutLine();
            OutLine(result, ConsoleColor.Yellow);
            value = new { Name = "Dude" };
            result = dc.Render(value);
            Expect.AreEqual("Hello Dude!", result);
            OutLine(result, ConsoleColor.Yellow);            
        }

        [UnitTest]
        public static void ShouldRenderDustUsingStatic()
        {
            DustTemplate dc = new DustTemplate("Hello {Name}!", "test");
            dc.Compile();
            object value = new { Name = "Guy" };
            MvcHtmlString result = Dust.RenderMvcHtmlString("test", value);
            Expect.AreEqual("Hello Guy!", result.ToHtmlString());
            OutLine();
            OutLine(result.ToString(), ConsoleColor.Yellow);
            value = new { Name = "Dude" };
            result = Dust.RenderMvcHtmlString("test", value);
            Expect.AreEqual("Hello Dude!", result.ToHtmlString());
            OutLine(result.ToString(), ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void TestFunkyEscapeSequenceInServerRender()
        {
            DustTemplate dc = new DustTemplate(@"Hello {Name}!
this is another line
and another", "test");
            dc.Compile();

            object value = new { Name = "Guy" };
            string result = Dust.RenderMvcHtmlString("test", value).ToString();
            OutLine(result, ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void TestUnescape()
        {
            string value = @"\r\n\t";
            OutLineFormat("Value: {0}", ConsoleColor.Yellow, value);
            OutLineFormat("Unescaped Value: {0}", ConsoleColor.Cyan, Regex.Unescape(value));
        }

        [UnitTest]
        public static void TemplateNameShouldBeFileNameWithoutExtension()
        {
            FileInfo file = new FileInfo("test.dust");
            FileInfo file2 = new FileInfo("test2.txt");
            if (!file.Exists)
            {
                "Hello {name}!".SafeWriteToFile(file.FullName);
                file.Refresh();
            }

            if (!file2.Exists)
            {
                "Hello2 {name}!".SafeWriteToFile(file2.FullName);
                file2.Refresh();
            }

            DustTemplate dc = new DustTemplate(file);
            Expect.AreEqual("test", dc.Name);
            dc = new DustTemplate(file2);
            Expect.AreEqual("test2", dc.Name);
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
