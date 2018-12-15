using Bam.Net.Testing;
using System;
using System.Threading;

namespace Bam.Net.Application
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            TryWritePid();
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
                    Thread.Sleep(1000);
                    Exit(1);
                });
                if (Arguments.Contains("singleProcess"))
                {
                    KillExistingProcess();
                }
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
