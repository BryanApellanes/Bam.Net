﻿using System;
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
                    T db = GetSysDatabaseFor(instance);
                    if (pi.HasCustomAttributeOfType(out SchemasAttribute schemas))
                    {
                        TryEnsureSchemas(db, schemas.DaoSchemaTypes);
                    }
                    pi.SetValue(ctx.Instance, db);
                });
            });
        }

        public virtual void SetDefaultDatabaseFor<TDao>() where TDao : Dao
        {
            SetDefaultDatabaseFor<TDao>(out Database db);
        }

        public virtual void SetDefaultDatabaseFor<TDao>(out Database db) where TDao: Dao
        {
            db = GetSysDatabaseFor(typeof(TDao));
            Db.For<TDao>(db);
        }

        public abstract T GetAppDatabaseFor(IApplicationNameProvider appNameProvider, object instance);
        public abstract T GetSysDatabaseFor(object instance);
        public abstract T GetAppDatabaseFor(IApplicationNameProvider appNameProvider, Type objectType, string info = null);
        public abstract T GetSysDatabaseFor(Type objectType, string info = null);
        public abstract string GetAppDatabasePathFor(IApplicationNameProvider appNameProvider, Type type, string info = null);
        public abstract string GetSysDatabasePathFor(Type type, string info = null);
        private void TryEnsureSchemas(Database db, params Type[] daoTypes)
        {
            daoTypes.Each(new { Database = db, Logger = Logger }, (daoContext, dao) =>
            {
                daoContext.Database.TryEnsureSchema(dao, daoContext.Logger);
            });
        }
    }
}
