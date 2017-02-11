using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Pdf;
using System.IO;

namespace Bam.Net.Pdf.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [ConsoleAction]
        public void TestPdfToHtml()
        {
            DirectoryInfo srcDir = new DirectoryInfo(Prompt("Enter the path to the source directory"));
            DirectoryInfo destDir = new DirectoryInfo(Prompt("Enter the path to the destination directory"));

            srcDir.PdfToHtml(destDir);
        }
    }
}
