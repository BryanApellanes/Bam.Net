using Bam.Net.Testing;
using System;

namespace Bam.Net.UserAccounts.Tests
{
    [Serializable]
    public partial class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            Initialize(args);
        }
    }
}
