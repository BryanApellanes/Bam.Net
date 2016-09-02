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
	// schema = ApplicationRegistry 
    public static class ApplicationRegistryContext
    {
		public static string ConnectionName
		{
			get
			{
				return "ApplicationRegistry";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
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
	public class ApplicationInstanceQueryContext
	{
			public ApplicationInstanceCollection Where(WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Where(where, db);
			}
		   
			public ApplicationInstanceCollection Where(WhereDelegate<ApplicationInstanceColumns> where, OrderBy<ApplicationInstanceColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Where(where, orderBy, db);
			}

			public ApplicationInstance OneWhere(WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.OneWhere(where, db);
			}

			public static ApplicationInstance GetOneWhere(WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.GetOneWhere(where, db);
			}
		
			public ApplicationInstance FirstOneWhere(WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.FirstOneWhere(where, db);
			}

			public ApplicationInstanceCollection Top(int count, WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Top(count, where, db);
			}

			public ApplicationInstanceCollection Top(int count, WhereDelegate<ApplicationInstanceColumns> where, OrderBy<ApplicationInstanceColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationInstanceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Count(where, db);
			}
	}

	static ApplicationInstanceQueryContext _applicationInstances;
	static object _applicationInstancesLock = new object();
	public static ApplicationInstanceQueryContext ApplicationInstances
	{
		get
		{
			return _applicationInstancesLock.DoubleCheckLock<ApplicationInstanceQueryContext>(ref _applicationInstances, () => new ApplicationInstanceQueryContext());
		}
	}
	public class HostNameQueryContext
	{
			public HostNameCollection Where(WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.Where(where, db);
			}
		   
			public HostNameCollection Where(WhereDelegate<HostNameColumns> where, OrderBy<HostNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.Where(where, orderBy, db);
			}

			public HostName OneWhere(WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.OneWhere(where, db);
			}

			public static HostName GetOneWhere(WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.GetOneWhere(where, db);
			}
		
			public HostName FirstOneWhere(WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.FirstOneWhere(where, db);
			}

			public HostNameCollection Top(int count, WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.Top(count, where, db);
			}

			public HostNameCollection Top(int count, WhereDelegate<HostNameColumns> where, OrderBy<HostNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HostNameColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.HostName.Count(where, db);
			}
	}

	static HostNameQueryContext _hostNames;
	static object _hostNamesLock = new object();
	public static HostNameQueryContext HostNames
	{
		get
		{
			return _hostNamesLock.DoubleCheckLock<HostNameQueryContext>(ref _hostNames, () => new HostNameQueryContext());
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
	public class UserOrganizationQueryContext
	{
			public UserOrganizationCollection Where(WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.Where(where, db);
			}
		   
			public UserOrganizationCollection Where(WhereDelegate<UserOrganizationColumns> where, OrderBy<UserOrganizationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.Where(where, orderBy, db);
			}

			public UserOrganization OneWhere(WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.OneWhere(where, db);
			}

			public static UserOrganization GetOneWhere(WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.GetOneWhere(where, db);
			}
		
			public UserOrganization FirstOneWhere(WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.FirstOneWhere(where, db);
			}

			public UserOrganizationCollection Top(int count, WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.Top(count, where, db);
			}

			public UserOrganizationCollection Top(int count, WhereDelegate<UserOrganizationColumns> where, OrderBy<UserOrganizationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserOrganizationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.Data.Daos.UserOrganization.Count(where, db);
			}
	}

	static UserOrganizationQueryContext _userOrganizations;
	static object _userOrganizationsLock = new object();
	public static UserOrganizationQueryContext UserOrganizations
	{
		get
		{
			return _userOrganizationsLock.DoubleCheckLock<UserOrganizationQueryContext>(ref _userOrganizations, () => new UserOrganizationQueryContext());
		}
	}    }
}																								
