using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Services.AsyncCallback.Data.Dao.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Bam.Net.Services.Hosts
{
    [Serializable]
    public class HostsService : AsyncProxyableService
    {
        public HostsService(AsyncCallbackService callbackService, DaoRepository repository, AppConf appConf) : base(callbackService, repository, appConf) { }
        public override object Clone()
        {
            HostsService clone = new HostsService(CallbackService, DaoRepository, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public HostEntry[] GetHosts()
        {
            return DaoRepository.RetrieveAll<HostEntry>().ToArray();
        }

        public HostEntry AddHostEntry(string hostName, string ipaddress, string mac)
        {
            return DaoRepository.Save<HostEntry>(new HostEntry { HostName = hostName, IpAddress = ipaddress, MacAddress = mac });
        }

        [Local]
        public HostEntry AddHostEntry()
        {
            Machine current = Machine.Current;
            List<HostAddress> hostAddresses = current.HostAddresses;
            HostEntry entry = new HostEntry
            {
                HostName = current.DnsName,
                IpAddress = hostAddresses.FirstOrDefault(ha => ha.AddressFamily.Equals(AddressFamily.InterNetwork.ToString()))?.ToString(),
                IpAddressV6 = hostAddresses.FirstOrDefault(ha => ha.AddressFamily.Equals(AddressFamily.InterNetworkV6.ToString()))?.ToString()
            };
            return DaoRepository.Save(entry);
        }

        [Local]
        public Task<HostEntry> AddHostEntryAsync()
        {
            return InvokeAsync<HostEntry>(nameof(AddHostEntry));
        }
    }
}
