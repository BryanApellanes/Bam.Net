/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;
using Bam.Net.ServiceProxy;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Data;
using Bam.Net.Automation.Testing;
using Bam.Net.Automation.Testing.Data;
using Bam.Net.Automation.Testing.Data.Dao.Repository;

namespace Bam.Net.Automation.Testing
{
	[Proxy("testReportSvc", MethodCase = MethodCase.CamelCase)]
	public class TestReportService : Loggable, IRequiresHttpContext, ITestReportService
    {
		public TestReportService(IDatabaseProvider dbProvider, ILogger logger = null)
		{
            DatabaseProvider = dbProvider;
            logger = logger ?? Log.Default;			
            TestReportingRepository repo = new TestReportingRepository();
            DatabaseProvider.SetDatabases(repo);
            
			repo.SchemaWarning += (s, e) =>
			{
                logger.AddEntry("SchemaWarning: {0}", LogEventType.Warning, e.TryPropertiesToString());
			};
            repo.CreateFailed += (s, e) =>
			{
                logger.AddEntry("CreateFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};
            repo.RetrieveFailed += (s, e) =>
			{
                logger.AddEntry("RetrieveFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};
            repo.UpdateFailed += (s, e) =>
			{
                logger.AddEntry("UpdateFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};

            Logger = logger;
			Repository = repo;
		}
		protected IDatabaseProvider DatabaseProvider { get; set; }
        protected ILogger Logger { get; set; }
		public Database Database { get; set; }

		public override void Subscribe(ILogger logger)
		{
			Repository.Subscribe(logger);
		}

		public override void Subscribe(Loggable loggable)
		{
			Repository.Subscribe(loggable);
		}

		#region handlers for client side reporter calls
		public SaveTestSuiteExecutionSummaryResponse Start()
		{
			return SaveTestSuiteExecutionSummary();
		}

		public GetSuiteDefinitionResponse Suite(TestSuiteDefinition suite)
		{
			return GetSuiteDefinition(suite.Title);
		}

		public SaveTestExecutionResponse Pass(int summaryId, string suiteTitle, string testTitle)
		{
			TestDefinition testDefinition = GetOrCreateTestDefinition(suiteTitle, testTitle);
			TestExecution execution = new TestExecution { TestSuiteExecutionSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Passed = true };
			return SaveTestExecution(execution);
		}

		public SaveTestExecutionResponse Fail(int summaryId, string suiteTitle, string testTitle, string error)
		{
			TestDefinition testDefinition = GetOrCreateTestDefinition(suiteTitle, testTitle);
			TestExecution execution = new TestExecution { TestSuiteExecutionSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Passed = false, Exception = error };
			return SaveTestExecution(execution);
		}

		#endregion

		public NotificationSubscriptionResponse SubscribeToNotifications(string emailAddress)
		{
			try
			{
				NotificationSubscription subscription = Repository.Query<NotificationSubscription>(Query.Where("EmailAddress") == emailAddress).FirstOrDefault();
				if (subscription == null)
				{
                    subscription = new NotificationSubscription()
                    {
                        EmailAddress = emailAddress
                    };
                }

				subscription.IsActive = true;
				subscription = Repository.Save(subscription);

				return new NotificationSubscriptionResponse { Success = true, Data = subscription, SubscriptionStatus = SubscriptionStatus.Active, Uuid = subscription.Uuid };
			}
			catch (Exception ex)
			{
				return new NotificationSubscriptionResponse { Success = false, Message = ex.Message };
			}
		}

		public NotificationSubscriptionResponse UnsubscribeFromNotifications(string emailAddress)
		{
			try
			{
				NotificationSubscription subscription = Repository.Query<NotificationSubscription>(Query.Where("EmailAddress") == emailAddress).FirstOrDefault();
				string uuid = string.Empty;
				SubscriptionStatus status = SubscriptionStatus.NotFound;
				if(subscription != null)
				{
					subscription.IsActive = false;
					subscription = Repository.Save(subscription);
					uuid = subscription.Uuid;
					status = SubscriptionStatus.NotActive;
				}
				
				return new NotificationSubscriptionResponse { Success = true, SubscriptionStatus = status, Uuid = uuid };
			}
			catch (Exception ex)
			{
				return new NotificationSubscriptionResponse { Success = false, Message = ex.Message };
			}
		}

		public RetrieveNotificationSubscriptionsResponse RetrieveNotificationSubscribers()
		{
			try
			{
				NotificationSubscription[] subscriptions = Repository.Query<NotificationSubscription>(Query.Where("IsActive") == true).ToArray();
				return new RetrieveNotificationSubscriptionsResponse { Success = true, Data = subscriptions };
			}
			catch (Exception ex)
			{
				return new RetrieveNotificationSubscriptionsResponse { Success = false, Message = ex.Message };
			}
		}

        /// <summary>
        /// Get an exisintg SuiteDefinition with the specified suiteTitle or
        /// create it if none exists
        /// </summary>
        /// <param name="suiteTitle"></param>
        /// <returns></returns>
		public GetSuiteDefinitionResponse GetSuiteDefinition(string suiteTitle)
		{
			try
			{
				TestSuiteDefinition result = GetOrCreateSuiteDefinition(new TestSuiteDefinition { Title = suiteTitle }, out CreateStatus createStatus);
                return new GetSuiteDefinitionResponse { Success = true, Data = result, CreateStatus = createStatus };
			}
			catch (Exception ex)
			{
				return new GetSuiteDefinitionResponse { Success = false, Message = ex.Message };
			}
		}

        /// <summary>
        /// Get an existing TestDefinition for the specified suiteTitle and testTitle
        /// or create it if none exists
        /// </summary>
        /// <param name="suiteTitle"></param>
        /// <param name="testTitle"></param>
        /// <returns></returns>
        public GetTestDefinitionResponse GetTestDefinition(string suiteTitle, string testTitle)
        {
            try
            {
                TestDefinition result = GetOrCreateTestDefinition(suiteTitle, testTitle, out CreateStatus createStatus);
                return new GetTestDefinitionResponse { Success = true, Data = result, CreateStatus = createStatus };
            }
            catch (Exception ex)
            {
                return new GetTestDefinitionResponse { Success = false, Message = ex.Message };
            }
        }

        public SaveTestSuiteExecutionSummaryResponse SaveTestSuiteExecutionSummary(TestSuiteExecutionSummary toCreate = null)
        {
            try
            {
                toCreate = toCreate ?? new TestSuiteExecutionSummary();
                Meta.SetAuditFields(toCreate);
                TestSuiteExecutionSummary sum = Repository.Save(toCreate);
                return new SaveTestSuiteExecutionSummaryResponse { Success = true, Data = sum, CreateStatus = toCreate.Id > 0 ? CreateStatus.Existing: CreateStatus.Created };
            }
            catch (Exception ex)
            {
                return new SaveTestSuiteExecutionSummaryResponse { Success = false, Message = ex.Message };
            }
        }

        public SaveTestExecutionResponse StartTest(long executionSummaryId, long testDefinitionId)
        {
            return SaveTestExecution(new TestExecution { StartedTime = DateTime.UtcNow, TestDefinitionId = testDefinitionId, TestSuiteExecutionSummaryId = executionSummaryId });
        }

        public SaveTestExecutionResponse FinishTest(long executionId)
        {
            TestExecution execution = Repository.OneTestExecutionWhere(c => c.Id == executionId);
            execution.FinishedTime = DateTime.UtcNow;
            return SaveTestExecution(execution);
        }

        public SaveTestExecutionResponse SaveTestExecution(TestExecution execution)
		{
			try
			{
				Meta.SetAuditFields(execution);
				TestExecution exec = Repository.Save(execution);
				return new SaveTestExecutionResponse { Success = true, Data = exec };
			}
			catch (Exception ex)
			{
				return new SaveTestExecutionResponse { Success = false, Message = ex.Message };
			}
		}

		public SearchTestExecutionResponse SearchTestExecutionsByDate(DateTime from, DateTime to)
		{
			throw new NotImplementedException();
		}

		public SearchTestExecutionResponse SearchTestExecutionsByTestDefinitionId(long testId)
		{
			throw new NotImplementedException();
		}

		public RetrieveTestExecutionResponse RetrieveTestExecutionById(long id)
		{
			try
			{
				TestExecution retrieved = Repository.Retrieve<TestExecution>(id);
				return new RetrieveTestExecutionResponse { Success = true, Data = retrieved, CreateStatus = CreateStatus.Existing };
			}
			catch (Exception ex)
			{
				return new RetrieveTestExecutionResponse { Success = false, Message = ex.Message };
			}
		}

		public RetrieveTestExecutionResponse RetrieveTestExecutionByUuid(string uuid)
		{
			try
			{
				TestExecution queried = Repository.Query<TestExecution>(Query.Where("Uuid") == uuid).FirstOrDefault();
				if (queried == null)
				{
					Args.Throw<ArgumentException>("TestExecution with the specified Uuid was not found: {0}", uuid);
				}
				TestExecution retrieved = Repository.Retrieve<TestExecution>(queried.Id);
				return new RetrieveTestExecutionResponse { Success = true, Data = retrieved };
			}
			catch (Exception ex)
			{
				return new RetrieveTestExecutionResponse { Success = false, Message = ex.Message };
			}
		}

		protected internal TestReportingRepository Repository { get; set; }

        private TestDefinition GetOrCreateTestDefinition(string suiteTitle, string testTitle)
        {
            return GetOrCreateTestDefinition(suiteTitle, testTitle, out CreateStatus createStatus);
        }

        static object _testLock = new object();
        private TestDefinition GetOrCreateTestDefinition(string suiteTitle, string testTitle, out CreateStatus createStatus)
		{
            lock (_testLock)
            {
                createStatus = CreateStatus.Existing;
                TestSuiteDefinition suite = Repository.Query<TestSuiteDefinition>(Query.Where("Title") == suiteTitle).FirstOrDefault();
                if (suite == null)
                {
                    suite = GetOrCreateSuiteDefinition(new TestSuiteDefinition { Title = suiteTitle });
                }

                TestDefinition result = Repository.Query<TestDefinition>(Query.Where("Title") == testTitle && Query.Where("SuiteDefinitionId") == suite.Id).FirstOrDefault();
                if (result == null)
                {
                    result = new TestDefinition()
                    {
                        Title = testTitle,
                        SuiteDefinitionId = suite.Id
                    };
                    Meta.SetAuditFields(result);
                    result = Repository.Create(result);
                    createStatus = CreateStatus.Created;
                }
                return result;
            }
		}

        private TestSuiteDefinition GetOrCreateSuiteDefinition(TestSuiteDefinition suite)
        {
            return GetOrCreateSuiteDefinition(suite, out CreateStatus createStatus);
        }

        static object _suiteLock = new object();
        private TestSuiteDefinition GetOrCreateSuiteDefinition(TestSuiteDefinition suite, out CreateStatus createStatus)
		{
            lock (_suiteLock)
            {
                TestSuiteDefinition result = null;
                createStatus = CreateStatus.Existing;
                if (!string.IsNullOrEmpty(suite.Uuid))
                {
                    result = (TestSuiteDefinition)Repository.Retrieve(typeof(TestSuiteDefinition), suite.Uuid);
                }
                if (result == null && suite.Id > 0)
                {
                    result = Repository.Retrieve<TestSuiteDefinition>(suite.Id);
                }
                if (result == null)
                {
                    result = Repository.Query<TestSuiteDefinition>(Query.Where("Title") == suite.Title).FirstOrDefault();
                    if (result != null)
                    {
                        result = (TestSuiteDefinition)Repository.Retrieve(typeof(TestSuiteDefinition), result.Uuid);
                    }
                }
                if (result == null)
                {
                    Meta.SetAuditFields(suite);
                    result = Repository.Create(suite);
                    createStatus = CreateStatus.Created;
                }
                return result;
            }
		}
		#region IRequiresHttpContext Members

		public IHttpContext HttpContext
		{
			get;
			set;
		}

        public object Clone()
        {
            TestReportService clone = new TestReportService(DatabaseProvider, Logger);
            clone.CopyProperties(this);
            return clone;
        }

        #endregion
    }
}
