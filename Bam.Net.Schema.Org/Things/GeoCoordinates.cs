using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The geographic coordinates of a place or event.</summary>
	public class GeoCoordinates: StructuredValue
	{
		///<summary>Physical address of the item.</summary>
		public ThisOrThat<Text , PostalAddress> Address {get; set;}
		///<summary>The country. For example, USA. You can also provide the two-letter ISO 3166-1 alpha-2 country code.</summary>
		public ThisOrThat<Text , Country> AddressCountry {get; set;}
		///<summary>The elevation of a location (WGS 84).</summary>
		public ThisOrThat<Text , Number> Elevation {get; set;}
		///<summary>The latitude of a location. For example 37.42242 (WGS 84).</summary>
		public ThisOrThat<Text , Number> Latitude {get; set;}
		///<summary>The longitude of a location. For example -122.08585 (WGS 84).</summary>
		public ThisOrThat<Text , Number> Longitude {get; set;}
		///<summary>The postal code. For example, 94043.</summary>
		public Text PostalCode {get; set;}
	}
}
