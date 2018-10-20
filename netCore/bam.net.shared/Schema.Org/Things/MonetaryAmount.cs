using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A monetary value or range. This type can be used to describe an amount of money such as $50 USD, or a range as in describing a bank account being suitable for a balance between Â£1,000 and Â£1,000,000 GBP, or the value of a salary, etc. It is recommended to use PriceSpecification Types to describe the price of an Offer, Invoice, etc.</summary>
	public class MonetaryAmount: StructuredValue
	{
		///<summary>The currency in which the monetary amount is expressed (in 3-letter ISO 4217 format).</summary>
		public Text Currency {get; set;}
		///<summary>The upper value of some characteristic or property.</summary>
		public Number MaxValue {get; set;}
		///<summary>The lower value of some characteristic or property.</summary>
		public Number MinValue {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidFrom {get; set;}
		///<summary>The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidThrough {get; set;}
		///<summary>The value of the quantitative value or property value node.For QuantitativeValue and MonetaryAmount, the recommended type for values is 'Number'.For PropertyValue, it can be 'Text;', 'Number', 'Boolean', or 'StructuredValue'.</summary>
		public OneOfThese<Boolean,Number,StructuredValue,Text> Value {get; set;}
	}
}
