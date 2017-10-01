using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Services.Distributed.Data;
using Bam.Net.Testing;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction]
        public void CanSerializeAndDeserializeDataPropertyList()
        {
            DataPropertyCollection list = new DataPropertyCollection();

            list.Prop("Monkey", true);

        }
    }
}
