using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.AsyncCallback.Data;

namespace Bam.Net.Services
{
    public class AsyncExecutionResponse: AsyncExecutionResponseData
    {
        public bool Success { get; set; }
        public ValidationFailure ValidationFailure { get; set; }
        public object Result { get; set; }

    }
}
