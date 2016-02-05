using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A DatedMoneySpecification represents monetary values with optional start and end dates. For example, this could represent an employee's salary over a specific period of time.</summary>
	public class DatedMoneySpecification: StructuredValue
	{
		///<summary>The amount of money.</summary>
		public Number Amount {get; set;}
		///<summary>The currency in which the monetary amount is expressed (in 3-letter ISO 4217 format).</summary>
		public Text Currency {get; set;}
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
	}
}
