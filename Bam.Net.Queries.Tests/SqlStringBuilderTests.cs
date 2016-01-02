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

namespace Bam.Net.Data.Tests
{
    public class SqlStringBuilderTests: CommandLineTestInterface
    {
        [UnitTest]
        public static void EnablingDropShouldFireDropEnabledEvent()
        {
            SqlClientSqlStringBuilder s = new SqlClientSqlStringBuilder();
            bool? fired = false;
            s.DropEnabled += (sb) =>
            {
                Expect.AreSame(s, sb);
                fired = true;
            };

            s.EnableDrop = true;
            Expect.IsNotNull(fired);
            Expect.IsTrue(fired.HasValue);
            Expect.IsTrue(fired.Value, "Event didn't fire");
        }

        [UnitTest]
        public static void SetFormatShouldOutputSqlSetter()
        {
            SetFormat set = new SetFormat();
            set.AddAssignment("monkey", "blah");
            set.AddAssignment("gorilla", "dksl");

            Expect.AreEqual(2, set.Parameters.Length);
            Out(set.Parse());
        }

        [UnitTest]
        public static void SetFormatShouldTakeStartNumber()
        {
            SetFormat set = new SetFormat();
            set.AddAssignment("monkey", "blah");
            set.AddAssignment("gorilla", "dksl");
            set.AddAssignment("blah", "dflkae");

            Expect.AreEqual(3, set.Parameters.Length);
            set.StartNumber = 5;
            Expect.AreEqual(8, set.NextNumber);
            Out(set.Parse());
        }

        [UnitTest]
        public static void UpdateShouldOutputUpdate()
        {
            SqlStringBuilder sql = new SqlStringBuilder();

            sql.Update("monkey", new AssignValue("columnOne", "lasdfj"), new AssignValue("asdlkds", "sdlkf"));

            Out(sql.ToString());

            for(int i = 0; i < sql.Parameters.Length; i++)
            {
                Expect.AreEqual(i + 1, sql.Parameters[i].Number);
            }

            Expect.AreEqual(3, sql.NextNumber);
        }

        [UnitTest]
        public static void InsertShouldOutputInsert()
        {
            SqlStringBuilder sql = new SqlStringBuilder();

            sql.Insert("monkey", new AssignValue("columnOne", "lasdfj"), new AssignValue("asdlkds", "sdlkf"));

            Out(sql.ToString());

            for (int i = 0; i < sql.Parameters.Length; i++)
            {
                Expect.AreEqual(i + 1, sql.Parameters[i].Number);
            }

            Expect.AreEqual(3, sql.NextNumber);
        }

        [UnitTest]
        public static void InsertAndUpdateShouldTrackParameters()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            sql
                .Insert("monkey", new AssignValue("blah", "asldk"), new AssignValue("dasfk", "dflsk"))
                .Go()
                .Update("monkey", new AssignValue("columnOne", "lasdfj"), new AssignValue("asdlkds", "sdlkf"));

            Expect.AreEqual(5, sql.NextNumber);
            Out(sql.ToString());
        }


        [UnitTest]
        public static void UpdateWhereShouldTrackParameters()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            TableColumns columns = new TableColumns();
            sql
                .Update("monkey", new AssignValue("blah", "asldk"), new AssignValue("dasfk", "dflsk"))
                .Where(columns.TestOne == "sdklfj" || columns.TestOne == "gorilla")
                .Go()
                .Insert("monkey", new AssignValue("columnOne", "lasdfj"), new AssignValue("asdlkds", "sdlkf"));

            Expect.AreEqual(7, sql.NextNumber);
            Out(sql.ToString());

            Expect.AreEqual("asldk", sql.Parameters[0].Value);
            Expect.AreEqual("dflsk", sql.Parameters[1].Value);
            Expect.AreEqual("sdklfj", sql.Parameters[2].Value);
            Expect.AreEqual("gorilla", sql.Parameters[3].Value);
            Expect.AreEqual("lasdfj", sql.Parameters[4].Value);
            Expect.AreEqual("sdlkf", sql.Parameters[5].Value);
        }

        [UnitTest]
        public static void SelectShouldFallInPlaceWithTheRest()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            TableColumns columns = new TableColumns();
            sql.Update("monkey", new AssignValue("blah", "sasdklf"))
                .Go()
                .Select("gorilla");

            Out(sql.ToString());

            Expect.AreEqual(1, sql.Parameters.Length);
        }

        [UnitTest]
        public static void DeleteWhereShouldFallIntoPlace()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            TableColumns columns = new TableColumns();
            sql.Select("Monkeys")
                .Where(columns.TestOne != "monkey" || columns.TestOne == "blah")
                .Go()
                .Update("Monkey", new AssignValue("columnOne", "someValue"))
                .Where(columns.TestOne < 9 && columns.TestOne > 4)
                .Go()
                .Delete("Monkey")
                .Where(columns.TestOne == 1)
                .Go();

            Out(sql.ToString());
            Expect.AreEqual(6, sql.Parameters.Length);
        }

        [UnitTest]
        public static void ParameterBuilderShouldBuildParametersFromSqlString()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            TableColumns columns = new TableColumns();
            sql.Select("Monkey").Where(columns.TestOne != "monkey");
            Expect.AreEqual(1, sql.Parameters.Length);

            SqlClientParameterBuilder builder = new SqlClientParameterBuilder();
            DbParameter[] parameters = builder.GetParameters(sql);
            Expect.AreEqual(1, parameters.Length);
            Out(sql.ToString());
            foreach (DbParameter param in parameters)
            {
                OutFormat("{0}={1}\r\n", param.ParameterName, param.Value.ToString());
            }
        }
    }
}

