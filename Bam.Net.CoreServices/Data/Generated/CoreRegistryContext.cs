/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.CoreServices.Data.Daos
{
	// schema = CoreRegistry 
    public static class CoreRegistryContext
    {
		public static string ConnectionName
		{
			get
			{
				return "CoreRegistry";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class IpAddressQueryContext
	{
			public IpAddressCollection Where(WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.Where(where, db);
			}
		   
			public IpAddressCollection Where(WhereDelegate<IpAddressColumns> where, OrderBy<IpAddressColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.Where(where, orderBy, db);
			}

			public IpAddress OneWhere(WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.OneWhere(where, db);
			}

			public static IpAddress GetOneWhere(WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.GetOneWhere(where, db);
			}
		
			public IpAddress FirstOneWhere(WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.FirstOneWhere(where, db);
			}

			public IpAddressCollection Top(int count, WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.Top(count, where, db);
			}

			public IpAddressCollection Top(int count, WhereDelegate<IpAddressColumns> where, OrderBy<IpAddressColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<IpAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.IpAddress.Count(where, db);
			}
	}

	static IpAddressQueryContext _ipAddresses;
	static object _ipAddressesLock = new object();
	public static IpAddressQueryContext IpAddresses
	{
		get
		{
			return _ipAddressesLock.DoubleCheckLock<IpAddressQueryContext>(ref _ipAddresses, () => new IpAddressQueryContext());
		}
	}
	public class ConfigurationQueryContext
	{
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.Where(where, db);
			}
		   
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.Where(where, orderBy, db);
			}

			public Configuration OneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.OneWhere(where, db);
			}

			public static Configuration GetOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.GetOneWhere(where, db);
			}
		
			public Configuration FirstOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.FirstOneWhere(where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.Top(count, where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Configuration.Count(where, db);
			}
	}

	static ConfigurationQueryContext _configurations;
	static object _configurationsLock = new object();
	public static ConfigurationQueryContext Configurations
	{
		get
		{
			return _configurationsLock.DoubleCheckLock<ConfigurationQueryContext>(ref _configurations, () => new ConfigurationQueryContext());
		}
	}
	public class MachineQueryContext
	{
			public MachineCollection Where(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.Where(where, db);
			}
		   
			public MachineCollection Where(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.Where(where, orderBy, db);
			}

			public Machine OneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.OneWhere(where, db);
			}

			public static Machine GetOneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.GetOneWhere(where, db);
			}
		
			public Machine FirstOneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.FirstOneWhere(where, db);
			}

			public MachineCollection Top(int count, WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.Top(count, where, db);
			}

			public MachineCollection Top(int count, WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Machine.Count(where, db);
			}
	}

	static MachineQueryContext _machines;
	static object _machinesLock = new object();
	public static MachineQueryContext Machines
	{
		get
		{
			return _machinesLock.DoubleCheckLock<MachineQueryContext>(ref _machines, () => new MachineQueryContext());
		}
	}
	public class ApplicationQueryContext
	{
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.Where(where, db);
			}
		   
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.Where(where, orderBy, db);
			}

			public Application OneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.OneWhere(where, db);
			}

			public static Application GetOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.GetOneWhere(where, db);
			}
		
			public Application FirstOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.FirstOneWhere(where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.Top(count, where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Application.Count(where, db);
			}
	}

	static ApplicationQueryContext _applications;
	static object _applicationsLock = new object();
	public static ApplicationQueryContext Applications
	{
		get
		{
			return _applicationsLock.DoubleCheckLock<ApplicationQueryContext>(ref _applications, () => new ApplicationQueryContext());
		}
	}
	public class ProcessDescriptorQueryContext
	{
			public ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Where(where, db);
			}
		   
			public ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Where(where, orderBy, db);
			}

			public ProcessDescriptor OneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.OneWhere(where, db);
			}

			public static ProcessDescriptor GetOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.GetOneWhere(where, db);
			}
		
			public ProcessDescriptor FirstOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.FirstOneWhere(where, db);
			}

			public ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Top(count, where, db);
			}

			public ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Count(where, db);
			}
	}

	static ProcessDescriptorQueryContext _processDescriptors;
	static object _processDescriptorsLock = new object();
	public static ProcessDescriptorQueryContext ProcessDescriptors
	{
		get
		{
			return _processDescriptorsLock.DoubleCheckLock<ProcessDescriptorQueryContext>(ref _processDescriptors, () => new ProcessDescriptorQueryContext());
		}
	}
	public class ApiKeyQueryContext
	{
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.Where(where, db);
			}
		   
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.Where(where, orderBy, db);
			}

			public ApiKey OneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.OneWhere(where, db);
			}

			public static ApiKey GetOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.GetOneWhere(where, db);
			}
		
			public ApiKey FirstOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.FirstOneWhere(where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.Top(count, where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApiKey.Count(where, db);
			}
	}

	static ApiKeyQueryContext _apiKeys;
	static object _apiKeysLock = new object();
	public static ApiKeyQueryContext ApiKeys
	{
		get
		{
			return _apiKeysLock.DoubleCheckLock<ApiKeyQueryContext>(ref _apiKeys, () => new ApiKeyQueryContext());
		}
	}
	public class ClientServerConnectionQueryContext
	{
			public ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Where(where, db);
			}
		   
			public ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Where(where, orderBy, db);
			}

			public ClientServerConnection OneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.OneWhere(where, db);
			}

			public static ClientServerConnection GetOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.GetOneWhere(where, db);
			}
		
			public ClientServerConnection FirstOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.FirstOneWhere(where, db);
			}

			public ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Top(count, where, db);
			}

			public ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Count(where, db);
			}
	}

	static ClientServerConnectionQueryContext _clientServerConnections;
	static object _clientServerConnectionsLock = new object();
	public static ClientServerConnectionQueryContext ClientServerConnections
	{
		get
		{
			return _clientServerConnectionsLock.DoubleCheckLock<ClientServerConnectionQueryContext>(ref _clientServerConnections, () => new ClientServerConnectionQueryContext());
		}
	}
	public class ExternalEventSubscriptionDescriptorQueryContext
	{
			public ExternalEventSubscriptionDescriptorCollection Where(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Where(where, db);
			}
		   
			public ExternalEventSubscriptionDescriptorCollection Where(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Where(where, orderBy, db);
			}

			public ExternalEventSubscriptionDescriptor OneWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.OneWhere(where, db);
			}

			public static ExternalEventSubscriptionDescriptor GetOneWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.GetOneWhere(where, db);
			}
		
			public ExternalEventSubscriptionDescriptor FirstOneWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.FirstOneWhere(where, db);
			}

			public ExternalEventSubscriptionDescriptorCollection Top(int count, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Top(count, where, db);
			}

			public ExternalEventSubscriptionDescriptorCollection Top(int count, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Count(where, db);
			}
	}

	static ExternalEventSubscriptionDescriptorQueryContext _externalEventSubscriptionDescriptors;
	static object _externalEventSubscriptionDescriptorsLock = new object();
	public static ExternalEventSubscriptionDescriptorQueryContext ExternalEventSubscriptionDescriptors
	{
		get
		{
			return _externalEventSubscriptionDescriptorsLock.DoubleCheckLock<ExternalEventSubscriptionDescriptorQueryContext>(ref _externalEventSubscriptionDescriptors, () => new ExternalEventSubscriptionDescriptorQueryContext());
		}
	}
	public class OrganizationQueryContext
	{
			public OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.Where(where, db);
			}
		   
			public OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.Where(where, orderBy, db);
			}

			public Organization OneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.OneWhere(where, db);
			}

			public static Organization GetOneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.GetOneWhere(where, db);
			}
		
			public Organization FirstOneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.FirstOneWhere(where, db);
			}

			public OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.Top(count, where, db);
			}

			public OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Organization.Count(where, db);
			}
	}

	static OrganizationQueryContext _organizations;
	static object _organizationsLock = new object();
	public static OrganizationQueryContext Organizations
	{
		get
		{
			return _organizationsLock.DoubleCheckLock<OrganizationQueryContext>(ref _organizations, () => new OrganizationQueryContext());
		}
	}
	public class UserQueryContext
	{
			public UserCollection Where(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.Where(where, db);
			}
		   
			public UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.Where(where, orderBy, db);
			}

			public User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.OneWhere(where, db);
			}

			public static User GetOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.GetOneWhere(where, db);
			}
		
			public User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.FirstOneWhere(where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.Top(count, where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.User.Count(where, db);
			}
	}

	static UserQueryContext _users;
	static object _usersLock = new object();
	public static UserQueryContext Users
	{
		get
		{
			return _usersLock.DoubleCheckLock<UserQueryContext>(ref _users, () => new UserQueryContext());
		}
	}
	public class SubscriptionQueryContext
	{
			public SubscriptionCollection Where(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.Where(where, db);
			}
		   
			public SubscriptionCollection Where(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.Where(where, orderBy, db);
			}

			public Subscription OneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.OneWhere(where, db);
			}

			public static Subscription GetOneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.GetOneWhere(where, db);
			}
		
			public Subscription FirstOneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.FirstOneWhere(where, db);
			}

			public SubscriptionCollection Top(int count, WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.Top(count, where, db);
			}

			public SubscriptionCollection Top(int count, WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.Subscription.Count(where, db);
			}
	}

	static SubscriptionQueryContext _subscriptions;
	static object _subscriptionsLock = new object();
	public static SubscriptionQueryContext Subscriptions
	{
		get
		{
			return _subscriptionsLock.DoubleCheckLock<SubscriptionQueryContext>(ref _subscriptions, () => new SubscriptionQueryContext());
		}
	}
	public class ConfigurationMachineQueryContext
	{
			public ConfigurationMachineCollection Where(WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.Where(where, db);
			}
		   
			public ConfigurationMachineCollection Where(WhereDelegate<ConfigurationMachineColumns> where, OrderBy<ConfigurationMachineColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.Where(where, orderBy, db);
			}

			public ConfigurationMachine OneWhere(WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.OneWhere(where, db);
			}

			public static ConfigurationMachine GetOneWhere(WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.GetOneWhere(where, db);
			}
		
			public ConfigurationMachine FirstOneWhere(WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.FirstOneWhere(where, db);
			}

			public ConfigurationMachineCollection Top(int count, WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.Top(count, where, db);
			}

			public ConfigurationMachineCollection Top(int count, WhereDelegate<ConfigurationMachineColumns> where, OrderBy<ConfigurationMachineColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationMachine.Count(where, db);
			}
	}

	static ConfigurationMachineQueryContext _configurationMachines;
	static object _configurationMachinesLock = new object();
	public static ConfigurationMachineQueryContext ConfigurationMachines
	{
		get
		{
			return _configurationMachinesLock.DoubleCheckLock<ConfigurationMachineQueryContext>(ref _configurationMachines, () => new ConfigurationMachineQueryContext());
		}
	}
	public class ConfigurationApplicationQueryContext
	{
			public ConfigurationApplicationCollection Where(WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.Where(where, db);
			}
		   
			public ConfigurationApplicationCollection Where(WhereDelegate<ConfigurationApplicationColumns> where, OrderBy<ConfigurationApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.Where(where, orderBy, db);
			}

			public ConfigurationApplication OneWhere(WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.OneWhere(where, db);
			}

			public static ConfigurationApplication GetOneWhere(WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.GetOneWhere(where, db);
			}
		
			public ConfigurationApplication FirstOneWhere(WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.FirstOneWhere(where, db);
			}

			public ConfigurationApplicationCollection Top(int count, WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.Top(count, where, db);
			}

			public ConfigurationApplicationCollection Top(int count, WhereDelegate<ConfigurationApplicationColumns> where, OrderBy<ConfigurationApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ConfigurationApplication.Count(where, db);
			}
	}

	static ConfigurationApplicationQueryContext _configurationApplications;
	static object _configurationApplicationsLock = new object();
	public static ConfigurationApplicationQueryContext ConfigurationApplications
	{
		get
		{
			return _configurationApplicationsLock.DoubleCheckLock<ConfigurationApplicationQueryContext>(ref _configurationApplications, () => new ConfigurationApplicationQueryContext());
		}
	}
	public class MachineApplicationQueryContext
	{
			public MachineApplicationCollection Where(WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.Where(where, db);
			}
		   
			public MachineApplicationCollection Where(WhereDelegate<MachineApplicationColumns> where, OrderBy<MachineApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.Where(where, orderBy, db);
			}

			public MachineApplication OneWhere(WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.OneWhere(where, db);
			}

			public static MachineApplication GetOneWhere(WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.GetOneWhere(where, db);
			}
		
			public MachineApplication FirstOneWhere(WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.FirstOneWhere(where, db);
			}

			public MachineApplicationCollection Top(int count, WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.Top(count, where, db);
			}

			public MachineApplicationCollection Top(int count, WhereDelegate<MachineApplicationColumns> where, OrderBy<MachineApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MachineApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.MachineApplication.Count(where, db);
			}
	}

	static MachineApplicationQueryContext _machineApplications;
	static object _machineApplicationsLock = new object();
	public static MachineApplicationQueryContext MachineApplications
	{
		get
		{
			return _machineApplicationsLock.DoubleCheckLock<MachineApplicationQueryContext>(ref _machineApplications, () => new MachineApplicationQueryContext());
		}
	}
	public class OrganizationUserQueryContext
	{
			public OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.Where(where, db);
			}
		   
			public OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.Where(where, orderBy, db);
			}

			public OrganizationUser OneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.OneWhere(where, db);
			}

			public static OrganizationUser GetOneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.GetOneWhere(where, db);
			}
		
			public OrganizationUser FirstOneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.FirstOneWhere(where, db);
			}

			public OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.Top(count, where, db);
			}

			public OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.OrganizationUser.Count(where, db);
			}
	}

	static OrganizationUserQueryContext _organizationUsers;
	static object _organizationUsersLock = new object();
	public static OrganizationUserQueryContext OrganizationUsers
	{
		get
		{
			return _organizationUsersLock.DoubleCheckLock<OrganizationUserQueryContext>(ref _organizationUsers, () => new OrganizationUserQueryContext());
		}
	}    }
}																								
