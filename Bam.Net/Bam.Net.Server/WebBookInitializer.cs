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

namespace Bam.Net.Server
{
    public class WebBookInitializer : Loggable, IInitialize<WebBookInitializer>
    {
        public WebBookInitializer(BamServer server)
        {
            this.Server = server;
        }

        public BamServer Server { get; private set; }
        public event Action<WebBookInitializer> Initializing;
        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }
        public event Action<WebBookInitializer> Initialized;
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }

      [Verbosity(LogEventType.Information, MessageFormat="WebBookInitializer:: {AppName} initializ(ING)")]
        public event EventHandler AppInitializing;

      [Verbosity(LogEventType.Information, MessageFormat="WebBookInitializer:: {AppName} initializ(ED)")]
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

        public void Initialize()
        {
            OnInitializing();
            Server.AppContentResponders.Keys.Each(appName =>
            {
                WriteBooks(Server.AppContentResponders[appName].AppConf);
            });
            OnInitialized();
        }

        public string CurrentPage { get; private set; }

        public string AppName { get; private set; }

        public void WriteBooks(AppConf appConfig)
        {
          AppName = appConfig.Name;
            FireEvent(AppInitializing, new WebBookEventArgs(appConfig));
            // get all the pages 
            BamApplicationManager manager = new BamApplicationManager(appConfig.BamConf);
            List<string> pageNames = new List<string>(manager.GetPageNames(appConfig.Name));
            // read all the pages
            pageNames.Each(pageName =>
            {
              FireEvent(WritingBook, new WebBookEventArgs(appConfig));
              CurrentPage = pageName;
                Fs appFs = appConfig.AppRoot;
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
                    string url = string.IsNullOrEmpty(navTo) ? href: navTo;
                    if(!string.IsNullOrEmpty(url))
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
                appFs.WriteFile("~/books/{0}.json"._Format(book.Name), book.ToJson(true), true);
                FireEvent(WroteBook, new WebBookEventArgs(appConfig));
            });
            FireEvent(AppInitialized, new WebBookEventArgs(appConfig));
        }
    }
}
