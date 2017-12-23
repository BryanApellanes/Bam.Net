using Bam.Net.Automation.Testing.Data;
using System;

namespace Bam.Net.Automation.Testing
{
    public interface ITestReportService
    {
        GetSuiteDefinitionResponse GetSuiteDefinition(string suiteTitle);
        GetTestDefinitionResponse GetTestDefinition(string suiteTitle, TestDefinition testDefinition);
        SaveTestSuiteExecutionSummaryResponse SaveTestSuiteExecutionSummary(TestSuiteExecutionSummary suiteExecutionSummary);
        SaveTestExecutionResponse StartTest(long executionSummaryId, long testDefinitionId, string tag = null);
        SaveTestExecutionResponse SaveTestExecution(TestExecution execution);
        SaveTestExecutionResponse Fail(int summaryId, string suiteTitle, string testTitle, string error);
        SaveTestExecutionResponse Pass(int summaryId, string suiteTitle, string testTitle);
        SaveTestExecutionResponse FinishTest(long executionId);
        RetrieveNotificationSubscriptionsResponse RetrieveNotificationSubscribers();
        RetrieveTestExecutionResponse RetrieveTestExecutionById(long id);
        RetrieveTestExecutionResponse RetrieveTestExecutionByUuid(string uuid);        
        SearchTestExecutionResponse SearchTestExecutionsByDate(DateTime from, DateTime to);
        SearchTestExecutionResponse SearchTestExecutionsByTestDefinitionId(long testId);
        NotificationSubscriptionResponse SubscribeToNotifications(string emailAddress);
        NotificationSubscriptionResponse UnsubscribeFromNotifications(string emailAddress);
    }
}