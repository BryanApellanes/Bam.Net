using System;
using Bam.Net.Testing.Data;
using Bam.Net.Testing.Tracking;
using Bam.Net.Testing.Tracking.Data;

namespace Bam.Net.Testing.Tracking
{
    public interface ITestExecutionTrackerService
    {
        CreateTestExecutionSummaryResponse CreateTestExecutionSummary();
        DefineSuiteResponse DefineSuite(SuiteDefinition suite);
        SaveTestExecutionResponse Fail(int summaryId, string suiteTitle, string testTitle, string error);
        SaveTestExecutionResponse Pass(int summaryId, string suiteTitle, string testTitle);
        RetrieveNotificationSubscriptionsResponse RetrieveNotificationSubscribers();
        RetrieveTestExecutionResponse RetrieveTestExecutionById(long id);
        RetrieveTestExecutionResponse RetrieveTestExecutionByUuid(string uuid);
        SaveTestExecutionResponse SaveTestExecution(TestExecution execution);
        SearchTestExecutionResponse SearchTestExecutionsByDate(DateTime from, DateTime to);
        SearchTestExecutionResponse SearchTestExecutionsByTestDefinitionId(long testId);
        NotificationSubscriptionResponse SubscribeToNotifications(string emailAddress);
        NotificationSubscriptionResponse UnsubscribeFromNotifications(string emailAddress);
    }
}