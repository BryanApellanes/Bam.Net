using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Web;
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
            GetBuildClientInfo(out string configFilePath, out BuildClientInfo info);
            info.BambotHost = GetArgument("bambothost", $"Please enter the hostname for bambot (current = {info.BambotHost ?? "null"}");
            string infoJson = info.ToJson();
            infoJson.SafeWriteToFile(configFilePath, true);
            OutLine(configFilePath, ConsoleColor.Cyan);
            OutLine(infoJson, ConsoleColor.Green);
        }

        [ConsoleAction("getBambotConfig", "get a bambot configuration by name")]
        public static void GetConfig()
        {
            GetBuildClientInfo(out string configFilePath, out BuildClientInfo info);
            string configName = GetArgument("configName", "Enter the name of the config to get.");
            string hmacify = $"/bambot/config/{configName}";
            string json = Http.Get($"http://{info.BambotHost}{hmacify}", new Dictionary<string, string>
            {
                { Headers.ValidationToken, $"sha1={hmacify.HmacSha1(info.BuildKey)}" }
            });
            OutLine(json, ConsoleColor.Cyan);
        }

        // TODO: account for config file format on the bambot server. ${serviceName}-${repoName}-${branch}.json
        // serviceName = github
        // repoName = Bam.Net
        // branch = the name of the branch to configure to build.
        [ConsoleAction("setBambotConfig", "upload a bambot configuration (a build file, also usable by bake.exe)")]
        public static void SetConfig()
        {
            GetBuildClientInfo(out string configFilePath, out BuildClientInfo info);
            string configFile = GetArgument("configFile", "Enter the path to the config to upload");
            string body = configFile.SafeReadFile();
            string configName = Path.GetFileNameWithoutExtension(configFile);
            string hmacify = $"/bambot/config/{configName}{body}";
            string response = Http.Post($"http://{info.BambotHost}/bambot/config/{configName}", body, new Dictionary<string, string>
            {
                { Headers.ValidationToken, $"sha1={hmacify.HmacSha1(info.BuildKey)}" }
            });
            OutLine(response, ConsoleColor.Cyan);
        }

        private static void GetBuildClientInfo(out string configFilePath, out BuildClientInfo info)
        {
            configFilePath = Path.Combine(Paths.Conf, "buildclient.json");
            if (!File.Exists(configFilePath))
            {
                new BuildClientInfo().ToJsonFile(configFilePath);
            }
            info = configFilePath.FromJsonFile<BuildClientInfo>();
        }

    }
}
