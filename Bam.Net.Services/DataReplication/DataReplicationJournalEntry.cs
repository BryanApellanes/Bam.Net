using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// Represents the write of a single property value on a single persisted instance.
    /// </summary>
    public class DataReplicationJournalEntry
    {
        /// <summary>
        /// Gets or sets the type id determined by DataReplicationtypeMap.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public long TypeId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public ulong InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public ulong Seq { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public long PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
        
        public DataReplicationJournalEntryInfo ToInfo()
        {
            return this.CopyAs<DataReplicationJournalEntryInfo>();
        }
        
        public T LoadInstance<T>(DataReplicationJournal journal) where T: KeyHashAuditRepoData, new()
        {
            return journal.LoadInstance<T>(this);
        }

        public DataReplicationJournalEntry LoadLatestValue(DataReplicationJournal journal)
        {
            return LoadLatestValue(journal.JournalDirectory, journal.TypeMap);
        }

        /// <summary>
        /// Loads the latest value for the property of the current type instance.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        public DataReplicationJournalEntry LoadLatestValue(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            return GetSequencedHistoryValues(journalDirectory, typeMap).FirstOrDefault();
        }

        /// <summary>
        /// /// Gets all previous values for this property that are still available in history ordered by the sequence number.
        /// </summary>
        /// <param name="journal">The journal.</param>
        /// <returns></returns>
        public IEnumerable<DataReplicationJournalEntry> GetSequencedHistoryValues(DataReplicationJournal journal)
        {
            return GetSequencedHistoryValues(journal.JournalDirectory, journal.TypeMap);
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history ordered by the sequence number.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        public IEnumerable<DataReplicationJournalEntry> GetSequencedHistoryValues(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            return new SortedSet<DataReplicationJournalEntry>(GetHistoryValues(journalDirectory, typeMap), new Comparer<DataReplicationJournalEntry>((x, y) => y.Seq.CompareTo(x.Seq)));
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history.
        /// </summary>
        /// <param name="journal">The journal.</param>
        /// <returns></returns>
        public IEnumerable<DataReplicationJournalEntry> GetHistoryValues(DataReplicationJournal journal)
        {
            return GetHistoryValues(journal.JournalDirectory, journal.TypeMap);
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        public IEnumerable<DataReplicationJournalEntry> GetHistoryValues(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            foreach(FileInfo file in propertyDirectory.GetFiles())
            {
                if (ulong.TryParse(file.Name, out ulong seq))
                {
                    DataReplicationJournalEntry entry = this.CopyAs<DataReplicationJournalEntry>();
                    entry.Value = File.ReadAllText(Path.Combine(propertyDirectory.FullName, file.Name));
                    entry.Seq = seq;
                    yield return entry;
                }
                else
                {
                    Log.Default.AddEntry("Failed to parse filename as sequence number ({0})", Path.Combine(propertyDirectory.FullName, file.Name));
                }
            }
        }

        /// <summary>
        /// Gets the file information if the current entry were written to the specified DirectoryInfo.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <returns></returns>
        public FileInfo GetFileInfo(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            FileInfo propertyFile = new FileInfo(Path.Combine(propertyDirectory.FullName, Seq.ToString()));
            return propertyFile;
        }

        /// <summary>
        /// Gets the property directory where this entry would be written for the specified journal directory and typemap.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        public DirectoryInfo GetPropertyDirectory(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo instanceDirectory = GetInstanceDirectory(journalDirectory, typeMap);
            string propertyDirectoryName = typeMap.GetPropertyShortName(PropertyId);
            DirectoryInfo propertyDirectory = new DirectoryInfo(Path.Combine(instanceDirectory.FullName, propertyDirectoryName));
            return propertyDirectory;
        }

        /// <summary>
        /// Gets the instance directory for the object instance this entry is for.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        public DirectoryInfo GetInstanceDirectory(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo typeDirectory = GetTypeDirectory(journalDirectory, typeMap);
            DirectoryInfo instanceDirectory = new DirectoryInfo(Path.Combine(typeDirectory.FullName, InstanceId.ToString()));
            return instanceDirectory;
        }

        public static DirectoryInfo GetTypeDirectory(DataReplicationJournal journal, long typeId)
        {
            return new DataReplicationJournalEntry { TypeId = typeId }.GetTypeDirectory(journal.JournalDirectory, journal.TypeMap);
        }

        public DirectoryInfo GetTypeDirectory(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            string typeDirectoryName = typeMap.GetTypeShortName(TypeId);
            DirectoryInfo typeDirectory = new DirectoryInfo(Path.Combine(journalDirectory.FullName, typeDirectoryName));
            return typeDirectory;
        }

        /// <summary>
        /// Sets the id on the specified instance then returns journal entries for all of the properties.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IEnumerable<DataReplicationJournalEntry> FromInstance(KeyHashAuditRepoData instance)
        {
            Args.ThrowIfNull(instance, "instance");
            long typeId = DataReplicationTypeMap.GetTypeId(instance, out object dynamicInstance, out Type dynamicType);
            instance.Id = instance.GetULongKeyHash();
            foreach (PropertyInfo prop in dynamicType.GetProperties())
            {
                yield return new DataReplicationJournalEntry { TypeId = typeId, InstanceId = instance.Id, PropertyId = DataReplicationTypeMap.GetPropertyId(prop, out string ignore), Value = prop.GetValue(dynamicInstance)?.ToString() };
            }
        }

        public static IEnumerable<DataReplicationJournalEntry> LoadInstanceEntries<T>(ulong id, DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap) where T: KeyHashAuditRepoData, new()
        {
            long typeId = DataReplicationTypeMap.GetTypeId(new T(), out object dynamicInstance, out Type dynamicType);
            foreach(PropertyInfo prop in dynamicType.GetProperties())
            {
                DataReplicationJournalEntry entry = new DataReplicationJournalEntry { TypeId = typeId, InstanceId = id, PropertyId = DataReplicationTypeMap.GetPropertyId(prop, out string ignore) };
                yield return entry.LoadLatestValue(journalDirectory, typeMap);                
            }
        }        
    }
}
