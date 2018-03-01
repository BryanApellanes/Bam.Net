/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;

namespace Bam.Net.Automation
{
    public abstract class Worker: Loggable, IWorker, IConfigurable
    {
        public Worker()
            : this(System.Guid.NewGuid().ToString())
        { }

        public Worker(string name, JobManagerService svc = null)
        {
            Name = name ?? System.Guid.NewGuid().ToString();
            JobConductorService = svc;
        }

        JobManagerService _jobConductorService;
        object _jobConductorLock = new object();
        public JobManagerService JobConductorService
        {
            get
            {
                return _jobConductorLock.DoubleCheckLock(ref _jobConductorService, () => new JobManagerService());
            }
            set
            {
                _jobConductorService = value;
            }
        }

        public Job Job { get; set; }
        public string Name { get; set; }
        public bool Busy { get; set; }

        /// <summary>
        /// Used by the job to sort this worker into its proper
        /// place in order relative to other workers
        /// </summary>
        public int StepNumber
        {
            get;
            set;
        }

        object _state;

        public WorkState State(WorkState state = null)
        {
            if (state != null)
            {
                _state = state;
            }

            return (WorkState)_state;
        }

        /// <summary>
        /// Gets or sets the current WorkState of this Worker
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public WorkState<T> State<T>(WorkState<T> state = null)
        {
            if (state != null)
            {
                _state = state;
            }

            return (WorkState<T>)_state;
        }

        object _doLock = new object();
        public WorkState Do(Job job)
        {
            lock (_doLock)
            {
                Busy = true;
                Job = job;
                WorkState nextWorkState = null;
                
                try
                {
                    nextWorkState = Do(job.CurrentWorkState);
                }
                catch (Exception ex)
                {
                    nextWorkState = new WorkState(this, ex);
                }

                Busy = false;
                return nextWorkState;
            }
        }

        public void Configure(WorkerConf conf)
        {
            Type workerType = this.GetType();
            conf.Properties.Each(kvp =>
            {
                PropertyInfo property = workerType.GetProperty(kvp.Key);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(this, kvp.Value);
                }
            });
        }

        public void SaveConf(string path)
        {
            Type workerType = this.GetType();
            PropertyInfo[] properties = workerType.GetProperties().Where(pi => pi.PropertyType == typeof(string) && !pi.Name.Equals("Name")).ToArray();
            WorkerConf conf = new WorkerConf(this)
            {
                StepNumber = StepNumber
            };
            properties.Each(prop =>
            {
                conf.AddProperty(prop.Name, (string)prop.GetValue(this));
            });
            conf.Save(path);
        }
        
        protected abstract WorkState Do(WorkState currentWorkState);

        public abstract string[] RequiredProperties { get;  }
       
        public virtual void Configure(IConfigurer configurer)
        {
            configurer.Configure(this);
            this.CheckRequiredProperties();
        }

        public virtual void Configure(object configuration)
        {
            this.CopyProperties(configuration);
            this.CheckRequiredProperties();
        }
    }
}
