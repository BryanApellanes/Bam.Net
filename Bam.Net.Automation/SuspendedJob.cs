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
        /// <param name="messageStore"></param>
        /// <param name="job"></param>
        public SuspendedJob(IIpcMessageStore messageStore, Job job)
        {
            InstanceId = Guid.NewGuid().ToString();
            IpcMessageStore = messageStore;
            messageStore.SetMessage(InstanceId, job);
        }

        /// <summary>
        /// Instantiate a new Suspended job instance which can 
        /// be reinstantiated from the specified messageRoot 
        /// using the specified instanceId.
        /// </summary>
        /// <param name="messageRoot"></param>
        /// <param name="instanceId"></param>
        public SuspendedJob(IIpcMessageStore messageRoot, string instanceId)
        {
            InstanceId = instanceId;
            IpcMessageStore = messageRoot;
        }

        public static Job ReinstantiateSuspendedJob(IIpcMessageStore messageStore, string instanceId)
        {
            IpcMessage message = messageStore.GetMessage<Job>(instanceId);
            return message.Read<Job>();
        }

        public Job Reinstantiate()
        {
            return ReinstantiateSuspendedJob(IpcMessageStore, InstanceId);
        }

        public string InstanceId
        {
            get;
            set;
        }

        public IIpcMessageStore IpcMessageStore
        {
            get;
            set;
        }
    }
}
