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

namespace baminf
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
            AddValidArgument("sai", true, "Set assembly information");
            AddValidArgument("baminfo.json", false, "Specify the path to the baminfo.json file (used to update nuspec files)");
            AddValidArgument("v", false, "Set version", "1.0.0");
            AddValidArgument("root", "The root of the source tree");
            AddValidArgument("nuspecRoot", "The root directory to search for nuspec files");
        }

        #region do not modify
        public static void Start()
        {
            if (Arguments.Contains("sai") || Arguments.Contains("baminfo.json"))
            {
                if (Arguments.Contains("sai"))
                {
                    ConsoleActions.SetAssemblyInfo();
                }

                if (Arguments.Contains("baminfo.json"))
                {
                    ConsoleActions.SetBamInfo();
                }
            }
            else
            {
                Interactive();
            }
        }
        #endregion
    }
}
