using Bam.Net.Testing;
using System;

namespace Bam.Net.Message.Tests
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
