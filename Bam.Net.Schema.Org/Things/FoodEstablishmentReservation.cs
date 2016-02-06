/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation to dine at a food-related business.</summary>
	public class FoodEstablishmentReservation: Reservation
	{
		///<summary>The endTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to end. For actions that span a period of time, when the action was performed. e.g. John wrote a book from January to *December*.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public DateTime EndTime {get; set;}
		///<summary>Number of people the reservation should accommodate.</summary>
		public ThisOrThat<Number , QuantitativeValue> PartySize {get; set;}
		///<summary>The startTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to start. For actions that span a period of time, when the action was performed. e.g. John wrote a book from *January* to December.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public DateTime StartTime {get; set;}
	}
}
