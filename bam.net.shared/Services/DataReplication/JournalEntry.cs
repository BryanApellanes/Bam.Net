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
    public partial class JournalEntry
    {
        public event EventHandler Written;

        internal void OnEntryWritten(object sender, EventArgs a)
        {
            Written?.Invoke(sender, a);
        }

        internal Journal Journal
        {
            get;
            set;
        }

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

        public string GetKey()
        {
            return $"{TypeId}.{InstanceId}.{PropertyId}.{Seq}";
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        protected internal string TypeName
        {
            get
            {
                string typeName = Journal?.TypeMap?.GetTypeName(TypeId);
                if (string.IsNullOrEmpty(typeName))
                {
                    typeName = TypeId.ToString();
                }
                return typeName;
            }
        }

        protected internal string PropertyName
        {
            get
            {
                string propertyName = Journal?.TypeMap?.GetPropertyName(PropertyId);
                if(string.IsNullOrEmpty(propertyName))
                {
                    propertyName = PropertyId.ToString();
                }
                return propertyName;
            }
        }

        public override string ToString()
        {
            if(Journal != null)
            {
                return $"{TypeName}.{PropertyName}.{InstanceId}({Value})";
            }
            return $"{TypeId}.{PropertyId}.{InstanceId}({Value})";
        }

        public override int GetHashCode()
        {
            return $"{TypeId}.{PropertyId}.{InstanceId}({Value})".ToSha1Int();
        }

        public JournalEntryInfo ToInfo()
        {
            return this.CopyAs<JournalEntryInfo>();
        }
        
        public T LoadInstance<T>(Journal journal) where T: KeyHashAuditRepoData, new()
        {
            return journal.LoadInstance<T>(this);
        }

        public JournalEntry LoadLatestValue(Journal journal)
        {
            return LoadLatestValue(journal.JournalDirectory, journal.TypeMap);
        }

        /// <summary>
        /// Loads the latest value for the property of the current type instance.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        internal protected JournalEntry LoadLatestValue(DirectoryInfo journalDirectory, JournalTypeMap typeMap, IJournalEntryValueLoader loader = null)
        {
            IEnumerable<JournalEntry> entries = GetSequencedHistoryValues(journalDirectory, typeMap, loader);
            return entries.FirstOrDefault();
        }

        /// <summary>
        /// /// Gets all previous values for this property that are still available in history ordered by the sequence number.
        /// </summary>
        /// <param name="journal">The journal.</param>
        /// <returns></returns>
        public IEnumerable<JournalEntry> GetSequencedHistoryValues(Journal journal)
        {
            return GetSequencedHistoryValues(journal.JournalDirectory, journal.TypeMap, journal.Loader);
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history ordered by the sequence number.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        internal IEnumerable<JournalEntry> GetSequencedHistoryValues(DirectoryInfo journalDirectory, JournalTypeMap typeMap, IJournalEntryValueLoader loader = null)
        {
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            return new SortedSet<JournalEntry>(LoadHistory(journalDirectory, typeMap, loader), new Comparer<JournalEntry>((x, y) => y.Seq.CompareTo(x.Seq)));
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history.
        /// </summary>
        /// <param name="journal">The journal.</param>
        /// <returns></returns>
        public IEnumerable<JournalEntry> LoadHistory(Journal journal)
        {
            return LoadHistory(journal.JournalDirectory, journal.TypeMap);
        }

        /// <summary>
        /// Gets all previous values for this property that are still available in history.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <param name="typeMap">The type map.</param>
        /// <returns></returns>
        internal IEnumerable<JournalEntry> LoadHistory(DirectoryInfo journalDirectory, JournalTypeMap typeMap, IJournalEntryValueLoader loader = null)
        {
            loader = loader ?? new JournalEntryValueLoader();
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            if (propertyDirectory.Exists)
            {
                foreach (FileInfo file in propertyDirectory.GetFiles())
                {
                    if (ulong.TryParse(file.Name, out ulong seq))
                    {
                        JournalEntry entry = this.CopyAs<JournalEntry>();
                        entry.Value = loader.LoadValue(Path.Combine(propertyDirectory.FullName, file.Name));
                        entry.Seq = seq;
                        yield return entry;
                    }
                    else
                    {
                        Log.Default.AddEntry("Failed to parse filename as sequence number ({0})", Path.Combine(propertyDirectory.FullName, file.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the file information if the current entry were written to the specified DirectoryInfo.
        /// </summary>
        /// <param name="journalDirectory">The journal directory.</param>
        /// <returns></returns>
        public FileInfo GetFileInfo(DirectoryInfo journalDirectory, JournalTypeMap typeMap)
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
        public DirectoryInfo GetPropertyDirectory(DirectoryInfo journalDirectory, JournalTypeMap typeMap)
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
        public DirectoryInfo GetInstanceDirectory(DirectoryInfo journalDirectory, JournalTypeMap typeMap)
        {
            DirectoryInfo typeDirectory = GetTypeDirectory(journalDirectory, typeMap);
            DirectoryInfo instanceDirectory = new DirectoryInfo(Path.Combine(typeDirectory.FullName, InstanceId.ToString()));
            return instanceDirectory;
        }

        public static DirectoryInfo GetTypeDirectory(Journal journal, long typeId)
        {
            return new JournalEntry { TypeId = typeId }.GetTypeDirectory(journal.JournalDirectory, journal.TypeMap);
        }

        public DirectoryInfo GetTypeDirectory(DirectoryInfo journalDirectory, JournalTypeMap typeMap)
        {
            string typeDirectoryName = typeMap.GetTypeShortName(TypeId);
            DirectoryInfo typeDirectory = new DirectoryInfo(Path.Combine(journalDirectory.FullName, typeDirectoryName));
            return typeDirectory;
        }       
    }
}
