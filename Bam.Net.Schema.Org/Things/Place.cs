using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Entities that have a somewhat fixed, physical extension.</summary>
	public class Place: Thing
	{
		///<summary>A property-value pair representing an additional characteristics of the entitity, e.g. a product feature or another characteristic for which there is no matching property in schema.org. Note: Publishers should be aware that applications designed to use specific schema.org properties (e.g. http://schema.org/width, http://schema.org/color, http://schema.org/gtin13, ...) will typically expect such data to be provided using those properties, rather than using the generic property/value mechanism.</summary>
		public PropertyValue AdditionalProperty {get; set;}
		///<summary>Physical address of the item.</summary>
		public OneOfThese<PostalAddress , Text> Address {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>A short textual code (also called "store code") that uniquely identifies a place of business. The code is typically assigned by the parentOrganization and used in structured URLs. For example, in the URL http://www.starbucks.co.uk/store-locator/etc/detail/3047 the code "3047" is a branchCode for a particular branch.</summary>
		public Text BranchCode {get; set;}
		///<summary>The basic containment relation between a place and one that contains it. Supersedes containedIn. Inverse property: containsPlace.</summary>
		public Place ContainedInPlace {get; set;}
		///<summary>The basic containment relation between a place and another that it contains. Inverse property: containedInPlace.</summary>
		public Place ContainsPlace {get; set;}
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The geo coordinates of the place.</summary>
		public OneOfThese<GeoCoordinates , GeoShape> Geo {get; set;}
		///<summary>The Global Location Number (GLN, sometimes also referred to as International Location Number or ILN) of the respective organization, person, or place. The GLN is a 13-digit number used to identify parties and physical locations.</summary>
		public Text GlobalLocationNumber {get; set;}
		///<summary>A URL to a map of the place. Supersedes map, maps.</summary>
		public OneOfThese<Map , URL> HasMap {get; set;}
		///<summary>The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.</summary>
		public Text IsicV4 {get; set;}
		///<summary>An associated logo.</summary>
		public OneOfThese<ImageObject , URL> Logo {get; set;}
		///<summary>The opening hours of a certain place.</summary>
		public OpeningHoursSpecification OpeningHoursSpecification {get; set;}
		///<summary>A photograph of this place. Supersedes photos.</summary>
		public OneOfThese<ImageObject , Photograph> Photo {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The special opening hours of a certain place.Use this to explicitly override general opening hours brought in scope by openingHoursSpecification or openingHours.</summary>
		public OpeningHoursSpecification SpecialOpeningHoursSpecification {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
	}
}
