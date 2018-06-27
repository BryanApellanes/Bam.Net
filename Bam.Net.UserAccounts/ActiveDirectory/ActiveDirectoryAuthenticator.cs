using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts.ActiveDirectory
{
    public class ActiveDirectoryAuthenticator : Loggable, IAuthenticator
    {
        public ActiveDirectoryAuthenticator(string server, ILogger logger = null)
        {
            Server = server;
            Logger = logger ?? Log.Default;
        }

        public string Server { get; set; }

        ILogger _logger;
        public ILogger Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
                Subscribe(_logger);
            }
        }

        protected ActiveDirectoryReader ActiveDirectoryReader { get; set; }

        public bool IsPasswordValid(string domainSlashUserName, string password)
        {
            return IsPasswordValid(domainSlashUserName, password, false);
        }

        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler InvalidUserNameSpecified;

        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler PasswordValidated;

        [Verbosity(VerbosityLevel.Warning)]
        public event EventHandler PasswordValidationFailed;

        public bool IsPasswordValid(string domainSlashUserName, string password, bool throwIfMissingDomain = false)
        {
            string[] split = domainSlashUserName.DelimitSplit("\\");
            if (split.Length != 2)
            {
                FireEvent(InvalidUserNameSpecified, new ActiveDirectoryEventArgs { UserName = domainSlashUserName, Server = Server });
                if (throwIfMissingDomain)
                {
                    throw new InvalidOperationException("Unrecognized username format; should be in the form [domain]\\[username]");
                }
            }
            string userName = split[1];
            string path = ActiveDirectoryReader.GetDirectoryPath(userName);
            using (DirectoryEntry entry = new DirectoryEntry(path, domainSlashUserName, password))
            {
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(sAMAccountName={userName})";
                    try
                    {
                        SearchResult result = searcher.FindOne();
                        FireEvent(PasswordValidated, new ActiveDirectoryEventArgs { UserName = userName, Server = Server });
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.AddEntry("Faild to validate password: {0}", ex, domainSlashUserName);
                        FireEvent(PasswordValidationFailed, new ActiveDirectoryEventArgs { UserName = userName, Server = Server });
                        return false;
                    }
                }
            }
        }
    }
}
