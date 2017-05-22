using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Services
{
    public class ApiResponder : HttpMethodResponder
    {
        public ApiResponder() : base(new BamConf())
        {
        }
        
        protected override bool Get(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Post(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Put(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Delete(IHttpContext context)
        {
            throw new NotImplementedException();
        }

    }
}
