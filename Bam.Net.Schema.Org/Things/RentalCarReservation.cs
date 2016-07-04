using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation for a rental car.Note: This type is for information about actual reservations, e.g. in confirmation emails or HTML pages with individual confirmations of reservations.</summary>
	public class RentalCarReservation: Reservation
	{
		///<summary>Where a rental car can be dropped off.</summary>
		public Place DropoffLocation {get; set;}
		///<summary>When a rental car can be dropped off.</summary>
		public DateTime DropoffTime {get; set;}
		///<summary>Where a taxi will pick up a passenger or a rental car can be picked up.</summary>
		public Place PickupLocation {get; set;}
		///<summary>When a taxi will pickup a passenger or a rental car can be picked up.</summary>
		public DateTime PickupTime {get; set;}
	}
}
