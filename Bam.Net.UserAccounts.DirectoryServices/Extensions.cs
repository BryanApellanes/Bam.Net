using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public static class Extensions
    {
        public static Dictionary<string, string> ToDictionary(this SearchResult searchResult)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(string propertyName in searchResult.Properties.PropertyNames)
            {
                if(searchResult.Properties[propertyName].Count > 0)
                {
                    result.Add(propertyName, searchResult.Properties[propertyName][0].ToString());
                }
            }
            return result;
        }

    }
}
