/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A review of an item - for example, of a restaurant, movie, or store.</summary>
	public class Review: CreativeWork
	{
		///<summary>The item that is being reviewed/rated.</summary>
		public Thing ItemReviewed {get; set;}
		///<summary>The actual body of the review.</summary>
		public Text ReviewBody {get; set;}
		///<summary>The rating given in this review. Note that reviews can themselves be rated. The reviewRating applies to rating given by the review. The aggregateRating property applies to the review itself, as a creative work.</summary>
		public Rating ReviewRating {get; set;}
	}
}
