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

        public static bool IsInGitRepo(this FileInfo file)
        {
            return IsInGitRepo(file, out DirectoryInfo ignore);
        }

        public static bool IsInGitRepo(this FileInfo file, out DirectoryInfo gitRepo)
        {
            gitRepo = UpToGitRoot(file.Directory);
            return gitRepo != null;
        }

        public static bool IsInGitRepo(this DirectoryInfo directory)
        {
            return IsInGitRepo(directory, out DirectoryInfo ignore);
        }

        public static bool IsInGitRepo(this DirectoryInfo directory, out DirectoryInfo gitRepo)
        {
            gitRepo = UpToGitRoot(directory);
            return gitRepo != null;
        }

        /// <summary>
        /// Finds the root of the git repository for the file or null if the
        /// file is not in a git repository.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static DirectoryInfo UpToGitRoot(this FileInfo file)
        {
            return UpToGitRoot(file.Directory);
        }

        /// <summary>
        /// Finds the root of the git repository for the directory or null if the
        /// directory is not in a git repository.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public static DirectoryInfo UpToGitRoot(this DirectoryInfo directory)
        {
            if(System.IO.Directory.Exists(Path.Combine(directory.FullName, ".git")))
            {
                return directory;
            }

            if(directory.Parent == null)
            {
                return null;
            }

            return UpToGitRoot(directory.Parent);
        }
    }
}
