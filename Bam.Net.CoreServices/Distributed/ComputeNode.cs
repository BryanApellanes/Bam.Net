/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Bam.Net.CoreServices.Distributed
{
    public class ComputeNode: IDistributedRepository, IHasInfo
    {
        [Info("The host name of the compute node.")]
        public string HostName { get; set; }

        public IPAddress IPAddress
        {
            get
            {
                IPAddress result = null;
                if (IPV4Address != null)
                {
                    result = IPV4Address;
                }

                if (result == null && IPV6Address != null)
                {
                    result = IPV6Address;
                }

                return result;
            }         
        }

        [Info("The ipv4 address of the compute node")]
        public IPAddress IPV4Address { get; set; }

        [Info("The ipv6 address of the compute node")]
        public IPAddress IPV6Address { get; set; }

        public static ComputeNode FromCurrentHost()
        {
            ComputeNode result = new ComputeNode();
            string hostName = Dns.GetHostName();
            result.HostName = hostName;

            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    result.IPV4Address = address;
                }
                else if (address.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    result.IPV6Address = address;
                }
            }

            return result;
        }

        public IDistributedRepository RepositoryProvider
        {
            get;
            set;
        }

		#region IDistributedRepository Members

		public void Create(CreateOperation value)
		{
			EnsureProvider();
			RepositoryProvider.Create(value);
		}

		public T Retrieve<T>(RetrieveOperation value)
		{
			EnsureProvider();
			return RepositoryProvider.Retrieve<T>(value);
		}

		public void Update(UpdateOperation value)
		{
			EnsureProvider();
			RepositoryProvider.Update(value);
		}

		public void Delete(DeleteOperation value)
		{
			EnsureProvider();
			RepositoryProvider.Delete(value);
		}

		public IEnumerable<T> Query<T>(QueryOperation query)
		{
			EnsureProvider();
			return RepositoryProvider.Query<T>(query);
		}

		public ReplicationResult RecieveReplica(Operation operation)
		{
			throw new NotImplementedException();
		}

		#endregion
        #region IHasInfo Members

        public object GetInfo()
        {
            return InfoAttribute.GetInfo(this);
        }

        public Dictionary<string, string> GetInfoDictionary()
        {
            return InfoAttribute.GetInfoDictionary(this);
        }

        #endregion



        public override string ToString()
        {
			return Identifier();
        }

		public string Identifier()
		{
			string host = this.HostName ?? "null";
			string ipv4 = this.IPV4Address == null ? "null" : this.IPV4Address.ToString();
			string ipv6 = this.IPV6Address == null ? "null" : this.IPV6Address.ToString();
			return "HostName:{0},IPV4:{1},IPV6:{2}"._Format(host, ipv4, ipv6);
		}

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ComputeNode))
            {
                return obj.GetHashCode().Equals(this.GetHashCode());
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
		
		private void EnsureProvider()
		{
			Expect.IsNotNull(RepositoryProvider, "The RepositoryProvider must be specified");
		}
	}
}
