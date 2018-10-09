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
            ProcessOutput po = $"{GitConfigStack.Default.GitPath} branch".Run(line =>
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
            if(directory == null)
            {
                return null;
            }
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

        /// <summary>
        /// Commits the hash.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns></returns>
        public static string CommitHash(this string directoryPath)
        {
            return GetCommitHash(new DirectoryInfo(directoryPath));
        }

        /// <summary>
        /// Commits the hash.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public static string CommitHash(this DirectoryInfo directory)
        {
            return GetCommitHash(directory);
        }

        /// <summary>
        /// Gets the commit hash.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns></returns>
        public static string GetCommitHash(this string directoryPath)
        {
            return GetCommitHash(new DirectoryInfo(directoryPath));
        }

        /// <summary>
        /// Gets the commit hash for the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public static string GetCommitHash(this DirectoryInfo directory)
        {
            if(directory.IsInGitRepo(out DirectoryInfo gitRepo))
            {
                string commitHash = string.Empty;
                $"{GitConfigStack.Default.GitPath}".ToStartInfo("rev-parse HEAD", gitRepo.FullName).RunAndWait(hash => commitHash = hash, (e) => { }, 60000);
                return commitHash;
            }

            return null;
        }
    }
}
