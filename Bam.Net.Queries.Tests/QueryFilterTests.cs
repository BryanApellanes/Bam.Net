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
using Bam;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.CommandLine;

namespace Bam.Net.Data.Tests
{
    public class FileExtQueryTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void AndShouldParseNoParens()
        {
            TableColumns one = new TableColumns();
            QueryFilter<TableColumns> e = (one.ColumnOne == "monkey").And(one.ColumnTwo != "banana");
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("[ColumnOne] = @ColumnOne1 AND [ColumnTwo] <> @ColumnTwo2", e.Parse());
        }

        [UnitTest]
        public static void ParseShouldNumberParameters()
        {
            TableColumns one = new TableColumns();
            QueryFilter<TableColumns> e = one.ColumnOne == "monkey" && one.ColumnTwo != "banana";
            Out(e.Parse(), ConsoleColor.Cyan);
        }

        [UnitTest]
        public static void OrShouldParseNoParens()
        {
            TableColumns one = new TableColumns();
            QueryFilter<TableColumns> e = (one.ColumnOne == "monkey").Or(one.ColumnTwo != "banana");
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("[ColumnOne] = @ColumnOne1 OR [ColumnTwo] <> @ColumnTwo2", e.Parse());
        }

        [UnitTest]
        public static void AndOperatorShouldParseParens()
        {
            TableColumns one = new TableColumns();
            QueryFilter<TableColumns> e = one.ColumnOne == "dookey" && one.ColumnTwo == "poo";
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("([ColumnOne] = @ColumnOne1) AND ([ColumnTwo] = @ColumnTwo2)", e.Parse());
        }

        [UnitTest]
        public static void ExtensionClassShouldWorkTheSameAsQueryFilter()
        {
            TableColumns test = new TableColumns();

            QueryFilter<TableColumns> e = test.TestOne == "monkey";
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("[TestOne] = @TestOne1", e.Parse());
        }

        [UnitTest]
        public static void StartsWithShouldParseValidWhereExpression()
        {
            TableColumns test = new TableColumns();
            QueryFilter<TableColumns> e = test.TestOne.StartsWith("gorilla boat");
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("TestOne LIKE @TestOne1 + '%'", e.Parse());
        }

        [UnitTest]
        public static void EndsWithShouldParseValidWhereExpression()
        {
            TableColumns test = new TableColumns();
            QueryFilter<TableColumns> e = test.TestOne.EndsWith("gorilla boat");
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("TestOne LIKE '%' + @TestOne1", e.Parse());
        }

        [UnitTest]
        public static void InShouldHaveNumberedParameters()
        {
            TableColumns test = new TableColumns();
            List<object> count = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                count.Add(i);
            }
            QueryFilter<TableColumns> e = test.TestOne.In(count.ToArray());
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("TestOne IN (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10)", e.Parse());
        }

        [UnitTest]
        public static void SubsequentParametersToInCallShoulBeNumberedInSequence()
        {
            TableColumns test = new TableColumns();
            List<object> count = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                count.Add(i);
            }
            QueryFilter<TableColumns> e = test.TestOne.In(count.ToArray()).And(test.TestOne != "monkey");
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("TestOne IN (@P1, @P2, @P3, @P4, @P5) AND [TestOne] <> @TestOne6", e.Parse());
        }
        
        [UnitTest]
        public static void SubsequentParametersToInCallShoulBeNumberedInSequence2()
        {
            TableColumns test = new TableColumns();
            List<object> count = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                count.Add(i);
            }
            QueryFilter<TableColumns> e = test.TestOne.In(count.ToArray()) && test.TestOne != "monkey";
            Out(e.Parse(), ConsoleColor.Cyan);
            Expect.AreEqual("(TestOne IN (@P1, @P2, @P3, @P4, @P5)) AND ([TestOne] <> @TestOne6)", e.Parse());
        }
    }
}
