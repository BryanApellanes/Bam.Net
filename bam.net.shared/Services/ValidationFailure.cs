using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Services
{
    public class ValidationFailure
    {
        public string Message { get; set; }
        public ValidationFailures[] Failures { get; set; }
    }
}
