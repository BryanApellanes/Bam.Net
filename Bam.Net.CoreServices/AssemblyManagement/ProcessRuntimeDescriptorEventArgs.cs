using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.AssemblyManagement.Data;

namespace Bam.Net.CoreServices
{
    public class ProcessRuntimeDescriptorEventArgs: EventArgs
    {
        public ProcessRuntimeDescriptor ProcessRuntimeDescriptor { get; set; }

        /// <summary>
        /// On restore represents the path restoration was to.
        /// </summary>
        public string DirectoryPath { get; set; }
        public string Message { get; set; }
    }
}
