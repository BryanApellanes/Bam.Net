using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The mailing address.</summary>
	public class PostalAddress: ContactPoint
	{
		///<summary>The country. For example, USA. You can also provide the two-letter ISO 3166-1 alpha-2 country code.</summary>
		public OneOfThese<Country,Text> AddressCountry {get; set;}
		///<summary>The locality. For example, Mountain View.</summary>
		public Text AddressLocality {get; set;}
		///<summary>The region. For example, CA.</summary>
		public Text AddressRegion {get; set;}
		///<summary>The post office box number for PO box addresses.</summary>
		public Text PostOfficeBoxNumber {get; set;}
		///<summary>The postal code. For example, 94043.</summary>
		public Text PostalCode {get; set;}
		///<summary>The street address. For example, 1600 Amphitheatre Pkwy.</summary>
		public Text StreetAddress {get; set;}
	}
}
