/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bam.Net
{
    public class Timed
    {
        public static void TryAsyncExecution(Action action, Action<TimeSpan> onComplete, Action<Exception> exceptionHandler = null, Action<Task> continueWith = null)
        {
            try
            {
                AsyncExecution(action, onComplete, continueWith);
            }
            catch (Exception ex)
            {
                Action<Exception> handler = exceptionHandler ?? ((e) => { });
                handler(ex);
            }
        }

        public static TimeSpan ExecutionAttempt(Action action, Action<Exception> exceptionHandler = null)
        {
            return TryExecution(action, exceptionHandler);
        }

        public static TimeSpan TryExecution(Action action, Action<Exception> exceptionHandler = null)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                return Execution(action);
            }
            catch (Exception ex)
            {
                Action<Exception> handler = exceptionHandler ?? ((e) => { });
                handler(ex);
                return DateTime.UtcNow.Subtract(now);
            }
        }

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
                continueWith?.Invoke(t);
                onComplete(elapsed);
            });

            actionTask.Start(TaskScheduler.Default);
        }
    }
}
