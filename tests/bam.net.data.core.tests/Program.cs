using Bam.Net.Testing;
using System;
using Bam.Net.CommandLine;
using Bam.Net.Logging;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    class Program : CommandLineTool
    {
        static void Main(string[] args)
        {
            AddSwitches();
            AddConfigurationSwitches();
            Initialize(args);
            if (Arguments.Length > 0 && !Arguments.Contains("i"))
            {
                ExecuteSwitches(false, new ConsoleLogger());
            }
        }
    }
}
