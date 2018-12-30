/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Bam.Net.Net.Dns
{
	/// <summary>
	/// Summary description for Dns.
	/// </summary>
	public sealed class DnsClient
	{
		const int		_dnsPort = 53;
		const int		_udpRetryAttempts = 2;
		static int		_uniqueId;

		/// <summary>
		/// Private constructor - this static class should never be instantiated
		/// </summary>
		private DnsClient()
		{
			// no implementation
		}

        public static MXRecord[] LookupMXRecord(string domain)
        {
            return Lookup<MXRecord>(domain);
        }

        public static NSRecord[] LookupNSRecord(string domain)
        {
            return Lookup<NSRecord>(domain);
        }

        public static ANameRecord[] LookupANameRecord(string domain)
        {
            return Lookup<ANameRecord>(domain);
        }

        public static SoaRecord[] LookupSoaRecord(string domain)
        {
            return Lookup<SoaRecord>(domain);
        }

        public static IPAddress GetDnsServer()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                IPAddressCollection dnsServers = nic.GetIPProperties().DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    return dnsServers[0];
                }
            }

            return null;
        }

        public static IPAddress[] LookupIPAddress(string hostname)
        {
            ANameRecord[] anames = LookupANameRecord(hostname);
            List<IPAddress> ret = new List<IPAddress>();
            foreach (ANameRecord record in anames)
            {
                ret.Add(record.IPAddress);
            }

            return ret.ToArray();
        }

        public static T[] Lookup<T>(string domain) where T : RecordBase
        {
            IPAddress dnsServer = GetDnsServer();
            return Lookup<T>(domain, dnsServer);
        }

        public static T[] Lookup<T>(string domain, IPAddress dnsServer) where T : RecordBase
        {

            // check the inputs
            if (domain == null) throw new ArgumentNullException("domain");
            if (dnsServer == null) throw new ArgumentNullException("dnsServer");

            // create a request for this
            DnsRequest request = new DnsRequest();

            // add one question - the MX IN lookup for the supplied domain
            request.AddQuestion(new Question(domain, typeof(T), DnsClass.IN));

            // fire it off
            DnsResponse response = Lookup(request, dnsServer);

            // if we didn't get a response, then return null
            if (response == null) return null;

            // create a growable array of MX records
            ArrayList resourceRecords = new ArrayList();

            // add each of the answers to the array
            foreach (Answer answer in response.Answers)
            {
                if (answer.Record != null)
                {
                    // if the answer is an MX record
                    if (answer.Record.GetType() == typeof(T))
                    {
                        // add it to our array
                        resourceRecords.Add(answer.Record);
                    }
                }
            }

            // create array of MX records
            T[] records = new T[resourceRecords.Count];

            // copy from the array list
            resourceRecords.CopyTo(records);

            // sort into lowest preference order
            if (typeof(T).GetInterface("IComparable") != null)
                Array.Sort(records);

            // and return
            return records;
        }

        public static DnsResponse Lookup(DnsRequest request)
        {
            IPAddress dnsServer = GetDnsServer();
            if (dnsServer != null)
                return Lookup(request, dnsServer);
            else
                throw new DnsServerNotFoundException();

            throw new NoResponseException();            
        }
		/// <summary>
		/// The principal look up function, which sends a request message to the given
		/// DNS server and collects a response. This implementation re-sends the message
		/// via UDP up to two times in the event of no response/packet loss
		/// </summary>
		/// <param name="request">The logical request to send to the server</param>
		/// <param name="dnsServer">The IP address of the DNS server we are querying</param>
		/// <returns>The logical response from the DNS server or null if no response</returns>
		public static DnsResponse Lookup(DnsRequest request, IPAddress dnsServer)
		{
			// check the inputs
			if (request == null) throw new ArgumentNullException("request");
			if (dnsServer == null) throw new ArgumentNullException("dnsServer");

			// We will not catch exceptions here, rather just refer them to the caller

			// create an end point to communicate with
			IPEndPoint server = new IPEndPoint(dnsServer, _dnsPort);
		
			// get the message
			byte[] requestMessage = request.GetMessage();

			// send the request and get the response
			byte[] responseMessage = UdpTransfer(server, requestMessage);

			// and populate a response object from that and return it
			return new DnsResponse(responseMessage);
		}

		private static byte[] UdpTransfer(IPEndPoint server, byte[] requestMessage)
		{
			// UDP can fail - if it does try again keeping track of how many attempts we've made
			int attempts = 0;

			// try repeatedly in case of failure
			while (attempts <= _udpRetryAttempts)
			{
				// firstly, uniquely mark this request with an id
				unchecked
				{
					// substitute in an id unique to this lookup, the request has no idea about this
					requestMessage[0] = (byte)(_uniqueId >> 8);
					requestMessage[1] = (byte)_uniqueId;
				}

				// we'll be send and receiving a UDP packet
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			
				// we will wait at most 1 second for a dns reply
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);

				// send it off to the server
				socket.SendTo(requestMessage, requestMessage.Length, SocketFlags.None, server);
		
				// RFC1035 states that the maximum size of a UDP datagram is 512 octets (bytes)
				byte[] responseMessage = new byte[512];

				try
				{
					// wait for a response upto 1 second
					socket.Receive(responseMessage);

					// make sure the message returned is ours
					if (responseMessage[0] == requestMessage[0] && responseMessage[1] == requestMessage[1])
					{
						// its a valid response - return it, this is our successful exit point
						return responseMessage;
					}
				}
				catch (SocketException)
				{
					// failure - we better try again, but remember how many attempts
					attempts++;
				}
				finally
				{
					// increase the unique id
					_uniqueId++;

					// close the socket
					socket.Close();
				}
			}
		
			// the operation has failed, this is our unsuccessful exit point
			throw new NoResponseException();
		}
	}
}
