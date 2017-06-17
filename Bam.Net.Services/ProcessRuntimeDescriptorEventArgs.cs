using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.AssemblyManagement.Data;

namespace Bam.Net.Services
{
    public class ProcessRuntimeDescriptorEventArgs: EventArgs
    {
        public ProcessRuntimeDescriptor ProcessRuntimeDescriptor { get; set; }
        public string DirectoryPath { get; set; }
    }
}
