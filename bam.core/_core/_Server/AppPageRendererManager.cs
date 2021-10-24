using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Presentation;
using Bam.Net.ServiceProxy;
using Bam.Net.Services;

namespace Bam.Net.Server
{
    public class AppPageRendererManager : AppPageRenderer
    {
        public AppPageRendererManager(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager) : base(appContentResponder, commonTemplateManager)
        {
            Init();
        }

        public AppPageRendererManager(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager, IApplicationTemplateManager applicationTemplateManager) : base(appContentResponder, commonTemplateManager, applicationTemplateManager)
        {
            Init();
        }

        private void Init()
        {
            _renderers = new HashSet<AppPageRenderer>();
            DefaultPageRenderer = new StaticContentPageRenderer(AppContentResponder, TemplateManager, ApplicationTemplateManager);
            AddPageRenderer(DefaultPageRenderer);
            LoadRenderers();
        }
        
        [Inject]
        public ILogger Logger { get; set; }
        public StaticContentPageRenderer DefaultPageRenderer { get; private set; }
        
        private HashSet<AppPageRenderer> _renderers;
        public AppPageRenderer[] Renderers
        {
            get => _renderers.ToArray();
            set => _renderers = new HashSet<AppPageRenderer>(value);
        }

        public AppPageRendererManager AddPageRenderer(AppPageRenderer renderer)
        {
            _renderers.Add(renderer);
            return this;
        }
       
        public override byte[] RenderPage(IRequest request, IResponse response)
        {
            AppPageRenderer renderer = ResolveRenderer(request);
            if (renderer != null)
            {
                return renderer.RenderPage(request, response);
            }

            return RenderNotFound(request, response);
        }
        
        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler RendererScanStarted;
        
        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler RendererScanComplete;
        
        [Verbosity(VerbosityLevel.Warning)]
        public event EventHandler RendererScanError;

        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler RendererLoaded;
        
        [Verbosity(VerbosityLevel.Information)]
        public event EventHandler RendererResolved;
        
        [Verbosity(VerbosityLevel.Warning)]
        public event EventHandler RendererNotResolved;

        protected ILogger GetLogger()
        {
            return Logger ?? Bam.Net.Logging.Log.Default;
        }
        protected void LoadRenderers()
        {
            HashSet<string> extensions = new HashSet<string>();
            DirectoryInfo htmlDir = new DirectoryInfo(Path.Combine(AppConf.AppRoot.Root, AppConf.HtmlDir));
            foreach (FileInfo fileInfo in htmlDir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (!fileInfo.Extension.Equals(".dll", StringComparison.InvariantCultureIgnoreCase) &&
                    !fileInfo.Extension.Equals(".exe", StringComparison.InvariantCultureIgnoreCase))
                {
                    extensions.Add(fileInfo.Extension);
                }
            }
            
            ScanForRenderers(extensions);
        }

        private void ScanForRenderers(HashSet<string> extensions)
        {
            Task.Run(() =>
            {
                FireEvent(RendererScanStarted);
                DirectoryInfo binDir = new DirectoryInfo(Path.Combine(AppConf.AppRoot.Root, AppConf.BinDir));
                foreach (FileInfo fileInfo in binDir.GetFiles("*.dll"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.ExtendsType<AppPageRenderer>())
                            {
                                AppPageRenderer appPageRenderer = type.Construct<AppPageRenderer>(AppContentResponder, CommonTemplateManager, ApplicationTemplateManager);
                                if (extensions.Contains(appPageRenderer.FileExtension))
                                {
                                    AddPageRenderer(appPageRenderer);
                                    FireEvent(RendererLoaded, new RendererLoadedEventArgs {AppConf = AppConf, Renderer = appPageRenderer});
                                }
                                else if (type.HasCustomAttributeOfType<AppAttribute>(out AppAttribute attribute))
                                {
                                    if (attribute.Name.Equals(AppConf.Name, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        AddPageRenderer(appPageRenderer);
                                        FireEvent(RendererLoaded, new RendererLoadedEventArgs {AppConf = AppConf, Renderer = appPageRenderer});
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        GetLogger().AddEntry("Exception occurred scanning for extension renderers: {0}", ex, ex.Message);
                        FireEvent(RendererScanError);
                    }
                }
                FireEvent(RendererScanComplete);
            });
        }

        
        private AppPageRenderer ResolveRenderer(IRequest request)
        {
            AppPageRenderer renderer = _renderers
                .ToImmutableSortedSet(new Comparer<AppPageRenderer>((x, y) => y.Precedence.CompareTo(x.Precedence)))
                .FirstOrDefault(apr => apr.FileExists(request));

            if (renderer != null)
            {
                FireEvent(RendererResolved, new RendererResolvedEventArgs {AppConf = AppConf, Request = request, AppPageRenderer = renderer});
            }
            else
            {
                renderer = DefaultPageRenderer;
                FireEvent(RendererNotResolved, new RendererResolvedEventArgs {AppConf = AppConf, Request = request, AppPageRenderer = renderer});
            }
            return renderer;
        }
    }
}