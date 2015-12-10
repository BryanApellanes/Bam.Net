/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Data;
using Naizari.Data;
using Naizari.Configuration;
using System.Web;
using Naizari.Extensions;
using Naizari.Helpers;
using Naizari.Testing;

namespace Naizari.Data
{
    public class DaoContext
    {
        class DaoContextStore
        {
            Dictionary<string, DaoContext> contexts;
            public DaoContextStore()
            {
                this.contexts = new Dictionary<string, DaoContext>();
            }

            public bool ContainsKey(string contextName)
            {
                return this.contexts.ContainsKey(contextName);
            }

            object contextLock = new object();
            public DaoContext this[string contextName]
            {
                get
                {
                    return this.contexts[contextName];
                }
                set
                {
                    lock (contextLock)
                    {
                        if (this.contexts.ContainsKey(contextName))
                        {
                            this.contexts[contextName] = value;
                        }
                        else
                        {
                            this.contexts.Add(contextName, value);
                        }
                    }
                }
            }

            public void Add(string contextName, DaoContext daoContext)
            {
                this.contexts.Add(contextName, daoContext);
            }
        }

        class DaoContextInfo
        {
            Dictionary<string, string> connectionStrings;
            public DaoContextInfo()
            {
                this.connectionStrings = new Dictionary<string, string>();
            }
            public void Add(string logicalName, string connectionString)
            {
                this.connectionStrings.Add(logicalName, connectionString);
            }

            public bool ContainsKey(string logicalName)
            {
                return this.connectionStrings.ContainsKey(logicalName);
            }

            public string this[string logicalName]
            {
                get
                {
                    return this.connectionStrings[logicalName];
                }
                set
                {
                    this.connectionStrings[logicalName] = value;
                }
            }
        }
        
        static DaoContextStore contexts;
        static Dictionary<string, DaoDbType> contextDatabaseTypes;
        static DaoContext()
        {
            contexts = SingletonHelper.GetApplicationProvider<DaoContextStore>(new DaoContextStore());//new Dictionary<string, DaoContext>();
            contextDatabaseTypes = new Dictionary<string, DaoDbType>();
        }

        /// <summary>
        /// Initializes a DaoContext with the specified logicalName using the 
        /// specified connectionString.
        /// </summary>
        /// <param name="contextName">The logical name of the DaoContext to initialize.  
        /// In the configuration file this would be in the form DaoContext.&lt;contextName&gt;</param>
        /// <param name="connectionString">The connection string to use with the
        /// specified DaoContext</param>
        public static void Init(string contextName, string connectionString)
        {
            Get(contextName, connectionString);
        }

        /// <summary>
        /// Get the current DaoContext instance for the specified logicalName.
        /// If a DaoContext has already been instantiated for the specified logicalName
        /// the connectin string is updated with the one specified.
        /// </summary>
        /// <param name="contextName">The name of the DaoContext to retrieve.</param>
       /// <returns>DaoContext instance.</returns>
        public static DaoContext Get(string contextName)
        {
            return Get(contextName, string.Empty);
        }

        /// <summary>
        /// Get the current DaoContext instance for the specified logicalName.
        /// If a DaoContext has already been instantiated for the specified logicalName
        /// the connection string is updated with the one specified.
        /// </summary>
        /// <param name="contextName">The name of the DaoContext to retrieve.</param>
        /// <param name="connString">The connection string to assign to the specified</param>
        /// <returns>DaoContext instance.</returns>
        public static DaoContext Get(string contextName, string connString)
        {
            if (contexts.ContainsKey(contextName))
            {
                return contexts[contextName];
            }

            // if no connection string was specified check if this logical name has
            // been initialized
            string connectionString = connString;

            if( string.IsNullOrEmpty(connectionString) )
                connectionString = GetContextConnectionString(contextName);

            DatabaseAgent agent = DatabaseAgent.CreateAgent(GetContextDatabaseType(contextName), connectionString);

            DaoContext dao = new DaoContext();
            
            dao.DatabaseAgent = agent;

            if (!contexts.ContainsKey(contextName))
            {
                contexts.Add(contextName, dao);
            }
            else
            {
                contexts[contextName] = dao;
            }

            OnDaoContextConnectionStringSet(dao, contextName);

            return dao;
        }

        public static event DaoContextConnectionStringSetEventHandler DaoContextConnectionStringSet;

        private static void OnDaoContextConnectionStringSet(DaoContext context, string logicalName)
        {
            if (DaoContextConnectionStringSet != null)
                DaoContextConnectionStringSet(context, logicalName);
        }

        static DaoContextInfo staticConnectionStrings;
        /// <summary>
        /// Used to explicitly set the connection string used by the specified contextName.
        /// </summary>
        /// <param name="contextName"></param>
        /// <param name="connectionString"></param>
        public static void SetContextConnectionstring(string contextName, string connectionString)
        {
            if (staticConnectionStrings == null)
                staticConnectionStrings = new DaoContextInfo();
            if (staticConnectionStrings.ContainsKey(contextName))
                staticConnectionStrings[contextName] = connectionString;
            else
            {
                staticConnectionStrings.Add(contextName, connectionString);
                //OnDaoContextConnectionStringSet(DaoContext.Get(logicalName), logicalName);
            }

        }
        /// <summary>
        /// Gets the connection string for the DaoContext with the specified logical name.
        /// The component will first check if any call has been made to SetContextConnectionString
        /// then check the default configuration (app.config or web.config) returning an empty string
        /// if the connection string is not found.
        /// </summary>
        /// <param name="contextName">The logical name returned by the implementation of
        /// DaoObject.ContextName.</param>
        /// <returns>A database connection string to be used for the specified logicalName.</returns>
        public static string GetContextConnectionString(string contextName)
        {
            if (contexts == null)
            {
                contexts = SingletonHelper.GetApplicationProvider<DaoContextStore>(new DaoContextStore());
            }

            if (staticConnectionStrings != null && staticConnectionStrings.ContainsKey(contextName))
                return staticConnectionStrings[contextName];
            
            if (contexts != null
                && contexts.ContainsKey(contextName))
                return contexts[contextName].ConnectionString;

            string property = DefaultConfiguration.GetProperty(typeof(DaoContext).Name, contextName);//CascadeConfiguration.GetProperty(logicalName, true, typeof(DaoContext));

            if (property.StartsWith("$"))
            {
                string propertyName = property.Replace("$", "");
                if (propertyName.Equals(contextName))
                {
                    throw new InvalidOperationException("Invalid DaoContext $ reference.  Must not be the same name as the context or Stack overflow will occur");
                }
                property = GetContextConnectionString(propertyName);
            }
            

            return property;
        }

        public static DaoDbType GetContextDatabaseType(string logicalName)
        {
            if (contextDatabaseTypes.ContainsKey(logicalName))
                return contextDatabaseTypes[logicalName];

            string type = typeof(DaoContext).Name;
            string property = DefaultConfiguration.GetProperty(type + "." + logicalName, "DaoDbType");

            if (string.IsNullOrEmpty(property))
                CascadeConfiguration.ThrowException(type + "." + logicalName, "DaoDbType");

            return (DaoDbType)Enum.Parse(typeof(DaoDbType), property, true);
        }

        static object lockContextTypes = new object();
        public static void SetContextDatabaseType(string contextName, DaoDbType type)
        {
            lock (lockContextTypes)
            {
                if (contextDatabaseTypes.ContainsKey(contextName))
                {
                    if (contextDatabaseTypes[contextName] == type)
                        return;

                    ExceptionHelper.Throw<InvalidOperationException>(
                        "Cannot set database provider type on DaoContext '{0}' to '{1}' because it has already been set to '{2}'",
                        contextName, type.ToString(), contextDatabaseTypes[contextName]);
                }
                else
                {
                    contextDatabaseTypes.Add(contextName, type);
                }
            }
        }
        /// <summary>
        /// Gets a map of properties to column names for the specified generic type T.
        /// </summary>
        /// <typeparam name="T">The type of the property to column map to get.</typeparam>
        /// <returns>Dictionary&lt;string, string&gt;</returns>
        public static Dictionary<string, string> GetPropertyToColumnMap<T>() where T : DaoObject, new()
        {
            T val = new T();
            return val.GetPropertyToColumnMap();
        }

        /// <summary>
        /// The connection string assigned to the current DaoContext instance.
        /// </summary>
        public string ConnectionString
        {
            get { return DatabaseAgent.ConnectionString; }
        }

        public DatabaseAgent DatabaseAgent { get; set; }
    }
}
