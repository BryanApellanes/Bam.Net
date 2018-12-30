/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public abstract class BaseCrawler : ICrawler
    {
        static Dictionary<string, ICrawler> _crawlers;
        static object _crawlerLock = new object();
        public static Dictionary<string, ICrawler> Instances
        {
            get
            {
                return _crawlerLock.DoubleCheckLock(ref _crawlers, () => new Dictionary<string, ICrawler>());
            }
        }

        #region ICrawler Members

        public virtual string Root
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public string ThreadName
        {
            get
            {
                return "Crawler_{0}"._Format(this.Name);
            }
        }
        /// <summary>
        /// When implemented by a derived class will extract
        /// more targets to be processed from the specified target.  
        /// (Think filepath or url)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract string[] ExtractTargets(string target);

        public abstract void ProcessTarget(string target);

        public virtual bool WasProcessed(string target = "")
        {
            if (_processed != null && !string.IsNullOrEmpty(target))
            {
                return _processed.Contains(target);
            }

            return false;
        }

        ConcurrentQueue<string> _targets;
        object _targetLock = new object();
        public ConcurrentQueue<string> TargetQueue
        {
            get
            {
                return _targetLock.DoubleCheckLock(ref _targets, () => new ConcurrentQueue<string>());
            }
        }

        public string[] QueuedTargets
        {
            get
            {
                return _targets.ToArray();
            }
        }

        int _maxQueue;
        public int MaxQueueSize
        {
            get
            {
                return _maxQueue;
            }
            set
            {
                _maxQueue = value;
            }
        }

        HashSet<string> _processed;
        public string[] Processed
        {
            get
            {
                return _processed.ToArray();
            }
        }

        public string Current
        {
            get;
            private set;
        }

        CrawlerState.Action _currentAction;
        protected internal CrawlerState.Action CurrentAction
        {
            get
            {
                return _currentAction;
            }
            set
            {
                CrawlerState.Action old = _currentAction;
                _currentAction = value;
                OnActionChanged(old, _currentAction);
            }
        }

        public event ActionChangedDelegate ActionChanged;

        protected void OnActionChanged(CrawlerState.Action oldAction, CrawlerState.Action newAction)
        {
            ActionChanged?.Invoke(this, new ActionChangedEventArgs(oldAction, newAction));
        }

        public CrawlerState GetState()
        {
            return new CrawlerState(this);
        }

        public void Crawl()
        {
            if (string.IsNullOrEmpty(Root))
            {
                throw new InvalidOperationException("Root not set");
            }

            Crawl(Root);
        }

        public void Crawl(string rootTarget)
        {
            Exec.Start(ThreadName, () =>
            {
                Root = rootTarget;

                _processed = new HashSet<string>();

                TargetQueue.Enqueue(Root);
                while (TargetQueue.TryDequeue(out string current))
                {
                    Current = current;
                    CurrentAction = CrawlerState.Action.Extracting;
                    ExtractTargetsFromCurrentAndEnqueue();

                    CurrentAction = CrawlerState.Action.Processing;
                    ProcessTarget(Current);
                    _processed.Add(IsCaseSensitive ? Current : Current.ToLowerInvariant());
                }
                CurrentAction = CrawlerState.Action.Idle;
            });
        }

        public bool IsCaseSensitive { get; set; }

        protected void ExtractTargetsFromCurrentAndEnqueue()
        {
            string[] more = ExtractTargets(Current);
            for (int i = 0; i < more.Length; i++)
            {
                string extracted = more[i];
                if (string.IsNullOrEmpty(extracted))
                {
                    continue;
                }
                string compareTo = IsCaseSensitive ? extracted.ToLowerInvariant() : extracted;
                if (!WasProcessed(compareTo))
                {
                    if (MaxQueueSize == 0 || (MaxQueueSize > 0 && TargetQueue.Count < MaxQueueSize))
                    {
                        TargetQueue.Enqueue(extracted);
                    }
                }
            }
        }

        #endregion
    }
}
