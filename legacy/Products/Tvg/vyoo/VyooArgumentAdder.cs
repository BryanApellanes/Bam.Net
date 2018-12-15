using Bam.Net.CommandLine;

namespace Bam.Net.Application
{
    internal class VyooArgumentAdder : CommandLineInterface
    {
        internal static void AddArguments(string[] args)
        {
            AddUtilityArguments();
            ParseArgs(args);
        }

        private static void AddUtilityArguments()
        {
            AddValidArgument("assemblyPath", false, addAcronym: true, description: "The path to the assembly to find the service class in");            
        }
    }
}
