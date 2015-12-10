/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using Bam.Net.Incubation;

namespace Bam.Net.Data
{
    /// <summary>
    /// Acts as a convenience container for any IDatabaseInitializer
    /// implementations that should be used to resolve Database 
    /// requests when using generated Dao objects.
    /// </summary>
    public abstract class DatabaseInitializers
    {
        static Dictionary<Type, IDatabaseInitializer> _initializers;

        static DatabaseInitializers()
        {
            _initializers = new Dictionary<Type, IDatabaseInitializer>();
            AddInitializer(DefaultDatabaseInitializer.Instance);
        }

        public DatabaseInitializers()
        {
            
        }

        /// <summary>
        /// If the specified IDatabaeInitializer of generic type T has been
        /// added, this will cause it to ignore initialization requests for 
        /// the speicfied connectionName to allow another initializer the
        /// chance to initialize it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static bool Ignore<T>(string connectionName) where T : IDatabaseInitializer
        {
            bool result = false;
            Type type = typeof(T);
            if(_initializers.ContainsKey(type))
            {
                _initializers[type].Ignore(connectionName);
                result = true;
            }

            return result;
        }

        public static void AddInitializer(IDatabaseInitializer initializer)
        {
            if (initializer != null)
            {
                Type type = initializer.GetType();
                if (!_initializers.ContainsKey(type))
                {
                    _initializers.Add(type, initializer);
                }
            }
        }

        public static void Clear()
        {
            _initializers.Clear();
        }

        public static void Remove(IDatabaseInitializer instance)
        {
            Type type = instance.GetType();
            _initializers.Remove(type);
        }

        /// <summary>
        /// Tries to initialize the database for the specified connectionName using
        /// the registered DatabaseInitializers
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static DatabaseInitializationResult TryInitialize(string connectionName)
        {
            List<string> _exceptionMessages = new List<string>();
            foreach (IDatabaseInitializer di in _initializers.Values)
            {
                DatabaseInitializationResult r = di.Initialize(connectionName);
                if (r.Success)
                {
                    r.Initializer = di;
                    return r;
                }
                else
                {
                    if (r.Exception != null)
                    {
                        _exceptionMessages.Add(r.Exception.Message);
                    }
                }
            }
            
            return new DatabaseInitializationResult(null, new DatabaseInitializationFailedException(_exceptionMessages));
        }
        
    }
}
