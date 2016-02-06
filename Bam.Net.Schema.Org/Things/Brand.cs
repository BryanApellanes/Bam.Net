/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A brand is a name used by an organization or business person for labeling a product, product group, or similar.</summary>
	public class Brand: Intangible
	{
		///<summary>An associated logo.</summary>
		public OneOfThese<URL , ImageObject> Logo {get; set;}
	}
}
