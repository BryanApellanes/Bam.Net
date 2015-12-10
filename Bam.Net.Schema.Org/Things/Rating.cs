/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A rating is an evaluation on a numeric scale, such as 1 to 5 stars.</summary>
	public class Rating: Intangible
	{
		///<summary>The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.</summary>
		public ThisOrThat<Number , Text> BestRating {get; set;}
		///<summary>The rating for the content.</summary>
		public Text RatingValue {get; set;}
		///<summary>The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.</summary>
		public ThisOrThat<Number , Text> WorstRating {get; set;}
	}
}
