using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Bam.Net.Services;

namespace Bam.Net.Automation
{
    public class EnqueingJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobConductorService.Default.EnqueueJob((string)context.Get("JobName"));
        }
    }
}
