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
using Bam.Net.CoreServices.ServiceRegistration.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository
{
	[Serializable]
	public class ServiceRegistryRepository: DaoRepository
	{
		public ServiceRegistryRepository()
		{
			SchemaName = "ServiceRegistry";
			BaseNamespace = "Bam.Net.CoreServices.ServiceRegistration.Data";			
﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>();﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>();﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>();
			DaoAssembly = typeof(ServiceRegistryRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(ServiceRegistryRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries GetOneMachineRegistriesWhere(WhereDelegate<MachineRegistriesColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single MachineRegistries instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MachineRegistriesColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineRegistriesColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries OneMachineRegistriesWhere(WhereDelegate<MachineRegistriesColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistriesColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistriesColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries> MachineRegistriesWhere(WhereDelegate<MachineRegistriesColumns> where, OrderBy<MachineRegistriesColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a MachineRegistriesColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineRegistriesColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries> TopMachineRegistriesWhere(int count, WhereDelegate<MachineRegistriesColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of MachineRegistries
		/// </summary>
		public long CountMachineRegistries()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MachineRegistriesColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MachineRegistriesColumns and other values
		/// </param>
        public long CountMachineRegistriesWhere(WhereDelegate<MachineRegistriesColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Count(where, Database);
        }
        
        public async Task BatchQueryMachineRegistries(int batchSize, WhereDelegate<MachineRegistriesColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>(batch));
            }, Database);
        }
		
        public async Task BatchAllMachineRegistries(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.MachineRegistries>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier GetOneServiceTypeIdentifierWhere(WhereDelegate<ServiceTypeIdentifierColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceTypeIdentifier instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceTypeIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceTypeIdentifierColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier OneServiceTypeIdentifierWhere(WhereDelegate<ServiceTypeIdentifierColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifierColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier> ServiceTypeIdentifiersWhere(WhereDelegate<ServiceTypeIdentifierColumns> where, OrderBy<ServiceTypeIdentifierColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ServiceTypeIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceTypeIdentifierColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier> TopServiceTypeIdentifiersWhere(int count, WhereDelegate<ServiceTypeIdentifierColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ServiceTypeIdentifiers
		/// </summary>
		public long CountServiceTypeIdentifiers()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceTypeIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceTypeIdentifierColumns and other values
		/// </param>
        public long CountServiceTypeIdentifiersWhere(WhereDelegate<ServiceTypeIdentifierColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Count(where, Database);
        }
        
        public async Task BatchQueryServiceTypeIdentifiers(int batchSize, WhereDelegate<ServiceTypeIdentifierColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>(batch));
            }, Database);
        }
		
        public async Task BatchAllServiceTypeIdentifiers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceTypeIdentifier>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor GetOneServiceDescriptorWhere(WhereDelegate<ServiceDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor OneServiceDescriptorWhere(WhereDelegate<ServiceDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor> ServiceDescriptorsWhere(WhereDelegate<ServiceDescriptorColumns> where, OrderBy<ServiceDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor> TopServiceDescriptorsWhere(int count, WhereDelegate<ServiceDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ServiceDescriptors
		/// </summary>
		public long CountServiceDescriptors()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorColumns and other values
		/// </param>
        public long CountServiceDescriptorsWhere(WhereDelegate<ServiceDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryServiceDescriptors(int batchSize, WhereDelegate<ServiceDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllServiceDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor GetOneServiceRegistryDescriptorWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceRegistryDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor OneServiceRegistryDescriptorWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor> ServiceRegistryDescriptorsWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor> TopServiceRegistryDescriptorsWhere(int count, WhereDelegate<ServiceRegistryDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ServiceRegistryDescriptors
		/// </summary>
		public long CountServiceRegistryDescriptors()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
        public long CountServiceRegistryDescriptorsWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryServiceRegistryDescriptors(int batchSize, WhereDelegate<ServiceRegistryDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllServiceRegistryDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor GetOneServiceRegistryLoaderDescriptorWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceRegistryLoaderDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor OneServiceRegistryLoaderDescriptorWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor> ServiceRegistryLoaderDescriptorsWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor> TopServiceRegistryLoaderDescriptorsWhere(int count, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ServiceRegistryLoaderDescriptors
		/// </summary>
		public long CountServiceRegistryLoaderDescriptors()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
        public long CountServiceRegistryLoaderDescriptorsWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryServiceRegistryLoaderDescriptors(int batchSize, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllServiceRegistryLoaderDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLoaderDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock GetOneServiceRegistryLockWhere(WhereDelegate<ServiceRegistryLockColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>();
			return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceRegistryLock instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLockColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLockColumns and other values
		/// </param>
		public Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock OneServiceRegistryLockWhere(WhereDelegate<ServiceRegistryLockColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>();
            return (Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock)Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLockColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLockColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock> ServiceRegistryLocksWhere(WhereDelegate<ServiceRegistryLockColumns> where, OrderBy<ServiceRegistryLockColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLockColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLockColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock> TopServiceRegistryLocksWhere(int count, WhereDelegate<ServiceRegistryLockColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>(Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ServiceRegistryLocks
		/// </summary>
		public long CountServiceRegistryLocks()
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLockColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLockColumns and other values
		/// </param>
        public long CountServiceRegistryLocksWhere(WhereDelegate<ServiceRegistryLockColumns> where)
        {
            return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Count(where, Database);
        }
        
        public async Task BatchQueryServiceRegistryLocks(int batchSize, WhereDelegate<ServiceRegistryLockColumns> where, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>(batch));
            }, Database);
        }
		
        public async Task BatchAllServiceRegistryLocks(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>> batchProcessor)
        {
            await Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryLock>(batch));
            }, Database);
        }
	}
}																								
