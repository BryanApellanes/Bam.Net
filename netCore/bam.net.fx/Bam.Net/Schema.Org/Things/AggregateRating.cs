using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The average rating based on multiple ratings or reviews.</summary>
	public class AggregateRating: Rating
	{
		///<summary>The item that is being reviewed/rated.</summary>
		public Thing ItemReviewed {get; set;}
		///<summary>The count of total number of ratings.</summary>
		public Integer RatingCount {get; set;}
		///<summary>The count of total number of reviews.</summary>
		public Integer ReviewCount {get; set;}
	}
}
