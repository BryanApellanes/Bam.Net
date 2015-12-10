/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A structured value providing information about the opening hours of a place or a certain service inside a place.</summary>
	public class OpeningHoursSpecification: StructuredValue
	{
		///<summary>The closing hour of the place or service on the given day(s) of the week.</summary>
		public Time Closes {get; set;}
		///<summary>The day of the week for which these opening hours are valid.</summary>
		public DayOfWeek DayOfWeek {get; set;}
		///<summary>The opening hour of the place or service on the given day(s) of the week.</summary>
		public Time Opens {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public DateTime ValidFrom {get; set;}
		///<summary>The end of the validity of offer, price specification, or opening hours data.</summary>
		public DateTime ValidThrough {get; set;}
	}
}
