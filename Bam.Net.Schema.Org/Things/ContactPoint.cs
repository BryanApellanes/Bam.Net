/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A contact point—for example, a Customer Complaints department.</summary>
	public class ContactPoint: StructuredValue
	{
		///<summary>The location served by this contact point (e.g., a phone number intended for Europeans vs. North Americans or only within the United States).</summary>
		public AdministrativeArea AreaServed {get; set;}
		///<summary>A language someone may use with the item.</summary>
		public Language AvailableLanguage {get; set;}
		///<summary>An option available on this contact point (e.g. a toll-free number or support for hearing-impaired callers).</summary>
		public ContactPointOption ContactOption {get; set;}
		///<summary>A person or organization can have different contact points, for different purposes. For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.</summary>
		public Text ContactType {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The hours during which this contact point is available.</summary>
		public OpeningHoursSpecification HoursAvailable {get; set;}
		///<summary>The product or service this support contact point is related to (such as product support for a particular product line). This can be a specific product or product line (e.g. "iPhone") or a general category of products or services (e.g. "smartphones").</summary>
		public ThisOrThat<Product , Text> ProductSupported {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
	}
}
