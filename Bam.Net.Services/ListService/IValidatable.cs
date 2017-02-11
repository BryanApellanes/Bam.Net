using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Distributed.Data;
using Bam.Net.Services.Distributed.Data;

namespace Bam.Net.Services.ListService
{
    public interface IValidatable
    {
        DataProperty[] GetDataProperties();
    }
}
