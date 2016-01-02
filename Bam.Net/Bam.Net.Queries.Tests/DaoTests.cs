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
//using Bam.Net.Hatagi.Data;
using Bam.Net.OAuth;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Data.Tests
{
    public class DaoTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void YouNeedToFixTheseTests()
        {
            Expect.Fail("The tests in this file need to be uncommented and reorganized to remove the Hatagi.Data dependency");
        }
    }

    //public class DaoTests : CommandLineTestInterface
    //{
    //    [UnitTest]
    //    public static void ReproduceHatagiSchema()
    //    {
    //        Dao.ProxyConnection(typeof(Item), "Hatagi_Tests");
    //        SqlClientRegistrar.Register<Item>();
    //        Database db = _.Db[typeof(Item)];
    //        Out(db.ConnectionString, ConsoleTextColor.Yellow);
            
    //        SchemaWriter sw = db.ServiceProvider.GetNew<SchemaWriter>();
    //        sw.EnableDrop = true;
    //        sw.DropAllTables<Item>();
    //        sw.Execute(db);
    //        sw = db.ServiceProvider.GetNew<SchemaWriter>();
    //        Expect.IsTrue(sw.WriteSchemaScript<Item>());
    //        Expect.IsTrue(sw.TryExecute(db));
    //    }

    //    [UnitTest]
    //    public static void FromFacebookIdentity()
    //    {
    //        Dao.ProxyConnection(typeof(Item), "Real");
    //        SqlClientRegistrar.Register<Item>();            

    //        JObject juser = new JObject();
    //        juser["id"] = "520054713";
    //        FacebookIdentity id = new FacebookIdentity(juser, "testToken");
    //        User u = User.FromFacebookIdentity(id);
    //        Expect.IsNotNull(u);            
    //    }

    //    [UnitTest]
    //    public static void ProxyConnectionNameShouldCauseNewNameForConnectionName()
    //    {
    //        string name1 = Dao.ConnectionName(typeof(Item));
            
    //        OutFormat("ConnectionName for Item: {0}", name1);
    //        Expect.AreEqual("Hatagi", name1);

    //        Dao.ProxyConnection(typeof(Item), "bananas");
            
    //        string name2 = Dao.ConnectionName(typeof(Item));
    //        Expect.IsFalse(name2.Equals(name1));
    //        Expect.IsTrue(name2.Equals("bananas"));
    //        OutFormat("ConnectionName after proxy: {0}", name2);

    //    }

    //    [UnitTest]
    //    public static void ToDataRowShouldReturnDataRowWithColumns()
    //    {
    //        TestDao td = new TestDao();
    //        td.Column = "Column".RandomString(8);
    //        td.Fk = 8;
    //        td.Id = 9;

    //        DataRow row = td.ToDataRow();
    //        bool tested = false;
    //        foreach (PropertyInfo prop in td.GetType().GetProperties())
    //        {
    //            ColumnAttribute cat;
    //            if (prop.HasCustomAttributeOfType<ColumnAttribute>(true, out cat))
    //            {
    //                Expect.IsNotNull(row.Table.Columns[prop.Name]);
    //                Expect.IsNotNull(row[cat.Name]);
    //                Out(row[cat.Name].ToString(), ConsoleTextColor.Yellow);
    //                tested = true;
    //            }
    //        }

    //        Expect.IsTrue(tested);
    //    }
    //}
}
