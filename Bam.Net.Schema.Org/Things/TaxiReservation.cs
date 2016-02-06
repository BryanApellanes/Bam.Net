/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation for a taxi.</summary>
	public class TaxiReservation: Reservation
	{
		///<summary>Number of people the reservation should accommodate.</summary>
		public OneOfThese<Number , QuantitativeValue> PartySize {get; set;}
		///<summary>Where a taxi will pick up a passenger or a rental car can be picked up.</summary>
		public Place PickupLocation {get; set;}
		///<summary>When a taxi will pickup a passenger or a rental car can be picked up.</summary>
		public DateTime PickupTime {get; set;}
	}
}
