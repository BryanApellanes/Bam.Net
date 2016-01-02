/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server.Renderers
{
    public interface IRenderer
    {
        string[] Extensions { get; set; }
        string ContentType { get; set; }
        Stream OutputStream { get; set; }
        void Render(object toRender);

        void Render(object toRender, Stream output);

        void Respond(ExecutionRequest request);
    }
}
