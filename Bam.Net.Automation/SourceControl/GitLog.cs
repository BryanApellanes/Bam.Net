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

        [GitOption("%ad", "Author date (format respects the --date=option)")]
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

        [GitOption("%s", "(Subject) commit message")]
        public string Subject { get; set; }

        public override int GetHashCode()
        {
            return CommitHash.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GitLog log = obj as GitLog;
            if(log != null)
            {
                return log.CommitHash.Equals(CommitHash);
            }
            return false;
        }

        public static HashSet<GitLog> GetSinceVersion(string gitRepoPath, int major, int minor = 0, int patch = 0)
        {
            return GetSinceTag(gitRepoPath, $"v{major}.{minor}.{patch}");
        }

        public static HashSet<GitLog> GetSinceTag(string gitRepoPath, string tag)
        {
            return GetSinceCommit(gitRepoPath, $"tags/{tag}");
        }

        public static HashSet<GitLog> GetSinceCommit(string gitRepoPath, string commitIdentifier)
        {
            return GetLogsBetweenCommits(gitRepoPath, commitIdentifier);
        }

        public static HashSet<GitLog> GetLogsBetweenCommits(string gitRepoPath, string commitIdentifier, string toCommit = "HEAD")
        {
            string startDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = gitRepoPath;
            string command = $"git --no-pager log --pretty=format:{GetPrettyFormatArg()} {commitIdentifier}..{toCommit}";
            ProcessOutput output = null;
            HashSet<GitLog> results = new HashSet<GitLog>();
            AutoResetEvent wait = new AutoResetEvent(false);
            output = command.Run((o, a) =>
            {
                Environment.CurrentDirectory = startDirectory;                
                output.StandardOutput.DelimitSplit("\r", "\n").Each(log =>
                {
                    results.Add(log.FromJson<GitLog>());
                });
                wait.Set();
            });
            wait.WaitOne();
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
                result.AppendFormat("\\\"{0}\\\": \\\"{1}\\\"", propInfo.Name, option.Value);
                first = false;
            });
            result.Append("}\"\r\n");
            return result.ToString();
        }
    }
}
