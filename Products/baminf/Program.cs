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
            AddValidArgument("sai", true, description: "Set assembly information");
            AddValidArgument("baminfo.json", false, description: "Specify the path to the baminfo.json file (used to update nuspec files)");
            AddValidArgument("v", false, description: "Set version", valueExample: "1.0.0");
            AddValidArgument("root", "The root of the source tree");
            AddValidArgument("nuspecRoot", "The root directory to search for nuspec files");
            AddValidArgument("aip", false, addAcronym: false, description: "The path to the aip (Advanced Installer Project) file");
            AddValidArgument("smsiv", true, addAcronym: false, description: "Set msi version in aip (Advanced Installer Project) file");
        }

        #region do not modify
        public static void Start()
        {
            if (Arguments.Contains("sai") || Arguments.Contains("baminfo.json") || Arguments.Contains("smsiv"))
            {
                if (Arguments.Contains("sai"))
                {
                    ConsoleActions.SetAssemblyInfo();
                }

                if (Arguments.Contains("baminfo.json"))
                {
                    ConsoleActions.SetBamInfo();
                }

                if (Arguments.Contains("smsiv"))
                {
                    ConsoleActions.SetMsiVersion();
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
