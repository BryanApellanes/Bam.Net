/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Logging;

namespace Bam.Net.Server.Renderers
{
    /// <summary>
    /// The renderer used to render the results of a 
    /// common (server level) dust template provided a given object
    /// </summary>
    public class CommonDustRenderer: Renderer, ITemplateRenderer
    {
        public CommonDustRenderer(ContentResponder content)
            : base("text/html", ".htm", ".html")
        {
            this.ContentResponder = content;
        }

        ILogger _logger;
        public ILogger Logger
        {
            get
            {
                return _logger ?? Log.Default;
            }
            set
            {
                _logger = value;
            }
        }

        string _compiledDustTemplates;
        object _compiledDustTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/common/views.
        /// </summary>
        public virtual string CompiledTemplates
        {
            get
            {
                return _compiledDustTemplatesLock.DoubleCheckLock(ref _compiledDustTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    DirectoryInfo commonDust = new DirectoryInfo(Path.Combine(ContentResponder.Root, "common", "views"));

                    string commonCompiledTemplates = DustScript.CompileDirectory(commonDust, "*.dust");
                    
                    templates.Append(commonCompiledTemplates);   
                    return templates.ToString();
                });
            }
        }

        string _compiledLayoutTemplates;
        object _compiledLayoutTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/common/views/layouts.
        /// </summary>
        public virtual string CompiledLayoutTemplates
        {
            get
            {
                return _compiledLayoutTemplatesLock.DoubleCheckLock(ref _compiledLayoutTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    DirectoryInfo layouts = new DirectoryInfo(Path.Combine(ContentResponder.Root, "common", "views", "layouts"));
                    string compiledLayouts = DustScript.CompileDirectory(layouts.FullName, "*.dust", Logger);
                    templates.Append(compiledLayouts);
                    return templates.ToString();
                });
            }
        }

        string _compiledCommonTemplates;
        object _compiledCommonTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/dust/layouts.
        /// </summary>
        public virtual string CompiledCommonTemplates
        {
            get
            {
                return _compiledCommonTemplatesLock.DoubleCheckLock(ref _compiledCommonTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    DirectoryInfo common = new DirectoryInfo(Path.Combine(ContentResponder.Root, "common", "views"));

                    string compiledCommon = DustScript.CompileDirectory(common.FullName, "*.dust", Logger);

                    templates.Append(compiledCommon);
                    return templates.ToString();
                });
            }
        }
        
        public ContentResponder ContentResponder
        {
            get;
            set;
        }
        
        object _renderLock = new object();
        public override void Render(object toRender, Stream output)
        {
            Render(toRender.GetType().Name, toRender, output);
        }

        public virtual void Render(string templateName, object toRender, Stream output)
        {
            string result = DustScript.Render(CompiledTemplates, templateName, toRender);

            byte[] data = Encoding.UTF8.GetBytes(result);
            output.Write(data, 0, data.Length);
        }

		/// <summary>
		/// Render the specified LayoutModel to the specifie output Stream
		/// </summary>
		/// <param name="toRender"></param>
		/// <param name="output"></param>
        public virtual void RenderLayout(LayoutModel toRender, Stream output)
        {
            string result = DustScript.Render(CompiledLayoutTemplates, toRender.LayoutName, toRender);

            byte[] data = Encoding.UTF8.GetBytes(result);
            output.Write(data, 0, data.Length);
        }
    }
}
