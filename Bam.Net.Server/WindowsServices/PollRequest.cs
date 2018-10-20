using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.WindowsServices
{
    public class PollRequest<T>
    {
        public Action<PollResponse<T>> PollAction { get; set; }
    }
}
