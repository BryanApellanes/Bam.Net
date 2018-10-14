/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CoreServices;

namespace Bam.Net.Automation
{
    /// <summary>
    /// A serializable Job configuration.
    /// </summary>
    public class JobConf
    {
        public JobConf()
        {
            _workerExtensions = new List<string>();
            _workerExtensions.AddRange(new string[] { ".xml", ".yaml", ".json" });

            string name = System.Guid.NewGuid().ToString();
            JobDirectory = new DirectoryInfo(name).FullName;
            Name = name;
            CurrentIndex = 0;
        }

        public JobConf(string name)
            : this()
        {
            Name = name;
            JobDirectory = new DirectoryInfo(name).FullName;
        }
        
        public string Name { get; set; }

        /// <summary>
        /// Represents the "Order" that will be assigned to the next
        /// worker added
        /// </summary>
        protected int CurrentIndex { get; set; }

        List<string> _workerExtensions;
        /// <summary>
        /// An array of worker config file paths to
        /// load
        /// </summary>
        public string[] WorkerFiles
        {
            get
            {
                lock (_addLock)
                {
                    List<string> results = new List<string>();
                    _jobDirectory.Refresh();

                    if (_jobDirectory.Exists)
                    {
                        FileInfo[] files = _jobDirectory.GetFiles();

                        files.Each(file =>
                        {
                            string ext = Path.GetExtension(file.FullName).ToLowerInvariant();
                            if (_workerExtensions.Contains(ext))
                            {
                                results.Add(file.FullName);
                            }
                        });
                    }

                    return results.ToArray();
                }
            }
        }

        DirectoryInfo _jobDirectory;
        /// <summary>
        /// The root of the Job
        /// </summary>
        public string JobDirectory
        {
            get
            {
                return _jobDirectory.FullName;
            }
            set
            {
                _jobDirectory = new DirectoryInfo(value);                
            }
        }

        public static JobConf Load(string path)
        {
            JobConf conf = path.FromJsonFile<JobConf>();
            conf.CurrentIndex = conf.WorkerFiles.Length;
            return conf;
        }

        public Job CreateJob()
        {
            return new Job(this);
        }

        object _addLock = new object();
        public void AddWorker(Type workerType, string name, bool overwrite = false)
        {
            Worker worker = (Worker)workerType.Construct();
            worker.Name = name;
            AddWorker(worker, overwrite);
        }

        public void AddWorker(Worker worker, bool overwrite = false)
        {
            lock (_addLock)
            {
                EnsureJobDirectory();

                string path = ValidateWorkerName(worker.Name, overwrite);
                worker.StepNumber = CurrentIndex;
                ++CurrentIndex;

                worker.SaveConf(path);
            }
        }

        public void SaveWorker(Worker worker)
        {
            lock (_addLock)
            {
                string path = GetWorkerPath(worker.Name);
                worker.SaveConf(path);
            }
        }

        protected bool WorkerExists(Worker worker)
        {
            return WorkerExists(worker.Name);
        }

        protected internal bool WorkerExists(string workerName)
        {
            return WorkerExists(workerName, out string ignore);
        }

        protected internal bool WorkerExists(string workerName, out string path)
        {
            bool result = false;
            path = string.Empty;
            string tmpPath = path;
            _workerExtensions.Each(ext =>
            {
                tmpPath = Path.Combine(_jobDirectory.FullName, "{0}{1}"._Format(workerName, ext));
                if (File.Exists(tmpPath))
                {
                    result = true;
                    return false; // stop each loop
                }
                else
                {
                    return true; // continue each loop
                }
            });

            path = tmpPath;
            return result;            
        }

        protected internal string GetWorkerPath(string workerName)
        {
            return Path.Combine(_jobDirectory.FullName, "{0}.json"._Format(workerName));
        }

        protected string ValidateWorkerName(string workerName, bool overwrite)
        {
            string path = GetWorkerPath(workerName);
            if (File.Exists(path) && !overwrite)
            {
                throw new InvalidOperationException("Worker with the specified name ({0}) already exists in this job configuration"._Format(workerName));
            }
            else if (File.Exists(path) && overwrite)
            {
                File.Delete(path);
            }
            return path;
        }
        
        /// <summary>
        /// Gets a worker of the specified generic type 
        /// loading it from a file if the worker file exists
        /// creating it otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workerName"></param>
        /// <returns></returns>
        protected internal T GetWorker<T>(string workerName) where T : Worker, new()
        {
            return (T)GetWorker(typeof(T), workerName);
        }
        protected internal object GetWorker(Type workerType, string workerName)
        {
            Args.ThrowIfNull(workerType, "workerType");

            object worker = workerType.Construct();
            if (!WorkerExists(workerName, out string path))
            {
                Worker w = (Worker)worker;
                w.Name = workerName;
                AddWorker(w);
            }
            else
            {
                WorkerConf conf = WorkerConf.Load(path);
                worker = conf.CreateWorker();
            }

            return worker;
        }
                
        protected internal void EnsureJobDirectory()
        {
            _jobDirectory.Refresh();
            if (!_jobDirectory.Exists)
            {
                _jobDirectory.Create();
            }
        }

        public string Save()
        {
            EnsureJobDirectory();

            string path = GetFilePath();
            this.ToJsonFile(path);
            return path;
        }

        /// <summary>
        /// Returns the save to path for the current
        /// JobConf.  In the form {JobDirectory}\\{Name}.job
        /// </summary>
        /// <returns></returns>
        protected internal string GetFilePath()
        {
            string path = Path.Combine(_jobDirectory.FullName, "{0}.job"._Format(Name));
            return path;
        }
    }
}
