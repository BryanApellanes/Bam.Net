/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Reflection;
using System.Text;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using System.Linq;
using System.Collections.Generic;

namespace Bam.Net.CoreServices
{
    public class ProxySettings
    {
        public Protocols Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public Type ServiceType { get; set; }
        public bool IncludeLocalMethods { get; set; }
        public StringBuilder ClientCode { get; set; }

        public ProxySettingsInfo ToInfo()
        {
            ProxySettingsInfo info = new ProxySettingsInfo();
            info.CopyProperties(this);
            info.ServiceType = ServiceType.FullName;
            return info;
        }

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
            if (obj is ProxySettings settings)
            {
                return settings.Protocol == this.Protocol &&
                    settings.Host.Equals(this.Host) &&
                    settings.Port == this.Port &&
                    settings.ServiceType == this.ServiceType &&
                    settings.IncludeLocalMethods == this.IncludeLocalMethods;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(Protocol, Host, Port);
        }

        /// <summary>
        /// Copy the current ProxySettings instance
        /// </summary>
        /// <returns></returns>
        public ProxySettings Clone()
        {
            return this.CopyAs<ProxySettings>();
        }

        public ProxySettingsValidation ValidateTypeMethods()
        {
            Args.ThrowIfNull(ServiceType, nameof(ServiceType));
            return ValidateTypeMethods(ServiceType, IncludeLocalMethods);
        }

        public void ValidateTypeMethodsOrThrow()
        {
            ProxySettingsValidation validation = ValidateTypeMethods();
            if (!validation.Success)
            {
                throw new ProxySettingsValidationException(validation);
            }
        }

        static Dictionary<Type, ProxySettingsValidation> _validatedTypes = new Dictionary<Type, ProxySettingsValidation>();
        public static ProxySettingsValidation ValidateTypeMethods(Type serviceType, bool includeLocalMethods)
        {
            if (_validatedTypes.ContainsKey(serviceType))
            {
                return _validatedTypes[serviceType];
            }
            ProxySettingsValidation result = new ProxySettingsValidation();
            List<MethodInfo> nonOverridableMethods = new List<MethodInfo>();
            ServiceProxySystem.GetProxiedMethods(serviceType, includeLocalMethods)
                .Where(mi => !mi.IsOverridable())
                .Each(new { NonOverridableMethods = nonOverridableMethods }, (ctx, mi) => ctx.NonOverridableMethods.Add(mi));

            string nonVirtualMethodsMessage = $"Non virtual proxied methods were found; proxies cannot be automatically generated for the specified type {serviceType.Namespace}.{serviceType.Name} because proxyable methods were not declared virtual and will subsequently not properly delegate to the remote";
            nonVirtualMethodsMessage += $"\r\n\t{string.Join("\r\n\t", nonOverridableMethods.Select(m => m.Name))}\r\n";
            result.Success = nonOverridableMethods.Count == 0;
            result.Message = result.Success ? string.Empty : nonVirtualMethodsMessage;
            result.NonVirtualMethods = nonOverridableMethods.ToArray();
            _validatedTypes.Add(serviceType, result);
            return result;
        }
    }
}
