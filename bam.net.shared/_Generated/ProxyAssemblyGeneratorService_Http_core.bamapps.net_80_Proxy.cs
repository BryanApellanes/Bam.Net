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
	using Bam.Net.Services;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
    public class ProxyAssemblyGeneratorServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IProxyAssemblyGeneratorService>, Bam.Net.CoreServices.Contracts.IProxyAssemblyGeneratorService
    {
        public ProxyAssemblyGeneratorServiceClient(): base(DefaultConfiguration.GetAppSetting("ProxyAssemblyGeneratorServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public ProxyAssemblyGeneratorServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public ServiceResponse GetBase64ProxyAssembly(System.String nameSpace, System.String typeName)
        {
            object[] parameters = new object[] { nameSpace, typeName };
            return Invoke<ServiceResponse>("GetBase64ProxyAssembly", parameters);
        }
        public ServiceResponse GetProxyCode(System.String nameSpace, System.String typeName)
        {
            object[] parameters = new object[] { nameSpace, typeName };
            return Invoke<ServiceResponse>("GetProxyCode", parameters);
        }
        public void ExecuteRemoteAsync(Bam.Net.Services.AsyncExecutionRequest request)
        {
            object[] parameters = new object[] { request };
            Invoke("ExecuteRemoteAsync", parameters);
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
	using Bam.Net.Services;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface IProxyAssemblyGeneratorService
        {
			ServiceResponse GetBase64ProxyAssembly(System.String nameSpace, System.String typeName);
			ServiceResponse GetProxyCode(System.String nameSpace, System.String typeName);
			void ExecuteRemoteAsync(Bam.Net.Services.AsyncExecutionRequest request);
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
	using Bam.Net.Services;
	using System;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

	public class ProxyAssemblyGeneratorServiceProxy: ProxyAssemblyGeneratorService, IProxy 
	{
		ProxyAssemblyGeneratorServiceClient _proxyClient;
		public ProxyAssemblyGeneratorServiceProxy()
		{
			_proxyClient = new ProxyAssemblyGeneratorServiceClient();
		}

		public ProxyAssemblyGeneratorServiceProxy(string baseUrl)
		{
			_proxyClient = new ProxyAssemblyGeneratorServiceClient(baseUrl);
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
				return typeof(ProxyAssemblyGeneratorService);
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


		public override ServiceResponse GetBase64ProxyAssembly(System.String nameSpace, System.String typeName)
		{
			return _proxyClient.GetBase64ProxyAssembly(nameSpace, typeName);
		}

		public override ServiceResponse GetProxyCode(System.String nameSpace, System.String typeName)
		{
			return _proxyClient.GetProxyCode(nameSpace, typeName);
		}

		public override void ExecuteRemoteAsync(Bam.Net.Services.AsyncExecutionRequest request)
		{
			_proxyClient.ExecuteRemoteAsync(request);
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

