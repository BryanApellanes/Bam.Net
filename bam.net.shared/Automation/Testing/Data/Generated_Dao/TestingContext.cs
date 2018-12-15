/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Automation.Testing.Data.Dao
{
	// schema = Testing 
    public static class TestingContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Testing";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class NotificationSubscriptionQueryContext
	{
			public NotificationSubscriptionCollection Where(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Where(where, db);
			}
		   
			public NotificationSubscriptionCollection Where(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Where(where, orderBy, db);
			}

			public NotificationSubscription OneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.OneWhere(where, db);
			}

			public static NotificationSubscription GetOneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.GetOneWhere(where, db);
			}
		
			public NotificationSubscription FirstOneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.FirstOneWhere(where, db);
			}

			public NotificationSubscriptionCollection Top(int count, WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Top(count, where, db);
			}

			public NotificationSubscriptionCollection Top(int count, WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.NotificationSubscription.Count(where, db);
			}
	}

	static NotificationSubscriptionQueryContext _notificationSubscriptions;
	static object _notificationSubscriptionsLock = new object();
	public static NotificationSubscriptionQueryContext NotificationSubscriptions
	{
		get
		{
			return _notificationSubscriptionsLock.DoubleCheckLock<NotificationSubscriptionQueryContext>(ref _notificationSubscriptions, () => new NotificationSubscriptionQueryContext());
		}
	}
	public class TestDefinitionQueryContext
	{
			public TestDefinitionCollection Where(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Where(where, db);
			}
		   
			public TestDefinitionCollection Where(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Where(where, orderBy, db);
			}

			public TestDefinition OneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.OneWhere(where, db);
			}

			public static TestDefinition GetOneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.GetOneWhere(where, db);
			}
		
			public TestDefinition FirstOneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.FirstOneWhere(where, db);
			}

			public TestDefinitionCollection Top(int count, WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Top(count, where, db);
			}

			public TestDefinitionCollection Top(int count, WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestDefinition.Count(where, db);
			}
	}

	static TestDefinitionQueryContext _testDefinitions;
	static object _testDefinitionsLock = new object();
	public static TestDefinitionQueryContext TestDefinitions
	{
		get
		{
			return _testDefinitionsLock.DoubleCheckLock<TestDefinitionQueryContext>(ref _testDefinitions, () => new TestDefinitionQueryContext());
		}
	}
	public class TestExecutionQueryContext
	{
			public TestExecutionCollection Where(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Where(where, db);
			}
		   
			public TestExecutionCollection Where(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Where(where, orderBy, db);
			}

			public TestExecution OneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.OneWhere(where, db);
			}

			public static TestExecution GetOneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.GetOneWhere(where, db);
			}
		
			public TestExecution FirstOneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.FirstOneWhere(where, db);
			}

			public TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Top(count, where, db);
			}

			public TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestExecution.Count(where, db);
			}
	}

	static TestExecutionQueryContext _testExecutions;
	static object _testExecutionsLock = new object();
	public static TestExecutionQueryContext TestExecutions
	{
		get
		{
			return _testExecutionsLock.DoubleCheckLock<TestExecutionQueryContext>(ref _testExecutions, () => new TestExecutionQueryContext());
		}
	}
	public class TestSuiteDefinitionQueryContext
	{
			public TestSuiteDefinitionCollection Where(WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Where(where, db);
			}
		   
			public TestSuiteDefinitionCollection Where(WhereDelegate<TestSuiteDefinitionColumns> where, OrderBy<TestSuiteDefinitionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Where(where, orderBy, db);
			}

			public TestSuiteDefinition OneWhere(WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.OneWhere(where, db);
			}

			public static TestSuiteDefinition GetOneWhere(WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.GetOneWhere(where, db);
			}
		
			public TestSuiteDefinition FirstOneWhere(WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.FirstOneWhere(where, db);
			}

			public TestSuiteDefinitionCollection Top(int count, WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Top(count, where, db);
			}

			public TestSuiteDefinitionCollection Top(int count, WhereDelegate<TestSuiteDefinitionColumns> where, OrderBy<TestSuiteDefinitionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestSuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteDefinition.Count(where, db);
			}
	}

	static TestSuiteDefinitionQueryContext _testSuiteDefinitions;
	static object _testSuiteDefinitionsLock = new object();
	public static TestSuiteDefinitionQueryContext TestSuiteDefinitions
	{
		get
		{
			return _testSuiteDefinitionsLock.DoubleCheckLock<TestSuiteDefinitionQueryContext>(ref _testSuiteDefinitions, () => new TestSuiteDefinitionQueryContext());
		}
	}
	public class TestSuiteExecutionSummaryQueryContext
	{
			public TestSuiteExecutionSummaryCollection Where(WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Where(where, db);
			}
		   
			public TestSuiteExecutionSummaryCollection Where(WhereDelegate<TestSuiteExecutionSummaryColumns> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Where(where, orderBy, db);
			}

			public TestSuiteExecutionSummary OneWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.OneWhere(where, db);
			}

			public static TestSuiteExecutionSummary GetOneWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.GetOneWhere(where, db);
			}
		
			public TestSuiteExecutionSummary FirstOneWhere(WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.FirstOneWhere(where, db);
			}

			public TestSuiteExecutionSummaryCollection Top(int count, WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Top(count, where, db);
			}

			public TestSuiteExecutionSummaryCollection Top(int count, WhereDelegate<TestSuiteExecutionSummaryColumns> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestSuiteExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.Count(where, db);
			}
	}

	static TestSuiteExecutionSummaryQueryContext _testSuiteExecutionSummaries;
	static object _testSuiteExecutionSummariesLock = new object();
	public static TestSuiteExecutionSummaryQueryContext TestSuiteExecutionSummaries
	{
		get
		{
			return _testSuiteExecutionSummariesLock.DoubleCheckLock<TestSuiteExecutionSummaryQueryContext>(ref _testSuiteExecutionSummaries, () => new TestSuiteExecutionSummaryQueryContext());
		}
	}    }
}																								
