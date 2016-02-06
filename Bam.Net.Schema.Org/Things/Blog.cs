/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A blog.</summary>
	public class Blog: CreativeWork
	{
		///<summary>A posting that is part of this blog. Supersedes blogPosts.</summary>
		public BlogPosting BlogPost {get; set;}
	}
}
