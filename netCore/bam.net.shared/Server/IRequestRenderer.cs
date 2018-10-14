/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    public interface IRequestRenderer: IRenderer
    {
        ExecutionRequest Request { get; set; }
    }
}
