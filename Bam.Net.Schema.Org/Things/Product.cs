using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Any offered product or service. For example: a pair of shoes; a concert ticket; the rental of a car; a haircut; or an episode of a TV show streamed online.</summary>
	public class Product: Thing
	{
		///<summary>A property-value pair representing an additional characteristics of the entitity, e.g. a product feature or another characteristic for which there is no matching property in schema.org.Note: Publishers should be aware that applications designed to use specific schema.org properties (e.g. http://schema.org/width, http://schema.org/color, http://schema.org/gtin13, ...) will typically expect such data to be provided using those properties, rather than using the generic property/value mechanism.</summary>
		public PropertyValue AdditionalProperty {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>An intended audience, i.e. a group for whom something was created. Supersedes serviceAudience.</summary>
		public Audience Audience {get; set;}
		///<summary>An award won by or for this item. Supersedes awards.</summary>
		public Text Award {get; set;}
		///<summary>The brand(s) associated with a product or service, or the brand(s) maintained by an organization or business person.</summary>
		public OneOfThese<Brand,Organization> Brand {get; set;}
		///<summary>A category for the item. Greater signs or slashes can be used to informally indicate a category hierarchy.</summary>
		public OneOfThese<Text,Thing> Category {get; set;}
		///<summary>The color of the product.</summary>
		public Text Color {get; set;}
		///<summary>The depth of the item.</summary>
		public OneOfThese<Distance,QuantitativeValue> Depth {get; set;}
		///<summary>The GTIN-12 code of the product, or the product to which the offer refers. The GTIN-12 is the 12-digit GS1 Identification Key composed of a U.P.C. Company Prefix, Item Reference, and Check Digit used to identify trade items. See GS1 GTIN Summary for more details.</summary>
		public Text Gtin12 {get; set;}
		///<summary>The GTIN-13 code of the product, or the product to which the offer refers. This is equivalent to 13-digit ISBN codes and EAN UCC-13. Former 12-digit UPC codes can be converted into a GTIN-13 code by simply adding a preceeding zero. See GS1 GTIN Summary for more details.</summary>
		public Text Gtin13 {get; set;}
		///<summary>The GTIN-14 code of the product, or the product to which the offer refers. See GS1 GTIN Summary for more details.</summary>
		public Text Gtin14 {get; set;}
		///<summary>The GTIN-8 code of the product, or the product to which the offer refers. This code is also known as EAN/UCC-8 or 8-digit EAN. See GS1 GTIN Summary for more details.</summary>
		public Text Gtin8 {get; set;}
		///<summary>The height of the item.</summary>
		public OneOfThese<Distance,QuantitativeValue> Height {get; set;}
		///<summary>A pointer to another product (or multiple products) for which this product is an accessory or spare part.</summary>
		public Product IsAccessoryOrSparePartFor {get; set;}
		///<summary>A pointer to another product (or multiple products) for which this product is a consumable.</summary>
		public Product IsConsumableFor {get; set;}
		///<summary>A pointer to another, somehow related product (or multiple products).</summary>
		public OneOfThese<Product,Service> IsRelatedTo {get; set;}
		///<summary>A pointer to another, functionally similar product (or multiple products).</summary>
		public OneOfThese<Product,Service> IsSimilarTo {get; set;}
		///<summary>A predefined value from OfferItemCondition or a textual description of the condition of the product or service, or the products or services included in the offer.</summary>
		public OfferItemCondition ItemCondition {get; set;}
		///<summary>An associated logo.</summary>
		public OneOfThese<ImageObject,Url> Logo {get; set;}
		///<summary>The manufacturer of the product.</summary>
		public Organization Manufacturer {get; set;}
		///<summary>The model of the product. Use with the URL of a ProductModel or a textual representation of the model identifier. The URL of the ProductModel can be from an external source. It is recommended to additionally provide strong product identifiers via the gtin8/gtin13/gtin14 and mpn properties.</summary>
		public OneOfThese<ProductModel,Text> Model {get; set;}
		///<summary>The Manufacturer Part Number (MPN) of the product, or the product to which the offer refers.</summary>
		public Text Mpn {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, perform a service, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>The product identifier, such as ISBN. For example: meta itemprop="productID" content="isbn:123-456-789".</summary>
		public Text ProductID {get; set;}
		///<summary>The date of production of the item, e.g. vehicle.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ProductionDate {get; set;}
		///<summary>The date the item e.g. vehicle was purchased by the current owner.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date PurchaseDate {get; set;}
		///<summary>The release date of a product or product model. This can be used to distinguish the exact variant of a product.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ReleaseDate {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The Stock Keeping Unit (SKU), i.e. a merchant-specific identifier for a product or service, or the product to which the offer refers.</summary>
		public Text Sku {get; set;}
		///<summary>The weight of the product or person.</summary>
		public QuantitativeValue Weight {get; set;}
		///<summary>The width of the item.</summary>
		public OneOfThese<Distance,QuantitativeValue> Width {get; set;}
	}
}
