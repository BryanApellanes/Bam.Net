using Bam.Net.Automation.TestReporting.Data;
using System;

namespace Bam.Net.Automation.TestReporting
{
    public interface ITestReportService
    {
        GetSuiteDefinitionResponse GetSuiteDefinition(string suiteTitle);
        GetTestDefinitionResponse GetTestDefinition(string suiteTitle, string testTitle);
        SaveTestSuiteExecutionSummaryResponse SaveTestSuiteExecutionSummary(TestSuiteExecutionSummary suiteExecutionSummary);
        SaveTestExecutionResponse StartTest(long executionSummaryId, long testDefinitionId);
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