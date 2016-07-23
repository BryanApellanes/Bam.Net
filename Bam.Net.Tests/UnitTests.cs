/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.Testing;

namespace Bam.Net.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        class TestLoggable: Loggable
        {
            [Verbosity(VerbosityLevel.Custom, MessageFormat="Name={Name}")]
            public event EventHandler TestEvent;

            public string Name
            {
                get { return "Test"; }
            }

            public void OnTestEvent()
            {
                if (TestEvent != null)
                {
                    TestEvent(this, null);
                }
            }

            public void Fire()
            {
                OnTestEvent();
            }
        }

        class TestLogger: Logger
        {
            public bool AddCalled { get; set; }

            public override void AddEntry(string messageSignature, int verbosity)
            {
                AddCalled = true;
                OutLineFormat(messageSignature, ConsoleColor.Cyan);
                OutLineFormat("Verbosity :{0}", ConsoleColor.DarkBlue, verbosity);
            }
            public override void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
            {
                AddCalled = true;
                OutLineFormat(messageSignature, ConsoleColor.Cyan, variableMessageValues);
            }
            
            public override void CommitLogEvent(LogEvent logEvent)
            {
                throw new NotImplementedException(); // no implementation necessary for this test
            }
        }

        [UnitTest]
        public void ReadUntilTest()
        {
            string first = "1234567890";
            string then = "More after that: ".RandomLetters(5);
            string toRead = $"{first}:{then}";
            string remainder;
            string read = toRead.ReadUntil(':', out remainder);
            Expect.AreEqual(first, read);
            Expect.AreEqual(then, remainder);
        }

        [UnitTest]
        public void ShouldSubscribe()
        {
            TestLoggable loggable = new TestLoggable();
            TestLogger logger = new TestLogger();
            Expect.IsFalse(logger.AddCalled);
            loggable.Subscribe(logger);
            loggable.Fire();
            Expect.IsTrue(logger.AddCalled);
        }

        [UnitTest]
        public void SetAndRemoveAttribute()
        {
            FileInfo file = new FileInfo(".\\test.txt");
            if (!file.Exists)
            {
                "test text _".RandomLetters(16).SafeWriteToFile(file.FullName);
            }

            Expect.IsFalse(file.Is(FileAttributes.ReadOnly));
            file.SetAttribute(FileAttributes.ReadOnly);
            Expect.IsTrue(file.Is(FileAttributes.ReadOnly));
            file.RemoveAttribute(FileAttributes.ReadOnly);
            Expect.IsFalse(file.Is(FileAttributes.ReadOnly));

            bool thrown = false;
            try
            {
                file.RemoveAttribute(FileAttributes.ReadOnly);
                Expect.IsFalse(file.Is(FileAttributes.ReadOnly));
            }
            catch (Exception ex)
            {
                thrown = true;
            }

            Expect.IsFalse(thrown, "Remove attribute threw exception");
        }

    }
}
