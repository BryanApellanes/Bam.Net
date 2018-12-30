using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bam.Net.Presentation.Markdown;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class BamParserTests: CommandLineTestInterface
    {
        [UnitTest]
        public void CanReadModelProps()
        {
            XDocument htmlDoc = XDocument.Load("./bam-test-template.html");
            int i = 1;
            foreach(XNode xnode in htmlDoc.Nodes())
            {
                Console.WriteLine("BAM {0}:: {1}", i++, xnode.ToString());
            }
        }
    }
}
