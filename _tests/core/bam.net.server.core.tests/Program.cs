using Bam.Net.Testing;
using System;

namespace Bam.Net.Server.Tests
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
