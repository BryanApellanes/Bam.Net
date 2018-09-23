using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;

namespace Bam.Net.ServiceProxy
{
    public interface IHasServiceProvider
    {
        Incubator ServiceProvider { get; set; }
    }
}
