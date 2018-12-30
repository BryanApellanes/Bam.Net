using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy;
using Newtonsoft.Json;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Machine: AuditRepoData
    {
        public Machine()
        {
            Name = Environment.MachineName;
            DnsName = Dns.GetHostName();
            SetNics();
        }

        public Machine(string dnsName)
        {
            Name = Environment.MachineName;
            DnsName = dnsName;
            SetNics();
        }

        public static object ConfigurationLock { get; set; } = new object();
        public virtual List<Application> Applications { get; set; }
        public virtual List<Configuration> Configurations { get; set; }

        List<HostAddress> _hostAddresses;
        public virtual List<HostAddress> HostAddresses
        {
            get
            {
                if(_hostAddresses == null || _hostAddresses.Count == 0)
                {
                    SetHostAddresses();
                }
                return _hostAddresses;
            }
            set
            {
                _hostAddresses = value;
            }
        }
        public string Name { get; set; }
        public string DnsName { get; set; }
        public virtual List<ProcessDescriptor> Processes { get; set; }

        List<Nic> _nics;
        public virtual List<Nic> NetworkInterfaces
        {
            get
            {
                if(_nics == null || _nics.Count == 0)
                {
                    SetNics();
                }
                return _nics;
            }
            set
            {
                _nics = value;
            }
        }

        public virtual List<Client> Clients { get; set; }

        public override string ToString()
        {
            return $"{Name}@{DnsName}";
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

        public override RepoData Save(IRepository repo)
        {
            Machine existing = repo.Query<Machine>(new { Name = Name }).FirstOrDefault();
            if(existing == null)
            {
                existing = repo.Save(this);
            }                        
            return existing;
        }

        public string ToJson()
        {
            return Extensions.ToJson(this, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private void SetNics()
        {
            var context = new { Nics = new List<Nic>() };
            List<NetworkInterface> nicList = new List<NetworkInterface>();
            NetworkInterface.GetAllNetworkInterfaces().Each(context, (ctx, nic) =>
            {
                IPInterfaceProperties nicProperties = nic.GetIPProperties();
                foreach (IPAddressInformation unicast in nicProperties.UnicastAddresses.Where(a => !a.Address.Equals(IPAddress.Loopback) && !a.Address.Equals(IPAddress.IPv6Loopback)))
                {
                    ctx.Nics.Add(
                        new Nic
                        {
                            AddressFamily = unicast.Address.AddressFamily.ToString(),
                            Address = unicast.Address.ToString(),
                            MacAddress = nic.GetPhysicalAddress().ToString()
                        });
                }
            });
            _nics = context.Nics;
        }

        IPAddress[] _hostIps;
        private void SetHostAddresses()
        {
            if(_hostIps == null)
            {
                _hostIps = Dns.GetHostAddresses(DnsName);
            }
            List<HostAddress> hostAddresses = new List<HostAddress>();
            _hostIps.Each(ip => hostAddresses.Add(new HostAddress { HostName = DnsName, AddressFamily = ip.AddressFamily.ToString(), IpAddress = ip.ToString(), Machine = this, MachineId = Id }));
            _hostAddresses = hostAddresses;
        }
    }
}
