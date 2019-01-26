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
using Bam.Net.Data.Dynamic.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao.Repository
{
	[Serializable]
	public class DynamicTypeDataRepository: DaoRepository
	{
		public DynamicTypeDataRepository()
		{
			SchemaName = "DynamicTypeData";
			BaseNamespace = "Bam.Net.Data.Dynamic.Data";			
﻿			
			AddType<Bam.Net.Data.Dynamic.Data.DataInstance>();﻿			
			AddType<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>();﻿			
			AddType<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>();﻿			
			AddType<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>();﻿			
			AddType<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>();﻿			
			AddType<Bam.Net.Data.Dynamic.Data.RootDocument>();
			DaoAssembly = typeof(DynamicTypeDataRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(DynamicTypeDataRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDataInstanceWhere(WhereDelegate<DataInstanceColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DataInstance.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDataInstanceWhere(WhereDelegate<DataInstanceColumns> where, out Bam.Net.Data.Dynamic.Data.DataInstance result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DataInstance.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.DataInstance daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.DataInstance>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.DataInstance GetOneDataInstanceWhere(WhereDelegate<DataInstanceColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DataInstance>();
			return (Bam.Net.Data.Dynamic.Data.DataInstance)Bam.Net.Data.Dynamic.Data.Dao.DataInstance.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single DataInstance instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.DataInstance OneDataInstanceWhere(WhereDelegate<DataInstanceColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DataInstance>();
            return (Bam.Net.Data.Dynamic.Data.DataInstance)Bam.Net.Data.Dynamic.Data.Dao.DataInstance.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.DataInstanceColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstance> DataInstancesWhere(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DataInstance>(Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstance> TopDataInstancesWhere(int count, WhereDelegate<DataInstanceColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DataInstance>(Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DataInstances
		/// </summary>
		public long CountDataInstances()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
        public long CountDataInstancesWhere(WhereDelegate<DataInstanceColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Count(where, Database);
        }
        
        public async Task BatchQueryDataInstances(int batchSize, WhereDelegate<DataInstanceColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstance>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DataInstance.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DataInstance>(batch));
            }, Database);
        }
		
        public async Task BatchAllDataInstances(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstance>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DataInstance.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DataInstance>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDataInstancePropertyValueWhere(WhereDelegate<DataInstancePropertyValueColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDataInstancePropertyValueWhere(WhereDelegate<DataInstancePropertyValueColumns> where, out Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue GetOneDataInstancePropertyValueWhere(WhereDelegate<DataInstancePropertyValueColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>();
			return (Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue)Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single DataInstancePropertyValue instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue OneDataInstancePropertyValueWhere(WhereDelegate<DataInstancePropertyValueColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>();
            return (Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue)Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.DataInstancePropertyValueColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue> DataInstancePropertyValuesWhere(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>(Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue> TopDataInstancePropertyValuesWhere(int count, WhereDelegate<DataInstancePropertyValueColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>(Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DataInstancePropertyValues
		/// </summary>
		public long CountDataInstancePropertyValues()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
        public long CountDataInstancePropertyValuesWhere(WhereDelegate<DataInstancePropertyValueColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Count(where, Database);
        }
        
        public async Task BatchQueryDataInstancePropertyValues(int batchSize, WhereDelegate<DataInstancePropertyValueColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>(batch));
            }, Database);
        }
		
        public async Task BatchAllDataInstancePropertyValues(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicNamespaceDescriptorWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicNamespaceDescriptorWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where, out Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor GetOneDynamicNamespaceDescriptorWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>();
			return (Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single DynamicNamespaceDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicNamespaceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicNamespaceDescriptorColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor OneDynamicNamespaceDescriptorWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>();
            return (Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor> DynamicNamespaceDescriptorsWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DynamicNamespaceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicNamespaceDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor> TopDynamicNamespaceDescriptorsWhere(int count, WhereDelegate<DynamicNamespaceDescriptorColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DynamicNamespaceDescriptors
		/// </summary>
		public long CountDynamicNamespaceDescriptors()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicNamespaceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicNamespaceDescriptorColumns and other values
		/// </param>
        public long CountDynamicNamespaceDescriptorsWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryDynamicNamespaceDescriptors(int batchSize, WhereDelegate<DynamicNamespaceDescriptorColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllDynamicNamespaceDescriptors(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicTypeDescriptorWhere(WhereDelegate<DynamicTypeDescriptorColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicTypeDescriptorWhere(WhereDelegate<DynamicTypeDescriptorColumns> where, out Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor GetOneDynamicTypeDescriptorWhere(WhereDelegate<DynamicTypeDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>();
			return (Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single DynamicTypeDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypeDescriptorColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor OneDynamicTypeDescriptorWhere(WhereDelegate<DynamicTypeDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>();
            return (Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor> DynamicTypeDescriptorsWhere(WhereDelegate<DynamicTypeDescriptorColumns> where, OrderBy<DynamicTypeDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypeDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor> TopDynamicTypeDescriptorsWhere(int count, WhereDelegate<DynamicTypeDescriptorColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DynamicTypeDescriptors
		/// </summary>
		public long CountDynamicTypeDescriptors()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypeDescriptorColumns and other values
		/// </param>
        public long CountDynamicTypeDescriptorsWhere(WhereDelegate<DynamicTypeDescriptorColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryDynamicTypeDescriptors(int batchSize, WhereDelegate<DynamicTypeDescriptorColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllDynamicTypeDescriptors(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicTypePropertyDescriptorWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneDynamicTypePropertyDescriptorWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, out Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor GetOneDynamicTypePropertyDescriptorWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>();
			return (Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single DynamicTypePropertyDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor OneDynamicTypePropertyDescriptorWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>();
            return (Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor)Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor> DynamicTypePropertyDescriptorsWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor> TopDynamicTypePropertyDescriptorsWhere(int count, WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>(Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DynamicTypePropertyDescriptors
		/// </summary>
		public long CountDynamicTypePropertyDescriptors()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
        public long CountDynamicTypePropertyDescriptorsWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryDynamicTypePropertyDescriptors(int batchSize, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllDynamicTypePropertyDescriptors(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneRootDocumentWhere(WhereDelegate<RootDocumentColumns> where)
		{
			Bam.Net.Data.Dynamic.Data.Dao.RootDocument.SetOneWhere(where, Database);
		}

				/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneRootDocumentWhere(WhereDelegate<RootDocumentColumns> where, out Bam.Net.Data.Dynamic.Data.RootDocument result)
		{
			Bam.Net.Data.Dynamic.Data.Dao.RootDocument.SetOneWhere(where, out Bam.Net.Data.Dynamic.Data.Dao.RootDocument daoResult, Database);
			result = daoResult.CopyAs<Bam.Net.Data.Dynamic.Data.RootDocument>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Data.Dynamic.Data.RootDocument GetOneRootDocumentWhere(WhereDelegate<RootDocumentColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.RootDocument>();
			return (Bam.Net.Data.Dynamic.Data.RootDocument)Bam.Net.Data.Dynamic.Data.Dao.RootDocument.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single RootDocument instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RootDocumentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RootDocumentColumns and other values
		/// </param>
		public Bam.Net.Data.Dynamic.Data.RootDocument OneRootDocumentWhere(WhereDelegate<RootDocumentColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Dynamic.Data.RootDocument>();
            return (Bam.Net.Data.Dynamic.Data.RootDocument)Bam.Net.Data.Dynamic.Data.Dao.RootDocument.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Data.Dynamic.Data.RootDocumentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Data.Dynamic.Data.RootDocumentColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.RootDocument> RootDocumentsWhere(WhereDelegate<RootDocumentColumns> where, OrderBy<RootDocumentColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.RootDocument>(Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a RootDocumentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RootDocumentColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Data.Dynamic.Data.RootDocument> TopRootDocumentsWhere(int count, WhereDelegate<RootDocumentColumns> where)
        {
            return Wrap<Bam.Net.Data.Dynamic.Data.RootDocument>(Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of RootDocuments
		/// </summary>
		public long CountRootDocuments()
        {
            return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RootDocumentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RootDocumentColumns and other values
		/// </param>
        public long CountRootDocumentsWhere(WhereDelegate<RootDocumentColumns> where)
        {
            return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Count(where, Database);
        }
        
        public async Task BatchQueryRootDocuments(int batchSize, WhereDelegate<RootDocumentColumns> where, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.RootDocument>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.RootDocument.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.RootDocument>(batch));
            }, Database);
        }
		
        public async Task BatchAllRootDocuments(int batchSize, Action<IEnumerable<Bam.Net.Data.Dynamic.Data.RootDocument>> batchProcessor)
        {
            await Bam.Net.Data.Dynamic.Data.Dao.RootDocument.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Dynamic.Data.RootDocument>(batch));
            }, Database);
        }
	}
}																								
