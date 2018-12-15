using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The geographic shape of a place. A GeoShape can be described using several properties whose values are based on latitude/longitude pairs. Either whitespace or commas can be used to separate latitude and longitude; whitespace should be used when writing a list of several such points.</summary>
	public class GeoShape: StructuredValue
	{
		///<summary>Physical address of the item.</summary>
		public OneOfThese<PostalAddress,Text> Address {get; set;}
		///<summary>The country. For example, USA. You can also provide the two-letter ISO 3166-1 alpha-2 country code.</summary>
		public OneOfThese<Country,Text> AddressCountry {get; set;}
		///<summary>A box is the area enclosed by the rectangle formed by two points. The first point is the lower corner, the second point is the upper corner. A box is expressed as two points separated by a space character.</summary>
		public Text Box {get; set;}
		///<summary>A circle is the circular region of a specified radius centered at a specified latitude and longitude. A circle is expressed as a pair followed by a radius in meters.</summary>
		public Text Circle {get; set;}
		///<summary>The elevation of a location (WGS 84).</summary>
		public OneOfThese<Number,Text> Elevation {get; set;}
		///<summary>A line is a point-to-point path consisting of two or more points. A line is expressed as a series of two or more point objects separated by space.</summary>
		public Text Line {get; set;}
		///<summary>A polygon is the area enclosed by a point-to-point path for which the starting and ending points are the same. A polygon is expressed as a series of four or more space delimited points where the first and final points are identical.</summary>
		public Text Polygon {get; set;}
		///<summary>The postal code. For example, 94043.</summary>
		public Text PostalCode {get; set;}
	}
}
