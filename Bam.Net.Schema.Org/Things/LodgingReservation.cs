/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation for lodging at a hotel, motel, inn, etc.</summary>
	public class LodgingReservation: Reservation
	{
		///<summary>The earliest someone may check into a lodging establishment.</summary>
		public DateTime CheckinTime {get; set;}
		///<summary>The latest someone may check out of a lodging establishment.</summary>
		public DateTime CheckoutTime {get; set;}
		///<summary>A full description of the lodging unit.</summary>
		public Text LodgingUnitDescription {get; set;}
		///<summary>Textual description of the unit type (including suite vs. room, size of bed, etc.).</summary>
		public OneOfThese<Text , QualitativeValue> LodgingUnitType {get; set;}
		///<summary>The number of adults staying in the unit.</summary>
		public OneOfThese<Number , QuantitativeValue> NumAdults {get; set;}
		///<summary>The number of children staying in the unit.</summary>
		public OneOfThese<Number , QuantitativeValue> NumChildren {get; set;}
	}
}
