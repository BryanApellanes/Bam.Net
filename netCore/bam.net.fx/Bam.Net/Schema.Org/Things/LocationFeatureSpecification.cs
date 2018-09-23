using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Specifies a location feature by providing a structured value representing a feature of an accommodation as a property-value pair of varying degrees of formality.</summary>
	public class LocationFeatureSpecification: PropertyValue
	{
		///<summary>The hours during which this service or contact is available.</summary>
		public OpeningHoursSpecification HoursAvailable {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidFrom {get; set;}
		///<summary>The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidThrough {get; set;}
	}
}
