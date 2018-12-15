using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A post to a social media platform, including blog posts, tweets, Facebook posts, etc.</summary>
	public class SocialMediaPosting: Article
	{
		///<summary>A CreativeWork such as an image, video, or audio clip shared as part of this posting.</summary>
		public CreativeWork SharedContent {get; set;}
	}
}
