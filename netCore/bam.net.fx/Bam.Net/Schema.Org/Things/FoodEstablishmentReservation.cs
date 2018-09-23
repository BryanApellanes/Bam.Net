using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A reservation to dine at a food-related business.Note: This type is for information about actual reservations, e.g. in confirmation emails or HTML pages with individual confirmations of reservations.</summary>
	public class FoodEstablishmentReservation: Reservation
	{
		///<summary>The endTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to end. For actions that span a period of time, when the action was performed. e.g. John wrote a book from January to December.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date EndTime {get; set;}
		///<summary>Number of people the reservation should accommodate.</summary>
		public OneOfThese<Integer,QuantitativeValue> PartySize {get; set;}
		///<summary>The startTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to start. For actions that span a period of time, when the action was performed. e.g. John wrote a book from January to December.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date StartTime {get; set;}
	}
}
