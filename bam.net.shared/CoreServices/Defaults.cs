using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices
{
    public class Defaults
    {
        static Defaults()
        {
            Protocol = Protocols.Http;
            HostName = HostNames.Heart;
        }

        public static Protocols Protocol
        {
            get;
            set;
        }

        public static string HostName
        {
            get;set;
        }

        public static string BaseUrl
        {
            get
            {
                return $"{Protocol.ToString().ToLowerInvariant()}://{HostName}/";
            }
        }
    }
}
