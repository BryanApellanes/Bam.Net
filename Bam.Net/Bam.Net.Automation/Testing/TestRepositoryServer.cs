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
using Bam.Net.Testing.Repository.Data;

namespace Bam.Net.Testing.Repository
{
	[Proxy("testRepo", MethodCase = MethodCase.CamelCase)]
	public class TestRepositoryServer: Loggable, IRequiresHttpContext
	{
		DaoRepository _repository;
		public TestRepositoryServer()
		{
			string dataDirectory = DefaultConfiguration.GetAppSetting("TestResultsDataDirectory", "C:\\BamContent\\apps\\hugh\\data\\");
			Database db = new SQLiteDatabase(dataDirectory, "TestResults");
			_repository = new DaoRepository(db);
			_repository.SchemaWarning += (s, e) =>
			{
				Log.AddEntry("SchemaWarning: {0}", LogEventType.Warning, e.TryPropertiesToString());
			};
			_repository.CreateFailed += (s, e) =>
			{
				Log.AddEntry("CreateFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};
			_repository.RetrieveFailed += (s, e) =>
			{
				Log.AddEntry("RetrieveFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};
			_repository.UpdateFailed += (s, e) =>
			{
				Log.AddEntry("UpdateFailed: {0}", LogEventType.Error, e.TryPropertiesToString());
			};

			AddStorableTypes(_repository);
			_repository.EnsureDaoAssembly();
			this.Repository = _repository;
		}
		
		public string DataFile
		{
			get
			{
				DaoRepository repo = Repository as DaoRepository;
				if (repo != null)
				{
					SQLiteDatabase db = repo.Database as SQLiteDatabase;
					if (db != null)
					{
						return db.DatabaseFile.FullName;
					}
				}

				return string.Empty;
			}
		}

		public override void Subscribe(ILogger logger)
		{
			_repository.Subscribe(logger);
		}

		public override void Subscribe(Loggable loggable)
		{
			_repository.Subscribe(loggable);
		}

		#region handlers for client side reporter calls
		public CreateTestSummaryResponse Start()
		{
			return CreateTestSummary();
		}

		public DefineSuiteResponse Suite(SuiteDefinition suite)
		{
			return GetSuiteDefinition(suite);
		}

		public SaveTestExecutionResponse Pass(int summaryId, string suiteTitle, string testTitle)
		{
			TestDefinition testDefinition = GetTestDefinition(suiteTitle, testTitle);
			TestExecution execution = new TestExecution { TestSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Pass = true };
			return SaveTestExecution(execution);
		}

		public SaveTestExecutionResponse Fail(int summaryId, string suiteTitle, string testTitle, string error)
		{
			TestDefinition testDefinition = GetTestDefinition(suiteTitle, testTitle);
			TestExecution execution = new TestExecution { TestSummaryId = summaryId, TestDefinitionId = testDefinition.Id, Pass = false, Error = error };
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
					subscription = new NotificationSubscription();
					subscription.EmailAddress = emailAddress;
				}

				subscription.IsActive = true;
				subscription = Repository.Save<NotificationSubscription>(subscription);

				return new NotificationSubscriptionResponse { Success = true, Data = subscription, SubscriptionStatus = SubscriptionStatus.Active, Uuid = subscription.Uuid };
			}
			catch (Exception ex)
			{
				return new NotificationSubscriptionResponse { Success = false, Message = ex.Message };
			}
		}

		public NotificationSubscriptionResponse UnsubscribeToNotifications(string emailAddress)
		{
			try
			{
				NotificationSubscription subscription = Repository.Query<NotificationSubscription>(Query.Where("EmailAddress") == emailAddress).FirstOrDefault();
				string uuid = string.Empty;
				SubscriptionStatus status = SubscriptionStatus.NotFound;
				if(subscription != null)
				{
					subscription.IsActive = false;
					subscription = Repository.Save<NotificationSubscription>(subscription);
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

		public CreateTestSummaryResponse CreateTestSummary()
		{
			try
			{
				TestSummary toCreate = new TestSummary();
				Meta.SetAuditFields(toCreate);
				TestSummary sum = Repository.Create<TestSummary>(toCreate);
				return new CreateTestSummaryResponse { Success = true, Data = sum };
			}
			catch (Exception ex)
			{
				return new CreateTestSummaryResponse { Success = false, Message = ex.Message };
			}
		}

		public DefineSuiteResponse GetSuiteDefinition(SuiteDefinition suite)
		{
			try
			{
				SuiteDefinition result = GetOrCreateSuiteDefinition(suite);
				return new DefineSuiteResponse { Success = true, Data = result };
			}
			catch (Exception ex)
			{
				return new DefineSuiteResponse { Success = false, Message = ex.Message };
			}
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
				return new RetrieveTestExecutionResponse { Success = true, Data = retrieved };
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

		protected internal DaoRepository Repository { get; set; }

		private static void AddStorableTypes(IRepository repository)
		{
			repository.AddNamespace(typeof(SuiteDefinition).Assembly, "Bam.Net.Testing.Repository.Data");	
			
		}
		private TestDefinition GetTestDefinition(string suiteTitle, string testTitle)
		{
			SuiteDefinition suite = Repository.Query<SuiteDefinition>(Query.Where("Title") == suiteTitle).FirstOrDefault();
			if (suite == null)
			{
				suite = GetOrCreateSuiteDefinition(new SuiteDefinition { Title = suiteTitle });
			}

			TestDefinition result = Repository.Query<TestDefinition>(Query.Where("Title") == testTitle && Query.Where("SuiteDefinitionId") == suite.Id).FirstOrDefault();
			if (result == null)
			{
				result = new TestDefinition();
				result.Title = testTitle;
				result.SuiteDefinitionId = suite.Id;
				Meta.SetAuditFields(result);
				result = Repository.Create(result);
			}
			return result;
		}
		
		private SuiteDefinition GetOrCreateSuiteDefinition(SuiteDefinition suite)
		{
			SuiteDefinition result = null;
			if (!string.IsNullOrEmpty(suite.Uuid))
			{
				result = (SuiteDefinition)Repository.Retrieve(typeof(SuiteDefinition), suite.Uuid);
			}
			if (result == null && suite.Id > 0)
			{
				result = Repository.Retrieve<SuiteDefinition>(suite.Id);
			}
			if (result == null)
			{
				result = Repository.Query<SuiteDefinition>(Query.Where("Title") == suite.Title).FirstOrDefault();
				if (result != null)
				{
					result = (SuiteDefinition)Repository.Retrieve(typeof(SuiteDefinition), result.Uuid);
				}
			}
			if (result == null)
			{
				Meta.SetAuditFields(suite);
				result = Repository.Create<SuiteDefinition>(suite);
			}
			return result;
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
