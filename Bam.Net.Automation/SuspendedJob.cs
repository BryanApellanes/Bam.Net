/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class SuspendedJob
    {
        /// <summary>
        /// Instantiate a new SuspendedJob instance saving
        /// the specified job as an IpcMessage to the 
        /// specified IpcMessageRoot.  A new InstanceId
        /// will be assigned.
        /// </summary>
        /// <param name="messageRoot"></param>
        /// <param name="job"></param>
        public SuspendedJob(IpcMessageRoot messageRoot, Job job)
        {
            this.InstanceId = Guid.NewGuid().ToString();
            this.IpcMessageRoot = messageRoot;
            IpcMessage message = messageRoot.GetMessage<Job>(this.InstanceId);
            message.Write(job);
        }

        /// <summary>
        /// Instantiate a new Suspended job instance which can 
        /// be reinstantiated from the specified messageRoot 
        /// using the specified instanceId
        /// </summary>
        /// <param name="messageRoot"></param>
        /// <param name="instanceId"></param>
        public SuspendedJob(IpcMessageRoot messageRoot, string instanceId)
        {
            this.InstanceId = instanceId;
            this.IpcMessageRoot = messageRoot;
        }

        public static Job ReinstantiateSuspendedJob(IpcMessageRoot messageRoot, string instanceId)
        {
            IpcMessage message = messageRoot.GetMessage<Job>(instanceId);
            return message.Read<Job>();
        }

        public Job Reinstantiate()
        {
            return ReinstantiateSuspendedJob(IpcMessageRoot, InstanceId);
        }

        public string InstanceId
        {
            get;
            set;
        }

        public IpcMessageRoot IpcMessageRoot
        {
            get;
            set;
        }
    }
}
