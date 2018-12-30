/**
This file was generated from http://core.bamapps.net/serviceproxy/csharpproxies.  This file should not be modified directly
**/


namespace Bam.Net.CoreServices
{
	using System;
	using Bam.Net.Configuration;
	using Bam.Net.ServiceProxy;
	using Bam.Net.ServiceProxy.Secure;
	using Bam.Net.CoreServices.Contracts;
	using System.Collections.Generic;
	using Bam.Net.Logging;
	using Bam.Net.CoreServices.Logging;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
		[ApiKeyRequired]
    public class SystemLogReaderServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.ISystemLogReaderService>, Bam.Net.CoreServices.Contracts.ISystemLogReaderService
    {
        public SystemLogReaderServiceClient(): base(DefaultConfiguration.GetAppSetting("SystemLogReaderServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public SystemLogReaderServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public List<Bam.Net.Logging.LogEntry> GetLogEntries(System.DateTime from, System.DateTime to)
        {
            object[] parameters = new object[] { from, to };
            return Invoke<List<Bam.Net.Logging.LogEntry>>("GetLogEntries", parameters);
        }
        public List<Bam.Net.Logging.LogEntry> GetLogEntriesFrom(System.DateTime since, System.String applicationName, System.String machineName)
        {
            object[] parameters = new object[] { since, applicationName, machineName };
            return Invoke<List<Bam.Net.Logging.LogEntry>>("GetLogEntriesFrom", parameters);
        }
        public List<Bam.Net.CoreServices.Logging.LogEntrySource> GetSources(System.DateTime since)
        {
            object[] parameters = new object[] { since };
            return Invoke<List<Bam.Net.CoreServices.Logging.LogEntrySource>>("GetSources", parameters);
        }
        public Dictionary<System.String, System.String> GetSettings()
        {
            object[] parameters = new object[] {  };
            return Invoke<Dictionary<System.String, System.String>>("GetSettings", parameters);
        }
        public LoginResponse ConnectClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client)
        {
            object[] parameters = new object[] { client };
            return Invoke<LoginResponse>("ConnectClient", parameters);
        }
        public LoginResponse Login(System.String userName, System.String passHash)
        {
            object[] parameters = new object[] { userName, passHash };
            return Invoke<LoginResponse>("Login", parameters);
        }
        public SignOutResponse EndSession()
        {
            object[] parameters = new object[] {  };
            return Invoke<SignOutResponse>("EndSession", parameters);
        }
        public String WhoAmI()
        {
            object[] parameters = new object[] {  };
            return Invoke<String>("WhoAmI", parameters);
        }
    }

}
namespace Bam.Net.CoreServices.Contracts
{
	using System;
	using Bam.Net.Configuration;
	using Bam.Net.ServiceProxy;
	using Bam.Net.ServiceProxy.Secure;
	using Bam.Net.CoreServices.Contracts;
	using System.Collections.Generic;
	using Bam.Net.Logging;
	using Bam.Net.CoreServices.Logging;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface ISystemLogReaderService
        {
			List<Bam.Net.Logging.LogEntry> GetLogEntries(System.DateTime from, System.DateTime to);
			List<Bam.Net.Logging.LogEntry> GetLogEntriesFrom(System.DateTime since, System.String applicationName, System.String machineName);
			List<Bam.Net.CoreServices.Logging.LogEntrySource> GetSources(System.DateTime since);
			Dictionary<System.String, System.String> GetSettings();
			LoginResponse ConnectClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client);
			LoginResponse Login(System.String userName, System.String passHash);
			SignOutResponse EndSession();
			String WhoAmI();

        }

}
/*
This file was generated and should not be modified directly
*/

namespace Bam.Net.CoreServices
{
    using System;
    using Bam.Net;
    using Bam.Net.ServiceProxy;
    using Bam.Net.ServiceProxy.Secure;
    using Bam.Net.CoreServices.Contracts;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;
	using System;

	public class SystemLogReaderServiceProxy: SystemLogReaderService, IProxy 
	{
		SystemLogReaderServiceClient _proxyClient;
		public SystemLogReaderServiceProxy()
		{
			_proxyClient = new SystemLogReaderServiceClient();
		}

		public SystemLogReaderServiceProxy(string baseUrl)
		{
			_proxyClient = new SystemLogReaderServiceClient(baseUrl);
		}

		public ServiceProxyClient Client
		{
			get
			{
				return _proxyClient;
			}		
		}

		public Type ProxiedType
		{
			get
			{
				return typeof(SystemLogReaderService);
			}
		}

		public IApiKeyResolver ApiKeyResolver 
		{
			get
			{
				return (IApiKeyResolver)_proxyClient.Property("ApiKeyResolver", false);
			}
			set
			{
				_proxyClient.Property("ApiKeyResolver", value, false);
			}
		}

		public IApplicationNameProvider ClientApplicationNameProvider
		{
			get
			{
				return (IApplicationNameProvider)_proxyClient.Property("ClientApplicationNameProvider", false);
			}
			set
			{
				_proxyClient.Property("ClientApplicationNameProvider", value, false);
			}
		}

		public void SubscribeToClientEvent(string eventName, EventHandler handler)
		{
			_proxyClient.Subscribe(eventName, handler);
		}


		public override List<Bam.Net.Logging.LogEntry> GetLogEntries(System.DateTime from, System.DateTime to)
		{
			return _proxyClient.GetLogEntries(from, to);
		}

		public override List<Bam.Net.Logging.LogEntry> GetLogEntriesFrom(System.DateTime since, System.String applicationName, System.String machineName)
		{
			return _proxyClient.GetLogEntriesFrom(since, applicationName, machineName);
		}

		public override List<Bam.Net.CoreServices.Logging.LogEntrySource> GetSources(System.DateTime since)
		{
			return _proxyClient.GetSources(since);
		}

		public override Dictionary<System.String, System.String> GetSettings()
		{
			return _proxyClient.GetSettings();
		}

		public override LoginResponse ConnectClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client)
		{
			return _proxyClient.ConnectClient(client);
		}

		public override LoginResponse Login(System.String userName, System.String passHash)
		{
			return _proxyClient.Login(userName, passHash);
		}

		public override SignOutResponse EndSession()
		{
			return _proxyClient.EndSession();
		}

		public override String WhoAmI()
		{
			return _proxyClient.WhoAmI();
		}
	}
}																								

