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
using Bam.Net.CoreServices.ApplicationRegistration.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository
{
	[Serializable]
	public class ApplicationRegistrationRepository: DaoRepository
	{
		public ApplicationRegistrationRepository()
		{
			SchemaName = "ApplicationRegistration";
			BaseNamespace = "Bam.Net.CoreServices.ApplicationRegistration.Data";			
﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>();﻿			
			AddType<Bam.Net.CoreServices.ApplicationRegistration.Data.User>();
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex GetOneActiveApiKeyIndexWhere(WhereDelegate<ActiveApiKeyIndexColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex OneActiveApiKeyIndexWhere(WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndexColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex> ActiveApiKeyIndexsWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex> TopActiveApiKeyIndexsWhere(int count, WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ActiveApiKeyIndexs
		/// </summary>
		public long CountActiveApiKeyIndexs()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.Count(where, Database);
        }
        
        public async Task BatchQueryActiveApiKeyIndexs(int batchSize, WhereDelegate<ActiveApiKeyIndexColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>(batch));
            }, Database);
        }
		
        public async Task BatchAllActiveApiKeyIndexs(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ActiveApiKeyIndex.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ActiveApiKeyIndex>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain GetOneHostDomainWhere(WhereDelegate<HostDomainColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain OneHostDomainWhere(WhereDelegate<HostDomainColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomainColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain> HostDomainsWhere(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain> TopHostDomainsWhere(int count, WhereDelegate<HostDomainColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of HostDomains
		/// </summary>
		public long CountHostDomains()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.Count(where, Database);
        }
        
        public async Task BatchQueryHostDomains(int batchSize, WhereDelegate<HostDomainColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>(batch));
            }, Database);
        }
		
        public async Task BatchAllHostDomains(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting GetOneUserSettingWhere(WhereDelegate<UserSettingColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting OneUserSettingWhere(WhereDelegate<UserSettingColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.UserSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.UserSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting> UserSettingsWhere(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting> TopUserSettingsWhere(int count, WhereDelegate<UserSettingColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of UserSettings
		/// </summary>
		public long CountUserSettings()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.Count(where, Database);
        }
        
        public async Task BatchQueryUserSettings(int batchSize, WhereDelegate<UserSettingColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>(batch));
            }, Database);
        }
		
        public async Task BatchAllUserSettings(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.UserSetting.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.UserSetting>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Client GetOneClientWhere(WhereDelegate<ClientColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Client)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Client OneClientWhere(WhereDelegate<ClientColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Client)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ClientColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ClientColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Client> ClientsWhere(WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Client> TopClientsWhere(int count, WhereDelegate<ClientColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Clients
		/// </summary>
		public long CountClients()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.Count(where, Database);
        }
        
        public async Task BatchQueryClients(int batchSize, WhereDelegate<ClientColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>(batch));
            }, Database);
        }
		
        public async Task BatchAllClients(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Client.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Client>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration GetOneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration OneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration> ConfigurationsWhere(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration> TopConfigurationsWhere(int count, WhereDelegate<ConfigurationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Configurations
		/// </summary>
		public long CountConfigurations()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurations(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>(batch));
            }, Database);
        }
		
        public async Task BatchAllConfigurations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Configuration.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress GetOneHostAddressWhere(WhereDelegate<HostAddressColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress OneHostAddressWhere(WhereDelegate<HostAddressColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddressColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddressColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress> HostAddressesWhere(WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress> TopHostAddressesWhere(int count, WhereDelegate<HostAddressColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of HostAddresses
		/// </summary>
		public long CountHostAddresses()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.Count(where, Database);
        }
        
        public async Task BatchQueryHostAddresses(int batchSize, WhereDelegate<HostAddressColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>(batch));
            }, Database);
        }
		
        public async Task BatchAllHostAddresses(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Nic GetOneNicWhere(WhereDelegate<NicColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Nic)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Nic OneNicWhere(WhereDelegate<NicColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Nic)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.NicColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic> NicsWhere(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic> TopNicsWhere(int count, WhereDelegate<NicColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Nics
		/// </summary>
		public long CountNics()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.Count(where, Database);
        }
        
        public async Task BatchQueryNics(int batchSize, WhereDelegate<NicColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>(batch));
            }, Database);
        }
		
        public async Task BatchAllNics(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting GetOneConfigurationSettingWhere(WhereDelegate<ConfigurationSettingColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting OneConfigurationSettingWhere(WhereDelegate<ConfigurationSettingColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSettingColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting> ConfigurationSettingsWhere(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting> TopConfigurationSettingsWhere(int count, WhereDelegate<ConfigurationSettingColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ConfigurationSettings
		/// </summary>
		public long CountConfigurationSettings()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurationSettings(int batchSize, WhereDelegate<ConfigurationSettingColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>(batch));
            }, Database);
        }
		
        public async Task BatchAllConfigurationSettings(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ConfigurationSetting.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ConfigurationSetting>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey GetOneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey OneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKeyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey> ApiKeysWhere(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey> TopApiKeysWhere(int count, WhereDelegate<ApiKeyColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ApiKeys
		/// </summary>
		public long CountApiKeys()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.Count(where, Database);
        }
        
        public async Task BatchQueryApiKeys(int batchSize, WhereDelegate<ApiKeyColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>(batch));
            }, Database);
        }
		
        public async Task BatchAllApiKeys(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApiKey.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ApiKey>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Application GetOneApplicationWhere(WhereDelegate<ApplicationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Application)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Application OneApplicationWhere(WhereDelegate<ApplicationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Application)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ApplicationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> ApplicationsWhere(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> TopApplicationsWhere(int count, WhereDelegate<ApplicationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Applications
		/// </summary>
		public long CountApplications()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.Count(where, Database);
        }
        
        public async Task BatchQueryApplications(int batchSize, WhereDelegate<ApplicationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>(batch));
            }, Database);
        }
		
        public async Task BatchAllApplications(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection GetOneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection OneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnectionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection> ClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection> TopClientServerConnectionsWhere(int count, WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ClientServerConnections
		/// </summary>
		public long CountClientServerConnections()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.Count(where, Database);
        }
        
        public async Task BatchQueryClientServerConnections(int batchSize, WhereDelegate<ClientServerConnectionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>(batch));
            }, Database);
        }
		
        public async Task BatchAllClientServerConnections(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ClientServerConnection.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ClientServerConnection>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Machine GetOneMachineWhere(WhereDelegate<MachineColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Machine)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Machine OneMachineWhere(WhereDelegate<MachineColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Machine)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.MachineColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine> MachinesWhere(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine> TopMachinesWhere(int count, WhereDelegate<MachineColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Machines
		/// </summary>
		public long CountMachines()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.Count(where, Database);
        }
        
        public async Task BatchQueryMachines(int batchSize, WhereDelegate<MachineColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>(batch));
            }, Database);
        }
		
        public async Task BatchAllMachines(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Organization GetOneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Organization)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Organization OneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Organization)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.OrganizationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization> OrganizationsWhere(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization> TopOrganizationsWhere(int count, WhereDelegate<OrganizationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Organizations
		/// </summary>
		public long CountOrganizations()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.Count(where, Database);
        }
        
        public async Task BatchQueryOrganizations(int batchSize, WhereDelegate<OrganizationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>(batch));
            }, Database);
        }
		
        public async Task BatchAllOrganizations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Organization>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor GetOneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor OneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor> ProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor> TopProcessDescriptorsWhere(int count, WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ProcessDescriptors
		/// </summary>
		public long CountProcessDescriptors()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryProcessDescriptors(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllProcessDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ProcessDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription GetOneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription OneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.SubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription> SubscriptionsWhere(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription> TopSubscriptionsWhere(int count, WhereDelegate<SubscriptionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Subscriptions
		/// </summary>
		public long CountSubscriptions()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.Count(where, Database);
        }
        
        public async Task BatchQuerySubscriptions(int batchSize, WhereDelegate<SubscriptionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>(batch));
            }, Database);
        }
		
        public async Task BatchAllSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Subscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.Subscription>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ApplicationRegistration.Data.User GetOneUserWhere(WhereDelegate<UserColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.User>();
			return (Bam.Net.CoreServices.ApplicationRegistration.Data.User)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.ApplicationRegistration.Data.User OneUserWhere(WhereDelegate<UserColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ApplicationRegistration.Data.User>();
            return (Bam.Net.CoreServices.ApplicationRegistration.Data.User)Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ApplicationRegistration.Data.UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ApplicationRegistration.Data.UserColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.User> UsersWhere(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.User>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.User> TopUsersWhere(int count, WhereDelegate<UserColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.User>(Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Users
		/// </summary>
		public long CountUsers()
        {
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Count(Database);
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
            return Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.Count(where, Database);
        }
        
        public async Task BatchQueryUsers(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.User>(batch));
            }, Database);
        }
		
        public async Task BatchAllUsers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ApplicationRegistration.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ApplicationRegistration.Data.User>(batch));
            }, Database);
        }
	}
}																								
