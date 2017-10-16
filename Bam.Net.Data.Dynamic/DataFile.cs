using Bam.Net.Data.Dynamic.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    public class DataFile
    {
        public DataFile()
        {
            Namespace = DynamicNamespaceDescriptor.DefaultNamespace;
        }

        public string Namespace { get; set; }
        public string TypeName { get; set; }
        public FileInfo FileInfo { get; set; }
    }
}
