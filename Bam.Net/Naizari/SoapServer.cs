/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using Naizari.Configuration;
using Naizari.Service;

namespace Naizari
{
    /// <summary>
    /// This class was extracted from reusable patterns identified in 
    /// ConfigurationService and provides convenience methods for 
    /// quickly creating a soap server that runs as a Windows service.
    /// </summary>
    public class SoapServer : ServiceExe
    {
        public SoapServer()
        {
            //The Dynamic and/or Private Ports are those from 49152 through 65535

        }

        /// <summary>
        /// The fully qualified name of the Interface that is implemented by 
        /// the Type specified in the ServiceType property.  The implemented interface
        /// must have the [ServiceContract] attribute along with other attributes
        /// defining the service provided by the ServiceType.  See System.Servicemodel
        /// for more information.  This is Windows Communication Foundation (WCF) stuff.
        /// </summary>
        public string ImplementedContract { get; set; }

        /// <summary>
        /// The Type of the web service implementer.
        /// </summary>
        public Type ServiceType { get; set; }

        protected Thread soapServiceThread;
        AutoResetEvent threadSleeper = new AutoResetEvent(false); // will be used to "sleep" the soapServiceThread
        ServiceHost host;
        protected override void OnStart(string[] args)
        {
            Log("Starting service");
            threadSleeper = new AutoResetEvent(false);
            this.BindingType = BindingType.WsHttp;
            soapServiceThread = new Thread(new ThreadStart(StartSoapService));
            soapServiceThread.IsBackground = true;
            soapServiceThread.Start();
            Log("Service started");
        }

        protected void StartSoapService()
        {
            Log("Service thread started");
            try
            {
                
                string stringUri = GetBaseAddress(this);
                if (string.IsNullOrEmpty(stringUri))
                    return;

                Uri baseAddressUri = new Uri(stringUri);

                if (ServiceType != null)
                {
                    host = new ServiceHost(ServiceType, baseAddressUri);
                    Log("Providing soap service of type " + ServiceType.Name);
                }
                else
                {
                    Log("ServiceType was not specified.");
                    return;
                }

                EndpointAddress endpoint = new EndpointAddress(stringUri);
                host.AddServiceEndpoint(ImplementedContract, GetBinding(), stringUri);

                // enable wsdl
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = baseAddressUri;
                host.Description.Behaviors.Add(smb);
                // end enable wsdl

                // enable debug info
                ((ServiceDebugBehavior)host.Description.Behaviors[typeof(ServiceDebugBehavior)]).IncludeExceptionDetailInFaults = true;
                // end enable debug info

                host.Open();
                string addresses = string.Empty;
                foreach (Uri uri in host.BaseAddresses)
                {
                    addresses += uri.ToString() + "\r\n";
                }
                Log("Responding to addresses:\r\n" + addresses);
                threadSleeper.WaitOne(); // this is apparently unecessary according to testing but it isn't hurting anything
            }
            catch (Exception ex)
            {
                Log("An error occurred.", ex);
            }
        }

        protected static string GetBaseAddress(SoapServer instance)
        {
            SoapConfig config = null;
            try
            {
                if (instance != null)
                    instance.Log("Getting configured port");

                config = new SoapConfig();
                DefaultConfiguration.SetProperties(config, true);

                if (instance != null)
                    instance.Log(serviceName + " port set to " + config.Port.ToString());
            }
            catch (Exception ex)
            {
                if (instance != null)
                    instance.Log(ex.Message);

                return null;
            }
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();

            // testing with localhost - change to computerProperties.HostName
            string stringUri = string.Format("http://{0}:{1}", computerProperties.HostName, config.Port);
            return stringUri;
        }

        protected void Log(string message)
        {
            Log(message, null);
        }

        protected void Log(string message, Exception ex)
        {
            try
            {
                if (windowsLogger == null)
                    CreateLog();

                if (ex != null)
                    windowsLogger.AddEntry(message, ex);
                else
                    windowsLogger.AddEntry(message);
            }
            catch (Exception fatal)
            {
                FileLog(fatal);
            }
        }

        protected override void OnStop()
        {
            host.Close(new TimeSpan(3000));
            threadSleeper.Set();
            soapServiceThread.Abort();
        }

        public BindingType BindingType
        {
            get;
            protected set;
        }

        public bool UseTransportSecurity { get; set; }

        protected Binding GetBinding()
        {
            BindingType bindingType = this.BindingType;
            switch (bindingType)
            {
                case BindingType.Invalid:
                    throw new InvalidOperationException("Invalid BindingType specified.");
                case BindingType.Basic:
                    return GetBasicBinding();
                case BindingType.WsHttp:
                    return GetWSHttpBinding();
            }

            return null;
        }

        public static BasicHttpBinding GetBasicBinding()
        {
            return GetBasicBinding(false);
        }

        public static BasicHttpBinding GetBasicBinding(bool useTransportSecurity)
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            
            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.BypassProxyOnLocal = false;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.MaxBufferPoolSize = 5242880;
            binding.MaxReceivedMessageSize = 5242880;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TextEncoding = Encoding.UTF8;


            binding.TransferMode = TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;
            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxDepth = 32;
            readerQuotas.MaxStringContentLength = 8192;
            readerQuotas.MaxArrayLength = 16384;
            readerQuotas.MaxBytesPerRead = 4096;
            readerQuotas.MaxNameTableCharCount = 16384;
            binding.ReaderQuotas = readerQuotas;

            binding.Security.Mode = useTransportSecurity ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Transport.Realm = "";
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;


            return binding;


        }

        public static WSHttpBinding GetWSHttpBinding()
        {
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message, false);

            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.BypassProxyOnLocal = false;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.MaxBufferPoolSize = 5242880;
            binding.MaxReceivedMessageSize = 5242880;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TextEncoding = Encoding.UTF8;
            
            

            binding.TransactionFlow = false;
            
            
            binding.UseDefaultWebProxy = true;
            binding.AllowCookies = false;

            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxDepth = 32;
            readerQuotas.MaxStringContentLength = 5242880;
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
