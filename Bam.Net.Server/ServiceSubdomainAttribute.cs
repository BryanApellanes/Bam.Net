using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    /// <summary>
    /// Used to specify the subdomain 
    /// a class should be served from when resolving
    /// hostname for a service
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceSubdomainAttribute: Attribute
    {
        public ServiceSubdomainAttribute(string subdomain)
        {
            Subdomain = subdomain;
        }
        public string Subdomain { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is ServiceSubdomainAttribute a)
            {
                return a.Subdomain.EndsWith(Subdomain);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Subdomain.ToSha1Int();
        }

        public HostPrefix ToHostPrefix(HostPrefix hostPrefix)
        {
            string[] segments = hostPrefix.HostName.DelimitSplit(".");            
            if(segments.Length >= 2)
            {
                int end = segments.Length - 1;
                int secondFromEnd = end - 1;
                segments = new string[] { segments[secondFromEnd], segments[end] };
            }

            HostPrefix result = hostPrefix.CopyAs<HostPrefix>();
            result.HostName = $"{Subdomain}.{string.Join(".", segments)}";
            return result;
        }
    }
}
