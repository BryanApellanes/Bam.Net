using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A blog post intended to provide a rolling textual coverage of an ongoing event through continuous updates.</summary>
	public class LiveBlogPosting: BlogPosting
	{
		///<summary>The time when the live blog will stop covering the Event. Note that coverage may continue after the Event concludes.</summary>
		public DateTime CoverageEndTime {get; set;}
		///<summary>The time when the live blog will begin covering the Event. Note that coverage may begin before the Event's start time. The LiveBlogPosting may also be created before coverage begins.</summary>
		public DateTime CoverageStartTime {get; set;}
		///<summary>An update to the LiveBlog.</summary>
		public BlogPosting LiveBlogUpdate {get; set;}
	}
}
