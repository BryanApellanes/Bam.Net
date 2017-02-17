using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Distributed.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.Distributed.Data;

namespace Bam.Net.Services.ListService.Data
{
    public class ItemDefinition: RepoData, IValidatable
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DataProperty[] GetDataProperties()
        {
            throw new NotImplementedException();
        }
    }
}
