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
using Bam.Net.Automation.Testing.Data;

namespace Bam.Net.Automation.Testing.Data.Dao.Repository
{
	[Serializable]
	public class TestingRepository: DaoRepository
	{
		public TestingRepository()
		{
			SchemaName = "Testing";
			BaseNamespace = "Bam.Net.Automation.Testing.Data";			
﻿			
			AddType<Bam.Net.Automation.Testing.Data.NotificationSubscription>();﻿			
			AddType<Bam.Net.Automation.Testing.Data.TestDefinition>();﻿			
			AddType<Bam.Net.Automation.Testing.Data.TestExecution>();﻿			
			AddType<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>();﻿			
			AddType<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>();
			DaoAssembly = typeof(TestingRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(TestingRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.Testing.Data.NotificationSubscription GetOneNotificationSubscriptionWhere(WhereDelegate<NotificationSubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.NotificationSubscription>();
			return (Bam.Net.Automation.Testing.Data.NotificationSubscription)Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single NotificationSubscription instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NotificationSubscriptionColumns and other values
		/// </param>
		public Bam.Net.Automation.Testing.Data.NotificationSubscription OneNotificationSubscriptionWhere(WhereDelegate<NotificationSubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.NotificationSubscription>();
            return (Bam.Net.Automation.Testing.Data.NotificationSubscription)Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.Testing.Data.NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.Testing.Data.NotificationSubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.NotificationSubscription> NotificationSubscriptionsWhere(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.NotificationSubscription>(Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NotificationSubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.NotificationSubscription> TopNotificationSubscriptionsWhere(int count, WhereDelegate<NotificationSubscriptionColumns> where)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.NotificationSubscription>(Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of NotificationSubscriptions
		/// </summary>
		public long CountNotificationSubscriptions()
        {
            return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NotificationSubscriptionColumns and other values
		/// </param>
        public long CountNotificationSubscriptionsWhere(WhereDelegate<NotificationSubscriptionColumns> where)
        {
            return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Count(where, Database);
        }
        
        public async Task BatchQueryNotificationSubscriptions(int batchSize, WhereDelegate<NotificationSubscriptionColumns> where, Action<IEnumerable<Bam.Net.Automation.Testing.Data.NotificationSubscription>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.NotificationSubscription>(batch));
            }, Database);
        }
		
        public async Task BatchAllNotificationSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.Automation.Testing.Data.NotificationSubscription>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.NotificationSubscription>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.Testing.Data.TestDefinition GetOneTestDefinitionWhere(WhereDelegate<TestDefinitionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestDefinition>();
			return (Bam.Net.Automation.Testing.Data.TestDefinition)Bam.Net.Automation.Testing.Data.Dao.TestDefinition.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single TestDefinition instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestDefinitionColumns and other values
		/// </param>
		public Bam.Net.Automation.Testing.Data.TestDefinition OneTestDefinitionWhere(WhereDelegate<TestDefinitionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestDefinition>();
            return (Bam.Net.Automation.Testing.Data.TestDefinition)Bam.Net.Automation.Testing.Data.Dao.TestDefinition.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.Testing.Data.TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.Testing.Data.TestDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestDefinition> TestDefinitionsWhere(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestDefinition>(Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestDefinition> TopTestDefinitionsWhere(int count, WhereDelegate<TestDefinitionColumns> where)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestDefinition>(Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestDefinitions
		/// </summary>
		public long CountTestDefinitions()
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestDefinitionColumns and other values
		/// </param>
        public long CountTestDefinitionsWhere(WhereDelegate<TestDefinitionColumns> where)
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Count(where, Database);
        }
        
        public async Task BatchQueryTestDefinitions(int batchSize, WhereDelegate<TestDefinitionColumns> where, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestDefinition.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestDefinition>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestDefinitions(int batchSize, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestDefinition.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestDefinition>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.Testing.Data.TestExecution GetOneTestExecutionWhere(WhereDelegate<TestExecutionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestExecution>();
			return (Bam.Net.Automation.Testing.Data.TestExecution)Bam.Net.Automation.Testing.Data.Dao.TestExecution.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single TestExecution instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		public Bam.Net.Automation.Testing.Data.TestExecution OneTestExecutionWhere(WhereDelegate<TestExecutionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestExecution>();
            return (Bam.Net.Automation.Testing.Data.TestExecution)Bam.Net.Automation.Testing.Data.Dao.TestExecution.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.Testing.Data.TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.Testing.Data.TestExecutionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestExecution> TestExecutionsWhere(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestExecution>(Bam.Net.Automation.Testing.Data.Dao.TestExecution.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestExecution> TopTestExecutionsWhere(int count, WhereDelegate<TestExecutionColumns> where)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestExecution>(Bam.Net.Automation.Testing.Data.Dao.TestExecution.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestExecutions
		/// </summary>
		public long CountTestExecutions()
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
        public long CountTestExecutionsWhere(WhereDelegate<TestExecutionColumns> where)
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Count(where, Database);
        }
        
        public async Task BatchQueryTestExecutions(int batchSize, WhereDelegate<TestExecutionColumns> where, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestExecution>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestExecution.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestExecution>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestExecutions(int batchSize, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestExecution>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestExecution.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestExecution>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.Testing.Data.TestSuiteDefinition GetOneTestSuiteDefinitionWhere(WhereDelegate<TestSuiteDefinitionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>();
			return (Bam.Net.Automation.Testing.Data.TestSuiteDefinition)Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single TestSuiteDefinition instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestSuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteDefinitionColumns and other values
		/// </param>
		public Bam.Net.Automation.Testing.Data.TestSuiteDefinition OneTestSuiteDefinitionWhere(WhereDelegate<TestSuiteDefinitionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>();
            return (Bam.Net.Automation.Testing.Data.TestSuiteDefinition)Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.Testing.Data.TestSuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.Testing.Data.TestSuiteDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteDefinition> TestSuiteDefinitionsWhere(WhereDelegate<TestSuiteDefinitionColumns> where, OrderBy<TestSuiteDefinitionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>(Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a TestSuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteDefinition> TopTestSuiteDefinitionsWhere(int count, WhereDelegate<TestSuiteDefinitionColumns> where)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>(Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestSuiteDefinitions
		/// </summary>
		public long CountTestSuiteDefinitions()
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestSuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteDefinitionColumns and other values
		/// </param>
        public long CountTestSuiteDefinitionsWhere(WhereDelegate<TestSuiteDefinitionColumns> where)
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Count(where, Database);
        }
        
        public async Task BatchQueryTestSuiteDefinitions(int batchSize, WhereDelegate<TestSuiteDefinitionColumns> where, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestSuiteDefinitions(int batchSize, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestSuiteDefinition>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary GetOneTestSuiteExecutionSummaryWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>();
			return (Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary)Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single TestSuiteExecutionSummary instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestSuiteExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteExecutionSummaryColumns and other values
		/// </param>
		public Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary OneTestSuiteExecutionSummaryWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>();
            return (Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary)Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummaryColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary> TestSuiteExecutionSummariesWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>(Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a TestSuiteExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteExecutionSummaryColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary> TopTestSuiteExecutionSummariesWhere(int count, WhereDelegate<TestSuiteExecutionSummaryColumns> where)
        {
            return Wrap<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>(Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestSuiteExecutionSummaries
		/// </summary>
		public long CountTestSuiteExecutionSummaries()
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestSuiteExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestSuiteExecutionSummaryColumns and other values
		/// </param>
        public long CountTestSuiteExecutionSummariesWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where)
        {
            return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Count(where, Database);
        }
        
        public async Task BatchQueryTestSuiteExecutionSummaries(int batchSize, WhereDelegate<TestSuiteExecutionSummaryColumns> where, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestSuiteExecutionSummaries(int batchSize, Action<IEnumerable<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>> batchProcessor)
        {
            await Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary>(batch));
            }, Database);
        }
	}
}																								
