using Bam.Net.Testing;
using System;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    class Program : CommandLineTool
    {
        static void Main(string[] args)
        {
            ExecuteMainOrInteractive(args);
        }
    }
}
