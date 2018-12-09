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

    
    public class RoleServiceClient: SecureServiceProxyClient<Bam.Net.CoreServices.Contracts.IRoleService>, Bam.Net.CoreServices.Contracts.IRoleService
    {
        public RoleServiceClient(): base(DefaultConfiguration.GetAppSetting("RoleServiceUrl", "http://core.bamapps.net/"))
        {
        }

        public RoleServiceClient(string baseAddress): base(baseAddress)
        {
        }
        
        
        public void AddUsersToRoles(System.String[] usernames, System.String[] roleNames)
        {
            object[] parameters = new object[] { usernames, roleNames };
            Invoke("AddUsersToRoles", parameters);
        }
        public void CreateRole(System.String roleName)
        {
            object[] parameters = new object[] { roleName };
            Invoke("CreateRole", parameters);
        }
        public Boolean DeleteRole(System.String roleName, System.Boolean throwOnPopulatedRole)
        {
            object[] parameters = new object[] { roleName, throwOnPopulatedRole };
            return Invoke<Boolean>("DeleteRole", parameters);
        }
        public String[] FindUsersInRole(System.String roleName, System.String usernameToMatch)
        {
            object[] parameters = new object[] { roleName, usernameToMatch };
            return Invoke<String[]>("FindUsersInRole", parameters);
        }
        public String[] GetAllRoles()
        {
            object[] parameters = new object[] {  };
            return Invoke<String[]>("GetAllRoles", parameters);
        }
        public String[] GetRolesForUser(System.String username)
        {
            object[] parameters = new object[] { username };
            return Invoke<String[]>("GetRolesForUser", parameters);
        }
        public String[] GetUsersInRole(System.String roleName)
        {
            object[] parameters = new object[] { roleName };
            return Invoke<String[]>("GetUsersInRole", parameters);
        }
        public Boolean IsUserInRole(System.String username, System.String roleName)
        {
            object[] parameters = new object[] { username, roleName };
            return Invoke<Boolean>("IsUserInRole", parameters);
        }
        public void RemoveUsersFromRoles(System.String[] usernames, System.String[] roleNames)
        {
            object[] parameters = new object[] { usernames, roleNames };
            Invoke("RemoveUsersFromRoles", parameters);
        }
        public Boolean RoleExists(System.String roleName)
        {
            object[] parameters = new object[] { roleName };
            return Invoke<Boolean>("RoleExists", parameters);
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

    
        public interface IRoleService
        {
			void AddUsersToRoles(System.String[] usernames, System.String[] roleNames);
			void CreateRole(System.String roleName);
			Boolean DeleteRole(System.String roleName, System.Boolean throwOnPopulatedRole);
			String[] FindUsersInRole(System.String roleName, System.String usernameToMatch);
			String[] GetAllRoles();
			String[] GetRolesForUser(System.String username);
			String[] GetUsersInRole(System.String roleName);
			Boolean IsUserInRole(System.String username, System.String roleName);
			void RemoveUsersFromRoles(System.String[] usernames, System.String[] roleNames);
			Boolean RoleExists(System.String roleName);
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

	public class RoleServiceProxy: RoleService, IProxy 
	{
		RoleServiceClient _proxyClient;
		public RoleServiceProxy()
		{
			_proxyClient = new RoleServiceClient();
		}

		public RoleServiceProxy(string baseUrl)
		{
			_proxyClient = new RoleServiceClient(baseUrl);
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
				return typeof(RoleService);
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


		public override void AddUsersToRoles(System.String[] usernames, System.String[] roleNames)
		{
			_proxyClient.AddUsersToRoles(usernames, roleNames);
		}

		public override void CreateRole(System.String roleName)
		{
			_proxyClient.CreateRole(roleName);
		}

		public override Boolean DeleteRole(System.String roleName, System.Boolean throwOnPopulatedRole)
		{
			return _proxyClient.DeleteRole(roleName, throwOnPopulatedRole);
		}

		public override String[] FindUsersInRole(System.String roleName, System.String usernameToMatch)
		{
			return _proxyClient.FindUsersInRole(roleName, usernameToMatch);
		}

		public override String[] GetAllRoles()
		{
			return _proxyClient.GetAllRoles();
		}

		public override String[] GetRolesForUser(System.String username)
		{
			return _proxyClient.GetRolesForUser(username);
		}

		public override String[] GetUsersInRole(System.String roleName)
		{
			return _proxyClient.GetUsersInRole(roleName);
		}

		public override Boolean IsUserInRole(System.String username, System.String roleName)
		{
			return _proxyClient.IsUserInRole(username, roleName);
		}

		public override void RemoveUsersFromRoles(System.String[] usernames, System.String[] roleNames)
		{
			_proxyClient.RemoveUsersFromRoles(usernames, roleNames);
		}

		public override Boolean RoleExists(System.String roleName)
		{
			return _proxyClient.RoleExists(roleName);
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

