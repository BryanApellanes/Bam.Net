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

namespace assver
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();

            AddValidArgument("t", true);
            DefaultMethod = typeof(Program).GetMethod("Start");

            Initialize(args);
        }

        public static void PreInit()
        {
            AddValidArgument("sv", true, "Set version");
            AddValidArgument("root", "The root of the source tree");
        }

        #region do not modify
        public static void Start()
        {
            if (Arguments.Contains("sv"))
            {
                ConsoleActions.SetVersion();
            }
            else
            {
                Interactive();
            }
        }
        #endregion
    }
}
