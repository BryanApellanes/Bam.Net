using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A rating is an evaluation on a numeric scale, such as 1 to 5 stars.</summary>
	public class Rating: Intangible
	{
		///<summary>The author of this content or rating. Please note that author is special in that HTML 5 provides a special mechanism for indicating authorship via the rel tag. That is equivalent to this and may be used interchangeably.</summary>
		public OneOfThese<Organization,Person> Author {get; set;}
		///<summary>The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.</summary>
		public OneOfThese<Number,Text> BestRating {get; set;}
		///<summary>The rating for the content.</summary>
		public OneOfThese<Number,Text> RatingValue {get; set;}
		///<summary>The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.</summary>
		public OneOfThese<Number,Text> WorstRating {get; set;}
	}
}
