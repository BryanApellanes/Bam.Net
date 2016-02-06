/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The average rating based on multiple ratings or reviews.</summary>
	public class AggregateRating: Rating
	{
		///<summary>The item that is being reviewed/rated.</summary>
		public Thing ItemReviewed {get; set;}
		///<summary>The count of total number of ratings.</summary>
		public Number RatingCount {get; set;}
		///<summary>The count of total number of reviews.</summary>
		public Number ReviewCount {get; set;}
	}
}
