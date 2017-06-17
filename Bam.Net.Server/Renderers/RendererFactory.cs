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
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server.Renderers
{
    public class RendererFactory: Renderer
    {
        Dictionary<string, Func<IRenderer>> _renderers;
        public RendererFactory(ILogger logger)
            : base("text/plain", "")
        {
            this._renderers = new Dictionary<string, Func<IRenderer>>();
			this.Logger = logger ?? Log.Default;
            this.AddBaseRenderers();
        }

        protected ILogger Logger { get; set; }

        public void Respond(ExecutionRequest request, ContentResponder contentResponder)
        {
            IRenderer renderer = CreateRenderer(request, contentResponder);

            renderer.Respond(request);
        }

        protected internal IRenderer CreateRenderer(ExecutionRequest request, ContentResponder contentResponder)
        {
            IRequest webRequest = request.Request;
            IResponse webResponse = request.Response;

            OnCreatingRenderer(webRequest);
            
            string path = request.Request.Url.AbsolutePath;
            string ext = Path.GetExtension(path);

            IRenderer renderer = CreateRenderer(webRequest, ext);

            if (request.HasCallback || ScriptRenderer.Extensions.Contains(ext.ToLowerInvariant()))
            {
                renderer = new ScriptRenderer(request, contentResponder);
            }

            OnCreatedRenderer(webRequest, renderer);
            return renderer;
        }

        protected internal IRenderer CreateRenderer(IRequest webRequest, string ext = null)
        {
            IRenderer renderer = null;
            if (_renderers.ContainsKey(ext))
            {
                renderer = _renderers[ext]();
            }

            if (string.IsNullOrEmpty(ext))
            {
                // check for a format querystring param
                string format = webRequest.QueryString["format"];
                if (!string.IsNullOrEmpty(format))
                {
                    if (_renderers.ContainsKey(format))
                    {
                        renderer = _renderers[format]();
                    }
                }
                else
                {
                    renderer = _renderers[".json"]();
                }

                renderer.ContentType = GetContentType(renderer, webRequest);
            }

            return renderer;
        }

        /// <summary>
        /// The event that fires before resolving the renderer for the current request
        /// </summary>
        public event Action<RendererFactory, IRequest> CreatingRenderer;
        protected void OnCreatingRenderer(IRequest request)
        {
            if (CreatingRenderer != null)
            {
                CreatingRenderer(this, request);
            }
        }
        public event Action<RendererFactory, IRequest, IRenderer> CreatedRenderer;
        protected void OnCreatedRenderer(IRequest request, IRenderer renderer)
        {
            if (CreatedRenderer != null)
            {
                CreatedRenderer(this, request, renderer);
            }
        }

        protected internal string GetContentType(IRenderer renderer, IRequest webRequest)
        {
            // TODO: revisit this to handle Accept and/or Content-Type headers 
            // see http://www.xml.com/pub/a/2005/06/08/restful.html
            // determine what the requestor wants the content type to be
            // set to in the response
            string contentType = renderer.ContentType;
            string queryContentType = webRequest.QueryString["contenttype"];

            if (!string.IsNullOrEmpty(queryContentType))
            {
                contentType = queryContentType;
            }

            return contentType;
        }

        public override void Render(object toRender, Stream output)
        {
            // if there is no extension ensure the result is a string
            // use our own Render method
            string msg = "No renderer was found and the specified object to render was not a string or byte array: ({0}):: ({1})"._Format(toRender.GetType().Name, toRender.ToString());
            if (toRender == null)
            {
                msg = "toRender was null";
            }

            string toRenderAsString = toRender as string;
            byte[] toRenderAsByteArray = toRender as byte[];            
            if (toRenderAsString == null && toRenderAsByteArray == null)
            {
                Tag div = new Tag("div").Class("error").Text(msg);
                toRenderAsString = div.ToHtmlString();
                toRenderAsByteArray = Encoding.UTF8.GetBytes(toRenderAsString);
            }
            else if(toRenderAsString != null)
            {
                toRenderAsByteArray = Encoding.UTF8.GetBytes(toRenderAsString);
            }

            output.Write(toRenderAsByteArray, 0, toRenderAsByteArray.Length);
        }

        private void AddBaseRenderers()
        {
            AddRenderer(() => new JsonRenderer());
            AddRenderer(() => new XmlRenderer());
            AddRenderer(() => new YamlRenderer());
            AddRenderer(() => new TxtRenderer());
        }

        public void AddRenderer(Func<IRenderer> renderer)
        {
            if (_renderers == null)
            {
                _renderers = new Dictionary<string, Func<IRenderer>>();
            }

            renderer().Extensions.Each(ext =>
            {
                if (!_renderers.ContainsKey(ext))
                {
                    _renderers.Add(ext, renderer);
                }
                else
                {
                    IRenderer alreadySet = _renderers[ext]();
                    Logger.AddEntry("{0}::Renderer of type ({1}) for extension ({2}) has already been added, Renderer of type ({3}) will not be added",
                        LogEventType.Warning,
                        typeof(ServiceProxyResponder).Name,
                        alreadySet.GetType().Name,
                        ext,
                        renderer.GetType().Name
                    );
                }
            });
        }
    }
}
