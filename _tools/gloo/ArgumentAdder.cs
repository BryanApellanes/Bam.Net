using Bam.Net.CommandLine;

namespace Bam.Net.Application
{
    internal class ArgumentAdder: CommandLineInterface
    {
        internal static void AddArguments(string[] args)
        {
            AddUtilityArguments();
            ParseArgs(args);
        }

        private static void AddUtilityArguments()
        {
            AddValidArgument("assemblyPath", false, addAcronym: true, description: "The path to the assembly to find the service class in");
            AddValidArgument("verbose", false, addAcronym: true, description: "If affirmative log responses to the console");
            AddValidArgument("singleProcess", true, addAcronym: false, description: "Kill other gloo processes currently running that were started with the same command line arguments");
            AddValidArgument("AssemblySearchPattern", false, true, "The search pattern to use when scanning for service registries");
            AddValidArgument("host", false, false, "When generating client proxies, the host name to download proxies from");
            AddValidArgument("port", false, false, "When generating client proxies, the port on the remote host to download proxies from");
            AddValidArgument("nameSpace", false, false, "When generating client proxies, the namespace the type is in");
            AddValidArgument("typeName", false, false, "When generating client proxies, the name of the type");
            AddValidArgument("output", false, false, "When generating client proxies, the directory path to write to");
        }
    }
}
