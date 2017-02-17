using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Distributed.Data;
using Bam.Net.Services.Distributed.Data;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    public interface IValidatable
    {
        string Name { get; set; }
        DataProperty[] GetDataProperties();
    }
}
