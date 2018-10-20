using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.ServiceProxy
{
    public partial class RequestWrapper
    {
        public static implicit operator HttpRequestBase(RequestWrapper wrapper)
        {
            return (HttpRequestBase)wrapper.Wrapped;
        }

        public RequestWrapper(HttpRequestBase request)
        {
            DefaultConfiguration.CopyProperties(request, this);
            this.Wrapped = request;
        }
    }
}
