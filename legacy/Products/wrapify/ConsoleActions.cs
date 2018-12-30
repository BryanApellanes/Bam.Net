/*
	Copyright © Bryan Apellanes 2015  
*/
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

namespace Wrapify
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        #region ConsoleAction examples
        [ConsoleAction("Configure")]
        public void Config()
        {
            string ignoreFilesPattern = Prompt("Enter the file search pattern to ignore");
            string ignoreFolderPattern = Prompt("Enter the folder search pattern to ignore");

            string prefix = Prompt("Enter the text to prefix the files with");
            string suffix = Prompt("Enter the text to suffix the files with");

            WrapifyConfig config = new WrapifyConfig();
            if (!string.IsNullOrEmpty(ignoreFilesPattern))
            {
                config.IgnoreFilePatterns = new string[] { ignoreFilesPattern };
            }

            if (!string.IsNullOrEmpty(ignoreFolderPattern))
            {
                config.IgnoreFolderPatterns = new string[] { ignoreFolderPattern };
            }

            if (!string.IsNullOrEmpty(prefix))
            {
                config.Prefix = prefix;
            }
            if (!string.IsNullOrEmpty(suffix))
            {
                config.Suffix = suffix;
            }

            config.ToJsonFile(Prompt("Enter name for the config file").Or(".\\WrapifyConfig.json"));
        }

        [ConsoleAction("wrapify", "Wrapify")]
        public static void Wrapify()
        {
            string configPath = Arguments.Contains("config") ? Arguments["config"] : Prompt("Enter the path of the config file to use");
            WrapifyConfig config = configPath.FromJsonFile<WrapifyConfig>();
            if (Arguments.Contains("root"))
            {
                config.RootFolder = Arguments["root"];
            }
            Wrapifier wrapifier = new Wrapifier(config);
            wrapifier.ActionChanged += (c, e) =>
            {
                OutLineFormat("{0}: {1}", e.NewAction.ToString(), ((Wrapifier)c).Current);
            };
            wrapifier.Wrapified = s => OutLineFormat("\t{0}", ConsoleColor.Green, s);
            wrapifier.Skipped = s => OutLineFormat("\t{0}", ConsoleColor.Yellow, s);
            wrapifier.Crawl();
        }

        [ConsoleAction("clean", "Clean up .wrapified files")]
        public static void Cleanup()
        {
            string root = Arguments["root"].Or(".");
            Queue<string> folderPaths = new Queue<string>();
            folderPaths.Enqueue(root);
            while (folderPaths.Count > 0)
            {
                string path = folderPaths.Dequeue();
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    dir.GetFiles("*.wrapified").Each(f => File.Delete(f.FullName));
                    dir.GetDirectories().Each(d => folderPaths.Enqueue(d.FullName));
                }
            }
        }
        #endregion
    }
}