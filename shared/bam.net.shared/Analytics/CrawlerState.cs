/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public class CrawlerState
    {
        public enum Action
        {
            Idle,
            Extracting,
            Processing
        }

        public CrawlerState(BaseCrawler crawler)
        {
            this.Target = crawler.Current;
            this.Queued = crawler.QueuedTargets;
            this.CurrentAction = crawler.CurrentAction;
        }

        public string Target
        {
            get;
            protected internal set;
        }

        public string[] Queued
        {
            get;
            protected internal set;
        }

        public Action CurrentAction
        {
            get;
            protected internal set;
        }
    }
}
