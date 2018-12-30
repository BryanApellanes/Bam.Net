using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl
{
    public class ResourceUri
    {
        public ResourceUri(string uri)
        {
            Value = uri;
            Parse();
        }

        public string Value { get; }

        public string Scheme { get; set; }

        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }

        string[] _pathSegments;
        public string[] PathSegments
        {
            get
            {
                return _pathSegments;
            }
        }
        public Dictionary<string, string> QueryParams { get; set; }

        private void Parse()
        {
            string scheme = Value.ReadUntil(':', out string remainder);
            Scheme = $"{scheme}://";
            if (remainder.StartsWith("//"))
            {
                remainder = remainder.TruncateFront(2);
            }
            Host = remainder.ReadUntil('/', out string pathRemainder);
            Path = $"/{pathRemainder.ReadUntil('?', out string queryRemainder)}";
            if (!string.IsNullOrEmpty(queryRemainder))
            {
                QueryString = queryRemainder;
                QueryParams = queryRemainder.QueryStringToDictionary();
            }
            _pathSegments = Path.DelimitSplit("/");
        }
    }
}
