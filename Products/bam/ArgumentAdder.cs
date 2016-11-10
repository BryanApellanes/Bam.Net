using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace bam
{
    internal class ArgumentAdder: CommandLineInterface
    {
        internal static void AddArguments()
        {
            AddManagementArguments();
            AddVaultArguments();
            AddGlooArguments();
        }

        private static void AddGlooArguments()
        {
            AddValidArgument("host", false, description: "gloo: The hostname to download gloo client from");
            AddValidArgument("port", false, description: "gloo: The port to download gloo client from");
        }

        private static void AddVaultArguments()
        {
            AddValidArgument("vaultPath", false, addAcronym: true, description: "readVault: The path to a vault to read");
            AddValidArgument("vaultName", false, addAcronym: true, description: "readVault: The name of the vault to read");
        }

        private static void AddManagementArguments()
        {
            AddValidArgument("root", false, description: "The root directory to pack files from");
            AddValidArgument("saveTo", false, description: "The zip file to create when packing the toolkit");
            AddValidArgument("appName", false, description: "The name of the app to create when calling /ca (create app)");
        }
    }
}
