using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Automation
{
    public class CommandInfo: AuditRepoData
    {
        public CommandInfo() { }
        
        public string Command { get; set; }
        public string StandardOut { get; set; }
        public string StandardError { get; set; }
    }
}
