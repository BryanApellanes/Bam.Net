using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public interface IPostServerInitialize: IInitialize
    {
        BamServer Server { get; set; }
    }
}
