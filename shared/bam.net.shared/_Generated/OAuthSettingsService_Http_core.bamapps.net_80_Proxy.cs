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
	using System.Collections.Generic;
	using Bam.Net.CoreServices.OAuth;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
		[ApiKeyRequired]
    public class OAuthSettingsServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IOAuthSettingsService>, Bam.Net.CoreServices.Contracts.IOAuthSettingsService
    {
        public OAuthSettingsServiceClient(): base(DefaultConfiguration.GetAppSetting("OAuthSettingsServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public OAuthSettingsServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public CoreServiceResponse<System.Collections.Generic.List<Bam.Net.CoreServices.OAuth.OAuthClientSettings>> GetClientSettings(System.Boolean includeSecret)
        {
            object[] parameters = new object[] { includeSecret };
            return Invoke<CoreServiceResponse<System.Collections.Generic.List<Bam.Net.CoreServices.OAuth.OAuthClientSettings>>>("GetClientSettings", parameters);
        }
        public CoreServiceResponse<Bam.Net.CoreServices.OAuth.OAuthClientSettings> SetProvider(System.String providerName, System.String clientId, System.String clientSecret)
        {
            object[] parameters = new object[] { providerName, clientId, clientSecret };
            return Invoke<CoreServiceResponse<Bam.Net.CoreServices.OAuth.OAuthClientSettings>>("SetProvider", parameters);
        }
        public CoreServiceResponse RemoveProvider(System.String providerName)
        {
            object[] parameters = new object[] { providerName };
            return Invoke<CoreServiceResponse>("RemoveProvider", parameters);
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
	using System.Collections.Generic;
	using Bam.Net.CoreServices.OAuth;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface IOAuthSettingsService
        {
			CoreServiceResponse<System.Collections.Generic.List<Bam.Net.CoreServices.OAuth.OAuthClientSettings>> GetClientSettings(System.Boolean includeSecret);
			CoreServiceResponse<Bam.Net.CoreServices.OAuth.OAuthClientSettings> SetProvider(System.String providerName, System.String clientId, System.String clientSecret);
			CoreServiceResponse RemoveProvider(System.String providerName);
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
	using Bam.Net.CoreServices;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;
	using System;

	public class OAuthSettingsServiceProxy: OAuthSettingsService, IProxy 
	{
		OAuthSettingsServiceClient _proxyClient;
		public OAuthSettingsServiceProxy()
		{
			_proxyClient = new OAuthSettingsServiceClient();
		}

		public OAuthSettingsServiceProxy(string baseUrl)
		{
			_proxyClient = new OAuthSettingsServiceClient(baseUrl);
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
				return typeof(OAuthSettingsService);
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


		public override CoreServiceResponse<System.Collections.Generic.List<Bam.Net.CoreServices.OAuth.OAuthClientSettings>> GetClientSettings(System.Boolean includeSecret)
		{
			return _proxyClient.GetClientSettings(includeSecret);
		}

		public override CoreServiceResponse<Bam.Net.CoreServices.OAuth.OAuthClientSettings> SetProvider(System.String providerName, System.String clientId, System.String clientSecret)
		{
			return _proxyClient.SetProvider(providerName, clientId, clientSecret);
		}

		public override CoreServiceResponse RemoveProvider(System.String providerName)
		{
			return _proxyClient.RemoveProvider(providerName);
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

