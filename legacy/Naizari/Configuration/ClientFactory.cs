/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using KLGates.Encryption;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Security;
using System.ServiceModel.Description;

namespace KLGates.Configuration.Service.SoapClient
{
    public class ClientFactory
    {
        static Dictionary<string, ConfigurationServiceClient> configurationServiceClients;

        public static ConfigurationServiceClient GetConfigurationServiceClient(string hostName)
        {
            return GetConfigurationServiceClient(hostName, string.Empty);
        }

        public static ConfigurationServiceClient GetConfigurationServiceClient(string hostName, string port)
        {
            if (!string.IsNullOrEmpty(port) &&
                !port.StartsWith(":"))
                port = ":" + port;

            string endPointUri = string.Format("http://{0}{1}/", hostName, port);

            if (configurationServiceClients == null)
                configurationServiceClients = new Dictionary<string, ConfigurationServiceClient>();

            if (configurationServiceClients.ContainsKey(endPointUri))
                return configurationServiceClients[endPointUri];

            WSHttpBinding binding = GetBinding();
            
            ConfigurationServiceClient retVal = new ConfigurationServiceClient(binding, new EndpointAddress(endPointUri));
            if (!configurationServiceClients.ContainsKey(endPointUri))
                configurationServiceClients.Add(endPointUri, retVal);

            return retVal;
        }

        private static WSHttpBinding GetBinding()
        {
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message, false);

            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.BypassProxyOnLocal = false;
            binding.TransactionFlow = false;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.MaxBufferPoolSize = 524288;
            binding.MaxReceivedMessageSize = 65536;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TextEncoding = Encoding.UTF8;
            binding.UseDefaultWebProxy = true;
            binding.AllowCookies = false;

            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxDepth = 32;
            readerQuotas.MaxStringContentLength = 8192;
            readerQuotas.MaxArrayLength = 16384;
            readerQuotas.MaxBytesPerRead = 4096;
            readerQuotas.MaxNameTableCharCount = 16384;
            binding.ReaderQuotas = readerQuotas;

            binding.ReliableSession.Ordered = true;
            binding.ReliableSession.InactivityTimeout = new TimeSpan(0, 10, 0);

            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Transport.Realm = string.Empty;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            binding.Security.Message.NegotiateServiceCredential = true;
            binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;
            binding.Security.Message.EstablishSecurityContext = true;
            return binding;
        }
    }
}
