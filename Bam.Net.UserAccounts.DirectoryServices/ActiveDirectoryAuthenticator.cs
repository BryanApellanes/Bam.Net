using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public class ActiveDirectoryAuthenticator : Loggable, IAuthenticator
    {
        public ActiveDirectoryAuthenticator(ActiveDirectoryReader activeDirectoryReader, ILogger logger = null)
        {
            ActiveDirectoryReader = activeDirectoryReader;
            Logger = logger ?? Log.Default;
        }

        public ActiveDirectoryAuthenticator(string server, ILogger logger = null) : this(new ActiveDirectoryReader(server, logger), logger)
        { }

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

        protected ActiveDirectoryReader ActiveDirectoryReader { get; }

        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler InvalidUserNameSpecified;

        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler PasswordValidated;

        [Verbosity(VerbosityLevel.Warning)]
        public event EventHandler PasswordValidationFailed;

        public bool IsPasswordValid(string domainSlashUserName, string password)
        {
            return IsPasswordValid(domainSlashUserName, password, false);
        }

        protected bool IsPasswordValid(string domainSlashUserName, string password, bool throwIfMissingDomain = false)
        {
            string[] split = domainSlashUserName.DelimitSplit("\\");
            if (split.Length != 2)
            {
                FireEvent(InvalidUserNameSpecified, new ActiveDirectoryEventArgs { UserName = domainSlashUserName, Server = ActiveDirectoryReader.Server });
                if (throwIfMissingDomain)
                {
                    throw new InvalidOperationException("Unrecognized username format; should be in the form [domain]\\[username]");
                }
                return false;
            }
            string domain = split[0];
            string userName = split[1];
            return IsPasswordValid(domain, userName, password);
        }

        public bool IsPasswordValid(string domain, string userName, string password)
        {
            if(!ActiveDirectoryReader.TryGetDirectoryPath(userName, out string path))
            {
                return false;
            }
            using (DirectoryEntry entry = new DirectoryEntry(path, $"{domain}\\{userName}", password))
            {
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(sAMAccountName={userName})";
                    try
                    {
                        SearchResult result = searcher.FindOne();
                        FireEvent(PasswordValidated, new ActiveDirectoryEventArgs { UserName = userName, Server = ActiveDirectoryReader.Server });
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.AddEntry("Failed to validate password: {0}", ex, $"{domain}\\{userName}");
                        FireEvent(PasswordValidationFailed, new ActiveDirectoryEventArgs { UserName = userName, Server = ActiveDirectoryReader.Server });
                        return false;
                    }
                }
            }
        }
    }
}
