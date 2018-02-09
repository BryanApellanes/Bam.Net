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
            AddValidArgument("apps", false, addAcronym: false, description: "Specify the name(s) of applications to serve, in a comma or semi-colon separated list; use the name of the app as specified in the apps 'appConf.json' file.");
            AddValidArgument("verbose", false, addAcronym: true, description: "If affirmative log responses to the console");
        }
    }
}
