using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A structured value representing a price or price range. Typically, only the subclasses of this type are used for markup. It is recommended to use MonetaryAmount to describe independent amounts of money such as a salary, credit card limits, etc.</summary>
	public class PriceSpecification: StructuredValue
	{
		///<summary>The interval and unit of measurement of ordering quantities for which the offer or price specification is valid. This allows e.g. specifying that a certain freight charge is valid only for a certain quantity.</summary>
		public QuantitativeValue EligibleQuantity {get; set;}
		///<summary>The transaction volume, in a monetary unit, for which the offer or price specification is valid, e.g. for indicating a minimal purchasing volume, to express free shipping above a certain order volume, or to limit the acceptance of credit cards to purchases to a certain minimal amount.</summary>
		public PriceSpecification EligibleTransactionVolume {get; set;}
		///<summary>The highest price if the price is a range.</summary>
		public Number MaxPrice {get; set;}
		///<summary>The lowest price if the price is a range.</summary>
		public Number MinPrice {get; set;}
		///<summary>The offer price of a product, or of a price component when attached to PriceSpecification and its subtypes.Usage guidelines:Use the priceCurrency property (with ISO 4217 codes e.g. "USD") instead of  including ambiguous symbols such as '$' in the value.Use '.' (Unicode 'FULL STOP' (U+002E)) rather than ',' to indicate a decimal point. Avoid using these symbols as a readability separator.Note that both RDFa and Microdata syntax allow the use of a "content=" attribute for publishing simple machine-readable values alongside more human-friendly formatting.Use values from 0123456789 (Unicode 'DIGIT ZERO' (U+0030) to 'DIGIT NINE' (U+0039)) rather than superficially similiar Unicode symbols.</summary>
		public OneOfThese<Number,Text> Price {get; set;}
		///<summary>The currency (in 3-letter ISO 4217 format) of the price or a price component, when attached to PriceSpecification and its subtypes.</summary>
		public Text PriceCurrency {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidFrom {get; set;}
		///<summary>The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidThrough {get; set;}
		///<summary>Specifies whether the applicable value-added tax (VAT) is included in the price specification or not.</summary>
		public Boolean ValueAddedTaxIncluded {get; set;}
	}
}
