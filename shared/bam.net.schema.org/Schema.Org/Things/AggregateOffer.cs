using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>When a single product is associated with multiple offers (for example, the same pair of shoes is offered by different merchants), then AggregateOffer can be used.</summary>
	public class AggregateOffer: Offer
	{
		///<summary>The highest price of all offers available.</summary>
		public OneOfThese<Number,Text> HighPrice {get; set;}
		///<summary>The lowest price of all offers available.</summary>
		public OneOfThese<Number,Text> LowPrice {get; set;}
		///<summary>The number of offers for the product.</summary>
		public Integer OfferCount {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, perform a service, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
	}
}
