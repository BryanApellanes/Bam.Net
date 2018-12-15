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

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
	// schema = ApplicationRegistration 
    public static class ApplicationRegistrationContext
    {
		public static string ConnectionName
		{
			get
			{
				return "ApplicationRegistration";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class ActiveApiKeyIndexQueryContext
	{
			public ActiveApiKeyIndexCollection Where(WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Where(where, db);
			}
		   
			public ActiveApiKeyIndexCollection Where(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Where(where, orderBy, db);
			}

			public ActiveApiKeyIndex OneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.OneWhere(where, db);
			}

			public static ActiveApiKeyIndex GetOneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.GetOneWhere(where, db);
			}
		
			public ActiveApiKeyIndex FirstOneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.FirstOneWhere(where, db);
			}

			public ActiveApiKeyIndexCollection Top(int count, WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Top(count, where, db);
			}

			public ActiveApiKeyIndexCollection Top(int count, WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ActiveApiKeyIndexColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Count(where, db);
			}
	}

	static ActiveApiKeyIndexQueryContext _activeApiKeyIndexs;
	static object _activeApiKeyIndexsLock = new object();
	public static ActiveApiKeyIndexQueryContext ActiveApiKeyIndexs
	{
		get
		{
			return _activeApiKeyIndexsLock.DoubleCheckLock<ActiveApiKeyIndexQueryContext>(ref _activeApiKeyIndexs, () => new ActiveApiKeyIndexQueryContext());
		}
	}
	public class HostDomainQueryContext
	{
			public HostDomainCollection Where(WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Where(where, db);
			}
		   
			public HostDomainCollection Where(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Where(where, orderBy, db);
			}

			public HostDomain OneWhere(WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.OneWhere(where, db);
			}

			public static HostDomain GetOneWhere(WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.GetOneWhere(where, db);
			}
		
			public HostDomain FirstOneWhere(WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.FirstOneWhere(where, db);
			}

			public HostDomainCollection Top(int count, WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Top(count, where, db);
			}

			public HostDomainCollection Top(int count, WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HostDomainColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Count(where, db);
			}
	}

	static HostDomainQueryContext _hostDomains;
	static object _hostDomainsLock = new object();
	public static HostDomainQueryContext HostDomains
	{
		get
		{
			return _hostDomainsLock.DoubleCheckLock<HostDomainQueryContext>(ref _hostDomains, () => new HostDomainQueryContext());
		}
	}
	public class ApplicationQueryContext
	{
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Where(where, db);
			}
		   
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Where(where, orderBy, db);
			}

			public Application OneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.OneWhere(where, db);
			}

			public static Application GetOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.GetOneWhere(where, db);
			}
		
			public Application FirstOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.FirstOneWhere(where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Top(count, where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Count(where, db);
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
	public class ApiKeyQueryContext
	{
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Where(where, db);
			}
		   
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Where(where, orderBy, db);
			}

			public ApiKey OneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.OneWhere(where, db);
			}

			public static ApiKey GetOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.GetOneWhere(where, db);
			}
		
			public ApiKey FirstOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.FirstOneWhere(where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Top(count, where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Count(where, db);
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
	public class ProcessDescriptorQueryContext
	{
			public ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Where(where, db);
			}
		   
			public ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Where(where, orderBy, db);
			}

			public ProcessDescriptor OneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.OneWhere(where, db);
			}

			public static ProcessDescriptor GetOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.GetOneWhere(where, db);
			}
		
			public ProcessDescriptor FirstOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.FirstOneWhere(where, db);
			}

			public ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Top(count, where, db);
			}

			public ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ProcessDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Count(where, db);
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
	public class ConfigurationQueryContext
	{
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Where(where, db);
			}
		   
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Where(where, orderBy, db);
			}

			public Configuration OneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.OneWhere(where, db);
			}

			public static Configuration GetOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.GetOneWhere(where, db);
			}
		
			public Configuration FirstOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.FirstOneWhere(where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Top(count, where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Count(where, db);
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
	public class ClientQueryContext
	{
			public ClientCollection Where(WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Where(where, db);
			}
		   
			public ClientCollection Where(WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Where(where, orderBy, db);
			}

			public Client OneWhere(WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.OneWhere(where, db);
			}

			public static Client GetOneWhere(WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.GetOneWhere(where, db);
			}
		
			public Client FirstOneWhere(WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.FirstOneWhere(where, db);
			}

			public ClientCollection Top(int count, WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Top(count, where, db);
			}

			public ClientCollection Top(int count, WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ClientColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Count(where, db);
			}
	}

	static ClientQueryContext _clients;
	static object _clientsLock = new object();
	public static ClientQueryContext Clients
	{
		get
		{
			return _clientsLock.DoubleCheckLock<ClientQueryContext>(ref _clients, () => new ClientQueryContext());
		}
	}
	public class MachineQueryContext
	{
			public MachineCollection Where(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Where(where, db);
			}
		   
			public MachineCollection Where(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Where(where, orderBy, db);
			}

			public Machine OneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.OneWhere(where, db);
			}

			public static Machine GetOneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.GetOneWhere(where, db);
			}
		
			public Machine FirstOneWhere(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.FirstOneWhere(where, db);
			}

			public MachineCollection Top(int count, WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Top(count, where, db);
			}

			public MachineCollection Top(int count, WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Count(where, db);
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
	public class ConfigurationSettingQueryContext
	{
			public ConfigurationSettingCollection Where(WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Where(where, db);
			}
		   
			public ConfigurationSettingCollection Where(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Where(where, orderBy, db);
			}

			public ConfigurationSetting OneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.OneWhere(where, db);
			}

			public static ConfigurationSetting GetOneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.GetOneWhere(where, db);
			}
		
			public ConfigurationSetting FirstOneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.FirstOneWhere(where, db);
			}

			public ConfigurationSettingCollection Top(int count, WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Top(count, where, db);
			}

			public ConfigurationSettingCollection Top(int count, WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Count(where, db);
			}
	}

	static ConfigurationSettingQueryContext _configurationSettings;
	static object _configurationSettingsLock = new object();
	public static ConfigurationSettingQueryContext ConfigurationSettings
	{
		get
		{
			return _configurationSettingsLock.DoubleCheckLock<ConfigurationSettingQueryContext>(ref _configurationSettings, () => new ConfigurationSettingQueryContext());
		}
	}
	public class HostAddressQueryContext
	{
			public HostAddressCollection Where(WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Where(where, db);
			}
		   
			public HostAddressCollection Where(WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Where(where, orderBy, db);
			}

			public HostAddress OneWhere(WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.OneWhere(where, db);
			}

			public static HostAddress GetOneWhere(WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.GetOneWhere(where, db);
			}
		
			public HostAddress FirstOneWhere(WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.FirstOneWhere(where, db);
			}

			public HostAddressCollection Top(int count, WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Top(count, where, db);
			}

			public HostAddressCollection Top(int count, WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HostAddressColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Count(where, db);
			}
	}

	static HostAddressQueryContext _hostAddresses;
	static object _hostAddressesLock = new object();
	public static HostAddressQueryContext HostAddresses
	{
		get
		{
			return _hostAddressesLock.DoubleCheckLock<HostAddressQueryContext>(ref _hostAddresses, () => new HostAddressQueryContext());
		}
	}
	public class NicQueryContext
	{
			public NicCollection Where(WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Where(where, db);
			}
		   
			public NicCollection Where(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Where(where, orderBy, db);
			}

			public Nic OneWhere(WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.OneWhere(where, db);
			}

			public static Nic GetOneWhere(WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.GetOneWhere(where, db);
			}
		
			public Nic FirstOneWhere(WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.FirstOneWhere(where, db);
			}

			public NicCollection Top(int count, WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Top(count, where, db);
			}

			public NicCollection Top(int count, WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<NicColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Count(where, db);
			}
	}

	static NicQueryContext _nics;
	static object _nicsLock = new object();
	public static NicQueryContext Nics
	{
		get
		{
			return _nicsLock.DoubleCheckLock<NicQueryContext>(ref _nics, () => new NicQueryContext());
		}
	}
	public class UserSettingQueryContext
	{
			public UserSettingCollection Where(WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Where(where, db);
			}
		   
			public UserSettingCollection Where(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Where(where, orderBy, db);
			}

			public UserSetting OneWhere(WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.OneWhere(where, db);
			}

			public static UserSetting GetOneWhere(WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.GetOneWhere(where, db);
			}
		
			public UserSetting FirstOneWhere(WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.FirstOneWhere(where, db);
			}

			public UserSettingCollection Top(int count, WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Top(count, where, db);
			}

			public UserSettingCollection Top(int count, WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserSettingColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Count(where, db);
			}
	}

	static UserSettingQueryContext _userSettings;
	static object _userSettingsLock = new object();
	public static UserSettingQueryContext UserSettings
	{
		get
		{
			return _userSettingsLock.DoubleCheckLock<UserSettingQueryContext>(ref _userSettings, () => new UserSettingQueryContext());
		}
	}
	public class ClientServerConnectionQueryContext
	{
			public ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Where(where, db);
			}
		   
			public ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Where(where, orderBy, db);
			}

			public ClientServerConnection OneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.OneWhere(where, db);
			}

			public static ClientServerConnection GetOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.GetOneWhere(where, db);
			}
		
			public ClientServerConnection FirstOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.FirstOneWhere(where, db);
			}

			public ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Top(count, where, db);
			}

			public ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ClientServerConnectionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Count(where, db);
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
	public class OrganizationQueryContext
	{
			public OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Where(where, db);
			}
		   
			public OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Where(where, orderBy, db);
			}

			public Organization OneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.OneWhere(where, db);
			}

			public static Organization GetOneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.GetOneWhere(where, db);
			}
		
			public Organization FirstOneWhere(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.FirstOneWhere(where, db);
			}

			public OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Top(count, where, db);
			}

			public OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Count(where, db);
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
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Where(where, db);
			}
		   
			public UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Where(where, orderBy, db);
			}

			public User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.OneWhere(where, db);
			}

			public static User GetOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.GetOneWhere(where, db);
			}
		
			public User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.FirstOneWhere(where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Top(count, where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Count(where, db);
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
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Where(where, db);
			}
		   
			public SubscriptionCollection Where(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Where(where, orderBy, db);
			}

			public Subscription OneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.OneWhere(where, db);
			}

			public static Subscription GetOneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.GetOneWhere(where, db);
			}
		
			public Subscription FirstOneWhere(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.FirstOneWhere(where, db);
			}

			public SubscriptionCollection Top(int count, WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Top(count, where, db);
			}

			public SubscriptionCollection Top(int count, WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Count(where, db);
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
	public class HostDomainApplicationQueryContext
	{
			public HostDomainApplicationCollection Where(WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.Where(where, db);
			}
		   
			public HostDomainApplicationCollection Where(WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.Where(where, orderBy, db);
			}

			public HostDomainApplication OneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.OneWhere(where, db);
			}

			public static HostDomainApplication GetOneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.GetOneWhere(where, db);
			}
		
			public HostDomainApplication FirstOneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.FirstOneWhere(where, db);
			}

			public HostDomainApplicationCollection Top(int count, WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.Top(count, where, db);
			}

			public HostDomainApplicationCollection Top(int count, WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HostDomainApplicationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication.Count(where, db);
			}
	}

	static HostDomainApplicationQueryContext _hostDomainApplications;
	static object _hostDomainApplicationsLock = new object();
	public static HostDomainApplicationQueryContext HostDomainApplications
	{
		get
		{
			return _hostDomainApplicationsLock.DoubleCheckLock<HostDomainApplicationQueryContext>(ref _hostDomainApplications, () => new HostDomainApplicationQueryContext());
		}
	}
	public class ApplicationMachineQueryContext
	{
			public ApplicationMachineCollection Where(WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.Where(where, db);
			}
		   
			public ApplicationMachineCollection Where(WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.Where(where, orderBy, db);
			}

			public ApplicationMachine OneWhere(WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.OneWhere(where, db);
			}

			public static ApplicationMachine GetOneWhere(WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.GetOneWhere(where, db);
			}
		
			public ApplicationMachine FirstOneWhere(WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.FirstOneWhere(where, db);
			}

			public ApplicationMachineCollection Top(int count, WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.Top(count, where, db);
			}

			public ApplicationMachineCollection Top(int count, WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationMachineColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine.Count(where, db);
			}
	}

	static ApplicationMachineQueryContext _applicationMachines;
	static object _applicationMachinesLock = new object();
	public static ApplicationMachineQueryContext ApplicationMachines
	{
		get
		{
			return _applicationMachinesLock.DoubleCheckLock<ApplicationMachineQueryContext>(ref _applicationMachines, () => new ApplicationMachineQueryContext());
		}
	}
	public class OrganizationUserQueryContext
	{
			public OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.Where(where, db);
			}
		   
			public OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.Where(where, orderBy, db);
			}

			public OrganizationUser OneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.OneWhere(where, db);
			}

			public static OrganizationUser GetOneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.GetOneWhere(where, db);
			}
		
			public OrganizationUser FirstOneWhere(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.FirstOneWhere(where, db);
			}

			public OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.Top(count, where, db);
			}

			public OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OrganizationUserColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.OrganizationUser.Count(where, db);
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
