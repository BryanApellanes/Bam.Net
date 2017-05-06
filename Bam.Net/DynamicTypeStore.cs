/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

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

        public static DynamicTypeStore Current
        {
            get
            {
                return Providers.GetApplicationSingleton<DynamicTypeStore>();
            }
        }

        public void AddType(string name, DynamicTypeInfo info)
        {
            lock (accessLock)
			{
				if (dynamicTypeStore.ContainsKey(name))
				{
					dynamicTypeStore[name] = info;
				}
				else
				{
					dynamicTypeStore.Add(name, info);
				}
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
