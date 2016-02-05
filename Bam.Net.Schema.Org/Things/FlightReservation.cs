using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation for air travel.</summary>
	public class FlightReservation: Reservation
	{
		///<summary>The airline-specific indicator of boarding order / preference.</summary>
		public Text BoardingGroup {get; set;}
		///<summary>The priority status assigned to a passenger for security or boarding (e.g. FastTrack or Priority).</summary>
		public ThisOrThat<QualitativeValue , Text> PassengerPriorityStatus {get; set;}
		///<summary>The passenger's sequence number as assigned by the airline.</summary>
		public Text PassengerSequenceNumber {get; set;}
		///<summary>The type of security screening the passenger is subject to.</summary>
		public Text SecurityScreening {get; set;}
	}
}
