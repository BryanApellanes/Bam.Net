using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;

namespace Bam.Net.UserAccounts.Tests.Integration
{
    public class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            IsolateMethodCalls = true;
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
            DefaultMethod = typeof(Program).GetMethod("Start");
        }

        public static void Start()
        {
            if (Arguments.Contains("t"))
            {
                IntegrationTestRunner.RunIntegrationTests(typeof(Program).Assembly);
            }
            else
            {
                Interactive();
            }
        }
    }
}
