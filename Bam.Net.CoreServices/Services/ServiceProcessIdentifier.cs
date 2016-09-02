using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data;

namespace Bam.Net.CoreServices
{
    public class CoreServiceProcessIdentifier
    {
        public override string ToString()
        {
            return $"{MachineName}~{ProcessId}~{IpAddresses}~{FilePath}~::{CommandLine}";
        }

        public override bool Equals(object obj)
        {
            return obj.ToString().Equals(ToString()) && obj is CoreServiceProcessIdentifier;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public string MachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }
        public int ProcessId
        {
            get
            {
                return Process.GetCurrentProcess().Id;
            }
        }
        public string FilePath
        {
            get
            {
                return Assembly.GetEntryAssembly().GetFilePath();
            }
        }
        public string CommandLine
        {
            get
            {
                return Environment.CommandLine;
            }
        }
        public string IpAddresses
        {
            get
            {
                return string.Join("::", IpAddressesArray);
            }
        }
        public string[] IpAddressesArray
        {
            get
            {
                List<string> addresses = new List<string>();
                foreach(IPAddress addr in NetworkExtensions.GetUnicastAddresses(null))
                {
                    addresses.Add(addr.ToString());
                }
                return addresses.ToArray();
            }
        }

        public ServiceProcessIdentifierData ToRepoData()
        {
            return this.CopyAs<ServiceProcessIdentifierData>();
        }
    }
}
