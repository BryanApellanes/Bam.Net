/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Testing;
using System.Linq.Expressions;
using Bam.Net.Data.SQLite;
using Bam.Net.DaoRef;

namespace Bam.Net.Data.Tests
{
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
			DefaultMethod = typeof(TestProgram).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(TestProgram).Assembly);
			}
			else
			{
				Interactive();
			}
		}
        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          */

		[UnitTest]
        public static void DropLeadingNonLettersTest()
        {
            Expect.AreEqual("Monkey", "_88%%$83345Monkey".DropLeadingNonLetters());
        }

        [UnitTest]
        public static void DaoEvaluatorTest()
        {
            SQLiteBitMonitor.MonitorBitness();
            Database database = new SQLiteDatabase(".\\", MethodBase.GetCurrentMethod().Name);
            ConsoleLogger logger = PrepareDatabaseAndGetLogger(database);

            TestTable testInstance = new TestTable();
            testInstance.Name = "banana";
            testInstance.Save(database);

            DaoExpressionFilter v = new DaoExpressionFilter(logger);
            QueryFilter testFilter = v.Where<TestTable>((t) => t.Name == testInstance.Name);

            TestTable check = TestTable.Where(Filter.Column("Name") == "banana", database).FirstOrDefault();
            Expect.IsNotNull(check);

            TestTable evalCheck = TestTable.Where(testFilter, database).FirstOrDefault();
            Expect.IsNotNull(evalCheck);

            Expect.AreEqual(check.Id, evalCheck.Id);
            Out(v.TraceLog, ConsoleColor.Cyan);
        }
        [UnitTest]
        public static void FilterOrExpression()
        {
            Database database = new SQLiteDatabase(".\\", MethodBase.GetCurrentMethod().Name);
            ConsoleLogger logger = PrepareDatabaseAndGetLogger(database);

            TestTable one = new TestTable();
            one.Name = "banana";
            one.Save(database);

            TestTable two = new TestTable();
            two.Name = "blah";
            two.Save(database);

            DaoExpressionFilter v = new DaoExpressionFilter(logger);
            QueryFilter testFilter = v.Where<TestTable>((t) => t.Name == one.Name).Or<TestTable>((t) => t.Name == two.Name);

            TestTableCollection check = TestTable.Where(c => c.Name == "banana" || c.Name == "blah", database);
            Expect.IsNotNull(check);

            TestTableCollection evalCheck = TestTable.Where(testFilter, database);
            Expect.IsNotNull(evalCheck);

            Expect.AreEqual(2, evalCheck.Count);
            Out(v.TraceLog, ConsoleColor.Cyan);
        }
        [UnitTest]
        public static void ConditionalExpressionThrowsException()
        {
            Database database = new SQLiteDatabase(".\\", MethodBase.GetCurrentMethod().Name);
            ConsoleLogger logger = PrepareDatabaseAndGetLogger(database);

            DaoExpressionFilter f = new DaoExpressionFilter(logger);
            bool thrown = false;
            try
            {
                Out(f.Where<TestTable>((t) => t.Name == "blah" || t.Name == "monkey").Parse(), ConsoleColor.DarkCyan);
            }
            catch (ExpressionTypeNotSupportedException etnse)
            {
                thrown = true;
                Out(etnse.Message, ConsoleColor.Cyan);
            }
            Expect.IsTrue(thrown);
        }

        [UnitTest]
        public static void ExecuteReaderTest()
        {
            Database database = new SQLiteDatabase(".\\", MethodBase.GetCurrentMethod().Name);
            ConsoleLogger logger = PrepareDatabaseAndGetLogger(database);
            TestTable one = new TestTable();
            one.Name = "banana";
            one.Save(database);
            TestTable two = new TestTable();
            two.Name = one.Name;
            two.Save(database);
            SqlStringBuilder sql = database.GetSqlStringBuilder();
            sql.Select("TestTable").Where("Name", "banana");
            List<TestTable> retrieved = database.ExecuteReader<TestTable>(sql).ToList();
            Expect.AreEqual(2, retrieved.Count);
            retrieved.Each(t =>
            {
                OutLineFormat("{0}, {1}", t.Id, t.Name);
            });
        }

        [UnitTest]
        public void SqlStringBuildersShouldBeEqual()
        {
            SQLiteDatabase db = new SQLiteDatabase($".\\{nameof(SqlStringBuildersShouldBeEqual)}", nameof(SqlStringBuildersShouldBeEqual));
            string name = 8.RandomLetters();
            TimeSpan elapsed = Timed.Execution(() =>
            {
                SqlStringBuilder one = TestTableQuery.Where(c => c.Name == name).ToSqlStringBuilder(db);
                SqlStringBuilder two = TestTableQuery.Where(c => c.Name == name).ToSqlStringBuilder(db);
                SqlStringBuilder three = TestTableQuery.Where(c => c.Name == "something else").ToSqlStringBuilder(db);

                Expect.IsTrue(one.EqualTo(two, db));
                Expect.IsFalse(one.EqualTo(three, db));
            });

            OutLineFormat("Operation took: {0}", elapsed.ToString());
        }

        private static ConsoleLogger PrepareDatabaseAndGetLogger(Database database)
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            database.TryEnsureSchema(typeof(TestTable), logger);
            ClearTestTable(database);
            return logger;
        }

        private static void ClearTestTable(Database db)
        {
            TestTable.LoadAll(db).Delete(db);
        }

        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
