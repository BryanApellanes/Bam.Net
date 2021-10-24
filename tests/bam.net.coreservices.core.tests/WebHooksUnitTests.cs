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
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class WebHooksUnitTests : CommandLineTool
    {
        private static HashSet<Database> _databases = new HashSet<Database>();
        [UnitTest]
        public void CanSaveWebHookSubscriberDao()
        {
            WebHooks.Data.Dao.WebHookSubscriber dao = new WebHooks.Data.Dao.WebHookSubscriber
            {
                Url = "test"
            };
            SQLiteDatabase db = new SQLiteDatabase(DataProvider.Current.AppDataDirectory, nameof(CanSaveWebHookSubscriberDao));
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
        public void CanGetWebHookDescriptor()
        {
            string testWebHookName = $"{nameof(CanSubscribeWebHook)}_testWebHook";
            WebHookService svc = GetTestWebHookService();

            CoreServiceResponse<WebHookSubscriber> subscribeToWebHookResponse = svc.SubscribeToWebHook(testWebHookName, "TestWebHookUrl");
            
            WebHookDescriptor[] descriptors = svc.WebHooksRepository.Query<WebHookDescriptor>(q => q.WebHookName == testWebHookName).ToArray();
            Expect.IsTrue(descriptors.Length == 1, "Failed to retrieve webhook descriptor");
        }

        [UnitTest]
        public void CanListWebHookSubscribers()
        {
            string testWebHookName = "TestWebHookName";
            string[] testWebHookUrls = new string[] { 8.RandomLetters(), 6.RandomLetters(), 4.RandomLetters() };
            WebHookService svc = GetTestWebHookService();

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
            DeleteWebHookData(repo.Database);
            _databases.Each(DeleteWebHookData);
        }

        private static void DeleteWebHookData(Database db)
        {
            Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.LoadAll(db).Delete();
            Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.LoadAll(db).Delete();
            Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.LoadAll(db).Delete();
            Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.LoadAll(db).Delete();
        }
        
        private static WebHookService GetTestWebHookService()
        {
            WebHooksRepository webHooksRepository = new WebHooksRepository();
            WebHookService svc = new WebHookService(webHooksRepository, new Data.Repositories.DaoRepository(), new Server.AppConf());
            Database db = webHooksRepository.Database;
            DeleteWebHookData(db);
            _databases.Add(db);
            return svc;
        }
    }
}
