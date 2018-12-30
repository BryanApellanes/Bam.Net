using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    public class FileErrorContent: ErrorContent
    {
        public FileErrorContent(FileInfo file, int code): base(code)
        {
            Content = file.ReadAllText().ToBytes();
        }
    }
}
