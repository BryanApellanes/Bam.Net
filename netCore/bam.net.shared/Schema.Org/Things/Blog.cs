using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A blog.</summary>
	public class Blog: CreativeWork
	{
		///<summary>A posting that is part of this blog. Supersedes blogPosts.</summary>
		public BlogPosting BlogPost {get; set;}
	}
}
