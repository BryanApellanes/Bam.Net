/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
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
    public class RestResponder : HttpHeaderResponder
    {
        public RestResponder(BamConf conf, IRepository repository, ILogger logger = null)
            : base(conf, logger)
        {
            Repository = repository;
            RendererFactory = new RendererFactory(logger);
        }

        public IRepository Repository { get; set; }

        // ** Create / POST (data in request body)**
        // /{Type}.{ext}
        /// <summary>
        /// Create an entry for the specified body
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool Post(IHttpContext context)
        {
            return HandleRequestWithBody(context, Repository.Create);
        }

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
                    IRenderer renderer = RendererFactory.CreateRenderer(context.Request, restRequest.Extension);
                    if (restRequest.Query.Count > 0)
                    {
                        renderer.Render(new RestResponse { Success = true, Data = Repository.Query(type, restRequest.Query.ToDynamicData(4.RandomLetters())) }, response.OutputStream);
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

        // ** Update / PUT (data in request body)**
        // /{Type}/{Id}.{ext}
        protected override bool Put(IHttpContext context)
        {
            return HandleRequestWithBody(context, Repository.Update);
        }
        // ** Delete / DELETE
        // /{Type}/{Id}.{ext}
        protected override bool Delete(IHttpContext context)
        {
            bool result = false;
            RestRequest restRequest = new RestRequest(context);
            if (restRequest.IsValid)
            {
                Type type = restRequest.GetStorableType(Repository.StorableTypes);
                if (type != null)
                {
                    IRenderer renderer = RendererFactory.CreateRenderer(context.Request, restRequest.Extension);
                    object instance = Repository.Retrieve(type, restRequest.Id);
                    if (instance != null)
                    {
                        Repository.Delete(instance);
                        renderer.Render(new RestResponse { Success = true }, context.Response.OutputStream);
                        result = true;
                    }
                }
            }

            return result;
        }

        protected bool HandleRequestWithBody(IHttpContext context, Func<object, object> bodyHandler)
        {
            bool result = false;
            RestRequest restRequest = new RestRequest(context);
            if (restRequest.IsValid)
            {
                IRequest request = context.Request;
                IResponse response = context.Response;

                string typeName = restRequest.TypeName;
                string fileExtension = restRequest.Extension;

                Type type = restRequest.GetStorableType(Repository.StorableTypes);

                if (type != null)
                {
                    if (Deserializers.ContainsKey(fileExtension))
                    {
                        string postBody = ReadInputBody(request);
                        object value = Deserializers[fileExtension](postBody, type);
                        value = bodyHandler(value);
                        IRenderer renderer = RendererFactory.CreateRenderer(request, fileExtension);
                        renderer.Render(new RestResponse { Success = true, Data = value }, response.OutputStream);
                        result = true;
                    }
                }
            }
            return result;
        }

        Dictionary<string, Func<string, Type, object>> _deserializers;
        object _deserializerLock = new object();
        protected Dictionary<string, Func<string, Type, object>> Deserializers
        {
            get
            {
                return _deserializerLock.DoubleCheckLock(ref _deserializers, () =>
                {
                    _deserializers = new Dictionary<string, Func<string, Type, object>>();
                    _deserializers.Add(".json", (s, t) =>
                    {
                        return s.FromJson(t);
                    });
                    _deserializers.Add(".xml", (s, t) =>
                    {
                        return s.FromXml(t);
                    });
                    Func<string, Type, object> yaml = (s, t) =>
                    {
                        return s.FromYaml(t);
                    };
                    _deserializers.Add(".yml", yaml);
                    _deserializers.Add(".yaml", yaml);
                    return _deserializers;
                });
            }
        }

        protected string ReadInputBody(IHttpContext context)
        {
            return ReadInputBody(context.Request);
        }

        protected string ReadInputBody(IRequest request)
        {
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(request.InputStream))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
