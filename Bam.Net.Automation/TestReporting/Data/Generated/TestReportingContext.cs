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

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
	// schema = TestReporting 
    public static class TestReportingContext
    {
		public static string ConnectionName
		{
			get
			{
				return "TestReporting";
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
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Where(where, db);
			}
		   
			public NotificationSubscriptionCollection Where(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Where(where, orderBy, db);
			}

			public NotificationSubscription OneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.OneWhere(where, db);
			}

			public static NotificationSubscription GetOneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.GetOneWhere(where, db);
			}
		
			public NotificationSubscription FirstOneWhere(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.FirstOneWhere(where, db);
			}

			public NotificationSubscriptionCollection Top(int count, WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Top(count, where, db);
			}

			public NotificationSubscriptionCollection Top(int count, WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<NotificationSubscriptionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.NotificationSubscription.Count(where, db);
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
	public class SuiteDefinitionQueryContext
	{
			public SuiteDefinitionCollection Where(WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Where(where, db);
			}
		   
			public SuiteDefinitionCollection Where(WhereDelegate<SuiteDefinitionColumns> where, OrderBy<SuiteDefinitionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Where(where, orderBy, db);
			}

			public SuiteDefinition OneWhere(WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.OneWhere(where, db);
			}

			public static SuiteDefinition GetOneWhere(WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.GetOneWhere(where, db);
			}
		
			public SuiteDefinition FirstOneWhere(WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.FirstOneWhere(where, db);
			}

			public SuiteDefinitionCollection Top(int count, WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Top(count, where, db);
			}

			public SuiteDefinitionCollection Top(int count, WhereDelegate<SuiteDefinitionColumns> where, OrderBy<SuiteDefinitionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SuiteDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Count(where, db);
			}
	}

	static SuiteDefinitionQueryContext _suiteDefinitions;
	static object _suiteDefinitionsLock = new object();
	public static SuiteDefinitionQueryContext SuiteDefinitions
	{
		get
		{
			return _suiteDefinitionsLock.DoubleCheckLock<SuiteDefinitionQueryContext>(ref _suiteDefinitions, () => new SuiteDefinitionQueryContext());
		}
	}
	public class TestDefinitionQueryContext
	{
			public TestDefinitionCollection Where(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Where(where, db);
			}
		   
			public TestDefinitionCollection Where(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Where(where, orderBy, db);
			}

			public TestDefinition OneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.OneWhere(where, db);
			}

			public static TestDefinition GetOneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.GetOneWhere(where, db);
			}
		
			public TestDefinition FirstOneWhere(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.FirstOneWhere(where, db);
			}

			public TestDefinitionCollection Top(int count, WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Top(count, where, db);
			}

			public TestDefinitionCollection Top(int count, WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestDefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestDefinition.Count(where, db);
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
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Where(where, db);
			}
		   
			public TestExecutionCollection Where(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Where(where, orderBy, db);
			}

			public TestExecution OneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.OneWhere(where, db);
			}

			public static TestExecution GetOneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.GetOneWhere(where, db);
			}
		
			public TestExecution FirstOneWhere(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.FirstOneWhere(where, db);
			}

			public TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Top(count, where, db);
			}

			public TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestExecutionColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecution.Count(where, db);
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
	public class TestExecutionSummaryQueryContext
	{
			public TestExecutionSummaryCollection Where(WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Where(where, db);
			}
		   
			public TestExecutionSummaryCollection Where(WhereDelegate<TestExecutionSummaryColumns> where, OrderBy<TestExecutionSummaryColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Where(where, orderBy, db);
			}

			public TestExecutionSummary OneWhere(WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.OneWhere(where, db);
			}

			public static TestExecutionSummary GetOneWhere(WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.GetOneWhere(where, db);
			}
		
			public TestExecutionSummary FirstOneWhere(WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.FirstOneWhere(where, db);
			}

			public TestExecutionSummaryCollection Top(int count, WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Top(count, where, db);
			}

			public TestExecutionSummaryCollection Top(int count, WhereDelegate<TestExecutionSummaryColumns> where, OrderBy<TestExecutionSummaryColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestExecutionSummaryColumns> where, Database db = null)
			{
				return Bam.Net.Automation.TestReporting.Data.Dao.TestExecutionSummary.Count(where, db);
			}
	}

	static TestExecutionSummaryQueryContext _testExecutionSummaries;
	static object _testExecutionSummariesLock = new object();
	public static TestExecutionSummaryQueryContext TestExecutionSummaries
	{
		get
		{
			return _testExecutionSummariesLock.DoubleCheckLock<TestExecutionSummaryQueryContext>(ref _testExecutionSummaries, () => new TestExecutionSummaryQueryContext());
		}
	}    }
}																								
