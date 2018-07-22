using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.Testing.CommandLineTestInterface" />
    [Serializable]
    public class BuildClient : CommandLineTestInterface
    {
        [ConsoleAction("setBambotHost", "set the hostname for bambot builds")]
        public static void SetHost()
        {
            string configFilePath = Path.Combine(Paths.Builds, "buildclient.json");
            if (!File.Exists(configFilePath))
            {
                new BuildClientInfo().ToJsonFile(configFilePath);
            }
            BuildClientInfo info = configFilePath.FromJsonFile<BuildClientInfo>();
            info.BambotHost = GetArgument("bambothost", $"Please enter the hostname for bambot (current = {info.BambotHost ?? "null"}");
            string infoJson = info.ToJson();
            infoJson.SafeWriteToFile(configFilePath, true);
            OutLine(configFilePath, ConsoleColor.Cyan);
            OutLine(infoJson, ConsoleColor.Green);
        }

        [ConsoleAction]
        public static void GetConfig()
        {

        }

        [ConsoleAction]
        public static void SetConfig()
        {

        }
    }
}
