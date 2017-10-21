using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Automation.Nuget;

namespace Bam.Net.Automation.SourceControl
{
    public class GitReleaseNotes
    {
        public GitReleaseNotes(string sinceVersion, string packageId = "")
        {
            PackageId = packageId;
            Bullet = " - ";
            Since = new PackageVersion(sinceVersion);
            Bullets = new StringBuilder();
        }
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
        public PackageVersion Since { get; set; }
        protected StringBuilder Bullets { get; set; }
        protected void AddBullet(string value, string commitHash)
        {
            Bullets.AppendFormat("{0} {1} ({2})\r\n", Bullet, value, commitHash);
        }
        static Dictionary<string, Dictionary<string, GitReleaseNotes>> _logCache = new Dictionary<string, Dictionary<string, GitReleaseNotes>>();
        static object _logCacheLock = new object();
        public static GitReleaseNotes MiscSinceLatestRelease(string gitRepoPath)
        {
            string version = Git.LatestRelease(gitRepoPath);
            HashSet<GitLog> logsSinceLast = GitLog.SinceLatestRelease(gitRepoPath);
            if(!_logCache.ContainsKey("Misc") || !_logCache["Misc"].ContainsKey(version))
            {
                lock (_logCacheLock)
                {
                    GitReleaseNotes result = new GitReleaseNotes(version);
                    logsSinceLast.Each(gl =>
                    {
                        if (!HasPossibleProjectPrefix(gl.Subject))
                        {
                            result.AddBullet(gl.Subject, gl.AbbreviatedCommitHash);
                        }
                    });
                    _logCache.AddMissing("Misc", new Dictionary<string, GitReleaseNotes>());
                    _logCache["Misc"].AddMissing(version, result);
                }
            }
            return _logCache["Misc"][version];
        }
        public static GitReleaseNotes SinceLatestRelease(string packageId, string gitRepoPath)
        {
            string version = Git.LatestRelease(gitRepoPath);

            if (!_logCache.ContainsKey(packageId) || !_logCache[packageId].ContainsKey(version))
            {
                lock (_logCacheLock)
                {
                    HashSet<GitLog> logsSinceLast = GitLog.SinceLatestRelease(gitRepoPath);
                    GitReleaseNotes result = new GitReleaseNotes(version, packageId);
                    logsSinceLast.Each(gl =>
                    {
                        string prefix = $"{packageId}:";
                        if (gl.Subject.StartsWith(prefix))
                        {
                            result.AddBullet(gl.Subject.TruncateFront(prefix.Length), gl.AbbreviatedCommitHash);
                        }
                    });

                    _logCache.AddMissing(packageId, new Dictionary<string, GitReleaseNotes>());
                    _logCache[packageId].AddMissing(version, result);
                }
            }
            return _logCache[packageId][version];
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
