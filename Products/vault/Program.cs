using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net;

namespace vault
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            Resolver.Register();
            IsolateMethodCalls = false;

            Type type = typeof(Program);
            AddSwitches(typeof(Program));

            DefaultMethod = type.GetMethod("Interactive");

            Initialize(args);

            if (Arguments.Length > 0 && !Arguments.Contains("i"))
            {
                ExecuteSwitches(Arguments, type);
            }
            else
            {
                Interactive();
            }
        }
    }
}
