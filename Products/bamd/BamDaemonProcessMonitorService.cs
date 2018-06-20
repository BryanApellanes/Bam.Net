using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    [Proxy("processMonitor")]
    public class BamDaemonProcessMonitorService : ProxyableService
    {
        public BamDaemonProcessMonitorService(ILogger logger)
        {
            _monitors = new Dictionary<string, BamDaemonProcessMonitor>();
            Logger = logger;

        }

        public override object Clone()
        {
            BamDaemonProcessMonitorService clone = new BamDaemonProcessMonitorService(Logger);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public void Start()
        {
            string configRoot = Path.Combine(ServiceConfig.ContentRoot, "conf");
            string fileName = $"{nameof(BamDaemonProcess).Pluralize()}.json";
            File = new FileInfo(Path.Combine(configRoot, fileName));
            if (!File.Exists)
            {
                Logger.AddEntry("{0} not found: {1}", fileName, File.FullName);
            }
            else
            {
                Processes = File.FullName.FromJsonFile<BamDaemonProcess[]>();
                Expect.IsNotNull(Processes, $"No processes defined in {fileName}");
                Logger.AddEntry("{0} processes in {1}", Processes.Length, fileName);
                foreach(BamDaemonProcess process in Processes)
                {
                    StartProcess(process);
                }
            }            
        }

        public void Stop()
        {
            foreach (string key in _monitors.Keys)
            {
                try
                {
                    Log.AddEntry("Stopping {0}", key);
                    _monitors[key].Process.Kill();
                    Log.AddEntry("Stopped {0}", key);
                }
                catch (Exception ex)
                {
                    Log.AddEntry("Exception stopping process {0}: {1}", ex, key, ex.Message);
                }
            }            
        }

        public FileInfo File { get; set; }

        public virtual List<BamDaemonProcessInfo> GetProcesses()
        {
            return MonitoredProcesses.Select(m => m.Process.CopyAs<BamDaemonProcessInfo>()).ToList();
        }

        public virtual CoreServiceResponse AddProcess(BamDaemonProcessInfo processInfo)
        {
            try
            {
                BamDaemonProcess process = processInfo.CopyAs<BamDaemonProcess>();
                List<BamDaemonProcess> current = new List<BamDaemonProcess>(Processes)
                {
                    process
                };
                Processes = current.ToArray();
                Processes.ToJsonFile(File);
                StartProcess(process);
                return new CoreServiceResponse { Success = true };
            }
            catch(Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual CoreServiceResponse KillProcess(string name)
        {
            try
            {
                if (_monitors.ContainsKey(name))
                {
                    _monitors[name].Process.Kill();
                    return new CoreServiceResponse { Success = true };
                }
                return new CoreServiceResponse { Success = false, Message = $"Process with the specified name was not found: {name}" };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public List<BamDaemonProcessMonitor> MonitoredProcesses
        {
            get
            {
                return _monitors.Values?.ToList() ?? new List<BamDaemonProcessMonitor>();
            }
        }


        public BamDaemonProcess[] Processes { get; set; }

        Dictionary<string, BamDaemonProcessMonitor> _monitors;
        private void StartProcess(BamDaemonProcess process)
        {
            try
            {
                string key = process.ToString();
                Logger.AddEntry("Starting {0}", key);
                process.Subscribe(Log.Default);
                _monitors.Add(key, BamDaemonProcessMonitor.Start(process));
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error starting process {0}: {1}", ex, process?.ToString(), ex.Message);
            }
        }
    }
}
