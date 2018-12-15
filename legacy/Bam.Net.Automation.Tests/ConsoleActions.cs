/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Automation;
using Bam.Net.Automation.Nuget;
using System.Collections.ObjectModel;
using System.Messaging;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using Bam.Net.CoreServices;
using Bam.Net.Automation.Testing;
using Bam.Net.Automation.SourceControl;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        [Serializable]
        public class TestMessage
        {
            public string Name { get; set; }
            public bool IsMonkey { get; set; }
        }
        string messageName = "test";


        [ConsoleAction]
        public void TestGetSettingsFromIntegrationServer()
        {
            ProxyFactory proxyFactory = new ProxyFactory();
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false
            };
            logger.StartLoggingThread();
            TestReportService svc = proxyFactory.GetProxy<TestReportService>("gloo.localhost", 9100, logger);
            Dictionary<string, string> settings = svc.GetSettings();
            if(settings != null)
            {
                foreach (string key in settings.Keys)
                {
                    OutLineFormat("{0}: {1}", key, settings[key]);
                }
            }
        }

        [ConsoleAction]
        public void GetGitBranch()
        {
            string dir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = "C:\\src\\Business\\Submodule\\Bam.Net";
            string currentBranch = string.Empty;
            ProcessOutput po = "git branch".Run(line =>
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("*"))
                {
                    currentBranch = trimmed.TruncateFront(1).Trim();
                }
            }, 60000);
            OutLineFormat("Current branch is: {0}", currentBranch);
            Environment.CurrentDirectory = dir;
        }

        [ConsoleAction("Start messaging")]
        public void StartMessaging()
        {
            if (IpcMessage.Exists<TestMessage>(messageName))
            {
                IpcMessage.Delete(messageName, typeof(TestMessage));
            }

            IpcMessage msg = IpcMessage.Create<TestMessage>(messageName);

            Timer timer = new Timer((o) =>
            {
                TestMessage message = new TestMessage();
                message.Name = "Name_".RandomLetters(4);
                message.IsMonkey = RandomHelper.Bool();

                OutLineFormat("Setting data to:\r\n {0}", ConsoleColor.Cyan, message.PropertiesToString());
                msg.Write(message);
            }, null, 0, 1000);

            Pause();
        }

        [ConsoleAction("Read messages")]
        public void ReadMessages()
        {
            IpcMessage msg = IpcMessage.Get<TestMessage>(messageName);

            Timer timer = new Timer((o) =>
            {
                OutLineFormat("Reading:\r\n {0}", ConsoleColor.Blue, msg.Read<TestMessage>().PropertiesToString());
            }, null, 0, 900);

            Pause();
        }

        [ConsoleAction("Test GitLog")]
        public void TestGitLog()
        {
            HashSet<GitLog> logs = GitLog.SinceVersion("c:\\bam\\src\\Bam.Net", 1, 14, 0);

            foreach(GitLog log in logs)
            {
                OutLine(log.PropertiesToString(), ConsoleColor.Cyan);
            }
        }

        [ConsoleAction("Test GitReleaseNotes")]
        public void TestGitReleaseNotes()
        {
            GitReleaseNotes notes = GitReleaseNotes.SinceLatestRelease("Bam.Net.CommandLine", "C:\\bam\\src\\Bam.Net");
            notes.Summary = "Put a nice summary here";
            OutLineFormat("{0}", ConsoleColor.Cyan, notes.Value);
        }

        [ConsoleAction("Test GitReleaseNotes since version")]
        public void TestGitReleaseNotesSinceVersion()
        {
            GitReleaseNotes notes = GitReleaseNotes.SinceVersion("Bam.Net", "C:\\bam\\src\\Bam.Net", 1, 14, 0);
            notes.Summary = "Put a nice summary here";
            OutLineFormat("{0}", ConsoleColor.Cyan, notes.Value);
        }

        [ConsoleAction("Test Misc GitReleaseNotes")]
        public void TestGitMiscReleaseNotes()
        {
            GitReleaseNotes notes = GitReleaseNotes.MiscSinceLatestRelease("C:\\bam\\src\\Bam.Net");
            notes.Summary = "Misc";
            OutLineFormat("{0}", ConsoleColor.Cyan, notes.Value);
        }

        [ConsoleAction("Test latest branch commit")]
        public void TestGitLatestBranchCommit()
        {
            Git git = new Git("C:\\bam\\src\\Bam.Net");
            OutLine(git.LatestBranchCommit("master"));
            Expect.Throws(() => git.LatestBranchCommit("badBranch"), (ex)=> OutLineFormat(ex.Message, ConsoleColor.Cyan), "Should have thrown exception");
        }
    }
}
