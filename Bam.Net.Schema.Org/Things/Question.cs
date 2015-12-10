/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A specific question - e.g. from a user seeking answers online, or collected in a Frequently Asked Questions (FAQ) document.</summary>
	public class Question: CreativeWork
	{
		///<summary>The answer that has been accepted as best, typically on a Question/Answer site. Sites vary in their selection mechanisms, e.g. drawing on community opinion and/or the view of the Question author.</summary>
		public Answer AcceptedAnswer {get; set;}
		///<summary>The number of answers this question has received.</summary>
		public Integer AnswerCount {get; set;}
		///<summary>The number of downvotes this question has received from the community.</summary>
		public Integer DownvoteCount {get; set;}
		///<summary>An answer (possibly one of several, possibly incorrect) to a Question, e.g. on a Question/Answer site.</summary>
		public Answer SuggestedAnswer {get; set;}
		///<summary>The number of upvotes this question has received from the community.</summary>
		public Integer UpvoteCount {get; set;}
	}
}
