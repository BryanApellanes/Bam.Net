using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Drawing;
using Bam.Net.Testing;

namespace Bam.Net.Drawing.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction]
        public void QrCodeTest()
        {
            string file = ".\\test2.bmp";
            "this is a qr code test".ToQrCodeFile(file);
            Out(Environment.CurrentDirectory);
        }
    }
}
