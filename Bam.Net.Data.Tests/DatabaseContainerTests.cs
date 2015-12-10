/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
//using Naizari.Testing;
using System.IO;
//using Naizari.Data;
//using Naizari.Helpers;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Testing;
using System.Configuration;
using MySql.Data.MySqlClient;
using Bam.Net.Incubation;
using Bam.Net.CommandLine;
using System.Data.OleDb;

namespace Bam.Net.Data.Tests
{
    [Table(ConnectionName = "Test")]
    public class TestDao : Dao
    {
        public override IQueryFilter GetUniqueFilter()
        {
            return null;
            //throw new NotImplementedException();
        }

        public override void Delete(Database db = null)
        {
            throw new NotImplementedException();
        }
    }

    public class DatabaseContainerTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void ShouldGetStaticReadOnlyInstanceField()
        {
            FieldInfo field = typeof(SqlClientFactory).GetField("Instance");
            object inst = field.GetValue(null);
            Expect.IsNotNull(inst);
        }

        [UnitTest]
        public static void DatabaseNameShouldNotBeNull()
        {
            Database db = Db.For("DaoRef");//_.Db["DaoRef"];
            Expect.IsNotNullOrEmpty(db.Name);
            OutLine(db.Name, ConsoleColor.Yellow);
        }

        [UnitTest]
        public static void ShouldGetDatabaseFactory()
        {
            DbProviderFactory factory = Db.For(typeof(TestDao)).ServiceProvider.Get<DbProviderFactory>();//_.Db[typeof(TestDao)].ServiceProvider.Get<DbProviderFactory>();
            Expect.IsNotNull(factory);
            factory.IsInstanceOfType<SqlClientFactory>();
        }

        [UnitTest]
        public static void ShouldGetDatabaseUsingGeneric()
        {
            DbProviderFactory factory = Db.For<TestDao>().ServiceProvider.Get<DbProviderFactory>();
            Expect.IsNotNull(factory);
            factory.IsInstanceOfType<SqlClientFactory>();
            Type type = factory.GetType();
            OutLineFormat("{0}.{1}", ConsoleColor.Cyan, type.Namespace, type.Name);
        }

    }
}
