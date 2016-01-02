/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Incubation;
using System.Data.SQLite;
using Bam.Net.Data;
using Bam.Net;
using System.Reflection;
using System.IO;

namespace Bam.Net.Data
{
    public class SQLiteRegistrar
    {
        static object _monitorLock = new object();
        static bool _added;
        static SQLiteRegistrar()
        {
            MonitorBitness();
        }

        public static void MonitorBitness()
        {
            if (!_added)
            {
                lock (_monitorLock)
                {
                    if (!_added)
                    {
                        _added = true;
                        AppDomain.CurrentDomain.AssemblyResolve += (o, a) =>
                        {
                            if (a.Name.StartsWith("System.Data.SQLite"))
                            {
                                string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                string fileName = Path.Combine(assemblyDir, string.Format("SQLite\\{0}\\{1}.dll", IntPtr.Size == 4 ? "x86" : "x64", a));
                                Console.WriteLine("Loading SQLite from: {0}"._Format(fileName));
                                return Assembly.LoadFrom(fileName);
                            }
                            return null;
                        };
                    }
                }
            }
        }

        public static void OutputSQLiteFactoryAssemblyQualifiedName()
        {
            Console.WriteLine(SQLiteFactoryAssemblyQualifiedName());
        }

        public static string SQLiteFactoryAssemblyQualifiedName()
        {
            return typeof(SQLiteFactory).AssemblyQualifiedName;
        }

        /// <summary>
        /// Register the SQLite components with the ServiceProvider 
        /// of the specified database.  This Register method will
        /// not call SetInitializerAndConnectionStringResolver
        /// like the other Register methods do.
        /// </summary>
        /// <param name="database"></param>
        public static void Register(Database database)
        {
            Register(database.ServiceProvider);
        }

        /// <summary>
        /// Registers SQLite as the handler for the specified connection name.
        /// This dao handler will register apropriate DatabaseInitializer and
        /// ConnectionStringResolver.  This behavior is different compared to the
        /// SqlClientRegistrar's Register method.
        /// </summary>
        /// <param name="connectionName"></param>
        public static void Register(string connectionName)
        {
            SetInitializerAndConnectionStringResolver(connectionName);
            Register(Db.For(connectionName).ServiceProvider);
        }

        public static void Register(Type daoType)
        {
            SetInitializerAndConnectionStringResolver(daoType);
            Register(Db.For(daoType).ServiceProvider);
        }

        public static void Register<T>() where T : Dao
        {
            SetInitializerAndConnectionStringResolver(typeof(T));
            Register(Db.For<T>().ServiceProvider);
        }

        private static void SetInitializerAndConnectionStringResolver(Type daoType)
        {
            SetInitializerAndConnectionStringResolver(Dao.ConnectionName(daoType));
        }

        private static void SetInitializerAndConnectionStringResolver(string connectionName)
        {
            DatabaseInitializers.Ignore<DefaultDatabaseInitializer>(connectionName);
            DatabaseInitializers.AddInitializer(new SQLiteDatabaseInitializer());
            SQLiteConnectionStringResolver.Register();
        }

        public static void Register(Incubator incubator)
        {
            SQLiteConnectionStringResolver.Register();

            incubator.Set<IParameterBuilder>(() => new SQLiteParameterBuilder());
            incubator.Set<SqlStringBuilder>(() => new SqlStringBuilder());
            incubator.Set<SchemaWriter>(() => new SQLiteSqlStringBuilder());
            incubator.Set<QuerySet>(() => new SQLiteQuerySet());
        }

        /// <summary>
        /// Registers SQLite as the fallback initializer for all databases.
        /// This means that if the default database initializers fail SQLite
        /// will register itself as the database container and retry database
        /// initialization.
        /// </summary>
        public static void RegisterFallback()
        {
            Bam.Net.Data.Db.DefaultContainer.FallBack = (connectionName, dbDictionary) =>
            {
                Register(connectionName);
            };
        }
    }
}
