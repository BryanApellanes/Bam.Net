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
using Bam.Net.CoreServices;

namespace Bam.Net.Automation.Testing
{
    [Proxy("testReportSvc", MethodCase = MethodCase.CamelCase)]
    public partial class TestReportService : ProxyableService, IRequiresHttpContext, ITestReportService
    {
        protected TestReportService() : this(new SQLiteDatabaseProvider(DefaultDataDirectoryProvider.Current.GetSysDatabaseDirectory().FullName, Log.Default), Log.Default)
        {
        }

        public TestReportService(IDatabaseProvider dbProvider, ILogger logger = null)
        {
            DatabaseProvider = dbProvider;
            logger = logger ?? Log.Default;
            TestingRepository repo = new TestingRepository();
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
            TestingRepository = repo;
        }

        [Local]
        public override object Clone()
        {
            TestReportService clone = new TestReportService(DatabaseProvider, Logger);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
        
        public override void Subscribe(ILogger logger)
        {
            TestingRepository.Subscribe(logger);
        }

        public override void Subscribe(Loggable loggable)
        {
            TestingRepository.Subscribe(loggable);
        }

        #region handlers for client side reporter calls
        public virtual SaveTestSuiteExecutionSummaryResponse Start()
        {
            return SaveTestSuiteExecutionSummary();
        }

        public virtual GetSuiteDefinitionResponse Suite(TestSuiteDefinition suite)
        {
            return GetSuiteDefinition(suite.Title);
        }

        public virtual SaveTestExecutionResponse Pass(uint summaryId, string suiteTitle, string testTitle)
        {
            TestDefinition testDefinition = GetOrCreateTestDefinition(suiteTitle, testTitle);
            TestExecution execution = new TestExecution { TestSuiteExecutionSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Passed = true };
            return SaveTestExecution(execution);
        }

        public virtual SaveTestExecutionResponse Fail(uint summaryId, string suiteTitle, string testTitle, string error)
        {
            TestDefinition testDefinition = GetOrCreateTestDefinition(suiteTitle, testTitle);
            TestExecution execution = new TestExecution { TestSuiteExecutionSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Passed = false, Exception = error };
            return SaveTestExecution(execution);
        }

        #endregion

        public virtual NotificationSubscriptionResponse SubscribeToNotifications(string emailAddress)
        {
            try
            {
                NotificationSubscription subscription = TestingRepository.Query<NotificationSubscription>(Query.Where(nameof(NotificationSubscription.EmailAddress)) == emailAddress).FirstOrDefault();
                if (subscription == null)
                {
                    subscription = new NotificationSubscription()
                    {
                        EmailAddress = emailAddress
                    };
                }

                subscription.IsActive = true;
                subscription = TestingRepository.Save(subscription);

                return new NotificationSubscriptionResponse { Success = true, Data = subscription, SubscriptionStatus = SubscriptionStatus.Active, Uuid = subscription.Uuid };
            }
            catch (Exception ex)
            {
                return new NotificationSubscriptionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual NotificationSubscriptionResponse UnsubscribeFromNotifications(string emailAddress)
        {
            try
            {
                NotificationSubscription subscription = TestingRepository.Query<NotificationSubscription>(Query.Where(nameof(NotificationSubscription.EmailAddress)) == emailAddress).FirstOrDefault();
                string uuid = string.Empty;
                SubscriptionStatus status = SubscriptionStatus.NotFound;
                if (subscription != null)
                {
                    subscription.IsActive = false;
                    subscription = TestingRepository.Save(subscription);
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

        public virtual RetrieveNotificationSubscriptionsResponse RetrieveNotificationSubscribers()
        {
            try
            {
                NotificationSubscription[] subscriptions = TestingRepository.Query<NotificationSubscription>(Query.Where("IsActive") == true).ToArray();
                return new RetrieveNotificationSubscriptionsResponse { Success = true, Data = subscriptions };
            }
            catch (Exception ex)
            {
                return new RetrieveNotificationSubscriptionsResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual SaveTestExecutionResponse StartTest(ulong executionSummaryId, ulong testDefinitionId, string tag = null)
        {
            return SaveTestExecution(new TestExecution { StartedTime = DateTime.UtcNow, TestDefinitionId = testDefinitionId, TestSuiteExecutionSummaryId = executionSummaryId, Tag = tag });
        }

        public virtual SaveTestExecutionResponse FinishTest(ulong executionId)
        {
            TestExecution execution = TestingRepository.OneTestExecutionWhere(c => c.Id == executionId);
            execution.FinishedTime = DateTime.UtcNow;
            return SaveTestExecution(execution);
        }

        public virtual SaveTestExecutionResponse SaveTestExecution(TestExecution execution)
        {
            try
            {
                Meta.SetAuditFields(execution);
                TestExecution exec = TestingRepository.Save(execution);
                return new SaveTestExecutionResponse { Success = true, Data = exec };
            }
            catch (Exception ex)
            {
                return new SaveTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        

        public virtual SearchTestExecutionResponse SearchTestExecutionsByDate(DateTime from, DateTime to)
        {
            try
            {
                List<TestExecution> results = TestingRepository.Query<TestExecution>(
                    QueryFilter.Where(nameof(TestExecution.StartedTime)) > from &&
                    QueryFilter.Where(nameof(TestExecution.StartedTime)) > to).ToList();
                return new SearchTestExecutionResponse { Success = true, Data = results };
            }
            catch (Exception ex)
            {
                return new SearchTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual SearchTestExecutionResponse SearchTestExecutionsByTestDefinitionId(ulong testId)
        {
            try
            {
                List<TestExecution> results = TestingRepository.Query<TestExecution>(
                    QueryFilter.Where(nameof(TestExecution.TestDefinitionId)) == testId).ToList();
                return new SearchTestExecutionResponse { Success = true, Data = results };
            }
            catch (Exception ex)
            {
                return new SearchTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual RetrieveTestExecutionResponse RetrieveTestExecutionById(ulong id)
        {
            try
            {
                TestExecution retrieved = TestingRepository.Retrieve<TestExecution>(id);
                return new RetrieveTestExecutionResponse { Success = true, Data = retrieved, CreateStatus = CreateStatus.Existing };
            }
            catch (Exception ex)
            {
                return new RetrieveTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual RetrieveTestExecutionResponse RetrieveTestExecutionByUuid(string uuid)
        {
            try
            {
                TestExecution queried = TestingRepository.Query<TestExecution>(Query.Where("Uuid") == uuid).FirstOrDefault();
                if (queried == null)
                {
                    Args.Throw<ArgumentException>("TestExecution with the specified Uuid was not found: {0}", uuid);
                }
                TestExecution retrieved = TestingRepository.Retrieve<TestExecution>(queried.Id);
                return new RetrieveTestExecutionResponse { Success = true, Data = retrieved };
            }
            catch (Exception ex)
            {
                return new RetrieveTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual RetrieveTestExecutionResponse RetrieveFailedTestExecutions(string tag)
        {
            try
            {
                List<TestExecution> failedTestRuns = TestingRepository.Query<TestExecution>(Filter.Where("Passed") == 0 && Filter.Where("Tag") == tag).ToList();
                return new RetrieveTestExecutionResponse
                {
                    Success = true,
                    Data = failedTestRuns.Select(te => new { te.TestDefinition.AssemblyFullName, te.TestDefinition.MethodName, te.TestDefinition.Description, te.Exception, te.StackTrace }).ToArray()
                };
            }
            catch (Exception ex)
            {
                return new RetrieveTestExecutionResponse { Success = false, Message = ex.Message };
            }
        }

        protected internal TestingRepository TestingRepository { get; set; }

        private TestDefinition GetOrCreateTestDefinition(string suiteTitle, string testTitle)
        {
            return GetOrCreateTestDefinition(suiteTitle, GetTestDefinition(suiteTitle, testTitle), out CreateStatus createStatus);
        }

        private TestDefinition GetTestDefinition(string suiteTitle, string testTitle)
        {
            TestSuiteDefinition suite = TestingRepository.Query<TestSuiteDefinition>(Query.Where(nameof(TestSuiteDefinition.Title)) == suiteTitle).FirstOrDefault();
            if (suite == null)
            {
                suite = GetOrCreateSuiteDefinition(new TestSuiteDefinition { Title = suiteTitle });
            }
            TestDefinition result = TestingRepository.Query<TestDefinition>(Query.Where(nameof(TestDefinition.Title)) == testTitle &&
                Query.Where(nameof(TestDefinition.TestSuiteDefinitionId)) == suite.Id).FirstOrDefault();
            if(result == null)
            {
                result = GetOrCreateTestDefinition(suiteTitle, new TestDefinition { Title = testTitle });
            }
            return result;
        }

        private TestDefinition GetOrCreateTestDefinition(string suiteTitle, TestDefinition testDefinition)
        {
            return GetOrCreateTestDefinition(suiteTitle, testDefinition, out CreateStatus ignore);
        }

        static object _testLock = new object();
        private TestDefinition GetOrCreateTestDefinition(string suiteTitle, TestDefinition testDefinition, out CreateStatus createStatus)
		{
            lock (_testLock)
            {
                createStatus = CreateStatus.Existing;
                TestSuiteDefinition suite = TestingRepository.Query<TestSuiteDefinition>(Query.Where(nameof(TestSuiteDefinition.Title)) == suiteTitle).FirstOrDefault();
                if (suite == null)
                {
                    suite = GetOrCreateSuiteDefinition(new TestSuiteDefinition { Title = suiteTitle });
                }
                string testTitle = testDefinition.Title;
                TestDefinition result = TestingRepository.Query<TestDefinition>(
                    Query.Where(nameof(TestDefinition.Title)) == testTitle && 
                    Query.Where(nameof(TestDefinition.TestSuiteDefinitionId)) == suite.Id).FirstOrDefault();
                if (result == null)
                {
                    result = testDefinition;
                    result.TestSuiteDefinitionId = suite.Id;
                    Meta.SetAuditFields(result);                    
                    result = TestingRepository.Create(result);
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
                    result = (TestSuiteDefinition)TestingRepository.Retrieve(typeof(TestSuiteDefinition), suite.Uuid);
                }
                if (result == null && suite.Id > 0)
                {
                    result = TestingRepository.Retrieve<TestSuiteDefinition>(suite.Id);
                }
                if (result == null)
                {
                    result = TestingRepository.Query<TestSuiteDefinition>(Query.Where("Title") == suite.Title).FirstOrDefault();
                    if (result != null)
                    {
                        result = (TestSuiteDefinition)TestingRepository.Retrieve(typeof(TestSuiteDefinition), result.Uuid);
                    }
                }
                if (result == null)
                {
                    Meta.SetAuditFields(suite);
                    result = TestingRepository.Create(suite);
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

        #endregion
    }
}
