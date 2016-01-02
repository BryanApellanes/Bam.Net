/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bam.Net.Analytics
{
    public class Timed
    {
        public static TimeSpan Execution(Action action)
        {
            DateTime now = DateTime.UtcNow;
            action();
            DateTime then = DateTime.UtcNow;
            return then.Subtract(now);
        }

        public static void AsyncExecution(Action action, Action<TimeSpan> onComplete, Action<Task> continueWith = null)
        {
            TimeSpan elapsed = new TimeSpan();
            Task actionTask = new Task(() =>
            {
                elapsed = Execution(action);
            });

            actionTask.ContinueWith((t) =>
            {
                if (continueWith != null)
                {
                    continueWith(t);
                }
                onComplete(elapsed);
            });

            actionTask.Start(TaskScheduler.Default);
        }
    }
}
