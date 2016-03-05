using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace troo
{
    internal class ArgumentAdder: CommandLineInterface
    {
        internal static void AddArguments()
        {
            AddUtilityArguments();
        }

        private static void AddUtilityArguments()
        {
            AddValidArgument("daoAssembly", false, addAcronym: true, description: "The path to the dao assembly");
            AddValidArgument("sourceDir", false, addAcronym: true, description: "The path to the directory to write source files to");
            AddValidArgument("compile", true, addAcronym: false, description: "If specified the source will be compiled to a dll suffixed by 'Dto' and the source deleted");
        }
    }
}
