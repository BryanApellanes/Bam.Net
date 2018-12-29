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
using Bam.Net.CoreServices.AccessControl.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao.Repository
{
	[Serializable]
	public class AccessControlRepository: DaoRepository
	{
		public AccessControlRepository()
		{
			SchemaName = "AccessControl";
			BaseNamespace = "Bam.Net.CoreServices.AccessControl.Data";			
﻿			
			AddType<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>();﻿			
			AddType<Bam.Net.CoreServices.AccessControl.Data.Resource>();﻿			
			AddType<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>();
			DaoAssembly = typeof(AccessControlRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(AccessControlRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneResourceHostWhere(WhereDelegate<ResourceHostColumns> where)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneResourceHostWhere(WhereDelegate<ResourceHostColumns> where, out Bam.Net.CoreServices.AccessControl.Data.ResourceHost result)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.SetOneWhere(where, out Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AccessControl.Data.ResourceHost GetOneResourceHostWhere(WhereDelegate<ResourceHostColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>();
			return (Bam.Net.CoreServices.AccessControl.Data.ResourceHost)Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single ResourceHost instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceHostColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceHostColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AccessControl.Data.ResourceHost OneResourceHostWhere(WhereDelegate<ResourceHostColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>();
            return (Bam.Net.CoreServices.AccessControl.Data.ResourceHost)Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AccessControl.Data.ResourceHostColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AccessControl.Data.ResourceHostColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.ResourceHost> ResourceHostsWhere(WhereDelegate<ResourceHostColumns> where, OrderBy<ResourceHostColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>(Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ResourceHostColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceHostColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.ResourceHost> TopResourceHostsWhere(int count, WhereDelegate<ResourceHostColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>(Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ResourceHosts
		/// </summary>
		public long CountResourceHosts()
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceHostColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceHostColumns and other values
		/// </param>
        public long CountResourceHostsWhere(WhereDelegate<ResourceHostColumns> where)
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Count(where, Database);
        }
        
        public async Task BatchQueryResourceHosts(int batchSize, WhereDelegate<ResourceHostColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>(batch));
            }, Database);
        }
		
        public async Task BatchAllResourceHosts(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.ResourceHost>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneResourceWhere(WhereDelegate<ResourceColumns> where)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneResourceWhere(WhereDelegate<ResourceColumns> where, out Bam.Net.CoreServices.AccessControl.Data.Resource result)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.SetOneWhere(where, out Bam.Net.CoreServices.AccessControl.Data.Dao.Resource daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.CoreServices.AccessControl.Data.Resource>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AccessControl.Data.Resource GetOneResourceWhere(WhereDelegate<ResourceColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.Resource>();
			return (Bam.Net.CoreServices.AccessControl.Data.Resource)Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single Resource instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AccessControl.Data.Resource OneResourceWhere(WhereDelegate<ResourceColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.Resource>();
            return (Bam.Net.CoreServices.AccessControl.Data.Resource)Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AccessControl.Data.ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AccessControl.Data.ResourceColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.Resource> ResourcesWhere(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.Resource>(Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.Resource> TopResourcesWhere(int count, WhereDelegate<ResourceColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.Resource>(Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of Resources
		/// </summary>
		public long CountResources()
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
        public long CountResourcesWhere(WhereDelegate<ResourceColumns> where)
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Count(where, Database);
        }
        
        public async Task BatchQueryResources(int batchSize, WhereDelegate<ResourceColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.Resource>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.Resource>(batch));
            }, Database);
        }
		
        public async Task BatchAllResources(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.Resource>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.Resource>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOnePermissionSpecificationWhere(WhereDelegate<PermissionSpecificationColumns> where)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOnePermissionSpecificationWhere(WhereDelegate<PermissionSpecificationColumns> where, out Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification result)
		{
			Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.SetOneWhere(where, out Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification GetOnePermissionSpecificationWhere(WhereDelegate<PermissionSpecificationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>();
			return (Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification)Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single PermissionSpecification instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionSpecificationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionSpecificationColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification OnePermissionSpecificationWhere(WhereDelegate<PermissionSpecificationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>();
            return (Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification)Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AccessControl.Data.PermissionSpecificationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AccessControl.Data.PermissionSpecificationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification> PermissionSpecificationsWhere(WhereDelegate<PermissionSpecificationColumns> where, OrderBy<PermissionSpecificationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>(Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a PermissionSpecificationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionSpecificationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification> TopPermissionSpecificationsWhere(int count, WhereDelegate<PermissionSpecificationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>(Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of PermissionSpecifications
		/// </summary>
		public long CountPermissionSpecifications()
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionSpecificationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionSpecificationColumns and other values
		/// </param>
        public long CountPermissionSpecificationsWhere(WhereDelegate<PermissionSpecificationColumns> where)
        {
            return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Count(where, Database);
        }
        
        public async Task BatchQueryPermissionSpecifications(int batchSize, WhereDelegate<PermissionSpecificationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>(batch));
            }, Database);
        }
		
        public async Task BatchAllPermissionSpecifications(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>> batchProcessor)
        {
            await Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>(batch));
            }, Database);
        }
	}
}																								
