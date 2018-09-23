using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using System.IO;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    /// <summary>
    /// Sets database properties for a given object instance.
    /// </summary>
    public class SQLiteDatabaseProvider : IDatabaseProvider
    {
        public SQLiteDatabaseProvider(string root, ILogger logger = null)
        {
            Root = root;
            Logger = logger ?? Log.Default;
        }
        
        public ILogger Logger { get; set; }
        public string Root { get; set; }

        public Database GetAppDatabase(IApplicationNameProvider appNameProvider, string databaseName)
        {
            throw new NotImplementedException();
        }

        public Database GetAppDatabaseFor(IApplicationNameProvider appNameProvider, object instance)
        {
            throw new NotImplementedException();
        }

        public Database GetAppDatabaseFor(IApplicationNameProvider appNameProvider, Type objectType, string info = null)
        {
            throw new NotImplementedException();
        }

        public Database GetSysDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public Database GetSysDatabaseFor(object instance)
        {
            throw new NotImplementedException();
        }

        public Database GetSysDatabaseFor(Type objectType, string info = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets all properties on the specified instances, where the property type 
        /// is Database, to a SQLiteDatabase with it's root folder set to {Root}
        /// and the name of the database to {Type.Name}.{PropertyInfo.Name}
        /// </summary>
        /// <param name="instances"></param>
        public void SetDatabases(params object[] instances)
        {
            instances.Each(new { Provider = this }, (ctx, instance) =>
            {
                ctx.Provider.SetDatabases(instance, Root);
            });
        }

        /// <summary>
        /// Sets all properties on the specified instances, where the property type 
        /// is Database, to a SQLiteDatabase with it's root folder set to {Root}
        /// and the name of the database to {Type.Name}.{PropertyInfo.Name}
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="root"></param>
        public void SetDatabases(object instance, string root)
        {
            Type type = instance.GetType();
            type.GetProperties().Where(pi => pi.PropertyType.Equals(typeof(Database))).Each(new { Instance = instance }, (ctx, pi) =>
            {
                Database db = new SQLiteDatabase(root, $"{type.Name}.{pi.Name}");
                if (pi.HasCustomAttributeOfType(out SchemasAttribute schemas))
                {
                    TryEnsureSchemas(db, schemas.DaoSchemaTypes);
                }
                pi.SetValue(ctx.Instance, db);
            });
        }

        private void TryEnsureSchemas(Database db, params Type[] daoTypes)
        {
            daoTypes.Each(new { Database = db, Logger = Logger }, (daoContext, dao) =>
            {
                daoContext.Database.TryEnsureSchema(dao, daoContext.Logger);
            });
        }
    }
}
