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
    /// <summary>
    /// The delegate used to define responder events
    /// </summary>
    /// <param name="responder"></param>
    /// <param name="context"></param>
    public delegate void ResponderEventHandler(IResponder responder, IHttpContext context);
}
