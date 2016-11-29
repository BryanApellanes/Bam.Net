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
using Bam.Net.Automation.SourceControl;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        public const string TFSServer = "http://tfs.bamapps.com:8080/tfs";
        public const string TeamProjectCollection = "ISDEV";

        [Serializable]
        public class TestMessage
        {
            public string Name { get; set; }
            public bool IsMonkey { get; set; }
        }
        string messageName = "test";

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
            HashSet<GitLog> logs = GitLog.GetSinceVersion("c:\\src\\Bam.Net", 1, 4, 2);
            foreach(GitLog log in logs)
            {
                OutLine(log.PropertiesToString(), ConsoleColor.Cyan);
            }
        }
    }
}
