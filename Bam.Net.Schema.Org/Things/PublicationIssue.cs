/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A part of a successively published publication such as a periodical or publication volume, often numbered, usually containing a grouping of works such as articles.      See also blog post.</summary>
	public class PublicationIssue: CreativeWork
	{
		///<summary>Identifies the issue of publication; for example, "iii" or "2".</summary>
		public ThisOrThat<Integer , Text> IssueNumber {get; set;}
		///<summary>The page on which the work ends; for example "138" or "xvi".</summary>
		public ThisOrThat<Integer , Text> PageEnd {get; set;}
		///<summary>The page on which the work starts; for example "135" or "xiii".</summary>
		public ThisOrThat<Integer , Text> PageStart {get; set;}
		///<summary>Any description of pages that is not separated into pageStart and pageEnd; for example, "1-6, 9, 55" or "10-12, 46-49".</summary>
		public Text Pagination {get; set;}
	}
}
