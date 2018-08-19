/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Bam.Net.Caching.File;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Yahoo.Yui.Compressor;
using Bam.Net.Presentation;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.Linq;
using Bam.Net.Configuration;

namespace Bam.Net.Server
{
    public class AppContentResponder : ContentResponder
    {
        public const string CommonFolder = "common";

        public AppContentResponder(ContentResponder commonResponder, AppConf conf, DefaultDataSettingsProvider dataSettings = null, ILogger logger = null)
            : base(commonResponder.BamConf, logger)
        {
            if (conf.BamConf == null)
            {
                conf.BamConf = commonResponder.BamConf;
            }
            DataSettings = dataSettings ?? DefaultDataSettingsProvider.Current;
            ContentResponder = commonResponder;
            ServerRoot = commonResponder.ServerRoot;
            AppConf = conf;
            AppRoot = AppConf.AppRoot;
            AppTemplateManager = new AppDustRenderer(this);
            AppContentLocator = ContentLocator.Load(this);
            Fs commonRoot = new Fs(new DirectoryInfo(Path.Combine(ServerRoot.Root, CommonFolder)));
            ContentHandlers = new Dictionary<string, ContentHandler>();
            AllRequestHandler = new ContentHandler($"{conf.Name}.AllRequestHandler", AppRoot) { CheckPaths = false };
            CustomHandlerMethods = new List<MethodInfo>();
            CommonContentLocator = ContentLocator.Load(commonRoot);
            SetUploadHandler();
            SetBaseIgnorePrefixes();
            ContentHandlerScanTask = ScanForContentHandlers();
            SetAllRequestHandler();
        }

        protected ContentHandler AllRequestHandler { get; set; }
        protected Dictionary<string, ContentHandler> ContentHandlers { get; set; }
        protected Task ContentHandlerScanTask { get; }

        protected Task ScanForContentHandlers()
        {
            Task scan = Task.Run(() =>
            {
                try
                {
                    string[] assemblySearchPatterns = DefaultConfiguration.GetAppSetting("AssemblySearchPattern", "*ContentHandlers.dll").DelimitSplit(",", true);
                    DirectoryInfo entryDir = Assembly.GetEntryAssembly().GetFileInfo().Directory;
                    DirectoryInfo sysAssemblies = DataSettings.GetSysAssemblyDirectory();
                    List<FileInfo> files = new List<FileInfo>();
                    foreach(string assemblySearchPattern in assemblySearchPatterns)
                    {
                        files.AddRange(entryDir.GetFiles(assemblySearchPattern));
                        if (sysAssemblies.Exists)
                        {
                            files.AddRange(sysAssemblies.GetFiles(assemblySearchPattern));
                        }
                    }
                    Parallel.ForEach(files, LoadCustomHandlers);
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Exception occurred scanning for custom content handlers: {0}", ex, ex.Message);
                }
            });
            return scan;
        }

        protected List<MethodInfo> CustomHandlerMethods { get; }

        /// <summary>
        /// Load all methods found in the specified file that are addorned with the ContentHandlerAttribute attribute
        /// into the CustomHandlerMethods list.
        /// </summary>
        /// <param name="file"></param>
        protected void LoadCustomHandlers(FileInfo file)
        {
            try
            {
                string extension = Path.GetExtension(file.FullName);
                if(!extension.Equals(".dll", StringComparison.InvariantCultureIgnoreCase) && !extension.Equals(".exe", StringComparison.InvariantCulture))
                {
                    return;
                }
                Assembly assembly = Assembly.LoadFrom(file.FullName);                
                CustomHandlerMethods.AddRange(
                    assembly.GetTypes()
                        .Where(type => type.HasCustomAttributeOfType<ContentHandlerAttribute>())
                            .SelectMany(type => type.GetMethods().Where(mi =>
                            {
                                bool hasAttribute = mi.HasCustomAttributeOfType<ContentHandlerAttribute>();
                                bool isStatic = mi.IsStatic;
                                if(hasAttribute && !isStatic)
                                {
                                    Logger.AddEntry("The method {0}.{1} is marked as a ContentHandler but it is not static", LogEventType.Warning, mi.DeclaringType.Name, mi.Name);
                                }
                                ParameterInfo[] parms = mi.GetParameters();
                                return hasAttribute && 
                                    isStatic &&
                                    mi.ReturnType == typeof(byte[]) &&                                    
                                    parms.Length == 2 &&
                                    parms[0].ParameterType == typeof(IHttpContext) &&
                                    parms[1].ParameterType == typeof(Fs);
                            }))
                );
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Failed to load custom handlers from file {0}: {1}", ex, file.FullName, ex.Message);
            }
        }

        public DefaultDataSettingsProvider DataSettings { get; }

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
        
        protected async void SetAllRequestHandler()
        {
            try
            {
                await ContentHandlerScanTask;
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine(e.Message);                    
                }
            }

            AllRequestHandler.GetContent = (ctx, fs) =>
            {
                byte[] result = null;
                foreach(MethodInfo method in CustomHandlerMethods)
                {
                    result = method.Invoke(null, ctx, fs) as byte[];
                    if(result != null)
                    {
                        Logger.AddEntry("{0}.{1} handled request {2}\r\n{3}", method.DeclaringType.Name, method.Name, ctx.Request.Url.ToString(), ctx.Request.PropertiesToString());
                        break;
                    }
                }
                return result;
            };
        }

        protected void SetUploadHandler()
        {
            SetCustomContentHandler("Application Upload", "/upload", (ctx, fs) =>
            {
                IRequest request = ctx.Request;
                HandleUpload(ctx, HttpPostedFile.FromRequest(request));
                string query = request.Url.Query.Length > 0 ? request.Url.Query : string.Empty;
                if (query.StartsWith("?"))
                {
                    query = query.TruncateFront(1);
                }
                return RenderLayout(ctx.Response, request.Url.AbsolutePath, query);
            });
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

        public ITemplateManager AppTemplateManager
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
            AppInitializing?.Invoke(this);
        }

        protected void OnAppInitialized()
        {
            AppInitialized?.Invoke(this);
        }

        public string ApplicationName
        {
            get
            {
                return AppConf.Name;
            }
        }

        public User GetUser(IHttpContext context)
        {
            UserManager mgr = BamConf.Server.GetAppService<UserManager>(ApplicationName).Clone<UserManager>();
            mgr.HttpContext = context;
            return mgr.GetUser(context);
        }

        /// <summary>
        /// The server content root
        /// </summary>
        public Fs ServerRoot { get; private set; }

        /// <summary>
        /// The application content root
        /// </summary>
        public Fs AppRoot { get; private set; }

        public void SetCustomContentHandler(string path, Func<IHttpContext, Fs, byte[]> handler)
        {
            SetCustomContentHandler(path, path, handler);
        }

        public void SetCustomContentHandler(string name, string path, Func<IHttpContext, Fs, byte[]> handler)
        {
            SetCustomContentHandler(name, new string[] { path }, handler);
        }

        public void SetCustomContentHandler(string name, string[] paths, Func<IHttpContext, Fs, byte[]> handler)
        {
            ContentHandler customHandler = new ContentHandler(name, AppRoot, paths) { GetContent = handler };
            SetCustomContentHandler(customHandler);
        }

        public void SetCustomContentHandler(ContentHandler customContentHandler)
        {
            foreach(string path in customContentHandler.Paths)
            {
                ContentHandlers[path.ToLowerInvariant()] = customContentHandler;
            }
        }

        /// <summary>
        /// Initializes the file system from the embedded zip resource
        /// that represents a bare bones app.
        /// </summary>
        public override void Initialize()
        {
            OnAppInitializing();
            WriteCompiledTemplates();

            AppRoot.WriteFile("appConf.json", AppConf.ToJson(true));

            OnAppInitialized();
        }

        public override bool TryRespond(IHttpContext context)
        {
            return TryRespond(context, out string[] ignore);
        }

        public bool TryRespond(IHttpContext context, out string[] checkedPaths)
        {
            checkedPaths = new string[] { };
            try
            {
                if (Etags.CheckEtags(context))
                {
                    return true;
                }
                IRequest request = context.Request;
                IResponse response = context.Response;
                string path = request.Url.AbsolutePath;

                string ext = Path.GetExtension(path);

                string[] split = path.DelimitSplit("/");
                byte[] content = new byte[] { };
                bool handled = AllRequestHandler.HandleRequest(context, out content);

                if (!handled)
                {
                    if (ContentHandlers.ContainsKey(path.ToLowerInvariant()))
                    {
                        handled = ContentHandlers[path.ToLowerInvariant()].HandleRequest(context, out content);
                    }
                    else if (AppContentLocator.Locate(path, out string locatedPath, out checkedPaths))
                    {
                        handled = true;
                        string foundExt = Path.GetExtension(locatedPath);
                        if (FileCachesByExtension.ContainsKey(foundExt))
                        {
                            FileCache cache = FileCachesByExtension[ext];
                            if (ShouldZip(request))
                            {
                                SetGzipContentEncodingHeader(response);
                                content = cache.GetZippedContent(locatedPath);
                            }
                            else
                            {
                                content = cache.GetContent(locatedPath);
                            }
                        }
                        else
                        {
                            content = File.ReadAllBytes(locatedPath);
                        }
                        Etags.SetLastModified(response, request.Url.ToString(), new FileInfo(locatedPath).LastWriteTime);
                    }
                    else if (string.IsNullOrEmpty(ext) && !ShouldIgnore(path) || (AppRoot.FileExists("~/pages{0}.html"._Format(path), out locatedPath)))
                    {

                        content = RenderLayout(response, path);
                        handled = true;
                    }
                }

                if (handled)
                {
                    SetContentType(response, path);
                    SetContentDisposition(response, path);
                    Etags.Set(response, request.Url.ToString(), content);
                    SetResponseHeaders(response, path);
                    SendResponse(response, content);
                    OnResponded(context);
                }
                else
                {
                    OnNotResponded(context);
                }
                return handled;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("An error occurred in {0}.{1}: {2}", ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                OnNotResponded(context);
                return false;
            }
        }

        protected virtual void SetResponseHeaders(IResponse response, string path)
        {
            if (path.StartsWith("/meta"))
            {
                path = path.TruncateFront("/meta".Length);
            }
            Fs meta = new Fs(Path.Combine(AppRoot.Root, "meta", "headers"));
            if (meta.FileExists(path, out string fullPath))
            {
                foreach (string header in fullPath.SafeReadFile().DelimitSplit("\n"))
                {
                    string[] split = header.Split(new char[] { ':' }, 2);
                    if (split.Length == 2)
                    {
                        response.AddHeader(split[0].Trim(), split[1].Trim());
                    }
                }
            }
        }

        [Verbosity(LogEventType.Information)]
        public new event EventHandler FileUploading;
        [Verbosity(LogEventType.Information)]
        public new event EventHandler FileUploaded;

        public virtual void HandleUpload(IHttpContext context, HttpPostedFile file)
        {
            FileUploadEventArgs args = new FileUploadEventArgs(context, file, ApplicationName);
            FireEvent(FileUploading, args);
            if (args.Continue)
            {
                string userName = GetUser(context).UserName;
                args.UserName = userName;
                string saveToPath = Path.Combine(AppRoot.Root, "workspace", "uploads", userName, "temp_".RandomLetters(8));
                FileInfo fileInfo = new FileInfo(saveToPath);
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                file.Save(saveToPath);
                string renameTo = Path.Combine(fileInfo.Directory.FullName, file.FileName);
                renameTo = renameTo.GetNextFileName();
                File.Move(saveToPath, renameTo);
                file.FullPath = renameTo;
                FireEvent(FileUploaded, args);
            }
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

        protected internal LayoutModel GetLayoutModelForPath(string path)
        {
            if (path.Equals("/"))
            {
                path = "/{0}"._Format(AppConf.DefaultPage.Or(AppConf.DefaultLayoutConst));
            }

            string lowered = path.ToLowerInvariant();
            string[] layoutSegments = string.Format("~/pages/{0}{1}", path, LayoutFileExtension).DelimitSplit("/", "\\");
            string[] htmlSegments = string.Format("~/pages/{0}.html", path).DelimitSplit("/", "\\");

            LayoutModel layoutModel = null;
            if (LayoutModelsByPath.ContainsKey(lowered))
            {
                layoutModel = LayoutModelsByPath[lowered];
            }
            else if (AppRoot.FileExists(out string absolutePath, layoutSegments))
            {
                LayoutConf layoutConf = new LayoutConf(AppConf);
                LayoutConf fromLayoutFile = FileCachesByExtension[LayoutFileExtension].GetText(new FileInfo(absolutePath)).FromJson<LayoutConf>();
                layoutConf.CopyProperties(fromLayoutFile);
                layoutModel = layoutConf.CreateLayoutModel(htmlSegments);
                LayoutModelsByPath[lowered] = layoutModel;
            }
            else
            {
                LayoutConf defaultLayoutConf = new LayoutConf(AppConf);
                layoutModel = defaultLayoutConf.CreateLayoutModel(htmlSegments);
                FileInfo file = new FileInfo(AppRoot.GetAbsolutePath(layoutSegments));
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                // write the file to disk                 
                defaultLayoutConf.ToJsonFile(file);
                LayoutModelsByPath[lowered] = layoutModel;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                string page = path.TruncateFront(1);
                if (!string.IsNullOrEmpty(page))
                {
                    layoutModel.StartPage = page;
                }
            }
            return layoutModel;
        }

        public Includes GetAppIncludes()
        {
            return GetAppIncludes(AppConf);
        }

        private byte[] RenderLayout(IResponse response, string path, string queryString = null)
        {
            byte[] content;
            AppTemplateManager.SetContentType(response);
            MemoryStream ms = new MemoryStream();
            LayoutModel layoutModel = GetLayoutModelForPath(path);
            layoutModel.QueryString = queryString ?? layoutModel.QueryString;
            AppTemplateManager.RenderLayout(layoutModel, ms);
            ms.Seek(0, SeekOrigin.Begin);
            content = ms.GetBuffer();
            return content;
        }

        private void WriteCompiledTemplates()
        {
            if (AppConf.CompileTemplates)
            {
                // TODO: see DustScript.cs line 91 to make Regex.Unescape calls unecessary
                AppConf.AppRoot.WriteFile("~/combinedTemplates.js", Regex.Unescape(AppTemplateManager.CombinedCompiledTemplates));

                Task.Run(() =>
                {
                    Parallel.ForEach(AppTemplateManager.CompiledTemplates, (template) =>
                    {
                        FileInfo templateFile = new FileInfo(template.SourceFilePath);
                        FileInfo jsFile = new FileInfo(Path.Combine(templateFile.Directory.FullName, $"{Path.GetFileNameWithoutExtension(templateFile.Name)}.js"));
                        jsFile.FullName.SafeWriteFile(template.UnescapedCompiled, true);
                    });
                });
            }
        }

        private void LocateIncludes(HashSet<string> filePaths, Includes includes)
        {
            foreach (string script in includes.Scripts)
            {
                if (AppContentLocator.Locate(script, out string scriptPath, out string[] checkedPaths))
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

        protected override void SetBaseIgnorePrefixes()
        {
            base.SetBaseIgnorePrefixes();
            AddIgnorPrefix("content");
        }
    }
}
