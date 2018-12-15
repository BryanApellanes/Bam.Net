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

namespace Bam.Net.ServiceProxy.Secure
{
	// schema = SecureServiceProxy 
    public static class SecureServiceProxyContext
    {
		public static string ConnectionName
		{
			get
			{
				return "SecureServiceProxy";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class ApplicationQueryContext
	{
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.Where(where, db);
			}
		   
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.Where(where, orderBy, db);
			}

			public Application OneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.OneWhere(where, db);
			}

			public static Application GetOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.GetOneWhere(where, db);
			}
		
			public Application FirstOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.FirstOneWhere(where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.Top(count, where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Application.Count(where, db);
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
	public class ConfigurationQueryContext
	{
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.Where(where, db);
			}
		   
			public ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.Where(where, orderBy, db);
			}

			public Configuration OneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.OneWhere(where, db);
			}

			public static Configuration GetOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.GetOneWhere(where, db);
			}
		
			public Configuration FirstOneWhere(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.FirstOneWhere(where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.Top(count, where, db);
			}

			public ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigurationColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.Configuration.Count(where, db);
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
	public class ConfigSettingQueryContext
	{
			public ConfigSettingCollection Where(WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.Where(where, db);
			}
		   
			public ConfigSettingCollection Where(WhereDelegate<ConfigSettingColumns> where, OrderBy<ConfigSettingColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.Where(where, orderBy, db);
			}

			public ConfigSetting OneWhere(WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.OneWhere(where, db);
			}

			public static ConfigSetting GetOneWhere(WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.GetOneWhere(where, db);
			}
		
			public ConfigSetting FirstOneWhere(WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.FirstOneWhere(where, db);
			}

			public ConfigSettingCollection Top(int count, WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.Top(count, where, db);
			}

			public ConfigSettingCollection Top(int count, WhereDelegate<ConfigSettingColumns> where, OrderBy<ConfigSettingColumns> orderBy, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ConfigSettingColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ConfigSetting.Count(where, db);
			}
	}

	static ConfigSettingQueryContext _configSettings;
	static object _configSettingsLock = new object();
	public static ConfigSettingQueryContext ConfigSettings
	{
		get
		{
			return _configSettingsLock.DoubleCheckLock<ConfigSettingQueryContext>(ref _configSettings, () => new ConfigSettingQueryContext());
		}
	}
	public class ApiKeyQueryContext
	{
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.Where(where, db);
			}
		   
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.Where(where, orderBy, db);
			}

			public ApiKey OneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.OneWhere(where, db);
			}

			public static ApiKey GetOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.GetOneWhere(where, db);
			}
		
			public ApiKey FirstOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.FirstOneWhere(where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.Top(count, where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.ApiKey.Count(where, db);
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
	public class SecureSessionQueryContext
	{
			public SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.Where(where, db);
			}
		   
			public SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.Where(where, orderBy, db);
			}

			public SecureSession OneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.OneWhere(where, db);
			}

			public static SecureSession GetOneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.GetOneWhere(where, db);
			}
		
			public SecureSession FirstOneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.FirstOneWhere(where, db);
			}

			public SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.Top(count, where, db);
			}

			public SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return Bam.Net.ServiceProxy.Secure.SecureSession.Count(where, db);
			}
	}

	static SecureSessionQueryContext _secureSessions;
	static object _secureSessionsLock = new object();
	public static SecureSessionQueryContext SecureSessions
	{
		get
		{
			return _secureSessionsLock.DoubleCheckLock<SecureSessionQueryContext>(ref _secureSessions, () => new SecureSessionQueryContext());
		}
	}    }
}																								
