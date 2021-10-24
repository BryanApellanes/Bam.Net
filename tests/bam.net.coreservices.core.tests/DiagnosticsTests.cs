using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.MySql;
using Bam.Net.Data.Oracle;
using Bam.Net.Yaml;
using Bam.Net.Testing.Unit;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class DiagnosticsTests: CommandLineTool
    {
        [UnitTest]
        public void DiagnosticInfoShouldHaveSQLiteDatabase()
        {
            string name = 8.RandomLetters();
            SQLiteDatabase db = new SQLiteDatabase("./{0}"._Format(MethodBase.GetCurrentMethod().Name), name);
            DiagnosticService svc = new DiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(SQLiteDatabase).FullName, dbInfo.DatabaseType);
            Message.PrintLine("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }

        [UnitTest]
        public void DiagnosticInfoShouldHaveOracleDatabase()
        {
            string name = 8.RandomLetters();
            OracleDatabase msDatabase = new OracleDatabase("chumsql2", name);
            DiagnosticService svc = new DiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(OracleDatabase).FullName, dbInfo.DatabaseType);
            Message.PrintLine("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }
        [UnitTest]
        public void DiagnosticInfoShouldHaveMySqlDatabase()
        {
            string name = 8.RandomLetters();
            MySqlDatabase mySqlDatabase = new MySqlDatabase("chumsql2", "DaoRef", name);
            Expect.IsNotNullOrEmpty(mySqlDatabase.ConnectionString, "Connection string was not set");
            DiagnosticService svc = new DiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(MySqlDatabase).FullName, dbInfo.DatabaseType);
            Message.PrintLine("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }

        [UnitTest]
        public void LoadAssembliesAndReferenced()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            bool got = false;
            bool gotReference = false;
            foreach(Assembly ass in assemblies)
            {
                got = true;
                try
                {
                    Message.PrintLine("{0}: {1}", ass.FullName, ass.GetFileInfo().Sha1(), ConsoleColor.Blue);
                    Message.PrintLine("{0}: {1}", ConsoleColor.Blue, ass.GetFilePath(), ass.FullName);
                    AssemblyName[] referenced = ass.GetReferencedAssemblies();
                    OutLine("referenced", ConsoleColor.DarkBlue);
                    foreach (AssemblyName name in referenced)
                    {
                        gotReference = true;
                        Assembly reference = Assembly.Load(name);
                        Message.PrintLine("\t{0}: {1}", ass.FullName, ass.GetFileInfo().Sha1(), ConsoleColor.Cyan);
                        Message.PrintLine("\t{0}: {1}", ConsoleColor.Cyan, ass.GetFilePath(), ass.FullName);
                    }
                    Message.PrintLine();
                }
                catch (Exception ex)
                {
                    Message.PrintLine("Error outputting {0}:\r\n{1}", ConsoleColor.Yellow, ass.FullName, ex.Message);
                }
            }

            Expect.IsTrue(got, "didn't get any assemblies");
            Expect.IsTrue(gotReference, "didn't get any references");
        }

        [UnitTest]
        public void UriTest()
        {
            Uri uri = new Uri("https://sub.domain.com/path/more");
            OutLine(uri.Host);
            OutLine(uri.Scheme);
            Expect.AreEqual("sub.domain.com", uri.Host);
        }
    }
}
