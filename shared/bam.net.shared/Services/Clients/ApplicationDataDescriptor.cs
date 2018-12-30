using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Clients
{
    public class ApplicationDataDescriptor
    {
        public int ApplicationDataVersion { get; set; }
        public Assembly ApplicationDataTypesAssembly { get; set; }
        public Database ApplicationDatabase { get; set; }
        public DaoRepository ApplicationRepository { get; set; }
    }
}
