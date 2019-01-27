using Bam.Net.Application;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;

namespace Bam.Net
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            AddArguments();
            AddValidArgument("pause", true, addAcronym: false, description: "pause before exiting, only valid if command line switches are specified");
            DefaultMethod = typeof(Program).GetMethod("Start");

            Initialize(args);
        }

        public static void AddArguments()
        {
            AddSwitches(typeof(WebActions));
            AddValidArgument("schemaName", false, addAcronym: true, description: "The name to use for the generated schema");
        }

        #region do not modify
        public static void Start()
        {
            ConsoleLogger logger = new ConsoleLogger
            {
                AddDetails = false
            };
            logger.StartLoggingThread();
            if (Arguments.Contains("pause"))
            {
                Pause("paused..."); // for debugging
            }
            if (ExecuteSwitches(Arguments, typeof(WebActions), false, logger))
            {
                logger.BlockUntilEventQueueIsEmpty();                
            }
            else
            {
                Interactive();
            }
        }
        #endregion
    }
}
