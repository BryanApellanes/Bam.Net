using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices.Data
{
    public class Machine: AuditRepoData
    {
        public Machine()
        {
            SetNics();
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

        List<Nic> _nics;
        public List<Nic> NetworkInterfaces
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

        public static Machine ClientOf(CoreRegistryRepository repo, string serverHost, int serverPort)
        {
            Machine result = repo.OneMachineWhere(c => c.Name == Current.Name && c.ServerHost == serverHost && c.Port == serverPort);
            if (result == null)
            {
                result = new Machine();
                result.Name = Current.Name;
                result.ServerHost = serverHost;
                result.Port = serverPort;
                result = repo.Save(result);
            }
            return result;
        }

        private void SetNics()
        {
            var context = new { Nics = new List<Nic>() };
            List<NetworkInterface> nicList = new List<NetworkInterface>();
            NetworkInterface.GetAllNetworkInterfaces().Each(context, (ctx, nic) =>
            {
                IPInterfaceProperties nicProperties = nic.GetIPProperties();
                foreach (IPAddressInformation unicast in nicProperties.UnicastAddresses)
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
    }
}
