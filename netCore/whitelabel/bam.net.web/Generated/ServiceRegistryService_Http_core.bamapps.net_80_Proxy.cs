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
	using Bam.Net.CoreServices.ServiceRegistration.Data;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
		[ApiKeyRequired]
    public class ServiceRegistryServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IServiceRegistryService>, Bam.Net.CoreServices.Contracts.IServiceRegistryService
    {
        public ServiceRegistryServiceClient(): base(DefaultConfiguration.GetAppSetting("ServiceRegistryServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public ServiceRegistryServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public ServiceRegistryDescriptor GetServiceRegistryDescriptor(System.String name)
        {
            object[] parameters = new object[] { name };
            return Invoke<ServiceRegistryDescriptor>("GetServiceRegistryDescriptor", parameters);
        }
        public ServiceRegistryLoaderDescriptor GetServiceRegistryLoaderDescriptor(System.String name)
        {
            object[] parameters = new object[] { name };
            return Invoke<ServiceRegistryLoaderDescriptor>("GetServiceRegistryLoaderDescriptor", parameters);
        }
        public ServiceRegistryDescriptor RegisterServiceRegistryDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor registry, System.Boolean overwrite)
        {
            object[] parameters = new object[] { registry, overwrite };
            return Invoke<ServiceRegistryDescriptor>("RegisterServiceRegistryDescriptor", parameters);
        }
        public ServiceRegistryLoaderDescriptor RegisterServiceRegistryLoaderDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor loader, System.Boolean overwrite)
        {
            object[] parameters = new object[] { loader, overwrite };
            return Invoke<ServiceRegistryLoaderDescriptor>("RegisterServiceRegistryLoaderDescriptor", parameters);
        }
        public void LockServiceRegistry(System.String name)
        {
            object[] parameters = new object[] { name };
            Invoke("LockServiceRegistry", parameters);
        }
        public void UnlockServiceRegistry(System.String name)
        {
            object[] parameters = new object[] { name };
            Invoke("UnlockServiceRegistry", parameters);
        }
        public Boolean IsLocked(System.String name)
        {
            object[] parameters = new object[] { name };
            return Invoke<Boolean>("IsLocked", parameters);
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
	using Bam.Net.CoreServices.ServiceRegistration.Data;
	using System.Collections.Generic;
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface IServiceRegistryService
        {
			ServiceRegistryDescriptor GetServiceRegistryDescriptor(System.String name);
			ServiceRegistryLoaderDescriptor GetServiceRegistryLoaderDescriptor(System.String name);
			ServiceRegistryDescriptor RegisterServiceRegistryDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor registry, System.Boolean overwrite);
			ServiceRegistryLoaderDescriptor RegisterServiceRegistryLoaderDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor loader, System.Boolean overwrite);
			void LockServiceRegistry(System.String name);
			void UnlockServiceRegistry(System.String name);
			Boolean IsLocked(System.String name);
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
	using Bam.Net.CoreServices.ServiceRegistration.Data;
	using System;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

	public class ServiceRegistryServiceProxy: ServiceRegistryService, IProxy 
	{
		ServiceRegistryServiceClient _proxyClient;
		public ServiceRegistryServiceProxy()
		{
			_proxyClient = new ServiceRegistryServiceClient();
		}

		public ServiceRegistryServiceProxy(string baseUrl)
		{
			_proxyClient = new ServiceRegistryServiceClient(baseUrl);
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
				return typeof(ServiceRegistryService);
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


		public override ServiceRegistryDescriptor GetServiceRegistryDescriptor(System.String name)
		{
			return _proxyClient.GetServiceRegistryDescriptor(name);
		}

		public override ServiceRegistryLoaderDescriptor GetServiceRegistryLoaderDescriptor(System.String name)
		{
			return _proxyClient.GetServiceRegistryLoaderDescriptor(name);
		}

		public override ServiceRegistryDescriptor RegisterServiceRegistryDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor registry, System.Boolean overwrite)
		{
			return _proxyClient.RegisterServiceRegistryDescriptor(registry, overwrite);
		}

		public override ServiceRegistryLoaderDescriptor RegisterServiceRegistryLoaderDescriptor(Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor loader, System.Boolean overwrite)
		{
			return _proxyClient.RegisterServiceRegistryLoaderDescriptor(loader, overwrite);
		}

		public override void LockServiceRegistry(System.String name)
		{
			_proxyClient.LockServiceRegistry(name);
		}

		public override void UnlockServiceRegistry(System.String name)
		{
			_proxyClient.UnlockServiceRegistry(name);
		}

		public override Boolean IsLocked(System.String name)
		{
			return _proxyClient.IsLocked(name);
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

