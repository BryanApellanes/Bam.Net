using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string Subject { get; set; }

        public static List<GitLog> GetLogs(string gitRepoPath, int numberOfYearsBack = 10)
        {
            string startDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = gitRepoPath;
            string logFormat = "git --no-pager log --pretty=format:{0} --since \"{1} years ago\"";
            string command = logFormat._Format(GetPrettyFormatArg(), numberOfYearsBack);
            ProcessOutput output = command.Run();
            Environment.CurrentDirectory = startDirectory;
            List<GitLog> results = new List<GitLog>();
            output.StandardOutput.DelimitSplit("\r", "\n").Each(log =>
            {
                results.Add(log.FromJson<GitLog>());
            });
            Dictionary<string, string> subjects = GetSubjectsByHash(gitRepoPath, numberOfYearsBack);
            results.Each(gitlog =>
            {
                gitlog.Subject = subjects[gitlog.AbbreviatedCommitHash];
            });
            return results;
        }

        private static Dictionary<string, string> GetSubjectsByHash(string gitRepoPath, int numberOfYearsBack)
        {
            string startDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = gitRepoPath;
            
            string logFormat = "git --no-pager log --pretty=format:\"%h~::~%s\" --since \"{0} years ago\"";
            string command = logFormat._Format(numberOfYearsBack);
            ProcessOutput output = command.Run();
            
            Dictionary<string, string> results = new Dictionary<string, string>();
            output.StandardOutput.DelimitSplit("\r", "\n").Each(log =>
            {
                string[] split = log.DelimitSplit("~::~");
                results.Add(split[0], split[1]);
            });
            Environment.CurrentDirectory = startDirectory;
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
            result.Append("}\"");
            return result.ToString();
        }
    }
}
