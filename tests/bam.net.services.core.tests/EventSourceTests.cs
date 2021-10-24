using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Services.DataReplication;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Services.Events;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class EventSourceTests : CommandLineTool
    {
        public class TestEventSourceLoggable : EventSourceService
        {
            public TestEventSourceLoggable(DaoRepository daoRepository, ILogger logger) : base(daoRepository, new AppConf("Test"), logger)
            {
                IHttpContext context = Substitute.For<IHttpContext>();
                context.Request = Substitute.For<IRequest>();
                context.Request.Cookies.Returns(new CookieCollection());
                context.Response = Substitute.For<IResponse>();
                context.Response.Cookies.Returns(new CookieCollection());
                HttpContext = context;
            }

            public event EventHandler TestEvent;
            public async Task Test()
            {
                await Trigger("TestEvent");
            }
        }

        [UnitTest]
        public async void CopyEventHandlersTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            TestEventSourceLoggable src2 = GetTestEventSource();
            bool? fired = false;
            int expectCount = 0;
            src.TestEvent += (o, a) => { fired = true; };
            src2.CopyEventHandlers(src);
            await src2.Test().ContinueWith(t =>
            {
                expectCount++;
                Expect.IsTrue(fired.Value);
            });
            Expect.IsTrue(expectCount == 1);
        }

        [UnitTest]
        public void GetEventSubscriptionsTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            bool? fired = false;
            src.TestEvent += (o, a) => { fired = true; };
            List<EventSubscription> subs = src.GetEventSubscriptions().ToList();
            Expect.AreEqual(1, subs.Count);
            subs.First().Invoke(src, EventArgs.Empty);
            Expect.IsTrue(fired.Value);
        }

        [UnitTest]
        public async void FireNamedEventTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            bool? fired = false;
            InProcessEvents.ClearSubscribers<TestEventSourceLoggable>("TestEvent");
            InProcessEvents.Subscribe<TestEventSourceLoggable>("TestEvent", (em, c) =>
            {
                fired = true;
            });
            await src.Test();
            Thread.Sleep(300);
            Expect.IsTrue(fired.Value);
            Message.PrintLine("fire named event test ran to completion", ConsoleColor.Green);
            Message.PrintLine("done", ConsoleColor.Green);
        }

        private static TestEventSourceLoggable GetTestEventSource()
        {
            Database db = new SQLiteDatabase("./EventSourceTests", "UserAccounts");
            db.TryEnsureSchema(typeof(UserAccounts.Data.User));
            Db.For<UserAccounts.Data.User>(db);
            TestEventSourceLoggable src = new TestEventSourceLoggable(new DaoRepository(db), new ConsoleLogger());
            return src;
        }
    }
}
