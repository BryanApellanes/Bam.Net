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

namespace Bam.Net.Server
{
    public class AppContentResponder : ContentResponder
    {
        public const string CommonFolder = "common";

        public AppContentResponder(ContentResponder commonResponder, AppConf conf)
            : base(commonResponder.BamConf)
        {
            if (conf.BamConf == null)
            {
                conf.BamConf = commonResponder.BamConf;
            }

            ContentResponder = commonResponder;
            ServerRoot = commonResponder.ServerRoot;
            AppConf = conf;
            AppRoot = AppConf.AppRoot;
            AppTemplateRenderer = new AppDustRenderer(this);
            AppContentLocator = ContentLocator.Load(this);
            Fs commonRoot = new Fs(new DirectoryInfo(Path.Combine(ServerRoot.Root, CommonFolder)));
            CustomContentHandlers = new Dictionary<string, CustomContentHandler>();
            CommonContentLocator = ContentLocator.Load(commonRoot);
            SetUploadHandler();
            SetBaseIgnorePrefixes();
        }

        protected Dictionary<string, CustomContentHandler> CustomContentHandlers { get; set; }

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

        protected void SetUploadHandler()
        {
            SetContentHandler("Application Upload", "/upload", (ctx, fs) =>
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

        public ITemplateRenderer AppTemplateRenderer
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

        public void SetContentHandler(string name, string path, Func<IHttpContext, Fs, byte[]> handler)
        {
            SetContentHandler(name, new string[] { path }, handler);
        }

        public void SetContentHandler(string name, string[] paths, Func<IHttpContext, Fs, byte[]> handler)
        {
            CustomContentHandler customHandler = new CustomContentHandler(name, AppRoot, paths) { GetContent = handler };
            foreach (string path in paths)
            {
                CustomContentHandlers[path.ToLowerInvariant()] = customHandler;
            }
        }

        /// <summary>
        /// Initializes the file system from the embedded zip resource
        /// that represents a bare bones app.
        /// </summary>
        public override void Initialize()
        {
            OnAppInitializing();
            WriteAppScripts();
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
                bool result = false;

                if (CustomContentHandlers.ContainsKey(path.ToLowerInvariant()))
                {
                    result = CustomContentHandlers[path.ToLowerInvariant()].HandleRequest(context, out content);
                }
                else if (string.IsNullOrEmpty(ext) && !ShouldIgnore(path) ||
                   (AppRoot.FileExists("~/pages{0}.html"._Format(path), out string locatedPath)))
                {
                    content = RenderLayout(response, path);
                    result = true;
                }
                else if (AppContentLocator.Locate(path, out locatedPath, out checkedPaths))
                {
                    result = true;
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

                if (result)
                {
                    SetContentType(response, path);
                    SetContentDisposition(response, path);
                    Etags.Set(response, path, content);
                    SetResponseHeaders(response, path);
                    SendResponse(response, content);
                    OnResponded(context);
                }
                else
                {
                    OnNotResponded(context);
                }
                return result;
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

            string absolutePath;
            string lowered = path.ToLowerInvariant();
            string[] layoutSegments = string.Format("~/pages/{0}{1}", path, LayoutFileExtension).DelimitSplit("/", "\\");
            string[] htmlSegments = string.Format("~/pages/{0}.html", path).DelimitSplit("/", "\\");

            LayoutModel result = null;
            if (LayoutModelsByPath.ContainsKey(lowered))
            {
                result = LayoutModelsByPath[lowered];
            }
            else if (AppRoot.FileExists(out absolutePath, layoutSegments))
            {
                LayoutConf layoutConf = new LayoutConf(AppConf);
                LayoutConf fromLayoutFile = FileCachesByExtension[LayoutFileExtension].GetText(new FileInfo(absolutePath)).FromJson<LayoutConf>();
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

        private byte[] RenderLayout(IResponse response, string path, string queryString = null)
        {
            byte[] content;
            AppTemplateRenderer.SetContentType(response);
            MemoryStream ms = new MemoryStream();
            LayoutModel layoutModel = GetLayoutModelForPath(path);
            layoutModel.QueryString = queryString ?? layoutModel.QueryString;
            AppTemplateRenderer.RenderLayout(layoutModel, ms);
            ms.Seek(0, SeekOrigin.Begin);
            content = ms.GetBuffer();
            return content;
        }

        private void WriteCompiledTemplates()
        {
            if (AppConf.CompileTemplates)
            {
                AppConf.AppRoot.WriteFile("~/compiledTemplates.js", Regex.Unescape(AppTemplateRenderer.CompiledTemplates));
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
