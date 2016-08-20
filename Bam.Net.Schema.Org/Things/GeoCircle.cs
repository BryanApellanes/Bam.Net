using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A GeoCircle is a GeoShape representing a circular geographic area. As it is a GeoShape          it provides the simple textual property 'circle', but also allows the combination of postalCode alongside geoRadius.          The center of the circle can be indicated via the 'geoMidpoint' property, or more approximately using 'address', 'postalCode'.</summary>
	public class GeoCircle: GeoShape
	{
		///<summary>Indicates the GeoCoordinates at the centre of a GeoShape e.g. GeoCircle.</summary>
		public GeoCoordinates GeoMidpoint {get; set;}
		///<summary>Indicates the approximate radius of a GeoCircle (metres unless indicated otherwise via Distance notation).</summary>
		public OneOfThese<Distance,Number,Text> GeoRadius {get; set;}
	}
}
