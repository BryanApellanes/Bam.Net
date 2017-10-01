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
        public abstract bool Authorize(ExecutionRequest request);
    }
}
