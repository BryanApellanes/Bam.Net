using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of ingesting information/resources/food.</summary>
	public class ConsumeAction: Action
	{
		///<summary>An Offer which must be accepted before the user can perform the Action. For example, the user may need to buy a movie before being able to watch it.</summary>
		public Offer ExpectsAcceptanceOf {get; set;}
	}
}
