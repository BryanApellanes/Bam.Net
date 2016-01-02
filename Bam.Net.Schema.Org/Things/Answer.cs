/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An answer offered to a question; perhaps correct, perhaps opinionated or wrong.</summary>
	public class Answer: CreativeWork
	{
		///<summary>The number of downvotes this question has received from the community.</summary>
		public Integer DownvoteCount {get; set;}
		///<summary>The parent of a question, answer or item in general.</summary>
		public Question ParentItem {get; set;}
		///<summary>The number of upvotes this question has received from the community.</summary>
		public Integer UpvoteCount {get; set;}
	}
}
