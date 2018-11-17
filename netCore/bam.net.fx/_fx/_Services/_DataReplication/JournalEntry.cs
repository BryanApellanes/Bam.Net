﻿using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
            long typeId = JournalTypeMap.GetTypeId(instance, out object dynamicInstance, out Type dynamicType);
            instance.Id = instance.GetULongKeyHash();
            foreach (PropertyInfo prop in dynamicType.GetProperties())
            {
                yield return new JournalEntry { TypeId = typeId, InstanceId = instance.Id, PropertyId = JournalTypeMap.GetPropertyId(prop, out string ignore), Value = prop.GetValue(dynamicInstance)?.ToString() };
            }
        }

        public static IEnumerable<JournalEntry> LoadInstanceEntries<T>(ulong id, DirectoryInfo journalDirectory, JournalTypeMap typeMap, IJournalEntryValueLoader loader = null) where T : KeyHashAuditRepoData, new()
        {
            long typeId = JournalTypeMap.GetTypeId(new T(), out object dynamicInstance, out Type dynamicType);
            foreach (PropertyInfo prop in dynamicType.GetProperties())
            {
                JournalEntry entry = new JournalEntry { TypeId = typeId, InstanceId = id, PropertyId = JournalTypeMap.GetPropertyId(prop, out string ignore) };
                yield return entry.LoadLatestValue(journalDirectory, typeMap, loader) ?? entry;
            }
        }
    }
}
