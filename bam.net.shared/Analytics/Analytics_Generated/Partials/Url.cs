/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Bam.Net.Analytics
{
    public partial class Url
    {
        public static implicit operator Uri(Url url)
        {
            return new Uri(url.ToString());
        }

        // TODO: add database argument to static methods that use dao
        static object _saveLock = new object();
        public static Url FromUri(Uri uri, bool save = false)
        {
            lock (_saveLock)
            {
                Protocol proto = GetProtocol(uri);

                Domain domain = GetDomain(uri);

                Port port = GetPort(uri);

                Path path = GetPath(uri);

                QueryString queryString = GetQueryString(uri);

				Fragment fragment = GetFragment(uri);

                Url check = Url.OneWhere(c => c.ProtocolId == proto.Id
                    && c.DomainId == domain.Id
                    && c.PortId == port.Id
                    && c.PathId == path.Id
                    && c.QueryStringId == queryString.Id
					&& c.FragmentId == fragment.Id);

                if (check == null)
                {
                    check = new Url
                    {
                        ProtocolId = proto.Id,
                        DomainId = domain.Id,
                        PortId = port.Id,
                        PathId = path.Id,
                        QueryStringId = queryString.Id,
                        FragmentId = fragment.Id
                    };
                }

                if (save)
                {
                    check.Save();
                }

                return check;
            }
        }

        private static WebClient GetWebClient(string userAgent = 
			"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
			"(compatible; MSIE 6.0; Windows NT 5.1; " +
			".NET CLR 1.1.4322; .NET CLR 2.0.50727)")
        {
            WebClient client = new WebClient();
			client.Headers["User-Agent"] = userAgent;
            return client;
        }

        public static Url FromUri(string uri, bool save = false)
        {
            return FromUri(new Uri(uri), save);
        }

        public string Html
        {
            get
            {
                try
                {
                    WebClient wc = GetWebClient();
                    return wc.DownloadString(this.ToString());
                }
                catch (Exception ex)
                {
                    Logging.Log.AddEntry("An error occurred in Url: {0}", ex, ex.Message);
                    return string.Empty;
                }
            }
        }

        private static Path GetPath(Uri uri)
        {
            string[] pathAndQuery = uri.PathAndQuery.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            Path path = Path.OneWhere(c => c.Value == pathAndQuery[0]);
            if (path == null)
            {
                path = new Path
                {
                    Value = pathAndQuery[0]
                };
                path.Save();
            }

            return path;
        }

		private static Fragment GetFragment(Uri uri)
		{
			Fragment f = Fragment.OneWhere(c => c.Value == uri.Fragment);
			if (f == null)
			{
                f = new Fragment
                {
                    Value = uri.Fragment
                };
                f.Save();
			}

			return f;
		}

        private static QueryString GetQueryString(Uri uri)
        {
            QueryString qs = QueryString.OneWhere(c => c.Value == uri.Query);
            if (qs == null)
            {
                qs = new QueryString
                {
                    Value = uri.Query
                };
                qs.Save();
            }
            return qs;
        }

        private void SetPathAndQuery(Uri uri)
        {
            string[] pathAndQuery = uri.PathAndQuery.Split(new string[]{"?"}, StringSplitOptions.RemoveEmptyEntries);
            Path path = Path.OneWhere(c => c.Value == pathAndQuery[0]);
            if (path == null)
            {
                path = new Path
                {
                    Value = pathAndQuery[0]
                };
                path.Save();
            }
            this.PathId = path.Id;

            if (pathAndQuery.Length > 1)
            {
                string query = pathAndQuery[1];
                if (string.IsNullOrEmpty(query))
                {
                    this.QueryStringId = QueryString.Empty.Id;
                }
                else
                {
                    QueryString qs = QueryString.OneWhere(c => c.Value == query);
                    if (qs == null)
                    {
                        qs = new QueryString
                        {
                            Value = pathAndQuery[1]
                        };
                        qs.Save();
                    }
                    this.QueryStringId = qs.Id;
                }
            }
        }

        private static Port GetPort(Uri uri)
        {
            Port port = null;
            if (uri.IsDefaultPort)
            {
                port = Port.Default;
            }
            else
            {
                port = Port.OneWhere(c => c.Value == uri.Port);
                if (port == null)
                {
                    port = new Port
                    {
                        Value = uri.Port
                    };
                    port.Save();
                }
            }
            return port;
        }

        private static Domain GetDomain(Uri uri)
        {
            Domain domain = Domain.OneWhere(c => c.Value == uri.Host);
            if (domain == null)
            {
                domain = new Domain
                {
                    Value = uri.Host
                };
                domain.Save();
            }
            return domain;
        }

        private static Protocol GetProtocol(Uri uri)
        {
            Protocol proto = Protocol.OneWhere(c => c.Value == uri.Scheme);
            if (proto == null)
            {
                proto = new Protocol
                {
                    Value = uri.Scheme
                };
                proto.Save();
            }
            return proto;
        }

        public override string ToString()
        {
            string proto = this.ProtocolOfProtocolId.Value;
            string domain = this.DomainOfDomainId.Value;
            string port = this.PortOfPortId?.Value > 0 ? string.Format(":{0}", this.PortOfPortId.Value.ToString()) : "";
            if (this.PortOfPortId?.Value == 80)
            {
                port = string.Empty;
            }

            string path = this.PathOfPathId.Value;
            string queryString = "";
            if (this.QueryStringOfQueryStringId != null && !string.IsNullOrEmpty(this.QueryStringOfQueryStringId.Value))
            {
                queryString = this.QueryStringOfQueryStringId.Value;
                if (!queryString.StartsWith("?"))
                {
                    queryString = string.Format("?{0}", queryString);
                }
            }

			string fragment = "";
			if (this.FragmentOfFragmentId != null)
			{
				fragment = this.FragmentOfFragmentId.Value;
				if (!fragment.StartsWith("#"))
				{
					fragment = string.Format("#{0}", fragment);
				}
			}
            return string.Format("{0}://{1}{2}{3}{4}{5}", proto, domain, port, path, queryString, fragment);
        }
    }
}
