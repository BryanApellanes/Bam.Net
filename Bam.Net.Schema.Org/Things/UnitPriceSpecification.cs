/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The price asked for a given offer by the respective organization or person.</summary>
	public class UnitPriceSpecification: PriceSpecification
	{
		///<summary>This property specifies the minimal quantity and rounding increment that will be the basis for the billing. The unit of measurement is specified by the unitCode property.</summary>
		public Number BillingIncrement {get; set;}
		///<summary>A short text or acronym indicating multiple price specifications for the same offer, e.g. SRP for the suggested retail price or INVOICE for the invoice price, mostly used in the car industry.</summary>
		public Text PriceType {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters).</summary>
		public Text UnitCode {get; set;}
	}
}
