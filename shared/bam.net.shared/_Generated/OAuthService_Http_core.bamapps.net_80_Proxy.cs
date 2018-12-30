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
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
    public class OAuthServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IOAuthService>, Bam.Net.CoreServices.Contracts.IOAuthService
    {
        public OAuthServiceClient(): base(DefaultConfiguration.GetAppSetting("OAuthServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public OAuthServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public void SetToken(System.String accessToken)
        {
            object[] parameters = new object[] { accessToken };
            Invoke("SetToken", parameters);
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
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface IOAuthService
        {
			void SetToken(System.String accessToken);
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

	public class OAuthServiceProxy: OAuthService, IProxy 
	{
		OAuthServiceClient _proxyClient;
		public OAuthServiceProxy()
		{
			_proxyClient = new OAuthServiceClient();
		}

		public OAuthServiceProxy(string baseUrl)
		{
			_proxyClient = new OAuthServiceClient(baseUrl);
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
				return typeof(OAuthService);
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


		public override void SetToken(System.String accessToken)
		{
			_proxyClient.SetToken(accessToken);
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

