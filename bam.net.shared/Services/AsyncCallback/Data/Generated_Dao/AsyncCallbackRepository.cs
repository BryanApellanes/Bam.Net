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
using Bam.Net.Services.AsyncCallback.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao.Repository
{
	[Serializable]
	public class AsyncCallbackRepository: DaoRepository
	{
		public AsyncCallbackRepository()
		{
			SchemaName = "AsyncCallback";
			BaseNamespace = "Bam.Net.Services.AsyncCallback.Data";			
﻿			
			AddType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>();﻿			
			AddType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>();﻿			
			AddType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>();
			DaoAssembly = typeof(AsyncCallbackRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(AsyncCallbackRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData GetOneAsyncExecutionDataWhere(WhereDelegate<AsyncExecutionDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>();
			return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData OneAsyncExecutionDataWhere(WhereDelegate<AsyncExecutionDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>();
            return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.AsyncCallback.Data.AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.AsyncCallback.Data.AsyncExecutionDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData> AsyncExecutionDatasWhere(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData> TopAsyncExecutionDatasWhere(int count, WhereDelegate<AsyncExecutionDataColumns> where)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AsyncExecutionDatas
		/// </summary>
		public long CountAsyncExecutionDatas()
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
        public long CountAsyncExecutionDatasWhere(WhereDelegate<AsyncExecutionDataColumns> where)
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Count(where, Database);
        }
        
        public async Task BatchQueryAsyncExecutionDatas(int batchSize, WhereDelegate<AsyncExecutionDataColumns> where, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>(batch));
            }, Database);
        }
		
        public async Task BatchAllAsyncExecutionDatas(int batchSize, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData GetOneAsyncExecutionRequestDataWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>();
			return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionRequestData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData OneAsyncExecutionRequestDataWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>();
            return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData> AsyncExecutionRequestDatasWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData> TopAsyncExecutionRequestDatasWhere(int count, WhereDelegate<AsyncExecutionRequestDataColumns> where)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AsyncExecutionRequestDatas
		/// </summary>
		public long CountAsyncExecutionRequestDatas()
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
        public long CountAsyncExecutionRequestDatasWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where)
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Count(where, Database);
        }
        
        public async Task BatchQueryAsyncExecutionRequestDatas(int batchSize, WhereDelegate<AsyncExecutionRequestDataColumns> where, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>(batch));
            }, Database);
        }
		
        public async Task BatchAllAsyncExecutionRequestDatas(int batchSize, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionRequestData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData GetOneAsyncExecutionResponseDataWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>();
			return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionResponseData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		public Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData OneAsyncExecutionResponseDataWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>();
            return (Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData)Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData> AsyncExecutionResponseDatasWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData> TopAsyncExecutionResponseDatasWhere(int count, WhereDelegate<AsyncExecutionResponseDataColumns> where)
        {
            return Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>(Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of AsyncExecutionResponseDatas
		/// </summary>
		public long CountAsyncExecutionResponseDatas()
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
        public long CountAsyncExecutionResponseDatasWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where)
        {
            return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Count(where, Database);
        }
        
        public async Task BatchQueryAsyncExecutionResponseDatas(int batchSize, WhereDelegate<AsyncExecutionResponseDataColumns> where, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>(batch));
            }, Database);
        }
		
        public async Task BatchAllAsyncExecutionResponseDatas(int batchSize, Action<IEnumerable<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>> batchProcessor)
        {
            await Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.AsyncCallback.Data.AsyncExecutionResponseData>(batch));
            }, Database);
        }
	}
}																								
