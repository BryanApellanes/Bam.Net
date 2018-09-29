using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.ServiceProxy
{
    public partial class ResponseWrapper
    {
        public ResponseWrapper(HttpResponseBase response)
        {
            DefaultConfiguration.CopyProperties(response, this);
            this.Wrapped = response;
        }

    }
}
