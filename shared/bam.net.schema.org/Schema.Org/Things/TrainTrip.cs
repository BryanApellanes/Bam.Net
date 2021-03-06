using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A trip on a commercial train line.</summary>
	public class TrainTrip: Intangible
	{
		///<summary>The platform where the train arrives.</summary>
		public Text ArrivalPlatform {get; set;}
		///<summary>The station where the train trip ends.</summary>
		public TrainStation ArrivalStation {get; set;}
		///<summary>The expected arrival time.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ArrivalTime {get; set;}
		///<summary>The platform from which the train departs.</summary>
		public Text DeparturePlatform {get; set;}
		///<summary>The station from which the train departs.</summary>
		public TrainStation DepartureStation {get; set;}
		///<summary>The expected departure time.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DepartureTime {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization,Person> Provider {get; set;}
		///<summary>The name of the train (e.g. The Orient Express).</summary>
		public Text TrainName {get; set;}
		///<summary>The unique identifier for the train.</summary>
		public Text TrainNumber {get; set;}
	}
}
