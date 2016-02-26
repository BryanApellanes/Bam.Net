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
using System.Data.SqlClient;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;

namespace Wrapify
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            IsolateMethodCalls = false;

            Type type = typeof(Program);
            AddSwitches(type);
            AddSwitches(typeof(ConsoleActions));
            AddValidArgument("i", true, description: "interactive");
            AddValidArgument("?", true, description: "usage");
            AddValidArgument("root", "The root directory");
            AddValidArgument("config", "The config to use");
            AddValidArgument("clean", true, description: "Clean up .wrapified files");

            DefaultMethod = type.GetMethod("Interactive");

            ParseArgs(args);
            if (Arguments.Contains("?"))
            {
                Usage(type.Assembly);
            }
            else
            {
                if (Arguments.Length > 0 && !Arguments.Contains("i"))
                {
                    ExecuteSwitches(Arguments, typeof(ConsoleActions), null, null);
                }
                else
                {
                    Interactive();
                }
            }
        }
    }
}
