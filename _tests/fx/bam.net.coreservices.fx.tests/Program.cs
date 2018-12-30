using Bam.Net.Testing;
using System;

namespace Bam.Net.CoreServices.Tests
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
