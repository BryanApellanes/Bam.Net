using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices.Data
{
    public class Machine: RepoData
    {
        public Machine()
        {
            SetIpAddresses();
            Name = Environment.MachineName;
            Secret = ServiceProxySystem.GenerateId();
        }
        public virtual List<Application> Applications { get; set; }
        public virtual List<Configuration> Configurations { get; set; }
        public string Name { get; set; }
        public string ServerHost { get; set; }
        public int Port { get; set; }
        public string Secret { get; set; }
        public virtual List<ProcessDescriptor> Processes { get; set; }

        List<IpAddress> _ipAddresses;
        public List<IpAddress> IpAddresses
        {
            get
            {
                if(_ipAddresses == null || _ipAddresses.Count == 0)
                {
                    SetIpAddresses();
                }
                return _ipAddresses;
            }
            set
            {
                _ipAddresses = value;
            }
        }
        public override string ToString()
        {
            return $"{Name}=>{ServerHost}:{Port}";
        }
        static Machine _current;
        static object _currentLock = new object();
        public static Machine Current
        {
            get
            {
                return _currentLock.DoubleCheckLock(ref _current, () => new Machine());
            }
        }

        public static Machine ClientOf(string serverHost, int port)
        {
            Machine entry = new Machine();
            entry.ServerHost = serverHost;
            entry.Port = port;
            return entry;
        }

        private void SetIpAddresses()
        {
            _ipAddresses = new List<IpAddress>();
            foreach (IPAddress addr in NetworkExtensions.GetUnicastAddresses(null)
                                .Where(ip => (ip.AddressFamily == AddressFamily.InterNetwork ||
                                        ip.AddressFamily == AddressFamily.InterNetworkV6) &&
                                        !IPAddress.IsLoopback(ip)))
            {
                _ipAddresses.Add(new IpAddress { AddressFamily = addr.AddressFamily.ToString(), Value = addr.ToString() });
            }
        }

    }
}
