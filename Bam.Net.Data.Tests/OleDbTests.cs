using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Data.OleDb;
using System.IO;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class OleDbTests: CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldWriteAccessDatabase()
        {
            FileInfo writeTo = new FileInfo($".\\{nameof(ShouldWriteAccessDatabase)}.accdb");
            Expect.IsFalse(File.Exists(writeTo.FullName));
            typeof(OleDbDatabase).Assembly.WriteResource(typeof(OleDbDatabase), "empty.accdb", writeTo);
            Expect.IsTrue(File.Exists(writeTo.FullName));
            OutLine(writeTo.FullName, ConsoleColor.Cyan);
        }
    }
}
