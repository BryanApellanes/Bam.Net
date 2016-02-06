/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A trip on a commercial bus line.</summary>
	public class BusTrip: Intangible
	{
		///<summary>The stop or station from which the bus arrives.</summary>
		public OneOfThese<BusStop , BusStation> ArrivalBusStop {get; set;}
		///<summary>The expected arrival time.</summary>
		public DateTime ArrivalTime {get; set;}
		///<summary>The name of the bus (e.g. Bolt Express).</summary>
		public Text BusName {get; set;}
		///<summary>The unique identifier for the bus.</summary>
		public Text BusNumber {get; set;}
		///<summary>The stop or station from which the bus departs.</summary>
		public OneOfThese<BusStop , BusStation> DepartureBusStop {get; set;}
		///<summary>The expected departure time.</summary>
		public DateTime DepartureTime {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Person , Organization> Provider {get; set;}
	}
}
