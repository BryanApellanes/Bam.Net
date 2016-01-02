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
using Bam.Net.Data.Schema;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Bam.Net.Incubation;
using Bam.Net.Hatagi.Data;
using Bam.Net.DaoReferenceObjects.Data;
using Bam.Net.CommandLine;
using Cr = Bam.Net.Analytics.Data;

namespace Bam.Net.Data.Tests
{
    //public class GeneratedClassTests : CommandLineTestInterface
    //{
    //    [UnitTest]
    //    public static void YouNeedToFixTheseTests()
    //    {
    //        Expect.Fail("The tests in this file need to be uncommented and reorganized to remove the Hatagi.Data dependency");
    //    }
    //}
    [Serializable]
    public class GeneratedClassTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void ProxiedDatabasesShouldBeTheSame()
        {

            Dao.ProxyConnection(typeof(Item), "CRUD_SqlClient");
            SqlClientRegistrar.Register<Item>();
            Database db = Db.For(typeof(Item));//_.Db[typeof(Item)];
            Database check = Db.For("CRUD_SqlClient");//_.Db["CRUD_SqlClient"];
            Database forCheck = Db.For<Item>();

            Expect.AreSame(db, check);
            Expect.AreSame(check, forCheck);
            Expect.AreSame(forCheck, db);
        }

        [UnitTest]
        public static void CrudSqlClient()
        {
            Dao.ProxyConnection(typeof(Item), "CRUD_SqlClient");
            SqlClientRegistrar.Register<Item>();
            Database db = Db.For<Item>();
            SchemaWriter sw = db.ServiceProvider.GetNew<SchemaWriter>();
            sw.EnableDrop = true;
            sw.DropAllTables<Item>();
            sw.TryExecute(db);
            sw.Reset();
            sw.WriteSchemaScript<Item>();

            Exception ex;
            sw.TryExecute(db, out ex);
            Expect.IsNull(ex);

            // Create
            string name = "".RandomString(8);
            Item created = new Item();
            created.Name = name;
            created.Commit();

            // Retrieve
            ItemCollection results = Item.Where(p => p.Name == name);
            Expect.AreEqual(1, results.Count);

            // Update
            Item check = results[0];
            check.Name = "".RandomString(8);
            check.Commit();

            results = Item.Where(p => p.Name == name);
            Expect.AreEqual(0, results.Count);

            results = Item.Where(p => p.Name == check.Name);
            Expect.AreEqual(1, results.Count);

            // Delete
            check.Delete();

            results = Item.Where(p => p.Name == name);
            Expect.AreEqual(0, results.Count);

            results = Item.Where(p => p.Name == check.Name);
            Expect.AreEqual(0, results.Count);
        }

        [UnitTest]
        public static void ShouldRecreateSchemaSQLite()
        {
            Database reproduceIn = RegisterSQLiteForConnection("ReproSchemaSQLite");
            DropAllTables(reproduceIn);
            Incubator sp = reproduceIn.ServiceProvider;
            SQLiteRegistrar.Register(reproduceIn.ServiceProvider);

            SchemaWriter writer = sp.GetNew<SchemaWriter>();

            Expect.IsTrue(writer is SQLiteSqlStringBuilder);
            Expect.IsTrue(writer.WriteSchemaScript<Item>(), "WriteSchemaScript returned false instead of true");
            Exception e;
            bool executeResult = writer.TryExecute(reproduceIn, out e);
            Expect.IsNull(e, e == null ? "" : e.Message); // no exception should have occurred

            Expect.IsTrue(executeResult, "TryExecute returned false instead of true");

            Expect.IsFalse(writer.WriteSchemaScript<Item>(), "WriteSchemaScript returned true instead of false");
        }

        private static Database RegisterSQLiteForConnection(string newConnectionName)
        {
            Dao.ProxyConnection(typeof(Item), newConnectionName);
            SQLiteRegistrar.Register<Item>();
            Database reproduceIn = Db.For(newConnectionName);//_.Db[newConnectionName];

            SetupSQLiteDatabase(reproduceIn);
            return reproduceIn;
        }

        private static Database RegisterSqlClientForConnection(string connection)
        {
            Dao.ProxyConnection(typeof(Item), connection);
            SqlClientRegistrar.Register<Item>();
            Database db = Db.For(connection);//_.Db[connection];

            SetupSqlClientDatabase(db);
            return db;
        }

        private static void SetupSQLiteDatabase(Database reproduceIn)
        {
            SetupDatabases<SQLiteSqlStringBuilder, SQLiteSqlStringBuilder>
                (reproduceIn, new SQLiteParameterBuilder());
        }

        private static void SetupSqlClientDatabase(Database db)
        {
            SetupDatabases<SqlClientSqlStringBuilder, SqlClientSqlStringBuilder>
                (db, new SqlClientParameterBuilder());
        }

        [UnitTest]
        public static void ShouldRecreateSchemaSqlClient()
        {
            SqlClientRegistrar.Register<Item>();
            Database reproduceIn = Db.For("ReproSchemaSqlClient");//_.Db["ReproSchemaSqlClient"];

            Database db = Db.For<Item>();

            SetupDatabases<SqlClientSqlStringBuilder, SqlClientSqlStringBuilder>
                (reproduceIn, db.ServiceProvider.Get<IParameterBuilder>());
            DropAllTables(reproduceIn);

            SchemaWriter writer = db.ServiceProvider.GetNew<SchemaWriter>();

            Expect.IsTrue(writer.WriteSchemaScript<Item>(), "WriteSchemaScript returned false instead of true");
            Exception e;
            bool executeResult = writer.TryExecute(reproduceIn, out e);
            Expect.IsNull(e, e == null ? "" : e.Message); // no exception should have occurred

            Expect.IsTrue(executeResult, "TryExecute returned false instead of true");

            Expect.IsFalse(writer.WriteSchemaScript<Item>(), "WriteSchemaScript returned true instead of false");
        }

        private static void DropAllTables(Database reproduceIn)
        {
            SchemaWriter dropper = reproduceIn.ServiceProvider.GetNew<SchemaWriter>();
            dropper.EnableDrop = true;
            dropper.DropAllTables<Item>();
            Exception ex = null;
            if (!dropper.TryExecute(reproduceIn, out ex))
            {
                Out(ex.Message, ConsoleColor.Yellow);
            }
        }

        private static void SetupDatabases<SSB, SW>(Database reproduceIn, IParameterBuilder paramBuilder)
            where SSB : SqlStringBuilder, new()
            where SW : SchemaWriter
        {
            reproduceIn.ServiceProvider.Set<IParameterBuilder>(paramBuilder);
            reproduceIn.ServiceProvider.Set<SqlStringBuilder>(new SSB());
            reproduceIn.ServiceProvider.SetCtor<SchemaWriter, SW>();
        }

        [UnitTest]
        public static void AddShouldSetAssociation()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject d = new DaoReferenceObject();
            d.StringProperty = "".RandomString(8);
            d.Commit();

            DaoReferenceObjectWithForeignKey test = new DaoReferenceObjectWithForeignKey();
            Expect.IsNull(test.DaoReferenceObjectId);
            d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.Add(test);
            Expect.IsNotNull(test.DaoReferenceObjectId);

            Expect.AreEqual(test.DaoReferenceObjectId, d.Id);
        }

        [UnitTest]
        public static void ParentOfCollectionShouldBeRootDao()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject d = new DaoReferenceObject();
            d.StringProperty = "".RandomString(8);
            d.Commit();

            DaoReferenceObjectWithForeignKey dfk = d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            Expect.AreSame(d, d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.Parent);
        }

        [UnitTest]
        public static void CommitParentShouldCallCommitOnCollections()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();
            DaoReferenceObject d = new DaoReferenceObject();
            d.StringProperty = "".RandomString(8);
            d.Commit();

            var child = d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            child.Name = "Name_".RandomString(3);

            bool? handled = false;
            child.BeforeWriteCommit += (a, i) =>
            {
                handled = true;
            };

            d.Commit();
            Expect.IsTrue(handled.Value);
        }

        [UnitTest]
        public static void ChildCollectionShouldHaveCorrectValues()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();

            string childName = "Name_".RandomString(3);

            DaoReferenceObject d = new DaoReferenceObject();
            d.StringProperty = "".RandomString(8);
            d.Commit();

            var child = d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            child.Name = childName;

            d.Commit();

            d = DaoReferenceObject.OneWhere(f => f.Id == d.Id);

            Expect.AreEqual(1, d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.Count);
            Expect.AreEqual(child.Name, d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId[0].Name);

            child = DaoReferenceObjectWithForeignKey.OneWhere(f => f.Id == d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId[0].Id);

            Expect.AreEqual(childName, child.Name);
            Expect.IsNotNull(child.DaoReferenceObjectOfDaoReferenceObjectId);
            Expect.AreEqual(child.DaoReferenceObjectOfDaoReferenceObjectId.Id, d.Id);
        }

        [UnitTest]
        public static void CommitOnCollectionShouldUpdateIds()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();

            string childName = "Name_".RandomString(3);

            DaoReferenceObject d = new DaoReferenceObject();
            d.StringProperty = "".RandomString(8);
            d.Commit();

            var child = d.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            child.Name = childName;

            d.Commit();

            Expect.IsNotNull(child.IdValue);
        }

        [UnitTest]
        public static void CommitShouldSetId()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject test = new DaoReferenceObject();
            test.BoolProperty = true;
            test.DecimalProperty = (decimal)10.00;
            test.StringProperty = "".RandomString(8);

            Expect.IsNull(test.Id);
            test.Commit();
            Expect.IsNotNull(test.Id);
            OutFormat("The id was {0}", test.Id);
        }

        [UnitTest]
        public static void UpdateShouldOnlyUpdateOne()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();
            string first = "".RandomString(8);
            string second = "".RandomString(4);
            DaoReferenceObject test = new DaoReferenceObject();
            test.BoolProperty = true;
            test.StringProperty = first;
            Expect.IsTrue(test.IsNew);
            test.Commit();

            DaoReferenceObject one = DaoReferenceObject.OneWhere(f => f.StringProperty == test.StringProperty);
            one.StringProperty = second;

            Expect.IsFalse(one.IsNew);
            one.Commit();

            DaoReferenceObjectCollection results = new DaoReferenceObjectCollection(
                Select<DaoReferenceObjectColumns>
                    .From<DaoReferenceObject>()
                    .Where(f => f.StringProperty == second));

            Expect.IsTrue(results.Count == 1);
        }

        [UnitTest]
        public static void DeleteShouldWork()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject test = new DaoReferenceObject();
            test.StringProperty = "".RandomString(8);
            test.Commit();

            DaoReferenceObject d = DaoReferenceObject.OneWhere(c => c.Id == test.Id);
            Expect.IsNotNull(d);
            Expect.AreEqual(test.StringProperty, d.StringProperty);

            d.Delete();

            d = DaoReferenceObject.OneWhere(c => c.StringProperty == test.StringProperty);
            Expect.IsNull(d);
        }

        [UnitTest]
        public static void WhereShortCutShouldWork()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject test = new DaoReferenceObject();
            test.StringProperty = "".RandomString(8);
            test.Commit();

            DaoReferenceObjectCollection checking = DaoReferenceObject.Where(c => c.StringProperty == test.StringProperty);
            Expect.IsTrue(checking.Count == 1);
        }

        [UnitTest]
        public static void ShortcutShouldWorkLikeLongcut()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject test = new DaoReferenceObject();
            string val = "".RandomString(8);
            test.StringProperty = val;
            test.Commit();
            DaoReferenceObjectCollection results = DaoReferenceObject.Where(f => f.Id == test.Id);

            Expect.IsTrue(results.Count > 0);
            Expect.IsTrue(results[0].StringProperty.Equals(val));
        }

        [UnitTest]
        public static void ShouldBeAbleToQueryById()
        {
            SqlClientRegistrar.Register(Db.For<DaoReferenceObject>().ServiceProvider);

            DaoReferenceObject test = new DaoReferenceObject();

            string val = "".RandomString(8);
            test.StringProperty = val;
            test.Commit();

            DaoCollection<DaoReferenceObjectColumns, DaoReferenceObject> results = new DaoCollection<DaoReferenceObjectColumns, DaoReferenceObject>(
                Select<DaoReferenceObjectColumns>
                    .From<DaoReferenceObject>()
                    .Where((c) => c.Id == test.Id)
            );

            Expect.IsNotNull(results);
            Expect.IsTrue(results.Count == 1);
            Expect.AreEqual(results[0].StringProperty, val);
        }

        [UnitTest]
        public static void ChildSetsShouldBeFull()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();
            DaoReferenceObject parent = new DaoReferenceObject();
            parent.StringProperty = "Parent";
            parent.Commit();

            DaoReferenceObjectWithForeignKey child = parent.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            child.Name = "Monkey";

            parent.Commit();

            Expect.IsNotNull(child.Id);

            DaoReferenceObject check = DaoReferenceObject.OneWhere(c => c.Id == parent.Id);
            Expect.IsNotNull(check);
            Expect.AreEqual(1, check.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.Count);
        }

        [UnitTest]
        public static void UnCommittedParentShouldThrowOnChildAdd()
        {
            bool thrown = false;
            try
            {
                SqlClientRegistrar.Register<DaoReferenceObject>();
                DaoReferenceObject parent = new DaoReferenceObject();
                parent.StringProperty = "Parent";
                DaoReferenceObjectWithForeignKey child = parent.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
                child.Name = "".RandomString(8);

                parent.Commit();
            }
            catch (InvalidOperationException ioe)
            {
                thrown = true;
                Out(ioe.Message, ConsoleColor.Cyan);
            }

            Expect.IsTrue(thrown);
        }

        [UnitTest]
        public static void DeleteShouldDeleteChildren()
        {
            SqlClientRegistrar.Register<DaoReferenceObject>();

            DaoReferenceObject parent = new DaoReferenceObject();
            parent.StringProperty = "Parent_".RandomString(3);

            parent.Commit();

            var d1 = parent.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            d1.Name = "".RandomString(3);

            var d2 = parent.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.AddNew();
            d2.Name = "".RandomString(3);

            parent.Commit();

            Expect.IsNotNull(d2.Id);

            DaoReferenceObject check = DaoReferenceObject.OneWhere(c => c.Id == parent.Id);
            DaoReferenceObjectWithForeignKey check2 = DaoReferenceObjectWithForeignKey.OneWhere(c => c.Id == d2.Id);

            Expect.AreEqual(2, check.DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId.Count);
            Expect.IsNotNull(check2);

            check.Delete();

            check2 = DaoReferenceObjectWithForeignKey.OneWhere(c => c.Id == d2.Id);
            Expect.IsNull(check2);
        }

        [UnitTest]
        public void ShouldRetrievePortEighty()
        {
            SQLiteRegistrar.Register("Crawlers");
            Db.TryEnsureSchema("Crawlers");

            Cr.Port _80 = Cr.Port.OneWhere(c => c.Value == 80);
            Expect.IsNotNull(_80);
        }

        [UnitTest]
        public static void FromUriShouldReturnSameUrlIfSameUri()
        {
            SQLiteRegistrar.Register("Crawlers");
            Db.TryEnsureSchema("Crawlers");

            string uri = "http://monkey.cxm/some/path/here?querystring=value";
            Cr.Url url = Cr.Url.FromUri(uri);
            Cr.Url url2 = Cr.Url.FromUri(uri);

            Expect.IsTrue(url.Equals(url2));
        }

        [UnitTest]
        public static void XrefAddAndDeleteTest()
        {
            SQLiteRegistrar.Register("Crawlers");
            Db.TryEnsureSchema("Crawlers");
            string testUri = "http://monkey.cxm/bananas?are=yummy";


            Cr.Url url = Cr.Url.FromUri(testUri);
            int tagCount = url.Tags.Count;
            OutFormat("UrlId={0}: Initial Tag count {1}: ", ConsoleColor.Cyan, url.Id, tagCount);
            foreach (Cr.Tag tag in url.Tags)
            {
                OutFormat("\t{0}", ConsoleColor.Cyan, tag.Value);
            }

            Cr.Tag newTag = new Cr.Tag();//url.Tags.AddNew();
            newTag.Value = "value_".RandomString(6);

            url.Tags.Add(newTag);
            url.Save();

            OutFormat("Tag cound after AddNew {0}", ConsoleColor.Yellow, url.Tags.Count);
            Expect.AreEqual(tagCount + 1, url.Tags.Count);
            Cr.Url check = Cr.Url.FromUri(url.ToString());
            Expect.AreEqual(url.ToString(), check.ToString());
            Expect.AreEqual(url.Id, check.Id);
            Expect.AreEqual(url.Tags.Count, check.Tags.Count);

            long id = url.Id.Value;
            url.Delete();
            Cr.UrlTagCollection xref = Cr.UrlTag.Where(c => c.Id == id);
            Expect.AreEqual(0, xref.Count);
        }

        [UnitTest]
        public static void XrefAddNewAndDeleteTest()
        {
            SQLiteRegistrar.Register("Crawlers");
            Db.TryEnsureSchema("Crawlers");
            string testUri = "http://monkey.cxm/bananas?are=yummy&random=".RandomLetters(4);


            Cr.Url url = Cr.Url.FromUri(testUri);
            int tagCount = url.Tags.Count;
            OutFormat("UrlId={0}: Initial Tag count {1}: ", ConsoleColor.Cyan, url.Id, tagCount);
            foreach (Cr.Tag tag in url.Tags)
            {
                OutFormat("\t{0}", ConsoleColor.Cyan, tag.Value);
            }

            Cr.Tag newTag = url.Tags.AddNew();
            newTag.Value = "value_".RandomString(6);

            url.Save();

            OutFormat("Tag count after AddNew {0}", ConsoleColor.Yellow, url.Tags.Count);
            Expect.AreEqual(tagCount + 1, url.Tags.Count);
            Cr.Url check = Cr.Url.FromUri(url.ToString());
            Expect.AreEqual(url.ToString(), check.ToString());
            Expect.AreEqual(url.Id, check.Id);
            Expect.AreEqual(url.Tags.Count, check.Tags.Count);

            long id = url.Id.Value;
            url.Delete();
            Cr.UrlTagCollection xref = Cr.UrlTag.Where(c => c.Id == id);
            Expect.AreEqual(0, xref.Count);
        }

        [UnitTest]
        public static void XrefAddToBothSidesTest()
        {
            SQLiteRegistrar.Register("Crawlers");
            Db.TryEnsureSchema("Crawlers");
            string testUri = "http://monkey.cxm/some/path?querystring=".RandomLetters(6);

            Cr.Url url = Cr.Url.FromUri(testUri);
            Cr.Tag tag = new Cr.Tag();
            tag.Value = "TheTag";

            url.Tags.Add(tag);
            Expect.AreEqual(1, url.Tags.Count);

            url.Save();

            Cr.Url check = Cr.Url.FromUri(testUri);
            Expect.AreEqual(1, check.Tags.Count);

            Expect.IsTrue(check.Tags.Contains(tag));

            check.Tags.Remove(tag);

            Expect.AreEqual(0, check.Tags.Count);

            Cr.Url checkAgain = Cr.Url.FromUri(testUri);
            Expect.AreEqual(0, checkAgain.Tags.Count);

            tag.Urls.Add(check);
            tag.Save();

            check.Tags.Reload();
            Expect.AreEqual(1, check.Tags.Count);
            Expect.IsTrue(check.Tags.Contains(tag));
        }

        [UnitTest]
        public static void BaseFilterTests()
        {
            QueryFilter filter = new QueryFilter("Bananas");
            QueryFilter filter2 = new QueryFilter("thecracken");
            filter = filter == "yummy" || filter2 == "released";
            Out(filter.Parse());
        }

        [UnitTest]
        public static void XrefFilterTest()
        {
            
            QueryFilter filter = new QueryFilter("ListColumnName") == 1 && new QueryFilter("ParentColumnName") == 2;

            Out(filter.Parse());
        }
    }
}
