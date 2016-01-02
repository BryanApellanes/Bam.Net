/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Configuration
{
    public static class CascadeConfiguration
    {
        public static string ApplicationSubKey
        {
            get { return RegistryConfiguration.ApplicationSubKey; }
            set { RegistryConfiguration.ApplicationSubKey = value; }
        }

        public static void SetProperty(object target, string propertyName, bool isRequired)
        {
            Type type = target.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if( property == null )
                throw new InvalidOperationException(string.Format("target is of type {0} which does not have a property named {1}", type.Name, propertyName));

            string propVal = GetProperty(propertyName, isRequired, type);

            if (!string.IsNullOrEmpty(propVal))
                property.SetValue(target, propVal, null);
        }

        public static string GetProperty(string propertyName, bool isRequired, Type type)
        {
            string propVal = DefaultConfiguration.GetProperty(type.Name, propertyName);
            //if (string.IsNullOrEmpty(propVal))
            //    propVal = RegistryConfiguration.GetProperty(type.Name, propertyName);

            if (string.IsNullOrEmpty(propVal) && isRequired)
                ThrowException(type.Name, propertyName);

            return propVal;
        }

        /// <summary>
        /// Sets the properties for the specified object, searching the 
        /// default config file and then the registry if all "RequiredProperties"
        /// were not found. <seealso cref="IHasRequiredProperties"/>
        /// </summary>
        /// <param name="target"></param>
        public static void SetProperties(object target)
        {
            try
            {
                DefaultConfiguration.SetProperties(target, true);
            }
            catch (RequiredPropertyNotSetException rpnse)
            {
                Console.WriteLine("Not all required properties were found in the default configuration file.");
                try
                {
                    RegistryConfiguration.SetPropertiesFromRegistry(target, true);
                }
                catch (RequiredPropertyNotSetException inner)
                {
                    Console.WriteLine("Not all required properties were found in the registry");
                    //add a check to an http service

                    // add a check to a database

                    ThrowException(inner);
                }
            }
        }

        public static void ThrowException(string type, string property)
        {
            throw new RequiredPropertyNotFoundException(string.Format("Unable to find required properties in the default config or the registry ({0}.{1})", type, property));
        }

        private static void ThrowException(RequiredPropertyNotSetException inner)
        {
            string type = string.Empty;
            string property = string.Empty;
            if (inner != null)
            {
                type = inner.Type == null ? "" : inner.Type;
                property = inner.PropertyName == null ? "" : inner.PropertyName;
            }

            throw new RequiredPropertyNotFoundException(string.Format("Unable to find required properties in the default config or the registry ({0}.{1})", type, property), inner);
        }
    }
}
