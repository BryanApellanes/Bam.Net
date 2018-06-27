using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.ActiveDirectory
{
    public class ActiveDirectoryReader: Loggable
    {
        const string DistinguishedName = "distinguishedname";
        const string DefaultNamingContext = "defaultNamingContext";

        public ActiveDirectoryReader(string server, ILogger logger = null)
        {
            SearchResults = new Dictionary<string, Dictionary<string, string>>();
            Server = server;
            Logger = logger ?? Log.Default;
        }

        protected Dictionary<string, Dictionary<string, string>> SearchResults { get; set; }

        /// <summary>
        /// Gets or sets the server.  This can be the domain or domain controller hostname.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
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

        [Verbosity(VerbosityLevel.Information, MessageFormat = "User not found: UserName = {UserName}, Server = {Server}")]
        public event EventHandler UserNotFound;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "User found: UserName = {UserName}, Server = {Server}")]
        public event EventHandler UserFound;

        /// <summary>
        /// Gets the distinguishedName (LDAP notation locator) of the specified user.
        /// </summary>
        /// <param name="userName">The sam account name of the user to retrieve the distinguishedName for.  Exclude the domain.</param>
        /// <param name="reload">If set to <c>true</c> reload the properties of the specified user.</param>
        /// <param name="throwIfNotFound">If set to <c>true</c> throw an InvalidOperationException if the specified user is not found.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GetDistinguishedName(string userName, bool reload = false, bool throwIfNotFound = false)
        {
            if (!SearchResults.ContainsKey(userName))
            {
                SearchResults.Add(userName, new Dictionary<string, string>());
            }

            if (!SearchResults[userName].ContainsKey(DistinguishedName) || reload)
            {
                using (DirectorySearcher searcher = new DirectorySearcher(new DirectoryEntry(GetDirectoryRootPath())))
                {
                    searcher.Filter = $"(sAMAccountName={userName})";
                    SearchResult result = searcher.FindOne();
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    if (result != null)
                    {
                        FireEvent(UserFound, new ActiveDirectoryEventArgs { UserName = userName, Server = Server });
                        ResultPropertyCollection fields = result.Properties;
                        foreach (string field in fields.PropertyNames)
                        {
                            foreach (object collection in fields[field])
                            {
                                if (!properties.ContainsKey(field))
                                {
                                    properties.Add(field, collection.ToString());
                                }
                                else
                                {
                                    properties[field] = collection.ToString();
                                }
                            }
                        }
                        SearchResults[userName] = properties;
                    }
                    else
                    {
                        FireEvent(UserNotFound);
                        if (throwIfNotFound)
                        {
                            throw new InvalidOperationException($"User {userName} not found");
                        }
                    }
                }
            }
            return SearchResults[userName][DistinguishedName];
        }

        public string GetDirectoryPath(string userName)
        {
            return $"LDAP://{Server}/{GetDistinguishedName(userName)}";
        }

        public string GetDirectoryRootPath()
        {
            return $"LDAP://{Server}/{GetDefaultNamingContext()}";
        }

        protected string GetDefaultNamingContext()
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://rootDSE");
            return entry.Properties[DefaultNamingContext].Value.ToString();
        }
    }
}
