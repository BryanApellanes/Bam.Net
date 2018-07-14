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
using Bam.Net.CoreServices.AssemblyManagement.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository
{
	[Serializable]
	public class AssemblyServiceRepository: DaoRepository
	{
		public AssemblyServiceRepository()
		{
			SchemaName = "AssemblyService";
			BaseNamespace = "Bam.Net.CoreServices.AssemblyManagement.Data";			
﻿			
			AddType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>();﻿			
			AddType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>();
			DaoAssembly = typeof(AssemblyServiceRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(AssemblyServiceRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor GetOneAssemblyDescriptorWhere(WhereDelegate<AssemblyDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>();
			return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor OneAssemblyDescriptorWhere(WhereDelegate<AssemblyDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>();
            return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor> AssemblyDescriptorsWhere(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor> TopAssemblyDescriptorsWhere(int count, WhereDelegate<AssemblyDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AssemblyDescriptors
		/// </summary>
		public long CountAssemblyDescriptors()
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
        public long CountAssemblyDescriptorsWhere(WhereDelegate<AssemblyDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryAssemblyDescriptors(int batchSize, WhereDelegate<AssemblyDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllAssemblyDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision GetOneAssemblyRevisionWhere(WhereDelegate<AssemblyRevisionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>();
			return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyRevision instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyRevisionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyRevisionColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision OneAssemblyRevisionWhere(WhereDelegate<AssemblyRevisionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>();
            return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevisionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevisionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision> AssemblyRevisionsWhere(WhereDelegate<AssemblyRevisionColumns> where, OrderBy<AssemblyRevisionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyRevisionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyRevisionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision> TopAssemblyRevisionsWhere(int count, WhereDelegate<AssemblyRevisionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AssemblyRevisions
		/// </summary>
		public long CountAssemblyRevisions()
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyRevisionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyRevisionColumns and other values
		/// </param>
        public long CountAssemblyRevisionsWhere(WhereDelegate<AssemblyRevisionColumns> where)
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.Count(where, Database);
        }
        
        public async Task BatchQueryAssemblyRevisions(int batchSize, WhereDelegate<AssemblyRevisionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>(batch));
            }, Database);
        }
		
        public async Task BatchAllAssemblyRevisions(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyRevision.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyRevision>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor GetOneAssemblyReferenceDescriptorWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>();
			return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyReferenceDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor OneAssemblyReferenceDescriptorWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>();
            return (Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor> AssemblyReferenceDescriptorsWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor> TopAssemblyReferenceDescriptorsWhere(int count, WhereDelegate<AssemblyReferenceDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AssemblyReferenceDescriptors
		/// </summary>
		public long CountAssemblyReferenceDescriptors()
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
        public long CountAssemblyReferenceDescriptorsWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryAssemblyReferenceDescriptors(int batchSize, WhereDelegate<AssemblyReferenceDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllAssemblyReferenceDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor GetOneProcessRuntimeDescriptorWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>();
			return (Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ProcessRuntimeDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessRuntimeDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor OneProcessRuntimeDescriptorWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>();
            return (Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor)Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor> ProcessRuntimeDescriptorsWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessRuntimeDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor> TopProcessRuntimeDescriptorsWhere(int count, WhereDelegate<ProcessRuntimeDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>(Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ProcessRuntimeDescriptors
		/// </summary>
		public long CountProcessRuntimeDescriptors()
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessRuntimeDescriptorColumns and other values
		/// </param>
        public long CountProcessRuntimeDescriptorsWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryProcessRuntimeDescriptors(int batchSize, WhereDelegate<ProcessRuntimeDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllProcessRuntimeDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>(batch));
            }, Database);
        }
	}
}																								
