using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace Bam.Net.UserAccounts
{
    public partial class UserInfo
    {
        public static UserInfo FromDirectoryEntry(DirectoryEntry directoryEntry)
        {
            return new UserInfo
            {
                FirstName = ReadProperty(directoryEntry, "cn")?.ToString(),
                LastName = ReadProperty(directoryEntry, "sn")?.ToString(),
                UserName = ReadProperty(directoryEntry, "sAMAccountName")?.ToString(),
                Email = ReadProperty(directoryEntry, "mail")?.ToString()
            };
        }

        private static object ReadProperty(DirectoryEntry directoryEntry, string propertyName)
        {
            return directoryEntry.Properties[propertyName].Value;
        }
    }
}
