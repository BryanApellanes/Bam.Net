using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class UriData: RepoData
    {
        /// <summary>
        /// Gets whether the port value of the URI is the default for this scheme.
        /// </summary>
        public bool IsDefaultPort { get; set; }

        /// <summary>
        /// Gets the Domain Name System (DNS) host name or IP address and the port number
        /// for a server.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Gets a host name that, after being unescaped if necessary, is safe to use for
        ///     DNS resolution.
        /// </summary>
        /// <value>
        /// The DNS safe host.
        /// </value>
        public string DnsSafeHost { get; set; }

        /// <summary>
        /// Gets the escaped URI fragment.
        /// </summary>
        /// <value>
        /// The fragment.
        /// </value>
        public string Fragment { get; set; }

        /// <summary>
        /// Gets the host component.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets the type of the host name specified in the URI.
        /// </summary>
        /// <value>
        /// The type of the host name.
        /// </value>
        public UriHostNameType HostNameType { get; set; }

        /// <summary>
        /// The RFC 3490 compliant International Domain Name of the host, using Punycode
        ///     as appropriate. This string, after being unescaped if necessary, is safe to use
        ///     for DNS resolution.
        /// </summary>
        public string IdnHost { get; set; }

        /// <summary>
        /// Gets whether the System.Uri instance is absolute.
        /// </summary>
        public bool IsAbsoluteUri { get; set; }

        /// <summary>
        /// Gets a value indicating whether the specified System.Uri is a file URI.
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// Gets an array containing the path segments that make up the specified URI.
        /// </summary>
        public string[] Segments { get; set; }

        /// <summary>
        /// Gets whether the specified System.Uri is a universal naming convention (UNC)
        ///     path.
        /// </summary>
        public bool IsUnc { get; set; }

        /// <summary>
        /// Gets a local operating-system representation of a file name.
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Gets the original URI string that was passed to the System.Uri constructor.
        /// </summary>
        public string OriginalString { get; set; }

        /// <summary>
        /// Gets the System.Uri.AbsolutePath and System.Uri.Query properties separated by a question mark (?).
        /// </summary>
        public string PathAndQuery { get; set; }

        /// <summary>
        /// Gets the port number of this URI.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets any query information included in the specified URI.
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// Gets the scheme name for this URI.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets the absolute URI.
        /// </summary>
        public string AbsoluteUri { get; set; }

        /// <summary>
        ///  Gets whether the specified System.Uri references the local host.
        /// </summary>
        public bool IsLoopback { get; set; }

        /// <summary>
        /// Gets the absolute path of the URI.
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        ///  Gets the user name, password, or other user-specific information associated with the specified URI.
        /// </summary>
        public string UserInfo { get; set; }

        /// <summary>
        /// Indicates that the URI string was completely escaped before the System.Uri instance was created.
        /// </summary>
        public bool UserEscaped { get; set; }

        public static UriData FromUri(Uri uri)
        {
            UriData data = uri.CopyAs<UriData>();
            data.QueryString = uri.Query;
            return data;
        }
    }
}
