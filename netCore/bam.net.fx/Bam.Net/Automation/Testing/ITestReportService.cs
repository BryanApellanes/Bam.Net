using Bam.Net.Automation.Testing.Data;
using System;

namespace Bam.Net.Automation.Testing
{
    public interface ITestReportService
    {
        GetSuiteDefinitionResponse GetSuiteDefinition(string suiteTitle);
        GetTestDefinitionResponse GetTestDefinition(string suiteTitle, TestDefinition testDefinition);
        SaveTestSuiteExecutionSummaryResponse SaveTestSuiteExecutionSummary(TestSuiteExecutionSummary suiteExecutionSummary);
        SaveTestExecutionResponse StartTest(ulong executionSummaryId, ulong testDefinitionId, string tag = null);
        SaveTestExecutionResponse SaveTestExecution(TestExecution execution);
        SaveTestExecutionResponse Fail(uint summaryId, string suiteTitle, string testTitle, string error);
        SaveTestExecutionResponse Pass(uint summaryId, string suiteTitle, string testTitle);
        SaveTestExecutionResponse FinishTest(ulong executionId);
        RetrieveNotificationSubscriptionsResponse RetrieveNotificationSubscribers();
        RetrieveTestExecutionResponse RetrieveTestExecutionById(ulong id);
        RetrieveTestExecutionResponse RetrieveTestExecutionByUuid(string uuid);        
        SearchTestExecutionResponse SearchTestExecutionsByDate(DateTime from, DateTime to);
        SearchTestExecutionResponse SearchTestExecutionsByTestDefinitionId(ulong testId);
        NotificationSubscriptionResponse SubscribeToNotifications(string emailAddress);
        NotificationSubscriptionResponse UnsubscribeFromNotifications(string emailAddress);
    }
}