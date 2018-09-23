using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A blog post intended to provide a rolling textual coverage of an ongoing event through continuous updates.</summary>
	public class LiveBlogPosting: BlogPosting
	{
		///<summary>The time when the live blog will stop covering the Event. Note that coverage may continue after the Event concludes.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CoverageEndTime {get; set;}
		///<summary>The time when the live blog will begin covering the Event. Note that coverage may begin before the Event's start time. The LiveBlogPosting may also be created before coverage begins.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CoverageStartTime {get; set;}
		///<summary>An update to the LiveBlog.</summary>
		public BlogPosting LiveBlogUpdate {get; set;}
	}
}
