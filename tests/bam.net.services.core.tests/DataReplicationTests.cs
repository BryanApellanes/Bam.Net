using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.Services.DataReplication;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class DataReplicationTests: CommandLineTool
    {      
        [BeforeEachUnitTest]
        public void Setup()
        {
            Log.DebugOut = false;
            Log.TraceOut = false;
            // preinitialize stuff
            GetTestJournals();
            GetCompressionJournal<Journal>();
            GetEncryptionJournal<Journal>();
        }

        [UnitTest("Data Replication: can get type map")]
        public void CanGetTypeMap()
        {
            TypeMap typeMap = GetEncryptionJournal<TypeMap>();
            Expect.IsNotNull(typeMap, "typeMap was null");
        }

        [UnitTest("Data Replication: can get journal")]
        public void CanGetJournal()
        {
            foreach(Journal journal in GetTestJournals())
            {                
                Expect.IsNotNull(journal, "journal was null");
            }
        }

        [UnitTest("Data Replication: type map should not be null")]
        public void TypeMapShouldNotBeNull()
        {
            foreach(Journal journal in GetTestJournals())
            {                
                Expect.IsNotNull(journal.TypeMap);
            }
        }

        [UnitTest("Data Replication: Can write entries")]
        public void CanWriteEntries()
        {
            foreach(Journal journal in GetTestJournals())
            {
                DataReplicationTestClass value = GetDataInstance();

                IEnumerable<JournalEntry> entries = journal.Enqueue(value);
                WriteToConsole(journal, entries);
                value.Address = "A new Address";

                entries = journal.Enqueue(value);
                WriteToConsole(journal, entries);

                Message.PrintLine("journal directory {0}", ConsoleColor.Cyan, journal.JournalDirectory.FullName);
                Thread.Sleep(3000);
            }
        }

        private static void WriteToConsole(Journal journal, IEnumerable<JournalEntry> entries)
        {
            foreach (JournalEntry entry in entries)
            {
                Console.WriteLine("TypeId={0}, PropertyId={1}, TypeName={2}, PropertyName={3}, Value={4}",
                    entry.TypeId,
                    entry.PropertyId,
                    journal.GetTypeName(entry.TypeId),
                    journal.GetPropertyName(entry.PropertyId),
                    entry.Value);
            }
        }

        [UnitTest("Data Replication: Write events fire")]
        public void WriteEventFires()
        {
            foreach(Journal journal in GetTestJournals())
            {
                bool? entryFlushedFired = false;
                bool? queueEmptyFired = false;
                journal.EntryFlushed += (o, a) => entryFlushedFired = true;
                journal.QueueEmpty += (o, a) => queueEmptyFired = true;
                DataReplicationTestClass value = GetDataInstance();
                IEnumerable<JournalEntry> entries = journal.Enqueue(value);
                foreach (JournalEntry entry in entries)
                {
                    Console.WriteLine("TypeId={0}, PropertyId={1}, TypeName={2}, PropertyName={3}, Value={4}",
                        entry.TypeId,
                        entry.PropertyId,
                        journal.GetTypeName(entry.TypeId),
                        journal.GetPropertyName(entry.PropertyId),
                        entry.Value);
                }

                if (Exec.TakesTooLong(() =>
                {
                    Exec.SleepUntil(() => queueEmptyFired.Value);
                }, 5000))
                {
                    Expect.Fail("took too long");
                }
                Expect.IsTrue(entryFlushedFired.Value);
                Expect.IsTrue(queueEmptyFired.Value);
                Message.PrintLine("journal directory {0}", ConsoleColor.Cyan, journal.JournalDirectory.FullName);
                Thread.Sleep(3000);
            }
        }

        [UnitTest("Data Replication: can read compressed entries")]
        public void CanReadCompressed()
        {
            Journal journal = GetCompressionJournal<Journal>();
            DoReadTest(journal);
        }

        [UnitTest("Data Replication: can read encrypted entries")]
        public void CanReadEncrypted()
        {
            Journal journal = GetEncryptionJournal<Journal>();
            DoReadTest(journal);
        }

        private static void DoReadTest(Journal journal)
        {
            DataReplicationTestClass value1 = GetRandomDataInstance();
            HashSet<DataReplicationTestClass> retrieved = new HashSet<DataReplicationTestClass>();
            List<JournalEntry> entries = new List<JournalEntry>();
            AutoResetEvent blocker = new AutoResetEvent(false);
            bool? fullyFlushed = false;
            entries.AddRange(journal.Enqueue(value1, (je)=>
            {
                fullyFlushed = true;
                blocker.Set();                
            }));
            blocker.WaitOne(60000);
            DataReplicationTestClass check = journal.LoadInstance<DataReplicationTestClass>(value1.Id);
            Expect.IsNotNull(check);
            Expect.IsTrue(fullyFlushed.Value);
            Expect.AreEqual(value1.FirstName, check.FirstName);
            Expect.AreEqual(value1.LastName, check.LastName);
            Expect.AreEqual(value1.Address, check.Address);

            Thread.Sleep(3000);
        }

        [UnitTest]
        public void CompressionJournalGetsLatestPropertyValue()
        {
            Log.DebugOut = true;
            AutoResetEvent blocker = new AutoResetEvent(false);
            DataReplicationTestClass value = GetRandomDataInstance();
            Journal journal = GetCompressionJournal<Journal>();
            journal.Logger = new ConsoleLogger();
            bool? checkedJournal = false;
            journal.Enqueue(value, (jes) => jes.Each(je =>
            {
                Message.PrintLine("Fully flushed called", ConsoleColor.DarkBlue);
                checkedJournal = true;
                Expect.AreSame(journal, je.Journal);
                blocker.Set();
            }));
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(30);
                Message.PrintLine("QueueLength={0}", ConsoleColor.Cyan, journal.QueueLength.ToString());
                Message.PrintLine("Flushed status: {0}", ConsoleColor.Yellow, journal.QueueFlusher.ThreadState.ToString());
            }
            if (!blocker.WaitOne(15000))
            {
                Warn("Inconclusive, blocker was not set");
            }
            checkedJournal.Value.IsTrue();
            string newAddress = "Updated " + 8.RandomLetters();
            value.Address = newAddress;
            journal.Enqueue(value, (jes) =>
            {
                DataReplicationTestClass check = journal.LoadInstance<DataReplicationTestClass>(value.Id);
                Expect.AreEqual(newAddress, check.Address);
                OutLine(check.Address);
                blocker.Set();
            });

            if (!blocker.WaitOne(15000))
            {
                Warn("Inconclusive, blocker was not set");
            }
        }

        [UnitTest]
        public void EncryptionJournalGetsLatestPropertyValue()
        {
            AutoResetEvent blocker = new AutoResetEvent(false);
            DataReplicationTestClass value = GetRandomDataInstance();
            Journal journal = GetEncryptionJournal<Journal>();
            bool? checkedJournal = false;
            journal.Enqueue(value, (jes) => jes.Each(je =>
            {
                checkedJournal = true;
                Expect.AreSame(journal, je.Journal);
                blocker.Set();
            }));
            if (!blocker.WaitOne(15000))
            {
                Warn("Inconclusive, blocker was not set");
            }
            Expect.IsTrue(checkedJournal.Value);
            string newAddress = "Updated " + 8.RandomLetters();
            value.Address = newAddress;
            journal.Enqueue(value, (jes) =>
            {
                DataReplicationTestClass check = journal.LoadInstance<DataReplicationTestClass>(value.Id);
                Expect.AreEqual(newAddress, check.Address);
                blocker.Set();
            });

            if (!blocker.WaitOne(15000))
            {
                Warn("Inconclusive, blocker was not set");
            }
        }
        
        [UnitTest("Data Replication: can save and load type map")]
        public void CanSaveAndLoadTypeMap()
        {
            TypeMap typeMap = GetEncryptionJournal<TypeMap>();
            typeMap.AddMapping(new DataReplicationTestClass());
            int typeCount = typeMap.TypeMappings.Count;
            int propCount = typeMap.PropertyMappings.Count;

            string filePath = typeMap.Save();
            Expect.IsTrue(File.Exists(filePath));

            TypeMap loaded = TypeMap.Load(filePath);
            Expect.AreEqual(loaded.TypeMappings.Count, typeCount);
            Expect.AreEqual(loaded.PropertyMappings.Count, propCount);
        }

        private static DataReplicationTestClass GetDataInstance()
        {
            return new DataReplicationTestClass
            {
                FirstName = "FirstNameValue",
                LastName = "LastNameValue",
                Address = "First Home"
            };
        }

        private static DataReplicationTestClass GetRandomDataInstance()
        {
            return new DataReplicationTestClass
            {
                FirstName = "FirstNameValue_".RandomLetters(6),
                LastName = "LastNameValue_".RandomLetters(4),
                Address = "First Home"
            };
        }
        
        private IEnumerable<Journal> GetTestJournals()
        {
            ServiceRegistry registry = JournalRegistryContainer.GetServiceRegistry();
            yield return registry.Get<Journal>();

            registry = JournalRegistryContainer.GetServiceRegistry();
            registry.For<IJournalEntryValueFlusher>().Use<CompressedJournalEntryValueFlusher>();
            registry.For<IJournalEntryValueLoader>().Use<CompressedJournalEntryValueLoader>();
            registry.For<Journal>().Use<Journal>();
            yield return registry.Get<Journal>();

            registry = JournalRegistryContainer.GetServiceRegistry();
            registry.For<IJournalEntryValueFlusher>().Use<EncryptedJournalEntryValueFlusher>();
            registry.For<IJournalEntryValueLoader>().Use<EncryptedJournalEntryValueLoader>();
            registry.For<Journal>().Use<Journal>();
            yield return registry.Get<Journal>();
        }

        private T GetCompressionJournal<T>()
        {
            ServiceRegistry registry = JournalRegistryContainer.GetServiceRegistry();
            registry.For<IJournalEntryValueFlusher>().Use<CompressedJournalEntryValueFlusher>();
            registry.For<IJournalEntryValueLoader>().Use<CompressedJournalEntryValueLoader>();
            registry.For<Journal>().Use<Journal>();
            return registry.Get<T>();
        }

        private T GetEncryptionJournal<T>()
        {
            ServiceRegistry registry = JournalRegistryContainer.GetServiceRegistry();
            registry.For<IJournalEntryValueFlusher>().Use<EncryptedJournalEntryValueFlusher>();
            registry.For<IJournalEntryValueLoader>().Use<EncryptedJournalEntryValueLoader>();
            registry.For<Journal>().Use<Journal>();
            return registry.Get<T>();
        }
    }
}
