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
using Bam.Net.Automation.TestReporting.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao.Repository
{
	[Serializable]
	public class TestReportingRepository: DaoRepository
	{
		public TestReportingRepository()
		{
			SchemaName = "TestReporting";
			BaseNamespace = "Bam.Net.Automation.TestReporting.Data";			
﻿			
			AddType<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>();﻿			
			AddType<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>();﻿			
			AddType<Bam.Net.Automation.TestReporting.Data.TestDefinition>();﻿			
			AddType<Bam.Net.Automation.TestReporting.Data.TestExecution>();﻿			
			AddType<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>();
			DaoAssembly = typeof(TestReportingRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(TestReportingRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.TestReporting.Data.NotificationSubscription GetOneNotificationSubscriptionWhere(WhereDelegate<NotificationSubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>();
			return (Bam.Net.Automation.TestReporting.Data.NotificationSubscription)Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single NotificationSubscription instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NotificationSubscriptionColumns and other values
		/// </param>
		public Bam.Net.Automation.TestReporting.Data.NotificationSubscription OneNotificationSubscriptionWhere(WhereDelegate<NotificationSubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>();
            return (Bam.Net.Automation.TestReporting.Data.NotificationSubscription)Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.TestReporting.Data.NotificationSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.TestReporting.Data.NotificationSubscriptionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.NotificationSubscription> NotificationSubscriptionsWhere(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>(Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.NotificationSubscription> TopNotificationSubscriptionsWhere(int count, WhereDelegate<NotificationSubscriptionColumns> where)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>(Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of NotificationSubscriptions
		/// </summary>
		public long CountNotificationSubscriptions()
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Count(Database);
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
            return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Count(where, Database);
        }
        
        public async Task BatchQueryNotificationSubscriptions(int batchSize, WhereDelegate<NotificationSubscriptionColumns> where, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>(batch));
            }, Database);
        }
		
        public async Task BatchAllNotificationSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.NotificationSubscription>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.TestReporting.Data.SuiteDefinition GetOneSuiteDefinitionWhere(WhereDelegate<SuiteDefinitionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>();
			return (Bam.Net.Automation.TestReporting.Data.SuiteDefinition)Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SuiteDefinition instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SuiteDefinitionColumns and other values
		/// </param>
		public Bam.Net.Automation.TestReporting.Data.SuiteDefinition OneSuiteDefinitionWhere(WhereDelegate<SuiteDefinitionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>();
            return (Bam.Net.Automation.TestReporting.Data.SuiteDefinition)Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.TestReporting.Data.SuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.TestReporting.Data.SuiteDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.SuiteDefinition> SuiteDefinitionsWhere(WhereDelegate<SuiteDefinitionColumns> where, OrderBy<SuiteDefinitionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a SuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SuiteDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.SuiteDefinition> TopSuiteDefinitionsWhere(int count, WhereDelegate<SuiteDefinitionColumns> where)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of SuiteDefinitions
		/// </summary>
		public long CountSuiteDefinitions()
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SuiteDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SuiteDefinitionColumns and other values
		/// </param>
        public long CountSuiteDefinitionsWhere(WhereDelegate<SuiteDefinitionColumns> where)
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Count(where, Database);
        }
        
        public async Task BatchQuerySuiteDefinitions(int batchSize, WhereDelegate<SuiteDefinitionColumns> where, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>(batch));
            }, Database);
        }
		
        public async Task BatchAllSuiteDefinitions(int batchSize, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.SuiteDefinition>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.TestReporting.Data.TestDefinition GetOneTestDefinitionWhere(WhereDelegate<TestDefinitionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestDefinition>();
			return (Bam.Net.Automation.TestReporting.Data.TestDefinition)Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestDefinition instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestDefinitionColumns and other values
		/// </param>
		public Bam.Net.Automation.TestReporting.Data.TestDefinition OneTestDefinitionWhere(WhereDelegate<TestDefinitionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestDefinition>();
            return (Bam.Net.Automation.TestReporting.Data.TestDefinition)Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.TestReporting.Data.TestDefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.TestReporting.Data.TestDefinitionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestDefinition> TestDefinitionsWhere(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestDefinition>(Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestDefinition> TopTestDefinitionsWhere(int count, WhereDelegate<TestDefinitionColumns> where)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestDefinition>(Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestDefinitions
		/// </summary>
		public long CountTestDefinitions()
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Count(Database);
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
            return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Count(where, Database);
        }
        
        public async Task BatchQueryTestDefinitions(int batchSize, WhereDelegate<TestDefinitionColumns> where, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestDefinition>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestDefinitions(int batchSize, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestDefinition>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestDefinition>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.TestReporting.Data.TestExecution GetOneTestExecutionWhere(WhereDelegate<TestExecutionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestExecution>();
			return (Bam.Net.Automation.TestReporting.Data.TestExecution)Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestExecution instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		public Bam.Net.Automation.TestReporting.Data.TestExecution OneTestExecutionWhere(WhereDelegate<TestExecutionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestExecution>();
            return (Bam.Net.Automation.TestReporting.Data.TestExecution)Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.TestReporting.Data.TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.TestReporting.Data.TestExecutionColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecution> TestExecutionsWhere(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestExecution>(Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Where(where, orderBy, Database));
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
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecution> TopTestExecutionsWhere(int count, WhereDelegate<TestExecutionColumns> where)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestExecution>(Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestExecutions
		/// </summary>
		public long CountTestExecutions()
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Count(Database);
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
            return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Count(where, Database);
        }
        
        public async Task BatchQueryTestExecutions(int batchSize, WhereDelegate<TestExecutionColumns> where, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecution>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestExecution>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestExecutions(int batchSize, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecution>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestExecution>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Automation.TestReporting.Data.TestExecutionSummary GetOneTestExecutionSummaryWhere(WhereDelegate<TestExecutionSummaryColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>();
			return (Bam.Net.Automation.TestReporting.Data.TestExecutionSummary)Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestExecutionSummary instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionSummaryColumns and other values
		/// </param>
		public Bam.Net.Automation.TestReporting.Data.TestExecutionSummary OneTestExecutionSummaryWhere(WhereDelegate<TestExecutionSummaryColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>();
            return (Bam.Net.Automation.TestReporting.Data.TestExecutionSummary)Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Automation.TestReporting.Data.TestExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Automation.TestReporting.Data.TestExecutionSummaryColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary> TestExecutionSummariesWhere(WhereDelegate<TestExecutionSummaryColumns> where, OrderBy<TestExecutionSummaryColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>(Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionSummaryColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary> TopTestExecutionSummariesWhere(int count, WhereDelegate<TestExecutionSummaryColumns> where)
        {
            return Wrap<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>(Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of TestExecutionSummaries
		/// </summary>
		public long CountTestExecutionSummaries()
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionSummaryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionSummaryColumns and other values
		/// </param>
        public long CountTestExecutionSummariesWhere(WhereDelegate<TestExecutionSummaryColumns> where)
        {
            return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Count(where, Database);
        }
        
        public async Task BatchQueryTestExecutionSummaries(int batchSize, WhereDelegate<TestExecutionSummaryColumns> where, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>(batch));
            }, Database);
        }
		
        public async Task BatchAllTestExecutionSummaries(int batchSize, Action<IEnumerable<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>> batchProcessor)
        {
            await Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Automation.TestReporting.Data.TestExecutionSummary>(batch));
            }, Database);
        }
	}
}																								
