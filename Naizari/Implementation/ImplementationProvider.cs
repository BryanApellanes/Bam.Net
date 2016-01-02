/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using Naizari.Extensions;
using Naizari.Configuration;

namespace Naizari.Implementation
{
    [Obsolete("This class was made with the intention of loading buisiness objects but is not flexible enough.  Use Naizari.Helpers.ImplementationSingletonManager instead.")]
    public class ImplementationProvider: IHasRequiredProperties
    {
        List<string> requiredProperties;
        static Dictionary<Type, object> implementations;
        static Dictionary<Type, Exception> loadExceptions;

        internal ImplementationProvider()
        {
            implementations = new Dictionary<Type, object>();
            loadExceptions = new Dictionary<Type, Exception>();

            requiredProperties = new List<string>();
            requiredProperties.Add("Assemblies");

            DefaultConfiguration.SetProperties(this, true);
            //this.SetProperties(true);
            LoadBusinessObjects();
        }

        public string[] RequiredProperties
        {
            get { return requiredProperties.ToArray(); }
        }

        private static void LoadBusinessObjects()
        {
            List<Type> typeList = new List<Type>();

            foreach (string assembly in assemblies.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                Assembly a = Assembly.LoadFrom(assembly.Trim());
                typeList.AddRange(a.GetTypes());
            }

            foreach (Type type in typeList.ToArray())
            {
              
                object[] attrs = type.GetCustomAttributes(typeof(BusinessObjectImplementation), false);
                if (attrs.Length > 0)
                {
                    BusinessObjectImplementation boi = attrs[0] as BusinessObjectImplementation;
                    if (boi != null)
                    {
                        Type interfaceType = type.GetInterface(boi.Interface.Name);
                        if (interfaceType == null)
                        {
                            loadExceptions.Add(boi.Interface, new InvalidOperationException(string.Format("{0} does not implement interface {1}, verify the attribute declaration is correct and the target BusinessObjectImplementation is actually an implementation of the interface {1}", type.Name, boi.Interface.Name)));
                            continue;
                        }

                        ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                        object impl = ctor.Invoke(null);
                        implementations.Add(interfaceType, impl);
                    }
                }               
            }
        }

        private T PrivateGetImplementation<T>() where T: class
        {
            if (implementations.ContainsKey(typeof(T)))
                return implementations[typeof(T)] as T;
            else
                throw new ImplementationNotLoadedException(typeof(T));
        }

        static ImplementationProvider manager;
        public static T GetImplementation<T>() where T : class
        {
            if (manager == null)
                manager = new ImplementationProvider();

            return manager.PrivateGetImplementation<T>();
        }

        public static object GetImplementation(Type t)
        {
            if (manager == null)
                manager = new ImplementationProvider();

            if (implementations.ContainsKey(t))
                return implementations[t];
            else
                throw new ImplementationNotLoadedException(t);
        }

        public static object GetImplementation(string typeName)
        {
            return GetImplementation(Type.GetType(typeName, false, true));
        }
        /// <summary>
        /// Returns the exception that 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //public static Exception GetLoadException<T>()
        //{
        //    if (loadExceptions.ContainsKey(typeof(T)))
        //        return loadExceptions[typeof(T)];

        //    return null;
        //}
        //public static TestManager Current
        //{
        //    get
        //    {
        //        if (manager == null)
        //            manager = new TestManager();
        //        return manager;
        //    }        
        //}

        static string assemblies;
        public string Assemblies
        {
            get { return assemblies; }
            set { assemblies = value; }
        }
    }
}
