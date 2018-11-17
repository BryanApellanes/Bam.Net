using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net.Services.DataReplication
{
    public partial class JournalEntry
    {
        /// <summary>
        /// Sets the id on the specified instance then returns journal entries for all of the properties.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IEnumerable<JournalEntry> FromInstance(KeyHashAuditRepoData instance)
        {
            Args.ThrowIfNull(instance, "instance");
            Type type = instance.GetType();
            long typeId = JournalTypeMap.GetTypeId(type, out string ignore);            
            instance.Id = instance.GetULongKeyHash();
            foreach (PropertyInfo prop in GetProperties(type))
            {
                yield return new JournalEntry { TypeId = typeId, InstanceId = instance.Id, PropertyId = JournalTypeMap.GetPropertyId(prop, out string i), Value = prop.GetValue(instance)?.ToString() };
            }
        }

        public static IEnumerable<JournalEntry> LoadInstanceEntries<T>(ulong id, DirectoryInfo journalDirectory, JournalTypeMap typeMap, IJournalEntryValueLoader loader = null) where T : KeyHashAuditRepoData, new()
        {
            long typeId = JournalTypeMap.GetTypeId(typeof(T));
            foreach (PropertyInfo prop in GetProperties(typeof(T)))
            {
                JournalEntry entry = new JournalEntry { TypeId = typeId, InstanceId = id, PropertyId = JournalTypeMap.GetPropertyId(prop, out string ignore) };
                yield return entry.LoadLatestValue(journalDirectory, typeMap, loader) ?? entry;
            }
        }

        private static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string)).ToArray();
        }
    }
}
