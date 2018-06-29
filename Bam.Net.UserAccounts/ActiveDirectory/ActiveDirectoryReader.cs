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
            DirectoryEntries = new Dictionary<string, DirectoryEntry>();
            Server = server;
            Logger = logger ?? Log.Default;
        }

        /// <summary>
        /// Gets or sets the search results, keyed by username (samaccountname) then by distinguishedname.
        /// </summary>
        /// <value>
        /// The search results.
        /// </value>
        protected Dictionary<string, Dictionary<string, string>> SearchResults { get; set; }


        protected Dictionary<string, DirectoryEntry> DirectoryEntries { get; set; }
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

        //public string[] GetGroups(string userName, bool reload = false)
        //{
        //    return GetGroups(GetDirectoryEntry(userName, reload), reload);
        //}

        //public string[] GetGroups(DirectoryEntry userEntry, bool reload = false)
        //{
        //    if(userEntry == null)
        //    {
        //        throw new ArgumentNullException("userEntry");
        //    }

        //    StringBuilder filter = new StringBuilder();
        //    filter.Append("(|");
        //    userEntry.RefreshCache(new string[] { "tokenGroups" });
        //    foreach(byte[] sid in userEntry.Properties["tokenGroups"])
        //    {
        //        filter.AppendFormat("(objectSid={0})", BuildOctetString(sid));
        //    }
        //    filter.Append(")");

        //    using ()
        //}

        public DirectoryEntry GetDirectoryEntry(string userName, bool reload = false)
        {
            if(DirectoryEntries.ContainsKey(userName) && DirectoryEntries[userName] != null && !reload)
            {
                return DirectoryEntries[userName];
            }
            GetDistinguishedName(userName, out DirectoryEntry result, reload);
            return result;
        }

        public string GetDistinguishedName(string userName, bool reload = false, bool throwIfNotFound = false)
        {
            return GetDistinguishedName(userName, out DirectoryEntry ignore, reload, throwIfNotFound);
        }

        /// <summary>
        /// Gets the distinguishedName (LDAP notation locator) of the specified user.
        /// </summary>
        /// <param name="userName">The sam account name of the user to retrieve the distinguishedName for.  Exclude the domain.</param>
        /// <param name="reload">If set to <c>true</c> reload the properties of the specified user.</param>
        /// <param name="throwIfNotFound">If set to <c>true</c> throw an InvalidOperationException if the specified user is not found.</param>
        /// <param name="entry"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GetDistinguishedName(string userName, out DirectoryEntry entry, bool reload = false, bool throwIfNotFound = false)
        {
            entry = null;
            if (!SearchResults.ContainsKey(userName))
            {
                SearchResults.Add(userName, new Dictionary<string, string>());
                DirectoryEntries.Add(userName, null);
            }

            if (!SearchResults[userName].ContainsKey(DistinguishedName) || reload)
            {
                using (DirectorySearcher searcher = GetDirectoryRootSearcher())
                {
                    searcher.Filter = $"(sAMAccountName={userName})";
                    SearchResult result = searcher.FindOne();
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    if (result != null)
                    {
                        entry = result.GetDirectoryEntry();
                        DirectoryEntries[userName] = entry;
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
 
        public bool TryGetDirectoryPath(string userName, out string directoryPath)
        {
            directoryPath = string.Empty;
            try
            {
                directoryPath = GetDirectoryPath(userName);
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

        public string GetDirectoryRootPath()
        {
            return $"LDAP://{Server}/{GetDefaultNamingContext()}";
        }

        protected string GetDefaultNamingContext()
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://rootDSE");
            return entry.Properties[DefaultNamingContext].Value.ToString();
        }

        private DirectorySearcher GetDirectoryRootSearcher()
        {
            return new DirectorySearcher(new DirectoryEntry(GetDirectoryRootPath()));
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
