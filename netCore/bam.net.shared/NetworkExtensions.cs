using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static class NetworkExtensions
    {
        public static IPAddress[] GetUnicastAddresses(this object instance)
        {
            return GetUnicastAddresses(instance, out NetworkInterface[] ignore);
        }
        public static IPAddress[] GetUnicastAddresses(this object instance, out NetworkInterface[] nics)
        {
            var context = new { IpAddresses = new List<IPAddress>()};
            List<NetworkInterface> nicList = new List<NetworkInterface>();
            NetworkInterface.GetAllNetworkInterfaces().Each(context, (ctx, nic) =>
            {
                nicList.Add(nic);
                IPInterfaceProperties nicProperties = nic.GetIPProperties();
                foreach(IPAddressInformation unicast in nicProperties.UnicastAddresses)
                {
                    ctx.IpAddresses.Add(unicast.Address);
                }
            });
            nics = nicList.ToArray();
            return context.IpAddresses.ToArray();
        }
    }
}
