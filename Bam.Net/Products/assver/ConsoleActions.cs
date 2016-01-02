using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;

namespace assver
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        // See the below for examples of ConsoleActions and UnitTests

        #region ConsoleAction examples
        [ConsoleAction("sv", "Set Version")]
        public static void SetVersion()
        {
            string srcRoot = Arguments["root"] ?? Prompt("Please enter the root of the source tree");
            string version = Arguments["sv"] ?? Prompt("Please enter the version number");            
            string format = "[assembly: AssemblyVersion(\"{0}\")]";

            DirectoryInfo srcRootDir = new DirectoryInfo(srcRoot);
            srcRootDir.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories).Each(infoFile =>
            {
                OutLineFormat("Writing assembly version into: {0}", ConsoleColor.Blue, infoFile.FullName);
                StringBuilder newContent = new StringBuilder();
                using (StreamReader reader = new StreamReader(infoFile.FullName))
                {
                    while (!reader.EndOfStream)
                    {
                        string currentLine = reader.ReadLine();
                        if (currentLine.StartsWith("[assembly: AssemblyVersion"))
                        {
                            currentLine = format._Format(version);
                        }
                        newContent.AppendLine(currentLine);
                    }
                }
                newContent.ToString().SafeWriteToFile(infoFile.FullName, true);
            });
        }
        #endregion
    }
}