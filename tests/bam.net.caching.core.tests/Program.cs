using Bam.Net.Testing;
using System;

namespace Bam.Net.Caching.tests
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
