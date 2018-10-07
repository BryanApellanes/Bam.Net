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
using Bam.Net.Services;
using Bam.Net.Automation;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Dynamic;

namespace Bam.Net.Automation
{
    /// <summary>
    /// The manager for all jobs.
    /// </summary>
    public partial class JobManagerService: AsyncProxyableService
    {
        public JobManagerService(IApplicationNameProvider appNameProvider,
            DefaultDataDirectoryProvider dataSettings,
            IWorkerTypeProvider workerTypeProvider,
            ITypeResolver typeResolver,
            IIpcMessageStore suspendedJobStore) : this(appNameProvider, dataSettings)
        {
            WorkerTypeProvider = workerTypeProvider;
            TypeResolver = typeResolver;
            SuspendedJobIpcMessageStore = suspendedJobStore;
        }

        IIpcMessageStore _messageStore;
        object _messageStoreLock = new object();
        protected internal IIpcMessageStore SuspendedJobIpcMessageStore
        {
            get
            {
                return _messageStoreLock.DoubleCheckLock(ref _messageStore, () => new LocalIpcMessageStore(System.IO.Path.Combine(JobsDirectory, "Suspended")));
            }
            set
            {
                _messageStore = value;
            }
        }
        
        [Local]
        public SuspendedJob SuspendJob(Job job)
        {
            SuspendedJob suspended = new SuspendedJob(SuspendedJobIpcMessageStore, job);
            return suspended;
        }
    }
}
