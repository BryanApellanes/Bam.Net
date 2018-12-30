using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class ProcessModeResolver
    {
        /// <summary>
        /// Returns Prod for a uri where only domain and tld are specified.
        /// Will return Dev or Test for uri's where the first part of the subdomain
        /// either starts with, ends with or is equal to Dev or Test.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ProcessMode Resolve(Uri uri)
        {
            string[] hostParts = uri.Host.DelimitSplit(".");
            if (hostParts.Length <= 2)
            {
                return ProcessMode.Prod;
            }
            else
            {
                string subdomain = hostParts[0];
                if (subdomain.Equals("Dev", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.StartsWith("Dev", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.EndsWith("Dev", StringComparison.InvariantCultureIgnoreCase))
                {
                    return ProcessMode.Dev;
                }

                if (subdomain.Equals("Test", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.StartsWith("Test", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.EndsWith("Test", StringComparison.InvariantCultureIgnoreCase))
                {
                    return ProcessMode.Test;
                }

                if (subdomain.Equals("Int", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.StartsWith("Int", StringComparison.InvariantCultureIgnoreCase) ||
                    subdomain.EndsWith("Int", StringComparison.InvariantCultureIgnoreCase))
                {
                    return ProcessMode.Test;
                }

                return ProcessMode.Prod;
            }
        }
    }
}
