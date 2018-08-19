using Bam.Net.CoreServices;
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
    public class DataReplicationTests: CommandLineTestInterface
    {        
        [UnitTest("Data Replication: can get type map")]
        public void CanGetTypeMap()
        {
            DataReplicationTypeMap typeMap = GetTestObject<DataReplicationTypeMap>();
            Expect.IsNotNull(typeMap, "typeMap was null");
        }

        [UnitTest("Data Replication: can get journal")]
        public void CanGetJournal()
        {
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();
            Expect.IsNotNull(journal, "journal was null");
        }

        [UnitTest("Data Replication: type map should not be null")]
        public void TypeMapShouldNotBeNull()
        {
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();
            Expect.IsNotNull(journal.TypeMap);
        }

        [UnitTest("Data Replication: Can write entries")]
        public void CanWriteEntries()
        {
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();
            DataReplicationTestClass value = GetDataInstance();

            IEnumerable<DataReplicationJournalEntry> entries = journal.Write(value);
            WriteToConsole(journal, entries);
            value.Address = "A new Address";

            entries = journal.Write(value);
            WriteToConsole(journal, entries);

            OutLineFormat("journal directory {0}", ConsoleColor.Cyan, journal.JournalDirectory.FullName);
        }

        private static void WriteToConsole(DataReplicationJournal journal, IEnumerable<DataReplicationJournalEntry> entries)
        {
            foreach (DataReplicationJournalEntry entry in entries)
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
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();
            bool? entryFlushedFired = false;
            bool? queueEmptyFired = false;
            journal.EntryFlushed += (o, a) => entryFlushedFired = true;
            journal.QueueEmpty += (o, a) => queueEmptyFired = true;
            DataReplicationTestClass value = GetDataInstance();
            IEnumerable<DataReplicationJournalEntry> entries = journal.Write(value);
            foreach (DataReplicationJournalEntry entry in entries)
            {
                Console.WriteLine("TypeId={0}, PropertyId={1}, TypeName={2}, PropertyName={3}, Value={4}",
                    entry.TypeId,
                    entry.PropertyId,
                    journal.GetTypeName(entry.TypeId),
                    journal.GetPropertyName(entry.PropertyId),
                    entry.Value);
            }

            if(Exec.TakesTooLong(() =>
            {
                Exec.SleepUntil(() => queueEmptyFired.Value);
            }, 5000))
            {
                Expect.Fail("took too long");
            }
            Expect.IsTrue(entryFlushedFired.Value);
            Expect.IsTrue(queueEmptyFired.Value);
            OutLineFormat("journal directory {0}", ConsoleColor.Cyan, journal.JournalDirectory.FullName);
        }
        
        [UnitTest("Data Replication: can read entries")]
        public void CanRead()
        {
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();            
            DataReplicationTestClass value1 = GetRandomDataInstance();            
            HashSet<DataReplicationTestClass> retrieved = new HashSet<DataReplicationTestClass>();
            List<DataReplicationJournalEntry> entries = new List<DataReplicationJournalEntry>();
            AutoResetEvent blocker = new AutoResetEvent(false);
            journal.QueueEmpty += (o, a) =>
            {
                OutLineFormat("queue empty fired");
                foreach (DataReplicationJournalEntry entry in entries)
                {
                    retrieved.Add(journal.LoadInstance<DataReplicationTestClass>(entry));
                }
                blocker.Set();
            };
            entries.AddRange(journal.Write(value1));
            blocker.WaitOne();
            DataReplicationTestClass check = journal.LoadInstance<DataReplicationTestClass>(value1.Id);
            Expect.IsNotNull(check);
            Expect.AreEqual(check.FirstName, value1.FirstName);
            Expect.AreEqual(check.LastName, value1.LastName);
            Expect.AreEqual(check.Address, value1.Address);
        }

        [UnitTest]
        public void WillGetLatestPropertyValue()
        {
            DataReplicationJournal journal = GetTestObject<DataReplicationJournal>();
            DataReplicationTestClass value1 = GetRandomDataInstance();
            DataReplicationTestClass value2 = GetDataInstance();
            DataReplicationTestClass value3 = GetRandomDataInstance();
            HashSet<DataReplicationTestClass> retrieved = new HashSet<DataReplicationTestClass>();
            List<DataReplicationJournalEntry> entries = new List<DataReplicationJournalEntry>();
            AutoResetEvent blocker = new AutoResetEvent(false);
            journal.QueueEmpty += (o, a) =>
            {
                OutLineFormat("queue empty fired");
                foreach (DataReplicationJournalEntry entry in entries)
                {
                    retrieved.Add(journal.LoadInstance<DataReplicationTestClass>(entry));
                }
                blocker.Set();
            };
            foreach (DataReplicationTestClass entry in new DataReplicationTestClass[] { value1, value2, value3 })
            {
                entries.AddRange(journal.Write(entry));
            }
            blocker.WaitOne();
            Expect.AreEqual(3, retrieved.Count);
            string newAddress = "Updated " + 8.RandomLetters();
            value2.Address = newAddress;
            journal.Write(value2).ToArray();
            blocker.WaitOne();
            DataReplicationTestClass check = journal.LoadInstance<DataReplicationTestClass>(value2.Id);
            Expect.IsNotNull(check);
            Expect.AreEqual(check.FirstName, value2.FirstName);
            Expect.AreEqual(check.LastName, value2.LastName);
            Expect.AreEqual(newAddress, check.Address);
        }

        [UnitTest("Data Replication: can save and load type map")]
        public void CanSaveAndLoadTypeMap()
        {
            DataReplicationTypeMap typeMap = GetTestObject<DataReplicationTypeMap>();
            typeMap.AddMapping(new DataReplicationTestClass());
            int typeCount = typeMap.TypeMappings.Count;
            int propCount = typeMap.PropertyMappings.Count;

            string filePath = typeMap.Save();
            Expect.IsTrue(File.Exists(filePath));

            DataReplicationTypeMap loaded = DataReplicationTypeMap.Load(filePath);
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

        private T GetTestObject<T>()
        {
            ServiceRegistry registry = DataReplicationRegistryContainer.GetServiceRegistry();
            return registry.Get<T>();
        }
    }
}
