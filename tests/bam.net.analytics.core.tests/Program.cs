using System;
using Bam.Net.Testing;

namespace Bam.Net.Analytics.Tests
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
