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

        public Worker(string name)
        {
            this.Name = name ?? System.Guid.NewGuid().ToString();
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

        /// <summary>
        /// Sets all the properties of the current
        /// worker from the properties of the current
        /// WorkState.  All writable string properties that
        /// match in name will be copied to the 
        /// current worker
        /// </summary>
        protected internal void ConfigureFromWorkstate()
        {
            WorkState state = State();
            if (state != null)
            {
                Type stateType = state.GetType();
                Type currentType = this.GetType();                
                PropertyInfo[] stateProperties = stateType.GetProperties().Where(pi => pi.PropertyType == typeof(string)).ToArray();
                stateProperties.Each(prop =>
                {
                    PropertyInfo currentProp = currentType.GetProperty(prop.Name);
                    if (currentProp.PropertyType == typeof(string) && currentProp.CanWrite)
                    {
                        currentProp.SetValue(this, prop.GetValue(state));
                    }
                });
            }
        }

        object _doLock = new object();
        public WorkState Do(Job job)
        {
            lock (_doLock)
            {
                Busy = true;
                Job = job;
                WorkState state = null;
                
                try
                {
                    state = Do();
                }
                catch (Exception ex)
                {
                    state = new WorkState(this, ex);
                }

                Busy = false;
                return state;
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
            WorkerConf conf = new WorkerConf(this);
            conf.StepNumber = StepNumber;
            properties.Each(prop =>
            {
                conf.AddProperty(prop.Name, (string)prop.GetValue(this));
            });
            conf.Save(path);
        }

        protected abstract WorkState Do();

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
