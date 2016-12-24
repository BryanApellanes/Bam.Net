using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.CoreServices.Data.Protobuf;
using System.IO;
using Google.Protobuf;
using Bam.Net.CommandLine;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class ProtoBufTests: CommandLineTestInterface
    {
        [ConsoleAction]
        [IntegrationTest]
        public void ProtoBufTest()
        {
            ClientMachine m = new ClientMachine();
            m.Name = "The name of the machine";
            FileInfo file = new FileInfo(".\\test.dat");
            using (var output = File.Create(file.FullName))
            {
                m.WriteTo(output);
            }
            OutLine(file.FullName);
        }
    }
}
