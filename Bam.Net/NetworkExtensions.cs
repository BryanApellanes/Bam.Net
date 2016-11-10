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
            var context = new { IpAddresses = new List<IPAddress>()};
            NetworkInterface.GetAllNetworkInterfaces().Each(context, (ctx, nic) =>
            {
                IPInterfaceProperties nicProperties = nic.GetIPProperties();
                foreach(IPAddressInformation unicast in nicProperties.UnicastAddresses)
                {
                    ctx.IpAddresses.Add(unicast.Address);
                }
            });

            return context.IpAddresses.ToArray();
        }
    }
}
