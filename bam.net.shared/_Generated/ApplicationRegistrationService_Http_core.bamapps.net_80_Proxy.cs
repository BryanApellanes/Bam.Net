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
	using Bam.Net.CoreServices;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

    
    public class ApplicationRegistrationServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IApplicationRegistrationService>, Bam.Net.CoreServices.Contracts.IApplicationRegistrationService
    {
        public ApplicationRegistrationServiceClient(): base(DefaultConfiguration.GetAppSetting("ApplicationRegistrationServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public ApplicationRegistrationServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
		[ApiKeyRequired]
        public ApiKeyInfo[] ListApiKeys()
        {
            object[] parameters = new object[] {  };
            return Invoke<ApiKeyInfo[]>("ListApiKeys", parameters);
        }
		[ApiKeyRequired]
        public ApiKeyInfo AddApiKey()
        {
            object[] parameters = new object[] {  };
            return Invoke<ApiKeyInfo>("AddApiKey", parameters);
        }
		[ApiKeyRequired]
        public ApiKeyInfo SetActiveApiKeyIndex(System.Int32 index)
        {
            object[] parameters = new object[] { index };
            return Invoke<ApiKeyInfo>("SetActiveApiKeyIndex", parameters);
        }
        public String GetApplicationName()
        {
            object[] parameters = new object[] {  };
            return Invoke<String>("GetApplicationName", parameters);
        }
        public ApiKeyInfo GetClientApiKeyInfo()
        {
            object[] parameters = new object[] {  };
            return Invoke<ApiKeyInfo>("GetClientApiKeyInfo", parameters);
        }
        public CoreServiceResponse RegisterApplication(System.String applicationName)
        {
            object[] parameters = new object[] { applicationName };
            return Invoke<CoreServiceResponse>("RegisterApplication", parameters);
        }
        public CoreServiceResponse RegisterApplicationProcess(Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor descriptor)
        {
            object[] parameters = new object[] { descriptor };
            return Invoke<CoreServiceResponse>("RegisterApplicationProcess", parameters);
        }
        public CoreServiceResponse RegisterClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client)
        {
            object[] parameters = new object[] { client };
            return Invoke<CoreServiceResponse>("RegisterClient", parameters);
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
	using Bam.Net.CoreServices;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

    
        public interface IApplicationRegistrationService
        {
			ApiKeyInfo[] ListApiKeys();
			ApiKeyInfo AddApiKey();
			ApiKeyInfo SetActiveApiKeyIndex(System.Int32 index);
			String GetApplicationName();
			ApiKeyInfo GetClientApiKeyInfo();
			CoreServiceResponse RegisterApplication(System.String applicationName);
			CoreServiceResponse RegisterApplicationProcess(Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor descriptor);
			CoreServiceResponse RegisterClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client);
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
	using Bam.Net.ServiceProxy.Secure;
	using System;
	using Bam.Net.CoreServices;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

	public class ApplicationRegistrationServiceProxy: ApplicationRegistrationService, IProxy 
	{
		ApplicationRegistrationServiceClient _proxyClient;
		public ApplicationRegistrationServiceProxy()
		{
			_proxyClient = new ApplicationRegistrationServiceClient();
		}

		public ApplicationRegistrationServiceProxy(string baseUrl)
		{
			_proxyClient = new ApplicationRegistrationServiceClient(baseUrl);
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
				return typeof(ApplicationRegistrationService);
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


		public override ApiKeyInfo[] ListApiKeys()
		{
			return _proxyClient.ListApiKeys();
		}

		public override ApiKeyInfo AddApiKey()
		{
			return _proxyClient.AddApiKey();
		}

		public override ApiKeyInfo SetActiveApiKeyIndex(System.Int32 index)
		{
			return _proxyClient.SetActiveApiKeyIndex(index);
		}

		public override String GetApplicationName()
		{
			return _proxyClient.GetApplicationName();
		}

		public override ApiKeyInfo GetClientApiKeyInfo()
		{
			return _proxyClient.GetClientApiKeyInfo();
		}

		public override CoreServiceResponse RegisterApplication(System.String applicationName)
		{
			return _proxyClient.RegisterApplication(applicationName);
		}

		public override CoreServiceResponse RegisterApplicationProcess(Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor descriptor)
		{
			return _proxyClient.RegisterApplicationProcess(descriptor);
		}

		public override CoreServiceResponse RegisterClient(Bam.Net.CoreServices.ApplicationRegistration.Data.Client client)
		{
			return _proxyClient.RegisterClient(client);
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

