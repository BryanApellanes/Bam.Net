using ActiveDs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    /// <summary>
    /// A class used to recursively resolve members of a group.  If a group is 
    /// a member of a given group the members of that group are resolved recursively
    /// until a flat list of users is achieved.
    /// </summary>
    public class ResultantGroupMemberResolver
    {
        public ResultantGroupMemberResolver(DirectoryEntry group, ActiveDirectoryReader activeDirectoryReader = null)
        {
            Group = group;
            Processed = new HashSet<string>();
            MemberNameResolver = GetMemberName;
            ActiveDirectoryReader = activeDirectoryReader;
        }

        public ActiveDirectoryReader ActiveDirectoryReader { get; set; }

        string[] _members;
        object _memberLock = new object();
        public string[] Members
        {
            get
            {
                return _memberLock.DoubleCheckLock(ref _members, () => Resolve());
            }
        }

        public string[] ResolveMembers(string groupName)
        {
            return ResolveMembers(ActiveDirectoryReader.GetDirectoryEntry(groupName));
        }

        public static string[] ResolveMembers(DirectoryEntry group)
        {
            return ResolveMembers(group, out ResultantGroupMemberResolver ignore);
        }

        public static string[] ResolveMembers(DirectoryEntry group, out ResultantGroupMemberResolver resolver)
        {
            resolver = new ResultantGroupMemberResolver(group);
            return resolver.Members;
        }
        
        public Func<Dictionary<string, string>, string> MemberNameResolver { get; set; }
        public DirectoryEntry Group { get; }
        protected HashSet<string> Processed { get; set; }

        public string[] Resolve()
        {
            Args.ThrowIfNull(Group);
            Processed = new HashSet<string>() { Group.Properties["distinguishedName"][0].ToString() };
            return Resolve(Group).ToArray();
        }
        
        protected string[] Resolve(DirectoryEntry group)
        {
            List<string> memberNames = new List<string>(5000);
            string objectClass = "objectClass";

            DirectorySearcher directorySearcher = new DirectorySearcher(
                group,
                "(objectClass=*)",
                new string[] {
                "member",
                "distinguishedName",
                "objectClass",
                "sAMAccountName",
                "mail",
                "name"},
                SearchScope.Base
                )
            {
                AttributeScopeQuery = "member",
                PageSize = 1000
            };

            using (SearchResultCollection searchResults = directorySearcher.FindAll())
            {
                string distinguishedName = null;
                foreach (SearchResult searchResult in searchResults)
                {
                    distinguishedName = (string)searchResult.Properties["distinguishedName"][0];

                    if (!Processed.Contains(distinguishedName))
                    {
                        Processed.Add(distinguishedName);

                        if (searchResult.Properties[objectClass].Contains("group"))
                        {
                            SetNextPath(Group, distinguishedName);
                            memberNames.AddRange(Resolve(Group));
                        }
                        else
                        {
                            memberNames.Add(MemberNameResolver(searchResult.ToDictionary()));
                        }
                    }
                }
            }
            return memberNames.ToArray();
        }

        private static string GetMemberName(Dictionary<string, string> searchResult)
        {
            return searchResult.ContainsKey("samaccountname") ? searchResult["samaccountname"] : searchResult.ContainsKey("mail") ? searchResult["mail"] : string.Empty;
        }

        private void SetNextPath(DirectoryEntry entry, string dn)
        {
            IADsPathname pathCracker = (IADsPathname)new Pathname();

            pathCracker.Set(entry.Path, 1);
            pathCracker.Set(dn, 4);

            entry.Path = pathCracker.Retrieve(5);
        }
    }
}
