using Bam.Net.Incubation;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public interface IClientProxyGenerator
    {
        string GetProxyCode(Incubator serviceProvider, IHttpContext context);
        void SendProxyCode(Incubator serviceProvider, IHttpContext context);
    }
}
