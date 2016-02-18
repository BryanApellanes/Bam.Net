/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Text;
using Bam.Net.Web;

namespace Bam.Net.CoreServices
{
    public class ProxySettings
    {
        public Protocols Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public Type ServiceType { get; set; }
        public StringBuilder ClientCode { get; set; }

        public override string ToString()
        {
            return "{Protocol}_{Host}_{Port}".NamedFormat(this);
        }
        /// <summary>
        /// Specifies whether client code should be downloaded
        /// </summary>
        public bool DownloadClient { get; set; }
        public Uri GetUri()
        {
            return new Uri("{0}://{1}:{2}/"._Format(Protocol.ToString().ToLowerInvariant(), Host, Port.ToString()));
        }

        public Uri GetServiceUri(Type type)
        {
            return new Uri("{0}ServiceProxy/CSharpProxies?namespace={1}&classes={2}"._Format(GetUri().ToString(), type.Namespace, type.Name));
        }

        public StringBuilder DownloadClientCode(Type type)
        {
            ClientCode = new StringBuilder(Http.Get(GetServiceUri(type)));
            return ClientCode;
        }

        public override bool Equals(object obj)
        {
            ProxySettings settings = obj as ProxySettings;
            if (settings != null)
            {
                return settings.Protocol == this.Protocol &&
                    settings.Host.Equals(this.Host) &&
                    settings.Port == this.Port &&
                    settings.ServiceType == this.ServiceType;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.Protocol.GetHashCode() + this.Host.GetHashCode() + this.Port.GetHashCode();
        }

        /// <summary>
        /// Copy the current ServiceSettings instance
        /// </summary>
        /// <returns></returns>
        public ProxySettings Clone()
        {
            return this.CopyAs<ProxySettings>();
        }
    }
}
