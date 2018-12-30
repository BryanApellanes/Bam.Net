using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A reservation for lodging at a hotel, motel, inn, etc.Note: This type is for information about actual reservations, e.g. in confirmation emails or HTML pages with individual confirmations of reservations.</summary>
	public class LodgingReservation: Reservation
	{
		///<summary>The earliest someone may check into a lodging establishment.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CheckinTime {get; set;}
		///<summary>The latest someone may check out of a lodging establishment.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CheckoutTime {get; set;}
		///<summary>A full description of the lodging unit.</summary>
		public Text LodgingUnitDescription {get; set;}
		///<summary>Textual description of the unit type (including suite vs. room, size of bed, etc.).</summary>
		public OneOfThese<QualitativeValue,Text> LodgingUnitType {get; set;}
		///<summary>The number of adults staying in the unit.</summary>
		public OneOfThese<Integer,QuantitativeValue> NumAdults {get; set;}
		///<summary>The number of children staying in the unit.</summary>
		public OneOfThese<Integer,QuantitativeValue> NumChildren {get; set;}
	}
}
