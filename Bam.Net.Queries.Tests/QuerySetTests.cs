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
using Bam.Net.DaoReferenceObjects.Data;
using System.Web.Mvc;

namespace Bam.Net.Data.Tests
{
    class TestQuerySet: QuerySet
    {
        public bool? SubscribeCalled { get; set; }
        protected internal override void SubscribeToExecute()
        {
            SubscribeCalled = true;
        }
    }

    [Serializable]
    public class QuerySetTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void OutputSQLiteName()
        {
            SQLiteRegistrar.OutputAssemblyQualifiedName();
        }

        [UnitTest]
        public static void QuerySetShouldCallSubscribeToExecute()
        {
            TestQuerySet t = new TestQuerySet();
            Expect.IsTrue(t.SubscribeCalled.Value);
        }

        [UnitTest]
        public static void QuerySetWhereShouldWorkAsExpected()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();
            DaoReferenceObject test = Create();
            DaoReferenceObject test2 = Create();
            
            QuerySet query = new QuerySet();
            DaoReferenceObject third = new DaoReferenceObject();
            third.StringProperty = test.StringProperty;
            
            query.Insert(third);
            query.Select<DaoReferenceObject>().Where<DaoReferenceObjectColumns>(c => c.StringProperty == third.StringProperty);


            query.Execute(Db.For<DaoReferenceObject>());

            DaoReferenceObjectCollection coll = query.Results.As<DaoReferenceObjectCollection>(1);
            foreach (DaoReferenceObject obj in coll)
            {
                Out(obj.PropertiesToString());
                Out();
            }
        }

        private static DaoReferenceObject Create()
        {
            DaoReferenceObject test = new DaoReferenceObject();
            test.StringProperty = "".RandomString(8);
            test.Commit();
            return test;
        }

        [UnitTest]
        public static void QuerySetShouldFillIHasDataTableList()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();
            QuerySet testQuerySet = new QuerySet();
            Expect.IsNull(testQuerySet.DataSet);
            DaoReferenceObject test = new DaoReferenceObject();
            test.StringProperty = "".RandomString(8);
            test.Commit();

            testQuerySet.Select<DaoReferenceObjectColumns, DaoReferenceObject>(f => f.Id == test.Id);

            DaoReferenceObject dao = new DaoReferenceObject();
            dao.StringProperty = "".RandomString(5);

            testQuerySet.Insert<DaoReferenceObject>(dao);

            testQuerySet.Execute(Db.For<DaoReferenceObject>());

            Expect.IsNotNull(testQuerySet.DataSet);
            Expect.IsTrue(testQuerySet.DataSet.Tables.Count == 2);
            Expect.IsGreaterThan(testQuerySet.Results.Count, 0);
            Expect.AreEqual(2, testQuerySet.Results.Count);

            DaoReferenceObject d = testQuerySet.Results.ToDao<DaoReferenceObject>(1);
            Expect.AreEqual(dao.StringProperty, d.StringProperty);

            DaoReferenceObjectCollection coll = testQuerySet.Results.As<DaoReferenceObjectCollection>(0);
            Expect.IsNotNull(coll);
            Expect.IsGreaterThan(coll.Count, 0);
        }

        [UnitTest]
        public static void QuerySetCount()
        {
            SqlClientRegistrar.Register<Item>();
            QuerySet query = new QuerySet();
            query.Count<Item>().Where<Bam.Net.Data.Tests.ItemColumns>(c => c.Name.StartsWith("Mort"));
            query.Execute(Db.For<Item>());

            long result = query.Results[0].As<CountResult>().Value;
            long toCountResult = query.Results.ToCountResult(0);

            Expect.AreEqual(result, toCountResult);
            Out(query.Results.ToCountResult(0).ToString());
        }

        [UnitTest]
        public void AlternativeSyntaxTest()
        {
            SqlClientRegistrar.Register<Item>();
            Db.TryEnsureSchema<Item>();

            Database db = Db.For(typeof(Item));

            Item createdItem = new Item();
            createdItem.Name = "Item_".RandomLetters(8);
            QuerySet query = new QuerySet();
            query.Insert<Item>(createdItem);
            query.Select<Item>().Where<ItemColumns>(c => c.Name.StartsWith("I"));
            query.Count<Item>();

            query.Execute(db);

            // alternative syntax

            //query.Insert<Item>(createdItem)
            //    .Select<Item>().Where<ItemColumns>(c => c.Name.StartsWith("I"))
            //    .Count<Item>()
            //    .Execute(db);

            // -- end alternative syntax

            Item insertedItem = query.Results.ToDao<Item>(0);
            OutLineFormat("InsertedItemId: {0}, Name: {1}", ConsoleColor.Green, insertedItem.Id, insertedItem.Name);

            ItemCollection items = query.Results[1].As<ItemCollection>();

            OutLine("** Item Results **", ConsoleColor.DarkYellow);
            items.Each(item =>
            {
                OutLineFormat("Id: {0}, Name: {1}", ConsoleColor.DarkYellow, item.Id, item.Name);
            });

            long count = query.Results[2].As<CountResult>().Value;
            OutLineFormat("Count Result: {0}", ConsoleColor.Yellow, count);
        }

        //[UnitTest]
        //public static void WhatIfNoResults()
        //{
        //    Dao.ProxyConnection(typeof(Item), "Real");
        //    SqlClientRegistrar.Register<Item>();

        //    User user = User.OneWhere(c => c.Email == "bryan.apellanes@gmail.com");
        //    int itemId = 1;
        //    QuerySet query = new QuerySet();
        //    // index 0
        //    query.Select<Want>().Where<WantColumns>(c => c.UserId == user.Id && c.ItemId == itemId);
        //    // index 1
        //    query.Select<Item>().Where<ItemColumns>(c => c.Id == itemId);
        //    query.Execute(Db.For<Item>());
        //    WantCollection wants = query.Results.As<WantCollection>(0);
        //    Expect.IsTrue(wants.Count == 0);
        //    ItemCollection items = query.Results.As<ItemCollection>(1);
        //    Expect.IsTrue(items.Count > 0);
        //    Out(items[0].Name);
        //    Dao.ProxyConnection(typeof(Item), "Hatagi");
        //}
    }
}
