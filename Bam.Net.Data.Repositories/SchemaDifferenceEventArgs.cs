using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Analytics;

namespace Bam.Net.Data.Repositories
{
    public class SchemaDifferenceEventArgs: EventArgs
    {
        public TypeSchema TypeSchema { get; set; }
        public GeneratedDaoAssemblyInfo GeneratedDaoAssemblyInfo { get; set; }
        public DiffReport DiffReport { get; set; }
    }
}
