/*
This file was generated and should not be modified directly
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.ApplicationRegistration;

namespace Bam.Net.CoreServices.ApplicationRegistration.Dao.Repository
{
	[Serializable]
	public class ApplicationRegistrationRepository: DaoRepository
	{
		public ApplicationRegistrationRepository()
		{
			SchemaName = "ApplicationRegistration";
			BaseNamespace = "Bam.Net.CoreServices.ApplicationRegistration";			
﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Client>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Configuration>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Nic>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Application>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Machine>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Organization>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Subscription>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.User>();
			DaoAssembly = typeof(ApplicationRegistrationRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(ApplicationRegistrationRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex GetOneActiveApiKeyIndexWhere(WhereDelegate<ActiveApiKeyIndexColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>();
			return (Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex)Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ActiveApiKeyIndex instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex OneActiveApiKeyIndexWhere(WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>();
            return (Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex)Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndexColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex> ActiveApiKeyIndexsWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex> TopActiveApiKeyIndexsWhere(int count, WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ActiveApiKeyIndexs
		/// </summary>
		public long CountActiveApiKeyIndexs()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
        public long CountActiveApiKeyIndexsWhere(WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.Count(where, Database);
        }
        
        public async Task BatchQueryActiveApiKeyIndexs(int batchSize, WhereDelegate<ActiveApiKeyIndexColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>(batch));
            }, Database);
        }
		
        public async Task BatchAllActiveApiKeyIndexs(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ActiveApiKeyIndex.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ActiveApiKeyIndex>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.HostDomain GetOneHostDomainWhere(WhereDelegate<HostDomainColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>();
			return (Bam.Net.CoreServices.ApplicationRegistration.HostDomain)Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HostDomain instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.HostDomain OneHostDomainWhere(WhereDelegate<HostDomainColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>();
            return (Bam.Net.CoreServices.ApplicationRegistration.HostDomain)Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.HostDomainColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostDomain> HostDomainsWhere(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>(Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostDomain> TopHostDomainsWhere(int count, WhereDelegate<HostDomainColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>(Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of HostDomains
		/// </summary>
		public long CountHostDomains()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
        public long CountHostDomainsWhere(WhereDelegate<HostDomainColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.Count(where, Database);
        }
        
        public async Task BatchQueryHostDomains(int batchSize, WhereDelegate<HostDomainColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>(batch));
            }, Database);
        }
		
        public async Task BatchAllHostDomains(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.UserSetting GetOneUserSettingWhere(WhereDelegate<UserSettingColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>();
			return (Bam.Net.CoreServices.ApplicationRegistration.UserSetting)Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UserSetting instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserSettingColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.UserSetting OneUserSettingWhere(WhereDelegate<UserSettingColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>();
            return (Bam.Net.CoreServices.ApplicationRegistration.UserSetting)Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.UserSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.UserSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.UserSetting> UserSettingsWhere(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>(Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a UserSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.UserSetting> TopUserSettingsWhere(int count, WhereDelegate<UserSettingColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>(Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of UserSettings
		/// </summary>
		public long CountUserSettings()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserSettingColumns and other values
		/// </param>
        public long CountUserSettingsWhere(WhereDelegate<UserSettingColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.Count(where, Database);
        }
        
        public async Task BatchQueryUserSettings(int batchSize, WhereDelegate<UserSettingColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>(batch));
            }, Database);
        }
		
        public async Task BatchAllUserSettings(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.UserSetting.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.UserSetting>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Client GetOneClientWhere(WhereDelegate<ClientColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Client>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Client)Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Client instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Client OneClientWhere(WhereDelegate<ClientColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Client>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Client)Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ClientColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ClientColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Client> ClientsWhere(WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Client>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ClientColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Client> TopClientsWhere(int count, WhereDelegate<ClientColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Client>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Clients
		/// </summary>
		public long CountClients()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientColumns and other values
		/// </param>
        public long CountClientsWhere(WhereDelegate<ClientColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.Count(where, Database);
        }
        
        public async Task BatchQueryClients(int batchSize, WhereDelegate<ClientColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Client>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Client>(batch));
            }, Database);
        }
		
        public async Task BatchAllClients(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Client>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Client.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Client>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Configuration GetOneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Configuration>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Configuration)Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Configuration instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Configuration OneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Configuration>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Configuration)Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ConfigurationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Configuration> ConfigurationsWhere(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Configuration>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Configuration> TopConfigurationsWhere(int count, WhereDelegate<ConfigurationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Configuration>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Configurations
		/// </summary>
		public long CountConfigurations()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
        public long CountConfigurationsWhere(WhereDelegate<ConfigurationColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurations(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Configuration>(batch));
            }, Database);
        }
		
        public async Task BatchAllConfigurations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Configuration.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Configuration>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.HostAddress GetOneHostAddressWhere(WhereDelegate<HostAddressColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>();
			return (Bam.Net.CoreServices.ApplicationRegistration.HostAddress)Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HostAddress instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostAddressColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostAddressColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.HostAddress OneHostAddressWhere(WhereDelegate<HostAddressColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>();
            return (Bam.Net.CoreServices.ApplicationRegistration.HostAddress)Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.HostAddressColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.HostAddressColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostAddress> HostAddressesWhere(WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>(Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a HostAddressColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostAddressColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostAddress> TopHostAddressesWhere(int count, WhereDelegate<HostAddressColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>(Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of HostAddresses
		/// </summary>
		public long CountHostAddresses()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostAddressColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostAddressColumns and other values
		/// </param>
        public long CountHostAddressesWhere(WhereDelegate<HostAddressColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.Count(where, Database);
        }
        
        public async Task BatchQueryHostAddresses(int batchSize, WhereDelegate<HostAddressColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>(batch));
            }, Database);
        }
		
        public async Task BatchAllHostAddresses(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.HostAddress.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.HostAddress>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Nic GetOneNicWhere(WhereDelegate<NicColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Nic>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Nic)Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Nic instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Nic OneNicWhere(WhereDelegate<NicColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Nic>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Nic)Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.NicColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Nic> NicsWhere(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Nic>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Nic> TopNicsWhere(int count, WhereDelegate<NicColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Nic>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Nics
		/// </summary>
		public long CountNics()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
        public long CountNicsWhere(WhereDelegate<NicColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.Count(where, Database);
        }
        
        public async Task BatchQueryNics(int batchSize, WhereDelegate<NicColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Nic>(batch));
            }, Database);
        }
		
        public async Task BatchAllNics(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Nic.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Nic>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting GetOneConfigurationSettingWhere(WhereDelegate<ConfigurationSettingColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>();
			return (Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting)Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ConfigurationSetting instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting OneConfigurationSettingWhere(WhereDelegate<ConfigurationSettingColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>();
            return (Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting)Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting> ConfigurationSettingsWhere(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting> TopConfigurationSettingsWhere(int count, WhereDelegate<ConfigurationSettingColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ConfigurationSettings
		/// </summary>
		public long CountConfigurationSettings()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
        public long CountConfigurationSettingsWhere(WhereDelegate<ConfigurationSettingColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurationSettings(int batchSize, WhereDelegate<ConfigurationSettingColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>(batch));
            }, Database);
        }
		
        public async Task BatchAllConfigurationSettings(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ConfigurationSetting.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.ApiKey GetOneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>();
			return (Bam.Net.CoreServices.ApplicationRegistration.ApiKey)Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ApiKey instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.ApiKey OneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>();
            return (Bam.Net.CoreServices.ApplicationRegistration.ApiKey)Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ApiKeyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ApiKey> ApiKeysWhere(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ApiKey> TopApiKeysWhere(int count, WhereDelegate<ApiKeyColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ApiKeys
		/// </summary>
		public long CountApiKeys()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
        public long CountApiKeysWhere(WhereDelegate<ApiKeyColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.Count(where, Database);
        }
        
        public async Task BatchQueryApiKeys(int batchSize, WhereDelegate<ApiKeyColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>(batch));
            }, Database);
        }
		
        public async Task BatchAllApiKeys(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ApiKey.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ApiKey>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Application GetOneApplicationWhere(WhereDelegate<ApplicationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Application>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Application)Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Application instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Application OneApplicationWhere(WhereDelegate<ApplicationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Application>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Application)Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ApplicationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Application> ApplicationsWhere(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Application>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Application> TopApplicationsWhere(int count, WhereDelegate<ApplicationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Application>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Applications
		/// </summary>
		public long CountApplications()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationColumns and other values
		/// </param>
        public long CountApplicationsWhere(WhereDelegate<ApplicationColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.Count(where, Database);
        }
        
        public async Task BatchQueryApplications(int batchSize, WhereDelegate<ApplicationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Application>(batch));
            }, Database);
        }
		
        public async Task BatchAllApplications(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Application.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Application>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection GetOneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>();
			return (Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection)Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ClientServerConnection instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection OneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>();
            return (Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection)Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnectionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection> ClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection> TopClientServerConnectionsWhere(int count, WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ClientServerConnections
		/// </summary>
		public long CountClientServerConnections()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
        public long CountClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.Count(where, Database);
        }
        
        public async Task BatchQueryClientServerConnections(int batchSize, WhereDelegate<ClientServerConnectionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>(batch));
            }, Database);
        }
		
        public async Task BatchAllClientServerConnections(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ClientServerConnection.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ClientServerConnection>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Machine GetOneMachineWhere(WhereDelegate<MachineColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Machine>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Machine)Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Machine instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Machine OneMachineWhere(WhereDelegate<MachineColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Machine>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Machine)Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.MachineColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Machine> MachinesWhere(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Machine>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Machine> TopMachinesWhere(int count, WhereDelegate<MachineColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Machine>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Machines
		/// </summary>
		public long CountMachines()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineColumns and other values
		/// </param>
        public long CountMachinesWhere(WhereDelegate<MachineColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.Count(where, Database);
        }
        
        public async Task BatchQueryMachines(int batchSize, WhereDelegate<MachineColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Machine>(batch));
            }, Database);
        }
		
        public async Task BatchAllMachines(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Machine>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Organization GetOneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Organization>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Organization)Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Organization instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Organization OneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Organization>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Organization)Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.OrganizationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Organization> OrganizationsWhere(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Organization>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Organization> TopOrganizationsWhere(int count, WhereDelegate<OrganizationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Organization>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Organizations
		/// </summary>
		public long CountOrganizations()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
        public long CountOrganizationsWhere(WhereDelegate<OrganizationColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.Count(where, Database);
        }
        
        public async Task BatchQueryOrganizations(int batchSize, WhereDelegate<OrganizationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Organization>(batch));
            }, Database);
        }
		
        public async Task BatchAllOrganizations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Organization>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor GetOneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>();
			return (Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor)Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ProcessDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor OneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>();
            return (Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor)Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor> ProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor> TopProcessDescriptorsWhere(int count, WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>(Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ProcessDescriptors
		/// </summary>
		public long CountProcessDescriptors()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
        public long CountProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryProcessDescriptors(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllProcessDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.ProcessDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Subscription GetOneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Subscription>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Subscription)Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Subscription instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubscriptionColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.Subscription OneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Subscription>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Subscription)Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.SubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Subscription> SubscriptionsWhere(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Subscription>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Subscription> TopSubscriptionsWhere(int count, WhereDelegate<SubscriptionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Subscription>(Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Subscriptions
		/// </summary>
		public long CountSubscriptions()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubscriptionColumns and other values
		/// </param>
        public long CountSubscriptionsWhere(WhereDelegate<SubscriptionColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.Count(where, Database);
        }
        
        public async Task BatchQuerySubscriptions(int batchSize, WhereDelegate<SubscriptionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Subscription>(batch));
            }, Database);
        }
		
        public async Task BatchAllSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.Subscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Subscription>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.User GetOneUserWhere(WhereDelegate<UserColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.User>();
			return (Bam.Net.CoreServices.ApplicationRegistration.User)Bam.Net.CoreServices.ApplicationRegistration.Dao.User.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single User instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ApplicationRegistration.User OneUserWhere(WhereDelegate<UserColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.User>();
            return (Bam.Net.CoreServices.ApplicationRegistration.User)Bam.Net.CoreServices.ApplicationRegistration.Dao.User.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.UserColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.User> UsersWhere(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.User>(Bam.Net.CoreServices.ApplicationRegistration.Dao.User.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.User> TopUsersWhere(int count, WhereDelegate<UserColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.User>(Bam.Net.CoreServices.ApplicationRegistration.Dao.User.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Users
		/// </summary>
		public long CountUsers()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.User.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
        public long CountUsersWhere(WhereDelegate<UserColumns> where)
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Dao.User.Count(where, Database);
        }
        
        public async Task BatchQueryUsers(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.User.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.User>(batch));
            }, Database);
        }
		
        public async Task BatchAllUsers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Dao.User.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.User>(batch));
            }, Database);
        }
	}
}																								
