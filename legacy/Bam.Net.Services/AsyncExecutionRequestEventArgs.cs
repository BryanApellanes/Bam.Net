using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class AsyncExecutionRequestEventArgs: EventArgs
    {
        public AsyncExecutionRequest Request { get; set; }
    }
}
