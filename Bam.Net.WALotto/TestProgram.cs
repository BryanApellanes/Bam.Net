/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Naizari.Helpers;
using CsQuery;
using CsQuery.Web;
using System.Net;
using System.Threading;

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
        [ConsoleAction("Scrape WALottery")]
        public static void ScrapeWALottery()
        {            
            int year = 1984;            

            string urlFormat = "http://www.walottery.com/WinningNumbers/Search.aspx?game=37&year={0}";

            while (year < DateTime.Now.Year + 1)
            {
                WebClient client = GetWebClient();
                string url = string.Format(urlFormat, year);
                string s = client.DownloadString(url);

                CQ q = CQ.Create(s);
                q[".winningNumbersSearch_1stcell"].Each((i, v) =>
                {
                    string drawingDate = q["[id$=DrawingDate]", v].Text();
                    string numbers = q["[id$=DrawingNumbers]", v].Text();
                    string filePath = Path.Combine("Raw", year.ToString());
                    FileInfo file = new FileInfo(filePath);
                    if (!file.Directory.Exists)
                    {
                        file.Directory.Create();
                    }
                    if(file.Exists)
                    {
                        file.Delete();
                    }
                    OutFormat("Date: {0}", ConsoleTextColor.Cyan, drawingDate);
                    OutFormat("Numbers: {0}", ConsoleTextColor.Yellow, numbers);

                    string value = string.Format("{0}\r\n{1}", drawingDate, numbers);
                    value.WriteToFile(file.FullName);
                });

                year++;
            }

            Out("Done", ConsoleTextColor.Green);
        }

        private static WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Headers["User-Agent"] =
        "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
        "(compatible; MSIE 6.0; Windows NT 5.1; " +
        ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            return client;
        }

        [ConsoleAction("downloadstring")]
        public static void ExampleTestMethod()
        {
            WebClient client = new WebClient();
            client.Headers["User-Agent"] =
        "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
        "(compatible; MSIE 6.0; Windows NT 5.1; " +
        ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            string s = client.DownloadString("http://www.walottery.com/WinningNumbers/Search.aspx?game=37&year=1984");
            OutFormat("{0}", ConsoleTextColor.Green, s);
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
