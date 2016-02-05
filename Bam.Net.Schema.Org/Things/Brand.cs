using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A brand is a name used by an organization or business person for labeling a product, product group, or similar.</summary>
	public class Brand: Intangible
	{
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>An associated logo.</summary>
		public ThisOrThat<ImageObject , URL> Logo {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
	}
}
