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
    public interface IResponder: IInitialize
    {
        string Name { get; }
        event ResponderEventHandler Responded;
        event ResponderEventHandler NotResponded;
        bool Respond(IHttpContext context);
        bool TryRespond(IHttpContext context);
    }
}
