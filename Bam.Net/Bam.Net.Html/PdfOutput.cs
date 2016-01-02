/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Html 
{
    public class PdfOutput : ProcessOutput
    {
        public PdfOutput(FileInfo fileInfo) 
        {
            this.FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; set; }
    }
}
