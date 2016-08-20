using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The price asked for a given offer by the respective organization or person.</summary>
	public class UnitPriceSpecification: PriceSpecification
	{
		///<summary>This property specifies the minimal quantity and rounding increment that will be the basis for the billing. The unit of measurement is specified by the unitCode property.</summary>
		public Number BillingIncrement {get; set;}
		///<summary>A short text or acronym indicating multiple price specifications for the same offer, e.g. SRP for the suggested retail price or INVOICE for the invoice price, mostly used in the car industry.</summary>
		public Text PriceType {get; set;}
		///<summary>The reference quantity for which a certain price applies, e.g. 1 EUR per 4 kWh of electricity. This property is a replacement for unitOfMeasurement for the advanced cases where the price does not relate to a standard unit.</summary>
		public QuantitativeValue ReferenceQuantity {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters) or a URL. Other codes than the UN/CEFACT Common Code may be used with a prefix followed by a colon.</summary>
		public OneOfThese<Text,Url> UnitCode {get; set;}
		///<summary>A string or text indicating the unit of measurement. Useful if you cannot provide a standard unit code forunitCode.</summary>
		public Text UnitText {get; set;}
	}
}
