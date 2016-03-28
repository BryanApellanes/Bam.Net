/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ionic.Zip;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.Server.Renderers;
using Bam.Net.Javascript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Bam.Net.Server;
using Yahoo.Yui.Compressor;

namespace Bam.Net.Server
{
    public class AppContentResponder : ContentResponder
    {
        public const string CommonFolder = "common";

        public AppContentResponder(ContentResponder serverRoot, string appName)
            : base(serverRoot.BamConf)
        {
            this.ContentResponder = serverRoot;
            this.ServerRoot = serverRoot.ServerRoot;
            this.AppConf = new AppConf(serverRoot.BamConf, appName);
            this.AppRoot = this.AppConf.AppRoot;
            this.AppDustRenderer = new AppDustRenderer(this);
            this.UseCache = serverRoot.UseCache;
            this.AppContentLocator = ContentLocator.Load(this);
            this.CommonContentLocator = ContentLocator.Load(ServerRoot);

            this.SetBaseIgnorePrefixes();
        }

        public AppContentResponder(ContentResponder serverRoot, AppConf conf)
            : base(serverRoot.BamConf)
        {
            if (conf.BamConf == null)
            {
                conf.BamConf = serverRoot.BamConf;
            }

            this.ContentResponder = serverRoot;
            this.ServerRoot = serverRoot.ServerRoot;
            this.AppConf = conf;
            this.AppRoot = this.AppConf.AppRoot;
            this.AppDustRenderer = new AppDustRenderer(this);
            this.UseCache = serverRoot.UseCache;
            this.AppContentLocator = ContentLocator.Load(this);
            Fs commonRoot = new Fs(new DirectoryInfo(Path.Combine(ServerRoot.Root, CommonFolder)));
            this.CommonContentLocator = ContentLocator.Load(commonRoot);

            this.SetBaseIgnorePrefixes();
        }

        private void SetBaseIgnorePrefixes()
        {
            AddIgnorPrefix("dao");
            AddIgnorPrefix("serviceproxy");
            AddIgnorPrefix("api");
            AddIgnorPrefix("bam");
            AddIgnorPrefix("get");
            AddIgnorPrefix("post");
        }

        public ContentLocator AppContentLocator
        {
            get;
            private set;
        }

        public ContentLocator CommonContentLocator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the main ContentResponder, which is the content responder
        /// for the server level of the current BamServer
        /// </summary>
        public ContentResponder ContentResponder
        {
            get;
            private set;
        }

        public AppDustRenderer AppDustRenderer
        {
            get;
            private set;
        }

        public AppConf AppConf
        {
            get;
            private set;
        }

        public event Action<AppContentResponder> AppInitializing;
        public event Action<AppContentResponder> AppInitialized;

        protected void OnAppInitializing()
        {
            if (AppInitializing != null)
            {
                AppInitializing(this);
            }
        }

        protected void OnAppInitialized()
        {
            if (AppInitialized != null)
            {
                AppInitialized(this);
            }
        }

        public string ApplicationName
        {
            get
            {
                return AppConf.Name;
            }
        }

        /// <summary>
        /// The server content root
        /// </summary>
        public Fs ServerRoot { get; private set; }

        /// <summary>
        /// The application content root
        /// </summary>
        public Fs AppRoot { get; private set; }

        /// <summary>
        /// Initializes the file system from the embedded zip resource
        /// that represents a bare bones app.
        /// </summary>
        public override void Initialize()
        {
            OnAppInitializing();
            ExtractBaseApp();
            WriteAppScripts();
            WriteCompiledTemplates();

            AppRoot.WriteFile("appConf.json", AppConf.ToJson(true));

            OnAppInitialized();
        }

        public override bool TryRespond(IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;

            string path = request.Url.AbsolutePath;
            string ext = Path.GetExtension(path);
            string mgmtPrefix = "/bam/apps/{0}"._Format(AppConf.DomApplicationIdFromAppName(ApplicationName));
            if (path.ToLowerInvariant().StartsWith(mgmtPrefix.ToLowerInvariant()))
            {
                path = path.TruncateFront(mgmtPrefix.Length);
            }

            string[] split = path.DelimitSplit("/");
            byte[] content = new byte[] { };
            bool result = false;

            string locatedPath;
            string[] checkedPaths;
            if (string.IsNullOrEmpty(ext) && !ShouldIgnore(path) ||
                (AppRoot.FileExists("~/pages{0}.html"._Format(path))))
            {
                CommonTemplateRenderer.SetContentType(response);
                MemoryStream ms = new MemoryStream();
                CommonTemplateRenderer.RenderLayout(GetLayoutModelForPath(path), ms);
                ms.Seek(0, SeekOrigin.Begin);
                content = ms.GetBuffer();
                result = true;
            }
            else if (AppContentLocator.Locate(path, out locatedPath, out checkedPaths))
            {
                if (Cache.ContainsKey(locatedPath) && UseCache)
                {
                    content = Cache[path];
                    result = true;
                }
                else if (MinCache.ContainsKey(locatedPath) && UseCache) // check the min cache
                {
                    content = MinCache[locatedPath];
                    result = true;
                }
                else
                {
                    byte[] temp = ReadFile(locatedPath);

                    content = temp;
                    result = true;
                }
            }
            else
            {
                StringBuilder checkedPathString = new StringBuilder();
                checkedPaths.Each(p =>
                {
                    checkedPathString.AppendLine(p);
                });

                Logger.AddEntry(
                  "App[{0}]::Path='{1}'::Not Found\r\nChecked Paths\r\n{2}",
                  LogEventType.Warning,
                  AppConf.Name,
                  path,
                  checkedPathString.ToString()
                );
            }


            if (result)
            {
                SetContentType(response, path);
                SendResponse(response, content);
            }
            return result;
        }

        Dictionary<string, LayoutModel> _layoutModelsByPath;
        object _layoutsByPathSync = new object();
        protected internal Dictionary<string, LayoutModel> LayoutModelsByPath
        {
            get
            {
                return _layoutsByPathSync.DoubleCheckLock(ref _layoutModelsByPath, () => new Dictionary<string, LayoutModel>());
            }
        }

        protected internal LayoutModel GetLayoutModelForPath(string path, string ext = ".layout")
        {
            if (path.Equals("/"))
            {
                path = "/{0}"._Format(AppConf.DefaultPage.Or(AppConf.DefaultLayoutConst));
            }

            string lowered = path.ToLowerInvariant();
            string[] layoutSegments = string.Format("~/pages/{0}{1}", path, ext).DelimitSplit("/", "\\");
            string[] htmlSegments = string.Format("~/pages/{0}.html", path).DelimitSplit("/", "\\");

            LayoutModel result = null;
            if (LayoutModelsByPath.ContainsKey(lowered))
            {
                result = LayoutModelsByPath[lowered];
            }
            else if (AppRoot.FileExists(layoutSegments))
            {
                LayoutConf layoutConf = new LayoutConf(AppConf);
                LayoutConf fromLayoutFile = AppRoot.ReadAllText(layoutSegments).FromJson<LayoutConf>();
                layoutConf.CopyProperties(fromLayoutFile);
                result = layoutConf.CreateLayoutModel(htmlSegments);
                LayoutModelsByPath[lowered] = result;                
            }
            else
            {
                LayoutConf defaultLayoutConf = new LayoutConf(AppConf);
                result = defaultLayoutConf.CreateLayoutModel(htmlSegments);
                FileInfo file = new FileInfo(AppRoot.GetAbsolutePath(layoutSegments));
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                // write the file to disk                 
                defaultLayoutConf.ToJsonFile(file);
                LayoutModelsByPath[lowered] = result;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                string page = path.TruncateFront(1);
                if (!string.IsNullOrEmpty(page))
                {
                    result.StartPage = page;
                }
            }
            return result;
        }

        public Includes GetAppIncludes()
        {
            return GetAppIncludes(AppConf);
        }

        private void WriteCompiledTemplates()
        {
            if (AppConf.CompileTemplates)
            {
                AppConf.AppRoot.WriteFile("~/compiledTemplates.js", Regex.Unescape(AppDustRenderer.CompiledTemplates));
            }
        }
        private void WriteAppScripts()
        {
            HashSet<string> filePaths = new HashSet<string>();
            // get all js files
            //		in ~/js folder non-recursively
            AppRoot.GetFiles("~/js", "*.js").Each(fi => filePaths.Add(fi.FullName));
            //		in ~/pages folder recursively
            AppRoot.GetFiles("~/pages", "*.js", SearchOption.AllDirectories).Each(fi => filePaths.Add(fi.FullName));
            //		in ~/viewModels folder recursively
            AppRoot.GetFiles("~/viewModels", "*.js", SearchOption.AllDirectories).Each(fi => filePaths.Add(fi.FullName));
            //		in ~/<appName>/include.js file
            //		in ~s/include.js file
            Includes appIncludes = GetAppIncludes();
            LocateIncludes(filePaths, appIncludes);
            Includes commonIncludes = GetCommonIncludes();
            LocateIncludes(filePaths, commonIncludes);

            StringBuilder combined = new StringBuilder();
            foreach (string scriptPath in filePaths)
            {
                combined.AppendLine(File.ReadAllText(scriptPath));
            }
            JavaScriptCompressor compressor = new JavaScriptCompressor();
            AppConf.AppRoot.WriteFile("~/{0}.js"._Format(ApplicationName), combined.ToString());
            string combinedSrc = combined.ToString();
            if (!string.IsNullOrEmpty(combinedSrc))
            {
                AppConf.AppRoot.WriteFile("~/{0}.min.js"._Format(ApplicationName), compressor.Compress(combinedSrc));
            }
        }

        private void LocateIncludes(HashSet<string> filePaths, Includes includes)
        {
            foreach (string script in includes.Scripts)
            {
                string scriptPath;
                string[] checkedPaths;
                if (AppContentLocator.Locate(script, out scriptPath, out checkedPaths))
                {
                    filePaths.Add(AppContentLocator.ContentRoot.GetAbsolutePath(scriptPath));
                }
                else
                {
                    if (CommonContentLocator.Locate(script, out scriptPath, out checkedPaths))
                    {
                        filePaths.Add(CommonContentLocator.ContentRoot.GetAbsolutePath(scriptPath));
                    }
                    else
                    {
                        Logger.AddEntry("script specified in app include.js file was not found: {0}\r\nchecked paths:\r\n\t{1}", LogEventType.Warning, script, checkedPaths.ToDelimited(p => p, "\r\n\t"));
                    }
                }
            }
        }

        private void ExtractBaseApp()
        {
            if (AppConf.ExtractBaseApp)
            {
                string baseDirectory = Path.Combine(BamConf.ContentRoot, "apps", ApplicationName);
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                string[] resourceNames = currentAssembly.GetManifestResourceNames();
                resourceNames.Each(rn =>
                {
                    bool isBase = Path.GetExtension(rn).ToLowerInvariant().Equals(".base");
                    if (isBase)
                    {
                        Stream zipStream = currentAssembly.GetManifestResourceStream(rn);
                        ZipFile zipFile = ZipFile.Read(zipStream);
                        zipFile.Each(entry =>
                        {
                            entry.Extract(baseDirectory, ExtractExistingFileAction.DoNotOverwrite);
                        });
                    }
                });
            }
        }
    }
}
