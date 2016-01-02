/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Web;
using Bam.Net.Html;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System.Reflection;

namespace Bam.Net.Server.Renderers
{
    public abstract class ContentRenderer: Renderer
    {
        public ContentRenderer(ExecutionRequest request, ContentResponder content, string contentType, params string[] extensions)
            :base(contentType, extensions)
        {
            this.ExecutionRequest = request;
            this.ContentResponder = content;
        }


        protected ExecutionRequest ExecutionRequest
        {
            get;
            set;
        }

        protected ContentResponder ContentResponder
        {
            get;
            set;
        }

    }
}
