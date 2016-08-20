using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A reservation for air travel.Note: This type is for information about actual reservations, e.g. in confirmation emails or HTML pages with individual confirmations of reservations. For offers of tickets, use Offer.</summary>
	public class FlightReservation: Reservation
	{
		///<summary>The airline-specific indicator of boarding order / preference.</summary>
		public Text BoardingGroup {get; set;}
		///<summary>The priority status assigned to a passenger for security or boarding (e.g. FastTrack or Priority).</summary>
		public OneOfThese<QualitativeValue,Text> PassengerPriorityStatus {get; set;}
		///<summary>The passenger's sequence number as assigned by the airline.</summary>
		public Text PassengerSequenceNumber {get; set;}
		///<summary>The type of security screening the passenger is subject to.</summary>
		public Text SecurityScreening {get; set;}
	}
}
