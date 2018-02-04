/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Logging;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.Server.Meta;

namespace Bam.Net.Server.Meta
{
    public class AppMetaInitializer : Loggable, IInitialize<AppMetaInitializer>
    {
        public const string BooksPath = "~/meta/books/{0}";
        public const string PagesPath = "~/meta/pages";
        
        public AppMetaInitializer(ContentResponder contentResponder)
        {
            ContentResponder = contentResponder;
            InitializationTask = InitializeAsync();
        }

        public ContentResponder ContentResponder
        {
            get; set;
        }

        public Dictionary<string, AppContentResponder> AppContentResponders
        {
            get
            {
                return ContentResponder.AppContentResponders;
            }
        }

        public event Action<AppMetaInitializer> Initializing;
        protected void OnInitializing()
        {
            Initializing?.Invoke(this);
        }
        public event Action<AppMetaInitializer> Initialized;
        protected void OnInitialized()
        {
            IsInitialized = true;
            Initialized?.Invoke(this);
        }

        [Verbosity(LogEventType.Information, MessageFormat = "WebBookInitializer:: {AppName} initializ(ING)")]
        public event EventHandler AppInitializing;

        [Verbosity(LogEventType.Information, MessageFormat = "WebBookInitializer:: {AppName} initializ(ED)")]
        public event EventHandler AppInitialized;

        [Verbosity(LogEventType.Information, MessageFormat = "WebBookInitializer:: {AppName}: writ(ING) book for page {CurrentPage}")]
        public event EventHandler WritingBook;

        [Verbosity(LogEventType.Information, MessageFormat = "WebBookInitializer:: {AppName}: writ(ED)(wrote) book for page {CurrentPage}")]
        public event EventHandler WroteBook;

        public bool IsInitialized
        {
            get;
            private set;
        }
        
        public Task InitializationTask { get; }

        protected Task InitializeAsync()
        {
            return Task.Run(() => Initialize());
        }

        public void Initialize()
        {
            OnInitializing();
            AppContentResponders.Keys.Each(appName =>
            {
                WriteBooks(AppContentResponders[appName].AppConf);
            });
            OnInitialized();
        }
        
        public string CurrentPage { get; private set; }

        public string AppName { get; private set; }

        public void WriteBooks(AppConf appConfig)
        {
            AppName = appConfig.Name;
            FireEvent(AppInitializing, new WebBookEventArgs(appConfig));
            Fs appFs = appConfig.AppRoot;
            // get all the pages 
            AppMetaManager manager = new AppMetaManager(appConfig.BamConf);
            List<string> pageNames = new List<string>(manager.GetPageNames(appConfig.Name));
            WritePageList(appFs, pageNames.ToArray());
            // read all the pages
            pageNames.Each(pageName =>
            {
                FireEvent(WritingBook, new WebBookEventArgs(appConfig));
                CurrentPage = pageName;                
                // create a new book for every page
                WebBook book = new WebBook { Name = pageName };
                string content = appFs.ReadAllText("pages", "{0}.html"._Format(pageName));
                // get all the [data-navigate-to] and a elements
                CQ cq = CQ.Create(content);
                CQ navElements = cq["a, [data-navigate-to]"];
                navElements.Each(nav =>
                {
                    // create a WebBookPage for each target
                    string href = nav.Attributes["href"];
                    string navTo = nav.Attributes["data-navigate-to"];
                    string url = string.IsNullOrEmpty(navTo) ? href : navTo;
                    if (!string.IsNullOrEmpty(url))
                    {
                        url = url.Contains('?') ? url.Split('?')[0] : url;
                        string layout = nav.Attributes["data-layout"];
                        layout = string.IsNullOrEmpty(layout) ? "basic" : layout;
                        if (pageNames.Contains(url))
                        {
                            book.Pages.Add(new WebBookPage { Name = url, Layout = layout });
                        }
                    }
                });
                WriteBook(appFs, book);
                FireEvent(WroteBook, new WebBookEventArgs(appConfig));
            });
            FireEvent(AppInitialized, new WebBookEventArgs(appConfig));
        }

        protected void WritePageList(Fs appFs, string[] pageNames)
        {
            appFs.WriteFile(PagesPath, pageNames.ToJson(true));
        }

        protected void WriteBook(Fs appFs, WebBook webBook)
        {
            appFs.WriteFile(BooksPath._Format(webBook.Name), webBook.ToJson(true), true);
        }
    }
}
