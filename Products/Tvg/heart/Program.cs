using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;

namespace Bam.Net.Application
{
    [Serializable]
    public class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            IsolateMethodCalls = false;
            AddSwitches(typeof(ConsoleActions));
            AddConfigurationSwitches();
            AddValidArgument("nopause", true, description: "Just sleep after starting without prompting for enter");
            Initialize(args, (a) =>
            {
                OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                Environment.Exit(1);
            });
            if (Arguments.Contains("i"))
            {
                Interactive();
            }
            else
            {
                ExecuteSwitches(Arguments, new ConsoleActions());
            }
        }
    }
}
