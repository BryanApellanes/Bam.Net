/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Analytics;
using System.IO;

namespace Wrapify
{
    public class Wrapifier: BaseCrawler
    {
        public Wrapifier()
            : this(new FileInfo(".\\WrapifyConfig.json"))
        { }

        public Wrapifier(FileInfo configFile)
            : this(configFile.FromJsonFile<WrapifyConfig>())
        { }

        public Wrapifier(WrapifyConfig config)
        {
            this.Config = config;
            this.IgnoreFiles = new HashSet<string>();
            this.IgnoreFolders = new HashSet<string>();
            this.Root = Config.RootFolder;
            this.Wrapified = s => { };
            this.Skipped = s => { };
        }

        public WrapifyConfig Config
        {
            get;
            set;
        }
        public override string[] ExtractTargets(string target)
        {
            List<string> results = new List<string>();
            // get subdirectories and return the ones that aren't ignored
            if (Directory.Exists(target))
            {
                DirectoryInfo dir = new DirectoryInfo(target);
                AddFoldersToBeIgnored(dir);

                dir.GetDirectories()
                    .Select(d => d.FullName)
                    .Each(t =>
                    {
                        if (!IgnoreFolders.Contains(t))
                        {
                            results.Add(t);
                        }
                    });

                AddFilesToBeIgnored(dir);

                dir.GetFiles(Config.TargetFilePattern)
                    .Select(f => f.FullName)
                    .Each(t =>
                    {
                        if (!IgnoreFiles.Contains(t))
                        {
                            results.Add(new FileInfo(t).FullName);
                        }
                    });
            }
            return results.ToArray();
        }

        private void AddFilesToBeIgnored(DirectoryInfo dir)
        {
            if (Config.IgnoreFilePatterns != null && Config.IgnoreFilePatterns.Length > 0)
            {
                dir.GetFiles(Config.IgnoreFilePatterns)
                    .Select(f => f.FullName)
                    .Each(f => IgnoreFiles.Add(f));
            }
        }

        private void AddFoldersToBeIgnored(DirectoryInfo dir)
        {
            if (Config.IgnoreFolderPatterns != null && Config.IgnoreFolderPatterns.Length > 0)
            {
                Config.IgnoreFolderPatterns.Each(ignoreFolder =>
                {
                    dir.GetDirectories(ignoreFolder)
                        .Select(d => d.FullName)
                        .Each(d => IgnoreFolders.Add(d));
                });
            }
        }

        public override void ProcessTarget(string target)
        {
            if (File.Exists(target))
            {
                string content = File.ReadAllText(target);
                string newContent = content;
                bool touched = false;

                if (!content.StartsWith(Config.Prefix))
                {
                    newContent = string.Format("{0}{1}", Config.Prefix, newContent);
                    touched = true;
                }
                if (!newContent.EndsWith(Config.Suffix))
                {
                    newContent = string.Format("{0}{1}", newContent, Config.Suffix);
                    touched = true;
                }
                string wrapified = string.Format("{0}.wrapified", target);
                if (!File.Exists(wrapified) && touched)
                {
                    File.Move(target, wrapified);
                    newContent.SafeWriteToFile(target);
                    Wrapified(target);
                }
                else
                {
                    Skipped(target);
                }
            }
        }

        public Action<string> Wrapified
        {
            get;
            set;
        }

        public Action<string> Skipped
        {
            get;
            set;
        }

        protected HashSet<string> IgnoreFiles { get; private set; }
        protected HashSet<string> IgnoreFolders { get; private set; }
    }
}
