using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public interface IServiceUrlProvider
    {
        string GetServiceUrl<T>();
        string GetServiceUrl(Type type);
    }
}
