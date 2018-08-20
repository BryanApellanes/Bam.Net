using Bam.Net.Application;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices.WebHooks;
using Bam.Net.CoreServices.WebHooks.Data;
using Bam.Net.CoreServices.WebHooks.Data.Dao.Repository;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
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
        public void CanSaveWebHookSubscriberDao()
        {
            WebHooks.Data.Dao.WebHookSubscriber dao = new WebHooks.Data.Dao.WebHookSubscriber
            {
                Url = "test"
            };
            SQLiteDatabase db = new SQLiteDatabase(DefaultDataDirectoryProvider.Current.AppDataDirectory, nameof(CanSaveWebHookSubscriberDao));
            db.TryEnsureSchema<WebHooks.Data.Dao.WebHookSubscriber>();
            dao.Save(db);
            Expect.IsGreaterThan(dao.IdValue.Value, 0);
        }

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
            WebHookSubscriptionInfo[] info = response.ToArray<WebHookSubscriptionInfo>();
            Expect.AreEqual(3, info.Length);
            List<string> actual = info.Select(whsi => whsi.Url).ToList();
            foreach(string expected in testWebHookUrls)
            {
                Expect.IsTrue(actual.Contains(expected));
            }
        }

        [AfterEachUnitTest]
        public void Cleanup()
        {
            WebHooksRepository repo = new WebHooksRepository();
            WebHooks.Data.Dao.WebHookCall.LoadAll(repo.Database).Delete(repo.Database);
            WebHooks.Data.Dao.WebHookSubscriber.LoadAll(repo.Database).Delete(repo.Database);
            WebHooks.Data.Dao.WebHookDescriptor.LoadAll(repo.Database).Delete(repo.Database);
            WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.LoadAll(repo.Database).Delete(repo.Database);
        }
    }
}
