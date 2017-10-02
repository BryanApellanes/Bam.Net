using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public abstract class Authorizer
    {
        ILogger _logger;
        public ILogger Logger
        {
            get
            {
                return _logger ?? Log.Default;
            }
            set
            {
                _logger = value;
            }
        }
        public abstract bool Authorize(ExecutionRequest request);
    }
}
