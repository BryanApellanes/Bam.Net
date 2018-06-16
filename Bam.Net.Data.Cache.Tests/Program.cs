using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Cache.Tests
{
    class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            DefaultMethod = typeof(Program).GetMethod("Start");
            Initialize(args);
        }
        public static void Start()
        {
            if (Arguments.Contains("t"))
            {
                RunAllUnitTests(typeof(Program).Assembly);
            }
            else
            {
                Interactive();
            }
        }
    }
}
