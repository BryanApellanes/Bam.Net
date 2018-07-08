using Bam.Net.Application;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices.WebHooks;
using Bam.Net.CoreServices.WebHooks.Data;
using Bam.Net.CoreServices.WebHooks.Data.Dao.Repository;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class WebHooksUnitTests : CommandLineTestInterface    
    {
        [UnitTest]
        public void CanSubscribeWebHook()
        {
            string testWebHookName = "TestWebHook";
            string testUrl = "testurl";
            WebHookService svc = new WebHookService(new WebHooksRepository(), new Data.Repositories.DaoRepository(), new Server.AppConf());

            CoreServiceResponse response = svc.SubscribeToWebHook(testWebHookName, testUrl);
            Expect.IsTrue(response.Success, "subscribe to webhook failed");

            Expect.AreEqual(testWebHookName, response.Data.WebHookName);
            Expect.AreEqual(testUrl, response.Data.Url);
        }

        [UnitTest]
        public void CanListWebHookSubscribers()
        {
            string testWebHookName = "TestWebHookName";
            string[] testWebHookUrls = new string[] { 8.RandomLetters(), 6.RandomLetters(), 4.RandomLetters() };
            WebHookService svc = new WebHookService(new WebHooksRepository(), new Data.Repositories.DaoRepository(), new Server.AppConf());

            foreach (string testWebHookUrl in testWebHookUrls)
            {
                svc.SubscribeToWebHook(testWebHookName, testWebHookUrl);
            }

            CoreServiceResponse<WebHookSubscriptionInfo> response = svc.ListSubscribers(testWebHookName);
            OutLine(response.Data.ToStrin());
        }

        [AfterEachUnitTest]
        public void Cleanup()
        {
            WebHooksRepository repo = new WebHooksRepository();
            WebHooks.Data.Dao.WebHookCall.LoadAll().Delete(repo.Database);
            WebHooks.Data.Dao.WebHookSubscriber.LoadAll().Delete(repo.Database);
            WebHooks.Data.Dao.WebHookDescriptor.LoadAll().Delete(repo.Database);
            
        }
    }
}
