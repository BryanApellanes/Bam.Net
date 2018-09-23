using Bam.Net.Services.DataReplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class OpertionEventArgs: EventArgs
    {
        public WriteEvent WriteEvent { get; set; }
    }
}
