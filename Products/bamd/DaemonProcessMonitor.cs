using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Server;

namespace Bam.Net.Application
{
    public class DaemonProcessMonitor
    {
        public DaemonProcessMonitor(DaemonProcess process)
        {
            Process = process;
            FlushLineCount = 25;
            FlushTimeout = 5000;
            string outlog = Path.Combine(ServiceConfig.LogRoot, $"{Process.Name}_out.txt");
            string errLog = Path.Combine(ServiceConfig.LogRoot, $"{Process.Name}_err.txt");
            Log.AddEntry("Outlog for {0} is {1}", Process.Name, outlog);
            Log.AddEntry("ErrorLog for {0} is {1}", Process.Name, errLog);
            StandardOutLog = new FileInfo(outlog);
            ErrorOutLog = new FileInfo(errLog);
            StartTimedFlush();
        }

        public static DaemonProcessMonitor Start(DaemonProcess process)
        {
            Log.AddEntry("Monitoring {0}", process.Name);
            DaemonProcessMonitor result = new DaemonProcessMonitor(process);
            process.Start(result.TryRestart);
            return result;
        }

        public DaemonProcess Process { get; set; }
        
        public int MaxRetries { get; set; }
        public int FlushLineCount { get; set; }
        public int FlushTimeout { get; set; }

        public FileInfo StandardOutLog { get; }
        public FileInfo ErrorOutLog { get; }

        protected int RetryCount { get; set; }

        protected void TryRestart(object sender, EventArgs e)
        {
            if(RetryCount < MaxRetries)
            {
                Log.AddEntry("Restarting {0}", Process.Name);
                RetryCount++;
                Process.Start(TryRestart);
            }
        }

        public void Flush()
        {
            LogIfNull(Process, "Process");
            LogIfNull(Process.ProcessOutput, "Process.ProcessOutput");

            string std = Process.StandardOutSoFar;
            std.SafeWriteToFile(StandardOutLog.FullName, true);
            Log.AddEntry("Flushed: {0}", std);
            Process.StandardOutSoFar = string.Empty;

            string err = Process.StandardErrorSoFar;
            err.SafeWriteToFile(ErrorOutLog.FullName, true);
            Log.AddEntry("Flushed err: {0}", err);
            Process.StandardErrorSoFar = string.Empty;            
        }

        private void LogIfNull(object obj, string name)
        {
            if(obj == null)
            {
                Log.AddEntry("{0} is null", LogEventType.Warning, name);
            }
        }

        public void TryFlush()
        {
            Log.AddEntry("trying flush {0}", Process.Name);
            try
            {
                int standardLineCount = Process.StandardOutLineCount;
                int errorLineCount = Process.ErrorOutLineCount;
                if (standardLineCount > FlushLineCount ||
                    errorLineCount > FlushLineCount)
                {
                    Log.AddEntry("Flushing {0}", Process.Name);
                    Flush();
                    Log.AddEntry("Flushed {0}", Process.Name);
                }
                else
                {
                    Log.AddEntry("Not flushing yet: Std = {0}, Err = {1}", standardLineCount.ToString(), errorLineCount.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error flushing process output for ({0}): {1}", LogEventType.Warning, Process.ToString(), ex.Message);
            }
        }

        Timer _flushTimer;
        public void StartTimedFlush()
        {
            Log.AddEntry("Starting flush timer for {0}", Process.Name);
            _flushTimer = new Timer((state) =>
            {
                Log.AddEntry("timer triggered flush");
                TryFlush();
            }, this, 0, FlushTimeout);
        }
    }
}
