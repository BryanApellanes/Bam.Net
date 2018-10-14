/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public static class FirebirdSqlRegistrar
    {
        static FirebirdSqlRegistrar()
        {

        }

        /// <summary>
		/// Register the FirebirdSql implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified connectionName.
        /// </summary>
        public static void Register(string connectionName)
        {
            Register(Db.For(connectionName).ServiceProvider);
        }

        /// <summary>
		/// Register the FirebirdSql implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified type.
        /// </summary>
        public static void Register(Type daoType)
        {
            Register(Db.For(daoType).ServiceProvider);
        }

        /// <summary>
		/// Register the FirebirdSql implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : Dao
        {
            Register(Db.For<T>().ServiceProvider);
        }

        public static void Register(Database database)
        {
            Register(database.ServiceProvider);
        }

        /// <summary>
        /// Registser the FirebirdSql implementation of IParameterBuilder, SchemaWriter and QuerySet  
        /// into the specified incubator
        /// </summary>
        /// <param name="incubator"></param>
        public static void Register(Incubator incubator)
        {
            FirebirdSqlParameterBuilder b = new FirebirdSqlParameterBuilder();
            incubator.Set<IParameterBuilder>(b);

            incubator.Set<SqlStringBuilder>(() => new FirebirdSqlSqlStringBuilder());
            incubator.Set<SchemaWriter>(() => new FirebirdSqlSqlStringBuilder());
            incubator.Set<QuerySet>(() => new FirebirdSqlQuerySet());
        }
    }
}
