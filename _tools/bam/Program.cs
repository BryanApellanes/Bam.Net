﻿using Bam.Net.Application;
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
        }

        #region do not modify
        public static void Start()
        {
            ConsoleLogger logger = new ConsoleLogger
            {
                AddDetails = false
            };
            logger.StartLoggingThread();
            if (ExecuteSwitches(Arguments, typeof(WebActions), false, logger))
            {
                logger.BlockUntilEventQueueIsEmpty();
                if (Arguments.Contains("pause"))
                {
                    Pause();
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
