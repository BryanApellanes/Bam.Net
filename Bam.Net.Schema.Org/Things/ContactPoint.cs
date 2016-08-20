using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A contact point—for example, a Customer Complaints department.</summary>
	public class ContactPoint: StructuredValue
	{
		///<summary>The geographic area where a service or offered item is provided. Supersedes serviceArea.</summary>
		public OneOfThese<AdministrativeArea,GeoShape,Place,Text> AreaServed {get; set;}
		///<summary>A language someone may use with the item. Please use one of the language codes from the IETF BCP 47 standard. See also inLanguage</summary>
		public OneOfThese<Language,Text> AvailableLanguage {get; set;}
		///<summary>An option available on this contact point (e.g. a toll-free number or support for hearing-impaired callers).</summary>
		public ContactPointOption ContactOption {get; set;}
		///<summary>A person or organization can have different contact points, for different purposes. For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.</summary>
		public Text ContactType {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The hours during which this service or contact is available.</summary>
		public OpeningHoursSpecification HoursAvailable {get; set;}
		///<summary>The product or service this support contact point is related to (such as product support for a particular product line). This can be a specific product or product line (e.g. "iPhone") or a general category of products or services (e.g. "smartphones").</summary>
		public OneOfThese<Product,Text> ProductSupported {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
	}
}
