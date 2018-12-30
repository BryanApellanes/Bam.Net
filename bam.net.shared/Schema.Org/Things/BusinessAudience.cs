using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A set of characteristics belonging to businesses, e.g. who compose an item's target audience.</summary>
	public class BusinessAudience: Audience
	{
		///<summary>The number of employees in an organization e.g. business.</summary>
		public QuantitativeValue NumberOfEmployees {get; set;}
		///<summary>The size of the business in annual revenue.</summary>
		public QuantitativeValue YearlyRevenue {get; set;}
		///<summary>The age of the business.</summary>
		public QuantitativeValue YearsInOperation {get; set;}
	}
}
