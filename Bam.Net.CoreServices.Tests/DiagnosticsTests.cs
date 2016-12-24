using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.MySql;
using Bam.Net.Data.Oracle;
using Bam.Net.Yaml;
using Bam.Net.CoreServices.Services;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class DiagnosticsTests: CommandLineTestInterface
    {
        [UnitTest]
        public void DiagnosticInfoShouldHaveSQLiteDatabase()
        {
            string name = 8.RandomLetters();
            SQLiteDatabase db = new SQLiteDatabase(".\\{0}"._Format(MethodBase.GetCurrentMethod().Name), name);
            CoreDiagnosticService svc = new CoreDiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(SQLiteDatabase).FullName, dbInfo.DatabaseType);
            OutLineFormat("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }

        [UnitTest]
        public void DiagnosticInfoShouldHaveMsSqlDatabase()
        {
            string name = 8.RandomLetters();
            MsSqlDatabase msDatabase = new MsSqlDatabase("chumsql2", "DaoRef", name);
            CoreDiagnosticService svc = new CoreDiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(MsSqlDatabase).FullName, dbInfo.DatabaseType);
            OutLineFormat("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }

        [UnitTest]
        public void DiagnosticInfoShouldHaveOracleDatabase()
        {
            string name = 8.RandomLetters();
            OracleDatabase msDatabase = new OracleDatabase("chumsql2", name);
            CoreDiagnosticService svc = new CoreDiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(OracleDatabase).FullName, dbInfo.DatabaseType);
            OutLineFormat("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }
        [UnitTest]
        public void DiagnosticInfoShouldHaveMySqlDatabase()
        {
            string name = 8.RandomLetters();
            MySqlDatabase msDatabase = new MySqlDatabase("chumsql2", "DaoRef", name);
            CoreDiagnosticService svc = new CoreDiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(MySqlDatabase).FullName, dbInfo.DatabaseType);
            OutLineFormat("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }
    }
}
