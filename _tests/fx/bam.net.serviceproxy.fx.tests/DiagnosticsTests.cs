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
using Bam.Net.Testing.Unit;
namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class DiagnosticsTests: CommandLineTestInterface
    {
        [UnitTest]
        public void DiagnosticInfoShouldHaveMsSqlDatabase()
        {
            string name = 8.RandomLetters();
            MsSqlDatabase msDatabase = new MsSqlDatabase("chumsql2", "DaoRef", name);
            DiagnosticService svc = new DiagnosticService(null);
            DiagnosticInfo info = svc.GetDiagnosticInfo();
            DatabaseInfo dbInfo = info.Databases.FirstOrDefault(dbi => dbi.ConnectionName.Equals(name));
            Expect.IsNotNull(dbInfo);
            Expect.AreEqual(name, dbInfo.ConnectionName);
            Expect.AreEqual(typeof(MsSqlDatabase).FullName, dbInfo.DatabaseType);
            OutLineFormat("{0}", ConsoleColor.DarkBlue, info.ToYaml());
        }
    }
}
