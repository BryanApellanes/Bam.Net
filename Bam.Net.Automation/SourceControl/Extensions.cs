using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.SourceControl
{
    public static class Extensions
    {
        public static string CurrentGitBranch(this DirectoryInfo dir)
        {
            string curDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = dir.FullName;
            string currentBranch = string.Empty;
            ProcessOutput po = "git branch".Run(line =>
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("*"))
                {
                    currentBranch = trimmed.TruncateFront(1).Trim();
                }
            }, 60000);
            Environment.CurrentDirectory = curDir;
            return currentBranch;
        }
    }
}
