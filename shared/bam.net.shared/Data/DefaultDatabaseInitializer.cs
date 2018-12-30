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
	/// A DatabaseInitializer that reads the connection string from the 
	/// default configuration file
	/// </summary>
    public class DefaultDatabaseInitializer: IDatabaseInitializer
    {
        static DefaultDatabaseInitializer _instance;
        static object _instanceLock = new object();
        /// <summary>
        /// The DefaultInitilizer Instance
        /// </summary>
        public static DefaultDatabaseInitializer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DefaultDatabaseInitializer();
                        }
                    }
                }
                return _instance;
            }
        }

        List<string> _ignoreConnectionNames = new List<string>();
        #region IDatabaseInitializer Members

        /// <summary>
        /// Causes the current DatabaseInitializer to NOT initialize the 
        /// database for the connection name associated with the specified
        /// types.
        /// </summary>
        /// <param name="types"></param>
        public void Ignore(params Type[] types)
        {
            foreach (Type type in types)
            {
                string connectionName = Dao.ConnectionName(type);
                if (!_ignoreConnectionNames.Contains(connectionName))
                {
                    _ignoreConnectionNames.Add(connectionName);
                }
            }
        }

        /// <summary>
        /// Causes the current DatabaseInitializer to NOT initialize the
        /// database for the connection names specified.
        /// </summary>
        /// <param name="connectionNames"></param>
        public void Ignore(params string[] connectionNames)
        {
            foreach (string con in connectionNames)
            {
                if (!_ignoreConnectionNames.Contains(con))
                {
                    _ignoreConnectionNames.Add(con);
                }
            }
        }

        public virtual DatabaseInitializationResult Initialize(string connectionName)
        {
            if (_ignoreConnectionNames.Contains(connectionName))
            {
                DatabaseInitializationResult result = new DatabaseInitializationResult(null, Args.Exception<Exception>("connection explicitly ignored: {0}", connectionName));                
                return result;
            }

            try
            {
                if (string.IsNullOrEmpty(connectionName))
                {
                    throw new ArgumentNullException("connectionName");
                }

                ConnectionStringResolveResult r = ResolveConnectionString(connectionName);

                ConnectionStringSettings conn = r.Settings;

                if (conn == null)
                {
                    throw new InvalidOperationException(string.Format("The connection name ({0}) was not found in the config file", connectionName));
                }

                Type factoryType = ResolveFactoryType(conn);
                
                DbProviderFactory factory = factoryType.GetField("Instance").GetValue(null) as DbProviderFactory;
                if (factory == null)
                {
                    throw new InvalidOperationException(string.Format("Unable to find Instance field of specified DbProviderFactory ({0})", conn.ProviderName));
                }

                Database database = GetDatabase(conn, factory);

                return new DatabaseInitializationResult(database);
            }
            catch (Exception ex)
            {
                return new DatabaseInitializationResult(null, ex);
            }
        }

        /// <summary>
        /// Instantiates a database uing the specified ConnectionStringSettings and DbProviderFactory
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public virtual Database GetDatabase(ConnectionStringSettings conn, DbProviderFactory factory)
        {
            Incubator serviceProvider = new Incubator();
            serviceProvider.Set<DbProviderFactory>(factory);
            Database database = new Database(serviceProvider, conn.ConnectionString, conn.Name);
            return database;
        }

        /// <summary>
        /// Reads the ProviderName property of the specified ConnectionStringSettings
        /// and uses Type.GetType() to find the type of the DbFactory.
        /// NOTE: This requires the ProviderName to be set to a string value
        /// that Type.GetType() can resolve to a CLI type.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>        
        public virtual Type ResolveFactoryType(ConnectionStringSettings conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException("conn");
            }

            if (string.IsNullOrEmpty(conn.ProviderName))
            {
                throw new ArgumentNullException(string.Format("ProviderName was not specified for connection ({0}).", conn.Name));
            }

            Type factoryType = Type.GetType(conn.ProviderName);

            if (factoryType == null)
            {
                throw new InvalidOperationException("The DbProviderFactory of type ({0}) was not found.  Make sure the AssemblyQualified name was specified in the providerName attribute of the connection string config entry.");
            }
            return factoryType;
        }

        public virtual ConnectionStringResolveResult ResolveConnectionString(string connectionName)
        {
            ConnectionStringResolveResult r = ConnectionStringResolvers.TryResolve(connectionName);
            if (!r.Success)
            {
                throw r.Exception;
            }
            return r;
        }

        #endregion
    }
}
