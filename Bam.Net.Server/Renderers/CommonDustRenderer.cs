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
using Bam.Net.Presentation;

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
            ContentResponder = content;
            TemplateDirectoryNames = content.TemplateDirectoryNames.ToArray();
        }

        public string[] TemplateDirectoryNames
        {
            get;            
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

        public string ContentRoot
        {
            get
            {
                return ContentResponder.Root;
            }
        }

        string _compiledLayoutTemplates;
        object _compiledLayoutTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/common/views/layouts.
        /// </summary>
        public virtual string CombinedCompiledLayoutTemplates
        {
            get
            {
                return _compiledLayoutTemplatesLock.DoubleCheckLock(ref _compiledLayoutTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    foreach(string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo layouts = new DirectoryInfo(Path.Combine(ContentRoot, "common", templateDirectoryName, "layouts"));
                        string compiledLayouts = DustScript.CompileTemplates(layouts.FullName, "*.dust", Logger);
                        templates.Append(compiledLayouts);
                    }
                    return templates.ToString();
                });
            }
        }

        string _compiledDustTemplates;
        object _compiledDustTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/common/views.
        /// </summary>
        public virtual string CombinedCompiledTemplates
        {
            get
            {
                return _compiledDustTemplatesLock.DoubleCheckLock(ref _compiledDustTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();

                    foreach (string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo commonDust = new DirectoryInfo(Path.Combine(ContentRoot, "common", templateDirectoryName));
                        string commonCompiledTemplates = DustScript.CompileTemplates(commonDust, "*.dust");
                        templates.Append(commonCompiledTemplates);
                    }
                    return templates.ToString();
                });
            }
        }

        List<ICompiledTemplate> _compiledTemplates;
        object _compiledTemplatesLock = new object();
        public virtual IEnumerable<ICompiledTemplate> CompiledTemplates
        {
            get
            {
                return _compiledTemplatesLock.DoubleCheckLock(ref _compiledTemplates, () =>
                {
                    List<ICompiledTemplate> allResults = new List<ICompiledTemplate>();
                    foreach (string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo commonDust = new DirectoryInfo(Path.Combine(ContentRoot, "common", templateDirectoryName));
                        DustScript.CompileTemplates(commonDust, out List<ICompiledTemplate> results, "*.dust");
                        allResults.AddRange(results);
                    }
                    return allResults;
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

        public override void Render(string templateName, object toRender, Stream output)
        {
            string result = DustScript.Render(CombinedCompiledTemplates, templateName, toRender);

            byte[] data = Encoding.UTF8.GetBytes(result);
            output.Write(data, 0, data.Length);
        }

		/// <summary>
		/// Render the specified LayoutModel to the specified output Stream
		/// </summary>
		/// <param name="toRender"></param>
		/// <param name="output"></param>
        public virtual void RenderLayout(LayoutModel toRender, Stream output)
        {
            string result = DustScript.Render(CombinedCompiledLayoutTemplates, toRender.LayoutName, toRender);

            byte[] data = Encoding.UTF8.GetBytes(result);
            output.Write(data, 0, data.Length);
        }
    }
}
