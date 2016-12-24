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
using Bam.Net.CoreServices.Data;

namespace Bam.Net.CoreServices.Data.Daos.Repository
{
	[Serializable]
	public class CoreRegistryRepository: DaoRepository
	{
		public CoreRegistryRepository()
		{
			SchemaName = "CoreRegistry";
			BaseNamespace = "Bam.Net.CoreServices.Data";			
﻿			
			AddType<Bam.Net.CoreServices.Data.Nic>();﻿			
			AddType<Bam.Net.CoreServices.Data.Configuration>();﻿			
			AddType<Bam.Net.CoreServices.Data.ApiKey>();﻿			
			AddType<Bam.Net.CoreServices.Data.Application>();﻿			
			AddType<Bam.Net.CoreServices.Data.ClientServerConnection>();﻿			
			AddType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.Data.Machine>();﻿			
			AddType<Bam.Net.CoreServices.Data.Organization>();﻿			
			AddType<Bam.Net.CoreServices.Data.ProcessDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.Data.Subscription>();﻿			
			AddType<Bam.Net.CoreServices.Data.User>();
			DaoAssembly = typeof(CoreRegistryRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(CoreRegistryRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Nic GetOneNicWhere(WhereDelegate<NicColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Nic>();
			return (Bam.Net.CoreServices.Data.Nic)Bam.Net.CoreServices.Data.Daos.Nic.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Nic OneNicWhere(WhereDelegate<NicColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Nic>();
            return (Bam.Net.CoreServices.Data.Nic)Bam.Net.CoreServices.Data.Daos.Nic.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.NicColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Nic> NicsWhere(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Nic>(Bam.Net.CoreServices.Data.Daos.Nic.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Nic> TopNicsWhere(int count, WhereDelegate<NicColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Nic>(Bam.Net.CoreServices.Data.Daos.Nic.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Nics
		/// </summary>
		public long CountNics()
        {
            return Bam.Net.CoreServices.Data.Daos.Nic.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Nic.Count(where, Database);
        }
        
        public async Task BatchQueryNics(int batchSize, WhereDelegate<NicColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Nic.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Nic>(batch));
            }, Database);
        }
		
        public async Task BatchAllNics(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Nic>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Nic.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Nic>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Configuration GetOneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Configuration>();
			return (Bam.Net.CoreServices.Data.Configuration)Bam.Net.CoreServices.Data.Daos.Configuration.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Configuration OneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Configuration>();
            return (Bam.Net.CoreServices.Data.Configuration)Bam.Net.CoreServices.Data.Daos.Configuration.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ConfigurationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Configuration> ConfigurationsWhere(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Configuration>(Bam.Net.CoreServices.Data.Daos.Configuration.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Configuration> TopConfigurationsWhere(int count, WhereDelegate<ConfigurationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Configuration>(Bam.Net.CoreServices.Data.Daos.Configuration.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Configurations
		/// </summary>
		public long CountConfigurations()
        {
            return Bam.Net.CoreServices.Data.Daos.Configuration.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Configuration.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurations(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Configuration.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Configuration>(batch));
            }, Database);
        }
		
        public async Task BatchAllConfigurations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Configuration.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Configuration>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.ApiKey GetOneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApiKey>();
			return (Bam.Net.CoreServices.Data.ApiKey)Bam.Net.CoreServices.Data.Daos.ApiKey.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.ApiKey OneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApiKey>();
            return (Bam.Net.CoreServices.Data.ApiKey)Bam.Net.CoreServices.Data.Daos.ApiKey.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ApiKeyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.ApiKey> ApiKeysWhere(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.ApiKey>(Bam.Net.CoreServices.Data.Daos.ApiKey.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.ApiKey> TopApiKeysWhere(int count, WhereDelegate<ApiKeyColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ApiKey>(Bam.Net.CoreServices.Data.Daos.ApiKey.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ApiKeys
		/// </summary>
		public long CountApiKeys()
        {
            return Bam.Net.CoreServices.Data.Daos.ApiKey.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.ApiKey.Count(where, Database);
        }
        
        public async Task BatchQueryApiKeys(int batchSize, WhereDelegate<ApiKeyColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApiKey.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApiKey>(batch));
            }, Database);
        }
		
        public async Task BatchAllApiKeys(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApiKey.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApiKey>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Application GetOneApplicationWhere(WhereDelegate<ApplicationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Application>();
			return (Bam.Net.CoreServices.Data.Application)Bam.Net.CoreServices.Data.Daos.Application.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Application OneApplicationWhere(WhereDelegate<ApplicationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Application>();
            return (Bam.Net.CoreServices.Data.Application)Bam.Net.CoreServices.Data.Daos.Application.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ApplicationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Application> ApplicationsWhere(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Application>(Bam.Net.CoreServices.Data.Daos.Application.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Application> TopApplicationsWhere(int count, WhereDelegate<ApplicationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Application>(Bam.Net.CoreServices.Data.Daos.Application.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Applications
		/// </summary>
		public long CountApplications()
        {
            return Bam.Net.CoreServices.Data.Daos.Application.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Application.Count(where, Database);
        }
        
        public async Task BatchQueryApplications(int batchSize, WhereDelegate<ApplicationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Application.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Application>(batch));
            }, Database);
        }
		
        public async Task BatchAllApplications(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Application.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Application>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.ClientServerConnection GetOneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ClientServerConnection>();
			return (Bam.Net.CoreServices.Data.ClientServerConnection)Bam.Net.CoreServices.Data.Daos.ClientServerConnection.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.ClientServerConnection OneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ClientServerConnection>();
            return (Bam.Net.CoreServices.Data.ClientServerConnection)Bam.Net.CoreServices.Data.Daos.ClientServerConnection.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ClientServerConnectionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection> ClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection> TopClientServerConnectionsWhere(int count, WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ClientServerConnections
		/// </summary>
		public long CountClientServerConnections()
        {
            return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Count(where, Database);
        }
        
        public async Task BatchQueryClientServerConnections(int batchSize, WhereDelegate<ClientServerConnectionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ClientServerConnection.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(batch));
            }, Database);
        }
		
        public async Task BatchAllClientServerConnections(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ClientServerConnection.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor GetOneExternalEventSubscriptionDescriptorWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();
			return (Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor)Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ExternalEventSubscriptionDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor OneExternalEventSubscriptionDescriptorWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();
            return (Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor)Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor> ExternalEventSubscriptionDescriptorsWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor> TopExternalEventSubscriptionDescriptorsWhere(int count, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ExternalEventSubscriptionDescriptors
		/// </summary>
		public long CountExternalEventSubscriptionDescriptors()
        {
            return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionDescriptorColumns and other values
		/// </param>
        public long CountExternalEventSubscriptionDescriptorsWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryExternalEventSubscriptionDescriptors(int batchSize, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllExternalEventSubscriptionDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Machine GetOneMachineWhere(WhereDelegate<MachineColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Machine>();
			return (Bam.Net.CoreServices.Data.Machine)Bam.Net.CoreServices.Data.Daos.Machine.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Machine OneMachineWhere(WhereDelegate<MachineColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Machine>();
            return (Bam.Net.CoreServices.Data.Machine)Bam.Net.CoreServices.Data.Daos.Machine.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.MachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.MachineColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Machine> MachinesWhere(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Machine>(Bam.Net.CoreServices.Data.Daos.Machine.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Machine> TopMachinesWhere(int count, WhereDelegate<MachineColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Machine>(Bam.Net.CoreServices.Data.Daos.Machine.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Machines
		/// </summary>
		public long CountMachines()
        {
            return Bam.Net.CoreServices.Data.Daos.Machine.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Machine.Count(where, Database);
        }
        
        public async Task BatchQueryMachines(int batchSize, WhereDelegate<MachineColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Machine.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Machine>(batch));
            }, Database);
        }
		
        public async Task BatchAllMachines(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Machine.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Machine>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Organization GetOneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Organization>();
			return (Bam.Net.CoreServices.Data.Organization)Bam.Net.CoreServices.Data.Daos.Organization.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Organization OneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Organization>();
            return (Bam.Net.CoreServices.Data.Organization)Bam.Net.CoreServices.Data.Daos.Organization.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.OrganizationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Organization> OrganizationsWhere(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Organization>(Bam.Net.CoreServices.Data.Daos.Organization.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Organization> TopOrganizationsWhere(int count, WhereDelegate<OrganizationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Organization>(Bam.Net.CoreServices.Data.Daos.Organization.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Organizations
		/// </summary>
		public long CountOrganizations()
        {
            return Bam.Net.CoreServices.Data.Daos.Organization.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Organization.Count(where, Database);
        }
        
        public async Task BatchQueryOrganizations(int batchSize, WhereDelegate<OrganizationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Organization.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Organization>(batch));
            }, Database);
        }
		
        public async Task BatchAllOrganizations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Organization.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Organization>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.ProcessDescriptor GetOneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ProcessDescriptor>();
			return (Bam.Net.CoreServices.Data.ProcessDescriptor)Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.ProcessDescriptor OneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ProcessDescriptor>();
            return (Bam.Net.CoreServices.Data.ProcessDescriptor)Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.ProcessDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor> ProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor> TopProcessDescriptorsWhere(int count, WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ProcessDescriptors
		/// </summary>
		public long CountProcessDescriptors()
        {
            return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryProcessDescriptors(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllProcessDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.Subscription GetOneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Subscription>();
			return (Bam.Net.CoreServices.Data.Subscription)Bam.Net.CoreServices.Data.Daos.Subscription.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.Subscription OneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Subscription>();
            return (Bam.Net.CoreServices.Data.Subscription)Bam.Net.CoreServices.Data.Daos.Subscription.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.SubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.SubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.Subscription> SubscriptionsWhere(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.Subscription>(Bam.Net.CoreServices.Data.Daos.Subscription.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.Subscription> TopSubscriptionsWhere(int count, WhereDelegate<SubscriptionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Subscription>(Bam.Net.CoreServices.Data.Daos.Subscription.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Subscriptions
		/// </summary>
		public long CountSubscriptions()
        {
            return Bam.Net.CoreServices.Data.Daos.Subscription.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.Subscription.Count(where, Database);
        }
        
        public async Task BatchQuerySubscriptions(int batchSize, WhereDelegate<SubscriptionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Subscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Subscription>(batch));
            }, Database);
        }
		
        public async Task BatchAllSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Subscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Subscription>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.Data.User GetOneUserWhere(WhereDelegate<UserColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.User>();
			return (Bam.Net.CoreServices.Data.User)Bam.Net.CoreServices.Data.Daos.User.GetOneWhere(where, Database).CopyAs(wrapperType, this);
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
		public Bam.Net.CoreServices.Data.User OneUserWhere(WhereDelegate<UserColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.User>();
            return (Bam.Net.CoreServices.Data.User)Bam.Net.CoreServices.Data.Daos.User.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.Data.UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.Data.UserColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.Data.User> UsersWhere(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.Data.User>(Bam.Net.CoreServices.Data.Daos.User.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.CoreServices.Data.User> TopUsersWhere(int count, WhereDelegate<UserColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.User>(Bam.Net.CoreServices.Data.Daos.User.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Users
		/// </summary>
		public long CountUsers()
        {
            return Bam.Net.CoreServices.Data.Daos.User.Count(Database);
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
            return Bam.Net.CoreServices.Data.Daos.User.Count(where, Database);
        }
        
        public async Task BatchQueryUsers(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.User.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.User>(batch));
            }, Database);
        }
		
        public async Task BatchAllUsers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.User.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.User>(batch));
            }, Database);
        }
	}
}																								
