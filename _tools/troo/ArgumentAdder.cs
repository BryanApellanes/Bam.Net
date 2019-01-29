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
            AddValidArgument("config", false, addAcronym: true, description: "The path to a json or yaml file containing a serialized GenerationConfig, if specified all other arguments are ignored.  Only valid for /generateSchemaRepository (/gsr) switch");
            AddValidArgument("typeAssembly", false, addAcronym: true, description: "The path to the dao assembly");
            AddValidArgument("schemaName", false, addAcronym: true, description: "The name to use for the generated schema");
            AddValidArgument("fromNameSpace", false, addAcronym: true, description: "The namespace containing types to generate daos for");
            AddValidArgument("toNameSpace", false, addAcronym: true, description: "The namespace to write generated daos into");
            AddValidArgument("writeTo", false, addAcronym: true, description: "Copy the resulting assembly to the specified directory");
            AddValidArgument("writeSrc", false, addAcronym: true, description: "The path to write source files to");            
            AddValidArgument("checkForIds", false, addAcronym: true, description: "Check the specified data classes for Id properties");
            AddValidArgument("useInheritanceSchema", false, addAcronym: true, description: "If yes the generated Repository will inherit from DatabaseRepository otherwise DaoRepository");
        }
    }
}
