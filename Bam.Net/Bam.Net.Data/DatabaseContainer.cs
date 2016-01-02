/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using Bam.Net.Incubation;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    public class DatabaseContainer
    {
        Dictionary<string, Database> _databases;        
        public DatabaseContainer()
        {
            this._databases = new Dictionary<string, Database>();
            this.TriedFallback = new List<string>();
        }

        public DatabaseInfo[] GetInfos()
        {
            List<DatabaseInfo> infos = new List<DatabaseInfo>();
            _databases.Keys.Each(ctx =>
            {
                infos.Add(new DatabaseInfo(_databases[ctx]));
            });
            return infos.ToArray();
        }

        /// <summary>
        /// Gets the Database for the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Database For<T>() where T : Dao
        {
            return this[typeof(T)];
        }

        /// <summary>
        /// Gets the Databse for the specified connection name.
		/// This correlates to a connection in the default 
		/// app config file
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public Database For(string connectionName)
        {
            return this[connectionName];
        }

        public Database For(Type type)
        {
            return this[type];
        }

        public DaoTransaction BeginTransaction<T>() where T : Dao
        {
            return Db.BeginTransaction<T>();
        }

        public DaoTransaction BeginTransaction(Type type)
        {
            return Db.BeginTransaction(type);
        }
        /// <summary>
        /// Gets the database for the specified type.
        /// </summary>
        /// <param name="daoType"></param>
        /// <returns></returns>
        public Database this[Type daoType]
        {
            get
            {
                return this[Dao.ConnectionName(daoType)];
            }
            internal set
            {
                this[Dao.ConnectionName(daoType)] = value;
            }
        }
        
        public Database this[string connectionName]
        {
            get
            {
                if (!_databases.ContainsKey(connectionName))
                {
                    InitializeDatabase(connectionName, _databases);
                }

                return _databases[connectionName];
            }
            internal set
            {
                if (_databases.ContainsKey(connectionName))
                {
                    _databases[connectionName] = value;
                }
                else
                {
                    _databases.Add(connectionName, value);
                }
            }
        }

        /// <summary>
        /// The Action to execute if initialization fails
        /// </summary>
        public Action<string, Dictionary<string, Database>> FallBack
        {
            get;
            set;
        }

        protected internal List<string> TriedFallback
        {
            get;
            private set;
        }

        internal void InitializeDatabase(string connectionName, Dictionary<string, Database> databases)
        {
            DatabaseInitializationResult dir = DatabaseInitializers.TryInitialize(connectionName);
            if (dir.Success)
            {
                databases.AddMissing(connectionName, dir.Database);
            }
            else
            {
                if (FallBack != null && !TriedFallback.Contains(connectionName))
                {
                    TriedFallback.Add(connectionName);
                    FallBack(connectionName, databases);
                    InitializeDatabase(connectionName, databases);
                }
                else
                {
                    throw dir.Exception;
                }
            }
        }

    }
}
