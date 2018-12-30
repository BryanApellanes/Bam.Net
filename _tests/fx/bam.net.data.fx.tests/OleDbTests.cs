using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Data.OleDb;
using System.IO;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class OleDbTests : CommandLineTestInterface
    {
        [BeforeUnitTests]
        public void Setup()
        {
            FileInfo writeTo = new FileInfo($".\\{nameof(ShouldWriteAccessDatabase)}.accdb");
            if (File.Exists(writeTo.FullName))
            {
                File.Delete(writeTo.FullName);
            }
        }

        [AfterUnitTests]
        public void Teardown()
        {
            FileInfo writeTo = new FileInfo($".\\{nameof(ShouldWriteAccessDatabase)}.accdb");
            if (File.Exists(writeTo.FullName))
            {
                File.Delete(writeTo.FullName);
            }
        }

        [UnitTest]
        public void ShouldWriteAccessDatabase()
        {
            Setup();
            FileInfo writeTo = new FileInfo($".\\{nameof(ShouldWriteAccessDatabase)}.accdb");
            Expect.IsFalse(File.Exists(writeTo.FullName));
            typeof(OleDbDatabase).Assembly.WriteResource("bam.net.fx.Data.OleDb.empty.accdb", writeTo);
            Expect.IsTrue(File.Exists(writeTo.FullName));
            OutLine(writeTo.FullName, ConsoleColor.Cyan);
            Teardown();
        }
    }
}
