using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class AsyncExecutionResponseEventArgs: EventArgs
    {
        public AsyncExecutionResponse Response { get; set; }
    }
}
