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
	using Bam.Net.Logging;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
		[ApiKeyRequired]
    public class SystemLoggerServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.ISystemLoggerService>, Bam.Net.CoreServices.Contracts.ISystemLoggerService
    {
        public SystemLoggerServiceClient(): base(DefaultConfiguration.GetAppSetting("SystemLoggerServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public SystemLoggerServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public void CommitLogEvent(Bam.Net.Logging.LogEvent logEvent)
        {
            object[] parameters = new object[] { logEvent };
            Invoke("CommitLogEvent", parameters);
        }
        public void Info(System.String messageSignature, System.Object[] formatArguments)
        {
            object[] parameters = new object[] { messageSignature, formatArguments };
            Invoke("Info", parameters);
        }
        public void Warning(System.String messageSignature, System.Object[] formatArguments)
        {
            object[] parameters = new object[] { messageSignature, formatArguments };
            Invoke("Warning", parameters);
        }
        public void Error(System.String messageSignature, System.Object[] formatArguments)
        {
            object[] parameters = new object[] { messageSignature, formatArguments };
            Invoke("Error", parameters);
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
	using Bam.Net.Logging;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface ISystemLoggerService
        {
			void CommitLogEvent(Bam.Net.Logging.LogEvent logEvent);
			void Info(System.String messageSignature, System.Object[] formatArguments);
			void Warning(System.String messageSignature, System.Object[] formatArguments);
			void Error(System.String messageSignature, System.Object[] formatArguments);
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
	using System;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

	public class SystemLoggerServiceProxy: SystemLoggerService, IProxy 
	{
		SystemLoggerServiceClient _proxyClient;
		public SystemLoggerServiceProxy()
		{
			_proxyClient = new SystemLoggerServiceClient();
		}

		public SystemLoggerServiceProxy(string baseUrl)
		{
			_proxyClient = new SystemLoggerServiceClient(baseUrl);
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
				return typeof(SystemLoggerService);
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


		public override void CommitLogEvent(Bam.Net.Logging.LogEvent logEvent)
		{
			_proxyClient.CommitLogEvent(logEvent);
		}

		public override void Info(System.String messageSignature, System.Object[] formatArguments)
		{
			_proxyClient.Info(messageSignature, formatArguments);
		}

		public override void Warning(System.String messageSignature, System.Object[] formatArguments)
		{
			_proxyClient.Warning(messageSignature, formatArguments);
		}

		public override void Error(System.String messageSignature, System.Object[] formatArguments)
		{
			_proxyClient.Error(messageSignature, formatArguments);
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

