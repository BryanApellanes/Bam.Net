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
    public class DaemonProcessMonitorService : ProxyableService
    {
        public DaemonProcessMonitorService(ILogger logger)
        {
            _monitors = new Dictionary<string, DaemonProcessMonitor>();
            Logger = logger;
        }

        public override object Clone()
        {
            DaemonProcessMonitorService clone = new DaemonProcessMonitorService(Logger);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public void Start()
        {
            string configRoot = Path.Combine(ServiceConfig.ContentRoot, "conf");
            string fileName = $"{nameof(DaemonProcess).Pluralize()}.json";
            ConfigFile = new FileInfo(Path.Combine(configRoot, fileName));
            if (!ConfigFile.Exists)
            {
                Logger.AddEntry("{0} not found: {1}", fileName, ConfigFile.FullName);
            }
            else
            {
                Processes = ConfigFile.FullName.FromJsonFile<DaemonProcess[]>() ?? new DaemonProcess[] { };
                Expect.IsNotNull(Processes, $"No processes defined in {fileName}");
                Logger.AddEntry("{0} processes in {1}", Processes.Length.ToString(), fileName);
                Parallel.ForEach(Processes, (process) =>
                {
                    StartProcess(process);
                });
            }            
        }

        public void Stop()
        {
            Parallel.ForEach(_monitors.Keys, (key) =>
            {
                try
                {
                    Logger.AddEntry("Stopping {0}", key);
                    _monitors[key].Process.Kill();
                    Logger.AddEntry("Stopped {0}", key);
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Exception stopping process {0}: {1}", ex, key, ex.Message);
                }
            });          
        }

        public FileInfo ConfigFile { get; set; }

        public virtual List<DaemonProcessInfo> GetProcesses()
        {
            return MonitoredProcesses.Select(m => m.Process.CopyAs<DaemonProcessInfo>()).ToList();
        }

        public virtual CoreServiceResponse AddProcess(DaemonProcessInfo processInfo)
        {
            try
            {
                DaemonProcess process = processInfo.CopyAs<DaemonProcess>();
                List<DaemonProcess> current = new List<DaemonProcess>(Processes)
                {
                    process
                };
                Processes = current.ToArray();
                Processes.ToJsonFile(ConfigFile);
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

        public List<DaemonProcessMonitor> MonitoredProcesses
        {
            get
            {
                return _monitors.Values?.ToList() ?? new List<DaemonProcessMonitor>();
            }
        }


        public DaemonProcess[] Processes { get; set; }

        Dictionary<string, DaemonProcessMonitor> _monitors;
        private void StartProcess(DaemonProcess process)
        {
            try
            {
                string key = process.ToString();
                Logger.AddEntry("Starting {0}", key);
                process.Subscribe(Logger);
                _monitors.Add(key, DaemonProcessMonitor.Start(process));
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error starting process {0}: {1}", ex, process?.ToString(), ex.Message);
            }
        }
    }
}
