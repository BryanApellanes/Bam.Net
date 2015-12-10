/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A set of characteristics describing parents, who can be interested in viewing some content.</summary>
	public class ParentAudience: PeopleAudience
	{
		///<summary>Maximal age of the child.</summary>
		public Number ChildMaxAge {get; set;}
		///<summary>Minimal age of the child.</summary>
		public Number ChildMinAge {get; set;}
	}
}
