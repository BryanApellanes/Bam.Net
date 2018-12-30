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
using Bam.Net.CoreServices.WebHooks.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao.Repository
{
	[Serializable]
	public class WebHooksRepository: DaoRepository
	{
		public WebHooksRepository()
		{
			SchemaName = "WebHooks";
			BaseNamespace = "Bam.Net.CoreServices.WebHooks.Data";			
﻿			
			AddType<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>();﻿			
			AddType<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>();
			DaoAssembly = typeof(WebHooksRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(WebHooksRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookCall GetOneWebHookCallWhere(WhereDelegate<WebHookCallColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>();
			return (Bam.Net.CoreServices.WebHooks.Data.WebHookCall)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single WebHookCall instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookCallColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookCallColumns and other values
		/// </param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookCall OneWebHookCallWhere(WhereDelegate<WebHookCallColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>();
            return (Bam.Net.CoreServices.WebHooks.Data.WebHookCall)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.WebHooks.Data.WebHookCallColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.WebHooks.Data.WebHookCallColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookCall> WebHookCallsWhere(WhereDelegate<WebHookCallColumns> where, OrderBy<WebHookCallColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a WebHookCallColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookCallColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookCall> TopWebHookCallsWhere(int count, WhereDelegate<WebHookCallColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of WebHookCalls
		/// </summary>
		public long CountWebHookCalls()
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookCallColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookCallColumns and other values
		/// </param>
        public long CountWebHookCallsWhere(WhereDelegate<WebHookCallColumns> where)
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Count(where, Database);
        }
        
        public async Task BatchQueryWebHookCalls(int batchSize, WhereDelegate<WebHookCallColumns> where, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>(batch));
            }, Database);
        }
		
        public async Task BatchAllWebHookCalls(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookCall>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor GetOneWebHookDescriptorWhere(WhereDelegate<WebHookDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>();
			return (Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single WebHookDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor OneWebHookDescriptorWhere(WhereDelegate<WebHookDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>();
            return (Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor> WebHookDescriptorsWhere(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor> TopWebHookDescriptorsWhere(int count, WhereDelegate<WebHookDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of WebHookDescriptors
		/// </summary>
		public long CountWebHookDescriptors()
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
        public long CountWebHookDescriptorsWhere(WhereDelegate<WebHookDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryWebHookDescriptors(int batchSize, WhereDelegate<WebHookDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>(batch));
            }, Database);
        }
		
        public async Task BatchAllWebHookDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber GetOneWebHookSubscriberWhere(WhereDelegate<WebHookSubscriberColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>();
			return (Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single WebHookSubscriber instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookSubscriberColumns and other values
		/// </param>
		public Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber OneWebHookSubscriberWhere(WhereDelegate<WebHookSubscriberColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>();
            return (Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber)Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriberColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber> WebHookSubscribersWhere(WhereDelegate<WebHookSubscriberColumns> where, OrderBy<WebHookSubscriberColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a WebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookSubscriberColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber> TopWebHookSubscribersWhere(int count, WhereDelegate<WebHookSubscriberColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>(Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of WebHookSubscribers
		/// </summary>
		public long CountWebHookSubscribers()
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookSubscriberColumns and other values
		/// </param>
        public long CountWebHookSubscribersWhere(WhereDelegate<WebHookSubscriberColumns> where)
        {
            return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Count(where, Database);
        }
        
        public async Task BatchQueryWebHookSubscribers(int batchSize, WhereDelegate<WebHookSubscriberColumns> where, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>(batch));
            }, Database);
        }
		
        public async Task BatchAllWebHookSubscribers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>> batchProcessor)
        {
            await Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>(batch));
            }, Database);
        }
	}
}																								
