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
using KLGates.Core.CommandLine;
using KLGates.Core;
using KLGates.Core.Testing;
using KLGates.Core.Encryption;

namespace TestBuildProject
{
    [Serializable]
    public class ExampleConsoleActions : CommandLineTestInterface
    {
        #region ConsoleAction examples
        [ConsoleAction("Non Static Test")]
        public void NonStatic()
        {
            Pass("Test passed");
        }

        [ConsoleAction("Static Test")]
        public static void StaticTest()
        {
            Pass("Test passed");
        }

        [ConsoleAction("With Parameters")]
        public static void WithParameters(string inputString)
        {
            OutFormat("You typed {0}", inputString);
        }

        [ConsoleAction("Warn")]
        public void DoWarn()
        {
            Warn("This is a warning message");
        }

        [ConsoleAction("Error")]
        public void DoError()
        {
            Error("This is an error", new Exception("This is an exception"));
        }
        #endregion

    }
}
