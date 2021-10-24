using System;
using System.Linq;
using System.Threading;
using Bam.Net.Logging;
using Bam.Net.Logging.Application;
using Bam.Net.Logging.Application.Data;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    public class ActivityTests : CommandLineTool
    {
        [UnitTest]
        [TestGroup("Activity")]
        public void CanLogActivity()
        {
            AppConf appConf = new AppConf("TestApp");
            string testActivityName = "TestActivityName_".RandomLetters(8);
            Activity.Add(appConf, testActivityName, "This is a test message: {0}", "arg value" );
            Thread.Sleep(1500);
            Expect.IsGreaterThan(Activity.Retrieve(testActivityName).ToArray().Length, 0);
        }
    }
}