using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A type of blood vessel that specifically carries blood away from the heart.</summary>
	public class Artery: Vessel
	{
		///<summary>The branches that comprise the arterial structure.</summary>
		public AnatomicalStructure ArterialBranch {get; set;}
		///<summary>The anatomical or organ system that the artery originates from.</summary>
		public AnatomicalStructure Source {get; set;}
		///<summary>The area to which the artery supplies blood.</summary>
		public AnatomicalStructure SupplyTo {get; set;}
	}
}
