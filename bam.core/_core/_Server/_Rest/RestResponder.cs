/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bam.Net;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using Bam.Net.Server.Renderers;
using Bam.Net.Yaml;
using System.Collections;
using System.IO;
using System.Collections.Specialized;
using Bam.Net.Presentation;

namespace Bam.Net.Server.Rest
{
    public partial class RestResponder : HttpHeaderResponder
    {
        // ** Retrieve / GET **
        // /{Type}.{ext}?{Query}
        // /{Type}/{Id}.{ext}
        // /{Type}/{Id}/{ChildListProperty}.{ext}        
        protected override bool Get(IHttpContext context)
        {
            RestRequest restRequest = new RestRequest(context);
            bool result = false;

            if (restRequest.IsValid)
            {
                IResponse response = context.Response;
                Type type = restRequest.GetStorableType(Repository.StorableTypes);
                if (type != null)
                {
                    IWebRenderer renderer = RendererFactory.CreateRenderer(context.Request, restRequest.Extension);
                    if (restRequest.Query.Count > 0)
                    {
                        renderer.Render(new RestResponse { Success = true, Data = Repository.Query(type, restRequest.Query) }, response.OutputStream);
                        result = true;
                    }
                    else if (restRequest.Id > 0)
                    {
                        object instance = Repository.Retrieve(type, restRequest.Id);
                        if (instance != null)
                        {
                            RestResponse restResponse = new RestResponse { Success = true, Data = instance };
                            if (!string.IsNullOrEmpty(restRequest.ChildListProperty))
                            {
                                restResponse.Data = instance.Property(restRequest.ChildListProperty);
                            }
                            renderer.Render(restResponse, response.OutputStream);
                            result = true;
                        }
                    }
                }
            }

            return result;
        }
    }
}
