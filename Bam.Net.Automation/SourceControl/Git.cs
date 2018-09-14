/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Bam.Net;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.SourceControl
{
    public class Git
    {
        GitConfigStack _configStack;
        public Git(string remoteRepository)
        {
            this._configStack = new GitConfigStack { RemoteRepository = remoteRepository };
        }

        public static Git RemoteRepository(string remoteRepository)
        {
            return new Git(remoteRepository);
        }
        
        public static string LatestRelease(string repository)
        {
            return new Git(repository).LatestRelease();
        }

        public Git CloneTo(string localDirectory, int timeout = 1800000)
        {
            return CloneTo(new DirectoryInfo(localDirectory), timeout);
        }

        public Git CloneTo(DirectoryInfo localDirectory, int timeout = 1800000)
        {
            EnsureUserInfo();

            if (!EnsureEnvironmentPath())
            {
                throw new UnableToInitializeGitToolsException("Couldn't update environment path");
            }

            bool configured = ConfigGit();
            if (!configured)
            {
                StringBuilder message = new StringBuilder();
                if (_configStack.LastOutput != null)
                {
                    message.AppendLine(_configStack.LastOutput.StandardOutput);
                    message.AppendLine(_configStack.LastOutput.StandardError);
                }
                else
                {
                    message.AppendLine("Unable to configure git");
                }
                throw new UnableToConfigureGitException(message.ToString());
            }
            else
            {
                ProcessOutput output = "git clone {0} \"{1}\""._Format(_configStack.RemoteRepository, localDirectory.FullName).Run(timeout);
                _configStack.LastOutput = output;
            }
            return this;
        }

        public Git UserName(string userName)
        {
            _configStack.UserName = userName;
            return this;
        }

        public Git UserEmail(string emailAddress)
        {
            _configStack.UserEmail = emailAddress;
            return this;
        }

        public Git CredentialHelper(string credentialHelper)
        {
            _configStack.CredentialHelper = credentialHelper;
            return this;
        }

        public Git Bin(string gitBinPath)
        {
            _configStack.GitPath = gitBinPath;
            return this;
        }

        public Git Checkout(string branchName)
        {
            CallGit($"checkout {branchName}");
            return this;
        }

        public Git Pull()
        {
            CallGit("pull");
            return this;
        }

        public string LatestRelease()
        {
            return CallGit("describe --abbrev=0");
        }

        public bool LocalBranchExists(string branchName)
        {
            return LocalBranchExists(branchName, out string ignore);
        }

        public bool LocalBranchExists(string branchName, out string commitHash)
        {
            string output = CallGit($"rev-parse --verify {branchName}");
            if (!output.StartsWith("fatal:"))
            {
                commitHash = output;
                return true;
            }
            commitHash = string.Empty;
            return false;
        }

        public bool RemoteBranchExists(string branchName)
        {
            return RemoteBranchExists(branchName, out string ignore);
        }

        public bool RemoteBranchExists(string branchName, out string commitHash)
        {
            string output = CallGit($"ls-remote --heads");
            string[] lines = output.DelimitSplit("\r\n");
            commitHash = string.Empty;
            if(lines.Length >= 2)
            {
                for(int i = 1; i < lines.Length; i++)
                {
                    if (lines[i].Trim().EndsWith($"refs/heads/{branchName}"))
                    {
                        commitHash = lines[i].DelimitSplit(" ")[0];
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the latest commit hash for the specified branch.
        /// </summary>
        /// <param name="branchName">Name of the branch.</param>
        /// <returns></returns>
        public string LatestBranchCommit(string branchName)
        {
            return CallGit($"rev-parse {branchName}");
        }

        public string CurrentCommitHash()
        {
            return CallGit("rev-parse HEAD");
        }

        public ProcessOutput LastOutput()
        {
            return _configStack.LastOutput;
        }

        private void EnsureUserInfo()
        {
            if (string.IsNullOrEmpty(_configStack.UserName))
            {
                string userName = CallGit("config user.name");
                if (!string.IsNullOrEmpty(userName))
                {
                    UserName(userName);
                }
                if (string.IsNullOrEmpty(_configStack.UserName))
                {
                    throw new UnableToInitializeGitToolsException("Git UserName must be specified");
                }
            }

            if (string.IsNullOrEmpty(_configStack.UserEmail))
            {
                string userEmail = CallGit("config user.email");
                if (!string.IsNullOrEmpty(userEmail))
                {
                    UserEmail(userEmail);
                }
                if (string.IsNullOrEmpty(_configStack.UserEmail))
                {
                    throw new UnableToInitializeGitToolsException("Git UserEmail must be specified");
                }
            }
        }

        private string CallGit(string args)
        {
            ProcessOutput output = $"{Path.Combine(_configStack.GitPath, "git.exe")} {args}".ToStartInfo().Run();
            if(output.ExitCode != 0)
            {
                throw new Exception(output.StandardError);
            }
            return output.StandardOutput.Trim();
        }

        private DirectoryInfo GitBin
        {
            get
            {
                return new DirectoryInfo(_configStack.GitPath);
            }
        }

        internal bool ConfigGit()
        {
            bool result = true;
            try
            {
                _configStack.LastOutput = "git.exe config --global user.name \"{0}\""._Format(_configStack.UserName).Run();
                _configStack.LastOutput = "git.exe config --global user.email \"{0}\""._Format(_configStack.UserEmail).Run();
                _configStack.LastOutput = "git.exe config --global credential.helper {0}"._Format(_configStack.CredentialHelper).Run();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        internal bool EnsureEnvironmentPath()
        {
            bool result = true;

            string path = Environment.GetEnvironmentVariable("PATH");
            if (!path.ToLowerInvariant().Contains(GitBin.FullName.ToLowerInvariant()))
            {
                AddGitToEnvironmentPath();
            }

            return result;
        }

        internal void AddGitToEnvironmentPath()
        {
            Environment.CurrentDirectory = GitBin.FullName;
            string path = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", "{0};{1}"._Format(path, GitBin.FullName));
        }
    }
}
