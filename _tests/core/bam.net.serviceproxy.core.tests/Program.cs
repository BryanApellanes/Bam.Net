using Bam.Net.Testing;
using System;

namespace Bam.Net.ServiceProxy.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            Initialize(args);
        }
    }
}
