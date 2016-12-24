/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Profiguration;
using Bam.Net.ServiceProxy;
using Bam.Net.Data;
using Bam.Net.Analytics;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.Threading;

namespace Bam.Net.Automation
{
    /// <summary>
    /// The master orchestrator for 
    /// all jobs.
    /// </summary>
    [Proxy]
    public class Orchestrator: Loggable
    {
        const string ProfigurationSetKey = "OrchestratorSettings";

        AutoResetEvent _enqueueSignal;
        AutoResetEvent _runCompleteSignal;
        Thread _runnerThread;
        public Orchestrator()
        {
            this.MaxConcurrentJobs = 3;
            this._enqueueSignal = new AutoResetEvent(false);
            this._runCompleteSignal = new AutoResetEvent(false);
        }

        static Orchestrator _default;
        static object _defaultLock = new object();
        public static Orchestrator Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new Orchestrator());
            }
        }

        public int MaxConcurrentJobs
        {
            get;
            set;
        }

        DirectoryInfo _jobsDirectory;
        public string JobsDirectory
        {
            get
            {
                if (_jobsDirectory == null)
                {
                    _jobsDirectory = new DirectoryInfo("{0}\\Jobs"._Format(this.GetAppDataFolder()));
                }

                return _jobsDirectory.FullName;
            }
            set
            {
                _jobsDirectory = new DirectoryInfo(value);
                _messageRoot = null; // forces reinit;
            }
        }

        List<Job> _running;
        object _runningLock = new object();
        public List<Job> Running
        {
            get
            {
                return _runningLock.DoubleCheckLock(ref _running, () => new List<Job>());
            }
        }

        IpcMessageRoot _messageRoot;
        object _messageRootLock = new object();
        protected internal IpcMessageRoot IpcMessageRoot
        {
            get
            {
                return _messageRootLock.DoubleCheckLock(ref _messageRoot, () => new IpcMessageRoot(System.IO.Path.Combine(JobsDirectory, "Suspended")));
            }
        }

        ProfigurationSet _profigurationSet;
        object _profigurationSetLock = new object();
        protected internal ProfigurationSet ProfigurationSet
        {
            get
            {
				return _profigurationSetLock.DoubleCheckLock(ref _profigurationSet, () => new ProfigurationSet(System.IO.Path.Combine(JobsDirectory, "ProfigurationSet")));
            }
        }

        /// <summary>
        /// Add a worker of the specified type assigning the
        /// specified workerName to the job with the specified
        /// jobName.
        /// </summary>
        /// <param name="workerTypeName"></param>
        /// <param name="workerName"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public void AddWorker(string workerTypeName, string workerName, string jobName)
        {
            Type type = Type.GetType(workerTypeName);
            if (type == null)
            {
                throw new ArgumentNullException("workerTypeName", "Unable to find the specified WorkerType");
            }

            JobConf jobConf = GetJob(jobName);
            AddWorker(type, workerName, jobConf);
        }

        /// <summary>
        /// Adds a worker of generic type T to the
        /// specified JobConf.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conf"></param>
        /// <returns></returns>
        public void AddWorker<T>(string name, JobConf conf)
        {
            AddWorker(typeof(T), name, conf);
        }

        public void AddWorker(Type type, string name, JobConf conf)
        {
            conf.AddWorker(type, name);
        }
        
        public SuspendedJob SuspendJob(Job job)
        {
            SuspendedJob suspended = new SuspendedJob(IpcMessageRoot, job);
            return suspended;
        }

        [Exclude]
        public string SecureGet(string key)
        {
            Profiguration.Profiguration prof = ProfigurationSet[ProfigurationSetKey];
            return prof.AppSettings[key];
        }

        [Exclude]
        public string SecureSet(string key, string value)
        {
            Profiguration.Profiguration prof = ProfigurationSet[ProfigurationSetKey];
            prof.AppSettings[key] = value;
            ProfigurationSet.Save();

            return value;
        }

        public JobConf CreateJob(string name)
        {
            return CreateJobConf(name);
        }

        [Verbosity(LogEventType.Information, MessageFormat = "Worker of type {WorkTypeName} and Step Number {StepNumber} of Job {JobName} started")]
        public event EventHandler WorkStarting;

        protected void OnWorkerStarting(WorkState state)
        {
            if (WorkStarting != null)
            {
                WorkStarting(this, new WorkStateEventArgs(state));
            }
        }

        [Verbosity(LogEventType.Information, MessageFormat = "Worker of type {WorkTypeName} and Step Number {StepNumber} of Job {JobName} finished")]
        public event EventHandler WorkerFinished;

        protected void OnWorkerFinished(WorkState state)
        {
            if (WorkerFinished != null)
            {
                WorkerFinished(this, new WorkStateEventArgs(state));
            }
        }

        [Verbosity(LogEventType.Error, MessageFormat = "EXCEPTION:{LastMessage}:Worker of type {WorkTypeName} and Step Number {StepNumber} of Job {JobName}")]
        public event EventHandler WorkerException;

        protected void OnWorkerException(WorkState state)
        {
            if (WorkerException != null)
            {
                WorkerException(this, new WorkStateEventArgs(state));
            }
        }

        [Verbosity(LogEventType.Information, MessageFormat = "JobName={Name}")]
        public event EventHandler JobFinished;

        protected void OnJobFinished(WorkState state)
        {
            if (JobFinished != null)
            {
                JobFinished(this, new WorkStateEventArgs(state));
            }
        }

        public JobConf GetJob(string name)
        {
            return GetJobConf(name);
        }

        protected internal JobConf GetJobConf(string name)
        {
            JobConf conf = new JobConf(name);
            conf.JobDirectory = GetJobDirectoryPath(name);
            if (JobExists(name))
            {
                conf = JobConf.Load(conf.GetFilePath());
            }
            else
            {
                conf = CreateJobConf(name);
            }

            return conf;
        }

        protected internal JobConf CreateJobConf(string name, bool overwrite = false)
        {
            if (JobExists(name))
            {
                throw new InvalidOperationException("The specified job {0} already exists, use GetJob to get the existing job"._Format(name));
            }

            JobConf conf = new JobConf(name);
            conf.JobDirectory = GetJobDirectoryPath(name);
            conf.Save();
            return conf;
        }

        public bool WorkerExists(string jobName, string workerName)
        {
            bool result = false;
            if (JobExists(jobName))
            {
                JobConf conf = GetJob(jobName);
                result = conf.WorkerExists(workerName);
            }

            return result;
        }

        /// <summary>
        /// Returns true is a job with the specified name
        /// exists under the current Foreman.  Determined
        /// by looking in the current Foreman's JobsDirectory.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool JobExists(string name)
        {
            string ignore;
            return JobExists(name, out ignore);
        }
        protected internal bool JobExists(string name, out string jobDirectoryPath)
        {
			jobDirectoryPath = System.IO.Path.Combine(JobsDirectory, name);
            return Directory.Exists(jobDirectoryPath);
        }

        public void EnqueueJob(string name, int stepNumber = 0)
        {
            JobConf conf = GetJobConf(name);

            EnqueueJob(conf, stepNumber);
        }

        protected internal void EnqueueJob(JobConf conf, int stepNumber = 0)
        {
            Args.ThrowIfNull(conf, "JobConf");

            lock (_jobQueueLock)
            {
                if (!_isRunning)
                {
                    StartJobRunnerThread();
                }

                Job job = conf.CreateJob();
                job.StepNumber = stepNumber;
                JobQueue.Enqueue(job);
                _enqueueSignal.Set();
            }
        }

        Queue<Job> _jobQueue;
        object _jobQueueLock = new object();
        protected internal Queue<Job> JobQueue
        {
            get
            {
                return _jobQueueLock.DoubleCheckLock(ref _jobQueue, () => new Queue<Job>());
            }
        }

        bool _isRunning;
        /// <summary>
        /// Starts the JobRunner thread.  This method
        /// must be called prior to queueing up jobs
        /// or the jobs will not be run.
        /// </summary>
        public void StartJobRunnerThread()
        {
            _runnerThread = new Thread(JobRunner);
            _runnerThread.IsBackground = true;
            _runnerThread.Start();
            _isRunning = true;
        }

        public void StopJobRunnerThread()
        {
            try
            {
                if(_runnerThread != null && _runnerThread.ThreadState == ThreadState.Running)
                {
                    _runnerThread.Abort();
                    _runnerThread.Join(2500);
                }                
            }
            catch { }

            _isRunning = false;
        }

        private void JobRunner()
        {
            while (true)
            {
                _enqueueSignal.WaitOne();

                while (JobQueue.Count > 0)
                {
                    Job job = null;
                    lock (_jobQueueLock)
                    {
                        if (JobQueue.Count > 0)
                        {
                            job = JobQueue.Dequeue();
                        }
                    }

                    if (job != null)
                    {
                        job.JobFinished += (o, a) =>
                        {
                            Job j = (Job)o;
                            if (Running.Contains(j))
                            {
                                Running.Remove(j);
                            }

                            _runCompleteSignal.Set();
                        };

                        RunJob(job);
                    }
                }
            }
        }

        protected internal void RunJob(Job job, int stepNumber = 0)
        {
            lock (_runningLock)
            {
                if (Running.Count >= MaxConcurrentJobs)
                {
                    _runCompleteSignal.WaitOne();
                }

                job.WorkerException += (o, a) =>
                {
                    OnWorkerException(job.CurrentWorkState);
                };
                job.WorkerStarting += (o, a) =>
                {
                    OnWorkerStarting(job.CurrentWorkState);
                };
                job.WorkerFinished += (o, a) =>
                {
                    OnWorkerFinished(job.CurrentWorkState);
                };
                job.JobFinished += (o, a) =>
                {
                    OnJobFinished(job.CurrentWorkState);
                };
                Running.Add(job);
            }

            job.StepNumber = stepNumber;
            job.Run();
        }

        protected string GetJobDirectoryPath(string name)
        {
			return System.IO.Path.Combine(JobsDirectory, name);
        }
    }
}
