/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;
using System.Reflection;
using System.Reflection.Emit;

namespace Naizari.Extensions
{
    public class DynamicTypeStore
    {
        Dictionary<string, DynamicTypeInfo> dynamicTypeStore;
        public DynamicTypeStore()
        {
            this.dynamicTypeStore = new Dictionary<string, DynamicTypeInfo>();
        }

        public string[] Names
        {
            get
            {
                return this.dynamicTypeStore.Keys.ToArray<string>();
            }
        }

        public static DynamicTypeStore Current
        {
            get
            {
                return SingletonHelper.GetApplicationSingleton<DynamicTypeStore>();
            }
        }

        object accessLock = new object();
        public void AddType(string name, DynamicTypeInfo info)
        {
            lock (accessLock)
            {
                this.dynamicTypeStore.Add(name, info);
            }
        }

        public void RemoveType(string name)
        {
            lock (accessLock)
            {
                this.dynamicTypeStore.Remove(name);
            }
        }

        public bool ContainsTypeInfo(string typeName)
        {
            return this.dynamicTypeStore.ContainsKey(typeName);
        }

        public DynamicTypeInfo[] Types
        {
            get
            {
                return this.dynamicTypeStore.Values.ToArray<DynamicTypeInfo>();

            }
        }

        public DynamicTypeInfo this[string name]
        {
            get
            {
                lock (accessLock)
                {
                    return this.dynamicTypeStore[name];
                }
            }
        }
    }
}
