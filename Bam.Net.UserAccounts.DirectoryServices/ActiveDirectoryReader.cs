using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public class ActiveDirectoryReader: Loggable
    {
        const string DistinguishedNameKey = "distinguishedname";
        const string DefaultNamingContextKey = "defaultNamingContext";

        static ActiveDirectoryReader()
        {
            UserGroups = new Dictionary<string, HashSet<string>>();
            GroupMembers = new Dictionary<string, HashSet<string>>();
        }

        public ActiveDirectoryReader(string server, ILogger logger = null)
        {
            SearchResults = new Dictionary<string, Dictionary<string, string>>();
            DirectoryEntries = new Dictionary<string, DirectoryEntry>();            
            Server = server;
            Logger = logger ?? Log.Default;
        }

        public ActiveDirectoryReader(string server, string defaultNamingContext, ILogger logger = null) 
            : this(server, logger)
        {
            DefaultNamingContext = defaultNamingContext;
        }

        public ActiveDirectoryReader(ActiveDirectoryCredentials credentials, ILogger logger = null) 
            : this(credentials.DomainControllerInfo.ServerName, credentials.DomainControllerInfo.DefaultNamingContext, logger)
        {
            ActiveDirectoryCredentials = credentials;
        }

        public ActiveDirectoryCredentials ActiveDirectoryCredentials { get; set; }

        public string DefaultNamingContext { get; set; }

        /// <summary>
        /// Gets or sets the search results, keyed by username (samaccountname) then by properties.
        /// All search results are "cached" in this property.
        /// </summary>
        /// <value>
        /// The search results.
        /// </value>
        protected Dictionary<string, Dictionary<string, string>> SearchResults { get; set; }


        protected Dictionary<string, DirectoryEntry> DirectoryEntries { get; set; }
        
        protected static Dictionary<string, HashSet<string>> UserGroups { get; set; }

        protected static Dictionary<string, HashSet<string>> GroupMembers { get; set; }
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

        static readonly object _memberLock = new object();
        public bool IsMemberOfGroup(string userName, string groupName, bool reload = false)
        {
            if (reload && UserGroups.ContainsKey(userName))
            {
                lock (_memberLock)
                {
                    UserGroups.Remove(userName);
                }
            }

            if (!UserGroups.ContainsKey(userName))
            {
                PreLoadGroups(userName);
            }
            return UserGroups[userName].Contains(groupName);
        }

        public void PreLoadGroups(string userName)
        {
            lock (_memberLock)
            {
                UserGroups.Add(userName, new HashSet<string>(GetGroupNames(userName)));
            }
        }

        public string[] GetGroupNames(string userName)
        {
            HashSet<string> results = new HashSet<string>();
            foreach(DirectoryEntry directoryEntry in GetGroups(userName))
            {
                results.Add(ReadProperty(directoryEntry, "sAMAccountName")?.ToString());
            }
            return results.ToArray();
        }
        
        public string GetDirectoryRootPath()
        {
            return $"LDAP://{Server}/{GetDefaultNamingContext()}";
        }

        private static object ReadProperty(DirectoryEntry directoryEntry, string propertyName)
        {
            return directoryEntry.Properties[propertyName].Value;
        }

        /// <summary>
        /// Gets the groups that the user is a member of.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public DirectoryEntry[] GetGroups(string userName)
        {
            return GetGroups(GetDirectoryEntry(userName));
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="userEntry">The user entry.</param>
        /// <param name="reload">if set to <c>true</c> [reload].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">userEntry</exception>
        public DirectoryEntry[] GetGroups(DirectoryEntry userEntry)
        {
            if (userEntry == null)
            {
                throw new ArgumentNullException("userEntry");
            }

            StringBuilder filter = new StringBuilder();
            filter.Append("(|");
            userEntry.RefreshCache(new string[] { "tokenGroups" });
            foreach (byte[] sid in userEntry.Properties["tokenGroups"])
            {
                filter.AppendFormat("(objectSid={0})", BuildOctetString(sid));
            }
            filter.Append(")");

            List<DirectoryEntry> groups = new List<DirectoryEntry>();
            using (DirectorySearcher searcher = GetDirectoryRootSearcher())
            {
                searcher.Filter = filter.ToString();
                searcher.PropertiesToLoad.Add("distinguishedName");
                searcher.PropertiesToLoad.Add("objectSid");
                using(SearchResultCollection results = searcher.FindAll())
                {
                    foreach(SearchResult result in results)
                    {
                        groups.Add(result.GetDirectoryEntry());
                    }
                }
            }
            return groups.ToArray();
        }

        public DirectoryEntry GetDirectoryEntry(string samAccountName, bool reload = false)
        {
            if(DirectoryEntries.ContainsKey(samAccountName) && DirectoryEntries[samAccountName] != null && !reload)
            {
                return DirectoryEntries[samAccountName];
            }
            GetDistinguishedName(samAccountName, out DirectoryEntry result, reload);
            return result;
        }

        public string GetDistinguishedName(string samAccountName, bool reload = false, bool throwIfNotFound = false)
        {
            return GetDistinguishedName(samAccountName, out DirectoryEntry ignore, reload, throwIfNotFound);
        }

        /// <summary>
        /// Gets the distinguishedName (LDAP notation locator) of the specified user.
        /// </summary>
        /// <param name="samAccountName">The sam account name of the user to retrieve the distinguishedName for.  Exclude the domain.</param>
        /// <param name="reload">If set to <c>true</c> reload the properties of the specified user.</param>
        /// <param name="throwIfNotFound">If set to <c>true</c> throw an InvalidOperationException if the specified user is not found.</param>
        /// <param name="entry"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GetDistinguishedName(string samAccountName, out DirectoryEntry entry, bool reload = false, bool throwIfNotFound = false)
        {
            entry = null;
            if (!SearchResults.ContainsKey(samAccountName))
            {
                SearchResults.Add(samAccountName, new Dictionary<string, string>());
                DirectoryEntries.Add(samAccountName, null);
            }

            if (!SearchResults[samAccountName].ContainsKey(DistinguishedNameKey) || reload)
            {
                using (DirectorySearcher searcher = GetDirectoryRootSearcher())
                {
                    searcher.Filter = $"(sAMAccountName={samAccountName})";
                    SearchResult result = searcher.FindOne();
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    if (result != null)
                    {
                        entry = result.GetDirectoryEntry();
                        DirectoryEntries[samAccountName] = entry;
                        FireEvent(UserFound, new ActiveDirectoryEventArgs { UserName = samAccountName, Server = Server });
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
                        SearchResults[samAccountName] = properties;
                    }
                    else
                    {
                        FireEvent(UserNotFound);
                        if (throwIfNotFound)
                        {
                            throw new InvalidOperationException($"User {samAccountName} not found");
                        }
                    }
                }
            }
            if(SearchResults.ContainsKey(samAccountName) && SearchResults[samAccountName].ContainsKey(DistinguishedNameKey))
            {
                return SearchResults[samAccountName][DistinguishedNameKey];
            }
            return string.Empty;
        }
 
        public bool TryGetDirectoryPath(string userName, out string directoryPath)
        {
            directoryPath = string.Empty;
            try
            {
                Logger.AddEntry("Trying to get user from active directory: DefaultNamingContext={0}, userName={1}", GetDefaultNamingContext(), userName);
                directoryPath = GetDirectoryPath(userName);
                Logger.AddEntry("Got user {0}, directory path {1}", userName, directoryPath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("{0}: Failed to get directory path for user: {1}", ex, Server, userName);
                return false;
            }
        }

        public string GetDirectoryPath(string userName)
        {
            return $"LDAP://{Server}/{GetDistinguishedName(userName)}";
        }

        public override string ToString()
        {
            return GetDirectoryRootPath();
        }

        protected string GetDefaultNamingContext()
        {
            if (!string.IsNullOrEmpty(DefaultNamingContext))
            {
                return DefaultNamingContext;
            }
            else
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://rootDSE");
                DefaultNamingContext = entry.Properties[DefaultNamingContextKey].Value.ToString();
                return DefaultNamingContext;
            }
        }

        private DirectorySearcher GetDirectoryRootSearcher()
        {
            if(ActiveDirectoryCredentials != null)
            {
                return new DirectorySearcher(new DirectoryEntry(GetDirectoryRootPath(), ActiveDirectoryCredentials.UserName, ActiveDirectoryCredentials.Password));
            }
            else
            {
                return new DirectorySearcher(new DirectoryEntry(GetDirectoryRootPath()));
            }
        }

        private string BuildOctetString(byte[] bytes)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.AppendFormat("\\{0}", bytes[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
