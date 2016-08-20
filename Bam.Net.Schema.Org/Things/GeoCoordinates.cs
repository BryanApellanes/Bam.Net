using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The geographic coordinates of a place or event.</summary>
	public class GeoCoordinates: StructuredValue
	{
		///<summary>Physical address of the item.</summary>
		public OneOfThese<PostalAddress,Text> Address {get; set;}
		///<summary>The country. For example, USA. You can also provide the two-letter ISO 3166-1 alpha-2 country code.</summary>
		public OneOfThese<Country,Text> AddressCountry {get; set;}
		///<summary>The elevation of a location (WGS 84).</summary>
		public OneOfThese<Number,Text> Elevation {get; set;}
		///<summary>The latitude of a location. For example 37.42242 (WGS 84).</summary>
		public OneOfThese<Number,Text> Latitude {get; set;}
		///<summary>The longitude of a location. For example -122.08585 (WGS 84).</summary>
		public OneOfThese<Number,Text> Longitude {get; set;}
		///<summary>The postal code. For example, 94043.</summary>
		public Text PostalCode {get; set;}
	}
}
