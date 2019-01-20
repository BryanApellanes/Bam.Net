using Bam.Net.Testing;
using System;

namespace Bam.Net
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            AddArguments();
            Initialize(args);
        }

        public static void AddArguments()
        {

        }
    }
}
