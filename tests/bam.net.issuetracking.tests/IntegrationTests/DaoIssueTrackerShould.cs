using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.IssueTracking.Data;
using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.IssueTracking.Tests
{
    [Serializable]
    public class DaoIssueTrackerShould : CommandLineTool
    {
        [ConsoleAction]
        [IntegrationTest]
        public async Task CreateIssue()
        {
            After.Setup(setupContext => 
            {
                SetupTest(setupContext, $"{nameof(CreateIssue)}_Test");
            })
            .WhenA<DaoIssueTracker>("creates an issue", (daoIssueTracker) => 
            {
                return daoIssueTracker.CreateIssueAsync("test Issue Id", "Test Issue Title", "Test Issue Body").Result;
            })
            .TheTest
            .ShouldPass((because, objectUnderTest) =>
            {
                objectUnderTest.IsCastableAs<DaoIssueTracker>();
                objectUnderTest.GetTypeReturns<DaoIssueTracker>();
                because.ResultIs<IssueData>();
                
                because.IllLookAtTheResult();
                //because.ResultIs<>
                //because.ItsTrue("the response was not successful", !result.Success, "request should have failed");
                //because.ItsTrue("the message says 'You must be logged in to do that'", result.Message.Equals("You must be logged in to do that"));
                //because.IllLookAtIt(result.Message);
            })
            .SoBeHappy()
            .UnlessItFailed();

            DaoIssueTracker daoIssueTracker = GetServiceRegistry($"{nameof(CreateIssue)}_Test").Get<DaoIssueTracker>();
            ITrackedIssue issueData = await daoIssueTracker.CreateIssueAsync("test Issue Id", "Test Issue Title", "Test Issue Body");
            Expect.IsNotNull(issueData, "issue was null");
        }

        private void SetupTest(SetupContext setupContext, string testName, IServiceLevelAgreementProvider serviceLevelAgreementProvider = null)
        {
            setupContext.CombineWith(GetServiceRegistry(testName));
        }

        private ServiceRegistry GetServiceRegistry(string testName, IServiceLevelAgreementProvider serviceLevelAgreementProvider = null)
        {
            serviceLevelAgreementProvider = serviceLevelAgreementProvider ?? GetMockServiceLevelAgreementProvider();
            return new ServiceRegistry()
                .For<Database>().Use(new SQLiteDatabase(new FileInfo($"./{testName}_Test.sqlite")))
                .For<ILogger>().Use<ConsoleLogger>()
                .For<IServiceLevelAgreementProvider>().Use(serviceLevelAgreementProvider);
        }

        private IServiceLevelAgreementProvider GetMockServiceLevelAgreementProvider(bool slaWasMet = true)
        {
            IServiceLevelAgreementProvider serviceLevelAgreementProvider = Substitute.For<IServiceLevelAgreementProvider>();
            serviceLevelAgreementProvider.SlaWasMet(Arg.Any<ITrackedIssue>()).Returns(slaWasMet);
            return serviceLevelAgreementProvider;
        }
    }
}
