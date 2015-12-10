/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using ActiveDs;
using System.Collections;
using Naizari.Extensions;

namespace Naizari.Ldap.ActiveDirectory
{
    public class GroupExpander
    {
        DirectoryEntry group;
        List<string> members;
        Hashtable processed;

        public GroupExpander(DirectoryEntry group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            this.group = group;
            this.processed = new Hashtable();
            this.processed.Add(
                this.group.Properties[
                    "distinguishedName"][0].ToString(),
                null
                );

            this.members = Expand(this.group);
        }

        public string[] Members
        {
            get { return this.members.ToArray(); }
        }

        private List<string> Expand(DirectoryEntry group)
        {
            List<string> al = new List<string>(5000);
            string oc = "objectClass";

            DirectorySearcher ds = new DirectorySearcher(
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
                );

            ds.AttributeScopeQuery = "member";
            ds.PageSize = 1000;

            using (SearchResultCollection src = ds.FindAll())
            {
                string dn = null;
                foreach (SearchResult sr in src)
                {
                    dn = (string)
                        sr.Properties["distinguishedName"][0];
                    string name = string.Empty;
                    try
                    {
                        name = (string)sr.Properties["sAMAccountName"][0];
                    }
                    catch 
                    {
                        // no sAMAccountName, must be a contact
                        name = (string)sr.Properties["mail"][0];
                    }

                    name += ":" + (string)sr.Properties["name"][0];

                    if (!this.processed.ContainsKey(dn))
                    {
                        this.processed.Add(dn, null);

                        //oc == "objectClass", we had to 
                        //truncate to fit in book.
                        //if it is a group, do this recursively
                        if (sr.Properties[oc].Contains("group"))
                        {
                            SetNewPath(this.group, dn);
                            al.AddRange(Expand(this.group));
                        }
                        else
                            al.Add(name);
                    }
                }
            }
            return al;
        }

        //we will use IADsPathName utility function instead
        //of parsing string values.  This particular function
        //allows us to replace only the DN portion of a path
        //and leave the server and port information intact
        private void SetNewPath(DirectoryEntry entry, string dn)
        {
            IADsPathname pathCracker = (IADsPathname)new Pathname();

            pathCracker.Set(entry.Path, 1);
            pathCracker.Set(dn, 4);

            entry.Path = pathCracker.Retrieve(5);
        }
    }
}
