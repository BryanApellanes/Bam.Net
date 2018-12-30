using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A structured value providing information about the opening hours of a place or a certain service inside a place.The place is open if the opens property is specified, and closed otherwise.If the value for the closes property is less than the value for the opens property then the hour range is assumed to span over the next day.</summary>
	public class OpeningHoursSpecification: StructuredValue
	{
		///<summary>The closing hour of the place or service on the given day(s) of the week.</summary>
		public Bam.Net.Schema.Org.DataTypes.Time Closes {get; set;}
		///<summary>The day of the week for which these opening hours are valid.</summary>
		public DayOfWeek DayOfWeek {get; set;}
		///<summary>The opening hour of the place or service on the given day(s) of the week.</summary>
		public Bam.Net.Schema.Org.DataTypes.Time Opens {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidFrom {get; set;}
		///<summary>The date after when the item is not valid. For example the end of an offer, salary period, or a period of opening hours.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidThrough {get; set;}
	}
}
