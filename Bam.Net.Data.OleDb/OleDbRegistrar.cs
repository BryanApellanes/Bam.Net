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
    public static class OleDbRegistrar
    {
        static OleDbRegistrar()
        {
            
        }

        /// <summary>
        /// Register the OledB implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified connectionName.
        /// </summary>
        public static void Register(string connectionName)
        {
            Register(Db.For(connectionName).ServiceProvider);
        }

        /// <summary>
        /// Register the MS implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified type.
        /// </summary>
        public static void Register(Type daoType)
        {
            Register(Db.For(daoType).ServiceProvider);
        }

        /// <summary>
        /// Register the MS implementation of IParameterBuilder, SchemaWriter and QuerySet for the 
        /// database associated with the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T: Dao
        {
            Register(Db.For<T>().ServiceProvider);
        }

        /// <summary>
        /// Register the MS implementation of IParameterBuilder, SchemaWriter and QuerySet for the
        /// specified database
        /// </summary>
        /// <param name="database"></param>
        public static void Register(Database database)
        {
            Register(database.ServiceProvider);
        }

        /// <summary>
        /// Registser the MS implementation of IParameterBuilder, SchemaWriter and QuerySet  
        /// into the specified incubator
        /// </summary>
        /// <param name="incubator"></param>
        public static void Register(Incubator incubator)
        {
            OleDbParameterBuilder b = new OleDbParameterBuilder();
            incubator.Set<IParameterBuilder>(b);

            incubator.Set<SqlStringBuilder>(() => new OleDbSqlStringBuilder());
            incubator.Set<SchemaWriter>(() => new OleDbSqlStringBuilder());
            incubator.Set<QuerySet>(() => new OleDbQuerySet());
        }
    }
}
