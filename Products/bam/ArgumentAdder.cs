using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Application
{
    internal class ArgumentAdder: CommandLineInterface
    {
        internal static void AddArguments()
        {
            AddManagementArguments();
            AddHeartArguments();
        }

        private static void AddHeartArguments()
        {
            AddValidArgument("org", false, description: "heart: The name of your organization");
            AddValidArgument("email", false, description: "heart: Your email address");
            AddValidArgument("password", false, description: "heart: Your password to automate operations that require authentication");
            AddValidArgument("app", false, description: "heart: The name of the application you are acting on");
        }

        private static void AddManagementArguments()
        {
            AddValidArgument("root", false, description: "The root directory to pack files from");
            AddValidArgument("saveTo", false, description: "The zip file to create when packing the toolkit");
            AddValidArgument("appName", false, description: "The name of the app to create when calling /cca (create client app) or /cm (create manifest)");
            AddValidArgument("appDirectory", false, description: "The directory path to create an app manifest for");

            AddValidArgument("configName", false, description: "The name of the config to retrieve");

            AddValidArgument("ProcessMode", false, description: "Set an alternate ProcessMode instead of the one specified in the config");

            AddValidArgument("subject", false, description: "/sendEmail: The subject of the email to send");
            AddValidArgument("message", false, description: "/sendEmail: The body of the email message");
        }
    }
}
