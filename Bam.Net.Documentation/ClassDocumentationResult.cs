/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net;
using Bam.Net.Incubation;
using System.Reflection;
using Bam.Net.Logging;

namespace Bam.Net.Documentation
{
    public class ClassDocumentationResult: ActionResult
    {
        public ClassDocumentationResult(string[] classNames)
        {
            this.ClassNames = classNames;
        }
        
        public string[] ClassNames { get; set; }
        public static DocumentationRenderDelegate DefaultRenderer { get; set; }

        DocumentationRenderDelegate _renderer;
        object _rendererLock = new object();
        public DocumentationRenderDelegate Renderer
        {
            get
            {
                return _rendererLock.DoubleCheckLock(ref _renderer, () => DefaultRenderer);
            }
            set
            {
                _renderer = value;
            }
        }
        
        public override void ExecuteResult(ControllerContext context)
        {
            if (ClassNames.Length > 0)
            {
                RenderFromDocAttributes(context);
            }
        }

        Incubator _serviceContainer;
        object _sync = new object();
        public Incubator ServiceContainer
        {
            get
            {
                return _sync.DoubleCheckLock(ref _serviceContainer, () => Incubator.Default);
            }
        }

        private void RenderFromDocAttributes(ControllerContext context)
        {
            StringBuilder output = new StringBuilder();
            Dictionary<string, List<ClassDocumentation>> documentation = new Dictionary<string, List<ClassDocumentation>>();

            if (ClassNames.Length > 0)
            {
                Incubator container = ServiceContainer;
                HttpServerUtilityBase server = context.HttpContext.Server;
                ClassNames.Each(cn =>
                {
                    Type type = container[cn];
                    documentation = ClassDocumentation.FromDocAttributes(type);
                });
            }

            if (Renderer != null)
            {
                Renderer(documentation, output);
                context.HttpContext.Response.Write(output.ToString());
            }
            else
            {
                context.HttpContext.Response.Write("Attribute documentation renderer not specified.  Set DocResult.AttributeRenderer per request or set DocResult.DefaultAttributeRenderer for global effect");
            }
        }
    }
}
