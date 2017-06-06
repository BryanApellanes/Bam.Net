/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Concurrent;

namespace Bam.Net
{
    public class DynamicTypeStore
    {
        Dictionary<string, DynamicTypeInfo> dynamicTypeStore;
        object accessLock = new object();

        public DynamicTypeStore()
        {
            dynamicTypeStore = new Dictionary<string, DynamicTypeInfo>();
        }

        public string[] Names
        {
            get
            {
                lock (accessLock)
                {
                    return dynamicTypeStore.Keys.ToArray<string>();
                }
            }
        }

        static DynamicTypeStore _current;
        static object _currentLock = new object();
        public static DynamicTypeStore Current
        {
            get
            {
                return _currentLock.DoubleCheckLock(ref _current, () => new DynamicTypeStore());                
            }
        }

        public void AddType(string name, DynamicTypeInfo info)
        {
            lock (accessLock)
			{
                dynamicTypeStore.Set(name, info);
            }
        }

        public void RemoveType(string name)
        {
            lock (accessLock)
            {
                dynamicTypeStore.Remove(name);
            }
        }

        public bool ContainsTypeInfo(string typeName)
        {
            lock (accessLock)
            {
                return dynamicTypeStore.ContainsKey(typeName);
            }
        }

        public DynamicTypeInfo[] Types
        {
            get
            {
                lock (accessLock)
                {
                    return dynamicTypeStore.Values.ToArray();
                }
            }
        }

        public DynamicTypeInfo this[string name]
        {
            get
            {
                lock (accessLock)
                {
                    return dynamicTypeStore[name];
                }
            }
        }
    }
}
