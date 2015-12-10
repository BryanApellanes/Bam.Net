/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Reflection;

namespace Naizari.Configuration
{
    public static class RegistryConfiguration
    {
        static RegistryHive rootHiveEnum;
        static RegistryKey rootHive;
        public const string SubKey = "SOFTWARE";

        public static RegistryHive RootHive
        {
            get{ return rootHiveEnum;}
            set
            {
                rootHiveEnum = value;
                if (rootHive == null)
                {
                    switch (value)
                    {
                        case RegistryHive.ClassesRoot:
                            rootHive = Registry.ClassesRoot;
                            break;
                        case RegistryHive.CurrentConfig:
                            rootHive = Registry.CurrentConfig;
                            break;
                        case RegistryHive.CurrentUser:
                            rootHive = Registry.CurrentUser;
                            break;
                        case RegistryHive.DynData:
                            rootHive = Registry.DynData;
                            break;
                        case RegistryHive.LocalMachine:
                            rootHive = Registry.LocalMachine;
                            break;
                        case RegistryHive.PerformanceData:
                            rootHive = Registry.PerformanceData;
                            break;
                        case RegistryHive.Users:
                            rootHive = Registry.Users;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static string ApplicationSubKey
        {
            get;
            set;
        }

        public static string GetProperty(string targetTypeName, string propertyName)
        {
            RegistryKey configSource = GetConfigSubkey();
            if (configSource == null)
                return string.Empty;

            object propValue = configSource.GetValue(targetTypeName + "." + propertyName);
            if (propValue == null)
                propValue = configSource.GetValue(propertyName);

            if (propValue == null)
                return string.Empty;
            else
                return propValue.ToString();
        }

        public static void SetPropertiesFromRegistry(object target)
        {
            SetPropertiesFromRegistry(target, false);
        }

        public static void SetPropertiesFromRegistry(object target, bool throwIfMissingRequiredProperties)
        {
            RegistryKey configSource = GetConfigSubkey(); 
            
            string subKey = string.Format("{0}\\{1}", RegistryConfiguration.SubKey, ApplicationSubKey);
            if (configSource == null)
                throw new InvalidOperationException(string.Format("Unable to open subkey {0}", subKey));

            Type targetType = target.GetType();
            foreach (PropertyInfo property in targetType.GetProperties())
            {
                if (property.CanWrite)
                {
                    object propValue = configSource.GetValue(targetType.Name + "." + property.Name);
                    if (propValue == null)
                        propValue = configSource.GetValue(property.Name);

                    if (propValue == null)
                        continue;

                    property.SetValue(target, propValue.ToString(), null);
                }
            }

            if (throwIfMissingRequiredProperties)
            {
                if (targetType.GetInterface("IHasRequiredProperties") != null)
                {
                    DefaultConfiguration.CheckRequiredProperties((IHasRequiredProperties)target);
                }
            }
        }

        private static RegistryKey GetConfigSubkey()
        {
            if (string.IsNullOrEmpty(ApplicationSubKey))
            {
                throw new InvalidOperationException(string.Format("ApplicationSubKey property has not been specified"));
            }

            if (rootHive == null)
                rootHive = Registry.LocalMachine;

            string subKey = string.Format("{0}\\{1}", RegistryConfiguration.SubKey, ApplicationSubKey);
            RegistryKey configSource = rootHive.OpenSubKey(subKey);

          

            return configSource;
        }
    }
}
