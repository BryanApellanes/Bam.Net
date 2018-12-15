using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A trip on a commercial bus line.</summary>
	public class BusTrip: Intangible
	{
		///<summary>The stop or station from which the bus arrives.</summary>
		public OneOfThese<BusStation,BusStop> ArrivalBusStop {get; set;}
		///<summary>The expected arrival time.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ArrivalTime {get; set;}
		///<summary>The name of the bus (e.g. Bolt Express).</summary>
		public Text BusName {get; set;}
		///<summary>The unique identifier for the bus.</summary>
		public Text BusNumber {get; set;}
		///<summary>The stop or station from which the bus departs.</summary>
		public OneOfThese<BusStation,BusStop> DepartureBusStop {get; set;}
		///<summary>The expected departure time.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DepartureTime {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization,Person> Provider {get; set;}
	}
}
