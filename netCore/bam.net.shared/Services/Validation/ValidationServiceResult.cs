using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class ValidationServiceResult
    {
        public string ValidationRuleCuid { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
