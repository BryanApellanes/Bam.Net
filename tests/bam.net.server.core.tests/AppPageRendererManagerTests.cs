using System;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class AppPageRendererManagerTests: CommandLineTool
    {
        [UnitTest]
        [TestGroup("AppPageRenderer")]
        public void CanGetAppPageRendererManagersFromServer()
        {
            BamServer server = UnitTests.CreateServer();
            
            server.Start();
            (server.Responders.Length > 0).IsTrue("No responders were loaded");
            (server.AppPageRendererManagers.Count > 0).IsTrue("No AppPageRendererManagers were loaded11");
            server.Stop();
        }
    }
}