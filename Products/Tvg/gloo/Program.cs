using Bam.Net.Testing;
using System;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            GlooService.SetInfo(GlooService.ServiceInfo);
            if (!GlooService.ProcessCommandLineArgs(args))
            {
                IsolateMethodCalls = false;
                AddSwitches(typeof(ConsoleActions));
                AddConfigurationSwitches();
                ArgumentAdder.AddArguments(args);

                Initialize(args, (a) =>
                {
                    OutLineFormat("Error parsing arguments: {0}", ConsoleColor.Red, a.Message);
                    Environment.Exit(1);
                });
                if (Arguments.Contains("i"))
                {
                    Interactive();
                }
                else if(!ExecuteSwitches(Arguments, new ConsoleActions()))
                {
                    GlooService.RunService<GlooService>();
                }
            }            
        }       
    }
}
