using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    /// <summary>
    /// Sets database properties for a given set of object instances.
    /// </summary>
    /// <typeparam name="T">The type of database to use</typeparam>
    public abstract class DatabaseProvider<T> : IDatabaseProvider where T : Database, new()
    {
        public ILogger Logger
        {
            get;set;
        }

        /// <summary>
        /// Sets all properties on the specified instances, where the property 
        /// is writeable and is of type Database, to an instance of T 
        /// </summary>
        /// <param name="instances"></param>
        public virtual void SetDatabases(params object[] instances)
        {
            instances.Each(instance =>
            {
                Type type = instance.GetType();
                type.GetProperties().Where(pi => pi.CanWrite && pi.PropertyType.Equals(typeof(Database))).Each(new { Instance = instance }, (ctx, pi) =>
                {
                    T db = GetDatabaseFor(instance);
                    if (pi.HasCustomAttributeOfType(out SchemasAttribute schemas))
                    {
                        TryEnsureSchemas(db, schemas.DaoSchemaTypes);
                    }
                    pi.SetValue(ctx.Instance, db);
                });
            });
        }

        public abstract T GetDatabaseFor(object instance);
        public abstract T GetDatabaseFor(Type objectType, string info = null);
        public abstract string GetDatabasePathFor(Type type, string info = null);
        private void TryEnsureSchemas(Database db, params Type[] daoTypes)
        {
            daoTypes.Each(new { Database = db, Logger = Logger }, (daoContext, dao) =>
            {
                daoContext.Database.TryEnsureSchema(dao, daoContext.Logger);
            });
        }
    }
}
