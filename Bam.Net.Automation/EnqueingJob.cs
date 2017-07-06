using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Bam.Net.Automation
{
    public class EnqueingJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Overseer.Default.EnqueueJob((string)context.Get("JobName"));
        }
    }
}
