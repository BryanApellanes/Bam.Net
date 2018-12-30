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
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
		[ApiKeyRequired]
    public class ConfigurationServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IConfigurationService>, Bam.Net.CoreServices.Contracts.IConfigurationService
    {
        public ConfigurationServiceClient(): base(DefaultConfiguration.GetAppSetting("ConfigurationServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public ConfigurationServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public ApplicationConfiguration GetConfiguration(System.String applicationName, System.String machineName, System.String configurationName)
        {
            object[] parameters = new object[] { applicationName, machineName, configurationName };
            return Invoke<ApplicationConfiguration>("GetConfiguration", parameters);
        }
        public void SetCommonConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings)
        {
            object[] parameters = new object[] { settings };
            Invoke("SetCommonConfiguration", parameters);
        }
        public Dictionary<System.String, System.String> GetCommonConfiguration()
        {
            object[] parameters = new object[] {  };
            return Invoke<Dictionary<System.String, System.String>>("GetCommonConfiguration", parameters);
        }
        public void SetApplicationConfiguration(System.String applicationName, System.Collections.Generic.Dictionary<System.String, System.String> configuration, System.String configurationName)
        {
            object[] parameters = new object[] { applicationName, configuration, configurationName };
            Invoke("SetApplicationConfiguration", parameters);
        }
        public void SetApplicationConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String applicationName, System.String configurationName)
        {
            object[] parameters = new object[] { settings, applicationName, configurationName };
            Invoke("SetApplicationConfiguration", parameters);
        }
        public void SetMachineConfiguration(System.String machineName, System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String configurationName)
        {
            object[] parameters = new object[] { machineName, settings, configurationName };
            Invoke("SetMachineConfiguration", parameters);
        }
        public Dictionary<System.String, System.String> GetApplicationConfiguration(System.String applicationName, System.String configurationName)
        {
            object[] parameters = new object[] { applicationName, configurationName };
            return Invoke<Dictionary<System.String, System.String>>("GetApplicationConfiguration", parameters);
        }
        public Dictionary<System.String, System.String> GetMachineConfiguration(System.String machineName, System.String configurationName)
        {
            object[] parameters = new object[] { machineName, configurationName };
            return Invoke<Dictionary<System.String, System.String>>("GetMachineConfiguration", parameters);
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
	using Bam.Net.CoreServices.ApplicationRegistration.Data;
	using Bam.Net.UserAccounts;

    
        public interface IConfigurationService
        {
			ApplicationConfiguration GetConfiguration(System.String applicationName, System.String machineName, System.String configurationName);
			void SetCommonConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings);
			Dictionary<System.String, System.String> GetCommonConfiguration();
			void SetApplicationConfiguration(System.String applicationName, System.Collections.Generic.Dictionary<System.String, System.String> configuration, System.String configurationName);
			void SetApplicationConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String applicationName, System.String configurationName);
			void SetMachineConfiguration(System.String machineName, System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String configurationName);
			Dictionary<System.String, System.String> GetApplicationConfiguration(System.String applicationName, System.String configurationName);
			Dictionary<System.String, System.String> GetMachineConfiguration(System.String machineName, System.String configurationName);
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
	using System;
	using System.Collections.Generic;
	using Bam.Net.UserAccounts;

	public class ConfigurationServiceProxy: ConfigurationService, IProxy 
	{
		ConfigurationServiceClient _proxyClient;
		public ConfigurationServiceProxy()
		{
			_proxyClient = new ConfigurationServiceClient();
		}

		public ConfigurationServiceProxy(string baseUrl)
		{
			_proxyClient = new ConfigurationServiceClient(baseUrl);
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
				return typeof(ConfigurationService);
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


		public override ApplicationConfiguration GetConfiguration(System.String applicationName, System.String machineName, System.String configurationName)
		{
			return _proxyClient.GetConfiguration(applicationName, machineName, configurationName);
		}

		public override void SetCommonConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings)
		{
			_proxyClient.SetCommonConfiguration(settings);
		}

		public override Dictionary<System.String, System.String> GetCommonConfiguration()
		{
			return _proxyClient.GetCommonConfiguration();
		}

		public override void SetApplicationConfiguration(System.String applicationName, System.Collections.Generic.Dictionary<System.String, System.String> configuration, System.String configurationName)
		{
			_proxyClient.SetApplicationConfiguration(applicationName, configuration, configurationName);
		}

		public override void SetApplicationConfiguration(System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String applicationName, System.String configurationName)
		{
			_proxyClient.SetApplicationConfiguration(settings, applicationName, configurationName);
		}

		public override void SetMachineConfiguration(System.String machineName, System.Collections.Generic.Dictionary<System.String, System.String> settings, System.String configurationName)
		{
			_proxyClient.SetMachineConfiguration(machineName, settings, configurationName);
		}

		public override Dictionary<System.String, System.String> GetApplicationConfiguration(System.String applicationName, System.String configurationName)
		{
			return _proxyClient.GetApplicationConfiguration(applicationName, configurationName);
		}

		public override Dictionary<System.String, System.String> GetMachineConfiguration(System.String machineName, System.String configurationName)
		{
			return _proxyClient.GetMachineConfiguration(machineName, configurationName);
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

