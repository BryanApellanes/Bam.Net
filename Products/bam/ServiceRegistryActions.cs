using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Application
{
    [Serializable]
    public class ServiceRegistryActions: CommandLineTestInterface
    {
        [ConsoleAction("writeRegistryCodeFile", "Write a registry container code file from a template")]
        public void WriteRegistryCodeFile()
        {
            string registryName = GetArgument("RegistryName", "Enter the name of the registry to create.");
            string nameSpace = GetArgument("Namespace", "Enter the namespace for the new registry to create.");
            string description = GetArgument("Description", "Enter a description for the new registry.");
            FileInfo file = new FileInfo($".\\{registryName}Container.cs");
            if (file.Exists)
            {
                if(!ConfirmFormat("File exists: {0}\r\nOverwrite?", file.FullName))
                {
                    return;
                }
            }
            RegistryInfo info = new RegistryInfo { Namespace = nameSpace, RegistryName = registryName, Description = "description" };
            string templateText = ".\\ServiceRegistryTemplate.txt".SafeReadFile();
            OutLine(templateText, ConsoleColor.Cyan);
            
            string output = templateText.Replace("{{RegistryName}}", info.RegistryName).Replace("{{Description}}", info.Description).Replace("{{Namespace}}", info.Namespace);
            OutLine(output, ConsoleColor.Yellow);
            output.SafeWriteToFile(file.FullName, true);
            OutLineFormat("File was written: {0}", ConsoleColor.Green, file.FullName);
        }
    }
}
