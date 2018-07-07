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
        public DaemonProcessMonitor(DaemonProcess process, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            MaxLogSizeInBytes = 1048576;
            Process = process;
            FlushLineCount = 25;
            FlushTimeout = 5000;
            OutLogName = Path.Combine(ServiceConfig.LogRoot, $"{Process.Name}_out.txt");
            ErrorLogName = Path.Combine(ServiceConfig.LogRoot, $"{Process.Name}_err.txt");
            Logger.AddEntry("Outlog for {0} is {1}", Process.Name, OutLogName);
            Logger.AddEntry("ErrorLog for {0} is {1}", Process.Name, ErrorLogName);
            StandardOutLog = new FileInfo(OutLogName);
            ErrorOutLog = new FileInfo(ErrorLogName);

            StartTimedFlush();
        }

        public static DaemonProcessMonitor Start(DaemonProcess process, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            logger.AddEntry("Monitoring {0}", process.Name);
            DaemonProcessMonitor result = new DaemonProcessMonitor(process, logger);
            process.StandardOut += (o, a) => logger.Info(((DaemonProcessEventArgs)a).Message);
            process.ErrorOut += (o, a) => logger.Warning(((DaemonProcessEventArgs)a).Message);
            process.Start(result.TryRestart);
            return result;
        }

        public DaemonProcess Process { get; set; }
        
        public ILogger Logger { get; set; }

        public int MaxLogSizeInBytes { get; set; }
        public int MaxRetries { get; set; }
        public int FlushLineCount { get; set; }
        public int FlushTimeout { get; set; }

        protected string OutLogName { get; set; }
        protected string ErrorLogName { get; set; }
        public FileInfo StandardOutLog { get; private set; }
        public FileInfo ErrorOutLog { get; private set; }

        protected int RetryCount { get; set; }

        protected void TryRestart(object sender, EventArgs e)
        {
            if(RetryCount < MaxRetries)
            {
                Logger.AddEntry("Restarting {0}", Process.Name);
                RetryCount++;
                Process.Start(TryRestart);
            }
        }

        public void Flush()
        {
            try
            {
                LogIfNull(Process, "Process");
                LogIfNull(Process.ProcessOutput, "Process.ProcessOutput");

                CreateLogFiles();

                string std = Process.StandardOutSoFar;
                SetNextLogFiles();
                std.SafeWriteToFile(StandardOutLog.FullName, true);
                Logger.AddEntry("Flushed: {0}", std);
                Process.StandardOutSoFar = string.Empty;

                string err = Process.StandardErrorSoFar;
                err.SafeWriteToFile(ErrorOutLog.FullName, true);
                Logger.AddEntry("Flushed err: {0}", err);
                Process.StandardErrorSoFar = string.Empty;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception during flush: {0}", ex, ex.Message);
            }
        }

        private void CreateLogFiles()
        {
            if (!StandardOutLog.Directory.Exists)
            {
                StandardOutLog.Directory.Create();
                "init".SafeWriteToFile(StandardOutLog.FullName, true);
            }

            if (!ErrorOutLog.Directory.Exists)
            {
                ErrorOutLog.Directory.Create();
                "init".SafeWriteToFile(ErrorOutLog.FullName, true);
            }
        }

        private void SetNextLogFiles()
        {
            if (StandardOutLog.Length >= MaxLogSizeInBytes)
            {
                StandardOutLog = new FileInfo(OutLogName.GetNextFileName());
            }
            if (ErrorOutLog.Length >= MaxLogSizeInBytes)
            {
                ErrorOutLog = new FileInfo(ErrorLogName.GetNextFileName());
            }
        }

        private void LogIfNull(object obj, string name)
        {
            if(obj == null)
            {
                Logger.AddEntry("{0} is null", LogEventType.Warning, name);
            }
        }

        public void TryFlush()
        {
            Logger.AddEntry("trying flush {0}", Process.Name);
            try
            {
                int standardLineCount = Process.StandardOutLineCount;
                int errorLineCount = Process.ErrorOutLineCount;
                if (standardLineCount > FlushLineCount ||
                    errorLineCount > FlushLineCount)
                {
                    Logger.AddEntry("Flushing {0}", Process.Name);
                    Flush();
                    Logger.AddEntry("Flushed {0}", Process.Name);
                }
                else
                {
                    Logger.AddEntry("Not flushing yet: Std = {0}, Err = {1}", standardLineCount.ToString(), errorLineCount.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error flushing process output for ({0}): {1}", LogEventType.Warning, Process.ToString(), ex.Message);
            }
        }

        Timer _flushTimer;
        public void StartTimedFlush()
        {
            Logger.AddEntry("Starting flush timer for {0}", Process.Name);
            _flushTimer = new Timer((state) =>
            {
                Logger.AddEntry("timer triggered flush");
                TryFlush();
            }, this, 0, FlushTimeout);
        }
    }
}
