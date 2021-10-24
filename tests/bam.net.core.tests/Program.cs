using Bam.Net.Testing;
using System;

namespace Bam.Net.Tests
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
