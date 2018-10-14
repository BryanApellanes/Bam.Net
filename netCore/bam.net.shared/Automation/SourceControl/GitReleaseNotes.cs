using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Automation.SourceControl
{
    public partial class GitReleaseNotes
    {
        public string PackageId { get; set; }
        public string Value
        {
            get
            {
                return $"{Summary}\r\n{Bullets.ToString()}";
            }
        }

        public string Summary { get; set; }
        public string Bullet { get; set; }
        
        public int CommitCount { get; set; }
        protected StringBuilder Bullets { get; set; }
        protected void AddBullet(string value, string commitHash)
        {
            CommitCount++;
            Bullets.AppendFormat("{0} {1} ({2})\r\n", Bullet, value, commitHash);
        }
        
        public static GitReleaseNotes MiscSinceLatestRelease(string gitRepoPath)
        {
            string version = Git.LatestRelease(gitRepoPath);
            HashSet<GitLog> logsSinceLast = GitLog.SinceLatestRelease(gitRepoPath);

            GitReleaseNotes result = new GitReleaseNotes(version);
            logsSinceLast.Each(gl =>
            {
                if (!HasPossibleProjectPrefix(gl.Subject))
                {
                    result.AddBullet(gl.Subject, gl.AbbreviatedCommitHash);
                }
            });

            return result;
        }

        public static GitReleaseNotes SinceLatestRelease(string packageId, string gitRepoPath)
        {
            return SinceLatestRelease(packageId, gitRepoPath, out string ignore);
        }

        public static GitReleaseNotes SinceLatestRelease(string packageId, string gitRepoPath, out string latestRelease)
        {
            latestRelease = Git.LatestRelease(gitRepoPath);
            HashSet<GitLog> logsSinceLast = GitLog.SinceLatestRelease(gitRepoPath);
            GitReleaseNotes notes = new GitReleaseNotes(latestRelease, packageId);
            foreach(GitLog log in logsSinceLast)
            {
                string prefix = $"{packageId}:";
                if (log.Subject.StartsWith(prefix))
                {
                    notes.AddBullet(log.Subject.TruncateFront(prefix.Length), log.AbbreviatedCommitHash);
                }                
            }
            return notes;
        }

        public static GitReleaseNotes SinceVersion(string packageId, string gitRepoPath, int major, int minor, int patch)
        {
            string sinceVersion = $"v{major}.{minor}.{patch}";
            HashSet<GitLog> logsSinceVersion = GitLog.SinceVersion(gitRepoPath, major, minor, patch, false);
            GitReleaseNotes notes = new GitReleaseNotes(sinceVersion, packageId);
            foreach(GitLog log in logsSinceVersion)
            {
                string prefix = $"{packageId}:";
                if (log.Subject.StartsWith(prefix))
                {
                    notes.AddBullet(log.Subject.TruncateFront(prefix.Length), log.AbbreviatedCommitHash);
                }
            }
            return notes;
        }

        protected internal static bool HasPossibleProjectPrefix(string message)
        {
            string[] split = message.Split(':');
            if(split.Length > 1)
            {
                if(split[0].Contains(" "))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
