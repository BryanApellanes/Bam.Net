using Bam.Net.Testing;
using System;

namespace Bam.Net.ServiceProxy.Tests
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
