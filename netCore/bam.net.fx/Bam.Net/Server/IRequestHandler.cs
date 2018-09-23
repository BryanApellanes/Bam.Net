/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public interface IRequestHandler
    {
        void HandleRequest(IHttpContext context);
    }
}
