using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class CookieData: KeyHashRepoData
    {
        public DateTime TimeStamp { get; set; }
        public bool Secure { get; set; }
        public string Port { get; set; }

        [CompositeKey]
        public string Path { get; set; }

        [CompositeKey]
        public string Name { get; set; }
        public bool HttpOnly { get; set; }
        public DateTime Expires { get; set; }

        [CompositeKey]
        public string Domain { get; set; }

        [CompositeKey]
        public string Value { get; set; }
        public bool Discard { get; set; }

        public ulong UriId { get; set; }
        public virtual UriData Uri { get; set; }
        public string Comment { get; set; }
        public bool Expired { get; set; }

        [CompositeKey]
        public int Version { get; set; }

        public static CookieData FromCookie(Cookie cookie)
        {
            return cookie.CopyAs<CookieData>();
        }
    }
}
