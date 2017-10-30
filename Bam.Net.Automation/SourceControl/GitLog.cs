using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.SourceControl
{
    public class GitLog
    {
        [GitOption("%H", "Commit hash")]
        public string CommitHash { get; set; }

        [GitOption("%h", "Abbreviated commit hash")]
        public string AbbreviatedCommitHash { get; set; }

        [GitOption("%T", "Tree hash")]
        public string TreeHash { get; set; }

        [GitOption("%t", "Abbreviated tree hash")]
        public string AbbreviatedTreeHash { get; set; }	

        [GitOption("%P", "Parent hashes")]
        public string ParentHashes { get; set; }

        [GitOption("%p", "Abbreviated parent hashes")]
        public string AbbreviatedParentHashes { get; set; }

        [GitOption("%an", "Author name")]
        public string AuthorName { get; set; }

        [GitOption("%ae", "Author email")]
        public string AuthorEmail { get; set; }

        [GitOption("%ad", "Author date ~ format respects the --date=option")]
        public string AuthorDate { get; set; }

        [GitOption("%ar", "Author date, relative")]
        public string AuthorDateRelative { get; set; }

        [GitOption("%cn", "Committer name")]
        public string CommitterName { get; set; }

        [GitOption("%ce", "Committer email")]
        public string CommitterEmail { get; set; }

        [GitOption("%cd", "Committer date")]
        public string CommitterDate { get; set; }

        [GitOption("%cr", "Committer date, relative")]
        public string CommitterDateRelative { get; set; }

        [GitOption("%s", "Subject ~ commit message")]
        public string Subject { get; set; }

        public override int GetHashCode()
        {
            return CommitHash.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GitLog log)
            {
                return log.CommitHash.Equals(CommitHash);
            }
            return false;
        }

        static Dictionary<string, HashSet<GitLog>> _logCache = new Dictionary<string, HashSet<GitLog>>();
        static object _logCacheLock = new object();
        public static HashSet<GitLog> SinceLatestRelease(string gitRepoPath, bool useCache = true)
        {
            string latestRelease = Git.LatestRelease(gitRepoPath);
            if (!useCache)
            {
                return SinceTag(gitRepoPath, latestRelease);
            }

            return SinceTagFromCache(gitRepoPath, latestRelease);
        }

        public static HashSet<GitLog> SinceVersion(string gitRepoPath, int major, int minor = 0, int patch = 0, bool useCache = true)
        {
            string version = $"v{major}.{minor}.{patch}";
            if (!useCache)
            {
                return SinceTag(gitRepoPath, version);
            }

            return SinceTagFromCache(gitRepoPath, version);
        }

        public static HashSet<GitLog> SinceTag(string gitRepoPath, string tag)
        {
            return SinceCommit(gitRepoPath, $"tags/{tag}");
        }

        public static HashSet<GitLog> SinceCommit(string gitRepoPath, string commitIdentifier)
        {
            return BetweenCommits(gitRepoPath, commitIdentifier);
        }

        public static HashSet<GitLog> BetweenCommits(string gitRepoPath, string commitIdentifier, string toCommit = "HEAD")
        {
            string startDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = gitRepoPath;
            string command = $"git --no-pager log --pretty=format:{GetPrettyFormatArg()} {commitIdentifier}..{toCommit}";            
            HashSet<GitLog> results = new HashSet<GitLog>();            
            ProcessOutput output = command.Run();
            int num = 0;
            output.StandardOutput.DelimitSplit("\r", "\n").Each(log => 
            {
                log.SafeWriteToFile($".\\gitlog_{++num}.txt");
                results.Add(log.FromJson<GitLog>());
            });

            return results;
        }

        private static string GetPrettyFormatArg()
        {
            StringBuilder result = new StringBuilder();
            result.Append("\"{");
            bool first = true;
            typeof(GitLog).GetPropertiesWithAttributeOfType<GitOption>().Each(propInfo =>
            {
                if (!first)
                {
                    result.Append(", ");
                }
                GitOption option = propInfo.GetCustomAttributeOfType<GitOption>();
                result.AppendFormat("\\\"{0}\\\": \\\"{1}\\\"", propInfo.Name, option.Value.Replace("\"", "'").ToJson());
                first = false;
            });
            result.Append("}\"");
            return result.ToString();
        }

        private static HashSet<GitLog> SinceTagFromCache(string gitRepoPath, string version)
        {
            lock (_logCacheLock)
            {
                if (!_logCache.ContainsKey(version))
                {
                    _logCache[version] = SinceTag(gitRepoPath, version);
                }
                return _logCache[version];
            }
        }

    }
}
