using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DataReplicationJournalEntry
    {
        /// <summary>
        /// Gets or sets the type int determined by DataReplicationtypeMap.
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
        
        public string LoadValue(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo propertyDirectory = GetPropertyDirectory(journalDirectory, typeMap);
            List<string> fileNames = propertyDirectory.GetFiles().Select(f => f.Name).ToList();
            fileNames.Sort((x, y) => x.CompareTo(y));
            string fileName = fileNames[0];
            Value = File.ReadAllText(Path.Combine(propertyDirectory.FullName, fileName));
            return Value;
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

        public DirectoryInfo GetPropertyDirectory(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DirectoryInfo instanceDirectory = GetInstanceDirectory(journalDirectory, typeMap);
            string propertyDirectoryName = typeMap.GetPropertyShortName(PropertyId);
            DirectoryInfo propertyDirectory = new DirectoryInfo(Path.Combine(instanceDirectory.FullName, propertyDirectoryName));
            return propertyDirectory;
        }

        public DirectoryInfo GetInstanceDirectory(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            string typeDirectoryName = typeMap.GetTypeShortName(TypeId);
            DirectoryInfo typeDirectory = new DirectoryInfo(Path.Combine(journalDirectory.FullName, typeDirectoryName));
            DirectoryInfo instanceDirectory = new DirectoryInfo(Path.Combine(typeDirectory.FullName, InstanceId.ToString()));
            return instanceDirectory;
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

        public static IEnumerable<DataReplicationJournalEntry> LoadInstance<T>(ulong id, DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap) where T: KeyHashAuditRepoData, new()
        {
            long typeId = DataReplicationTypeMap.GetTypeId(new T(), out object dynamicInstance, out Type dynamicType);
            foreach(PropertyInfo prop in dynamicType.GetProperties())
            {
                DataReplicationJournalEntry entry = new DataReplicationJournalEntry { TypeId = typeId, InstanceId = id, PropertyId = DataReplicationTypeMap.GetPropertyId(prop, out string ignore) };
                entry.LoadValue(journalDirectory, typeMap);
                yield return entry;
            }
        }        
    }
}
