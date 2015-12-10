/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A comment on an item - for example, a comment on a blog post. The comment's content is expressed via the "text" property, and its topic via "about", properties shared with all CreativeWorks.</summary>
	public class Comment: CreativeWork
	{
		///<summary>The number of downvotes this question has received from the community.</summary>
		public Integer DownvoteCount {get; set;}
		///<summary>The parent of a question, answer or item in general.</summary>
		public Question ParentItem {get; set;}
		///<summary>The number of upvotes this question has received from the community.</summary>
		public Integer UpvoteCount {get; set;}
	}
}
