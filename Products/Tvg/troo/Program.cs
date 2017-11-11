using Bam.Net.Logging;
using Bam.Net.Testing;
using System;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            TrooService.SetInfo(TrooService.ServiceInfo);
            if (!TrooService.ProcessCommandLineArgs(args))
            {
                IsolateMethodCalls = false;             
                AddSwitches(typeof(UtilityActions));
                AddConfigurationSwitches();
                ArgumentAdder.AddArguments(args);

                Initialize(args, (a) =>
                {
                    OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                    Environment.Exit(1);
                });

                if (Arguments.Length > 0 && !Arguments.Contains("i"))
                {
                    ExecuteSwitches(Arguments, typeof(UtilityActions), false, Log.Default);
                }
                else if (Arguments.Contains("i"))
                {
                    Interactive();
                }
                else
                {
                    TrooService.RunService<TrooService>();
                }
            }
        }
    }
}
