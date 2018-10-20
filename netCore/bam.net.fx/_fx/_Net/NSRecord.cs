/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Net;

namespace Bam.Net.Net.Dns
{
	/// <summary>
	/// A Name Server Resource Record (RR) (RFC1035 3.3.11)
	/// </summary>
	public class NSRecord : RecordBase
	{
		// the fields exposed outside the assembly
		private readonly string		_domainName;

		// expose this domain name address r/o to the world
		public string DomainName	{ get { return _domainName; }}
				
		/// <summary>
		/// Constructs a NS record by reading bytes from a return message
		/// </summary>
		/// <param name="pointer">A logical pointer to the bytes holding the record</param>
		internal NSRecord(Pointer pointer)
		{
			_domainName = pointer.ReadDomain();
		}

		public override string ToString()
		{
			return _domainName;
		}
	}
}