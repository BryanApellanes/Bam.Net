using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A type of blood vessel that specifically carries blood to the heart.</summary>
	public class Vein: Vessel
	{
		///<summary>The vasculature that the vein drains into.</summary>
		public Vessel DrainsTo {get; set;}
		///<summary>The anatomical or organ system drained by this vessel; generally refers to a specific part of an organ.</summary>
		public OneOfThese<AnatomicalSystem , AnatomicalStructure> RegionDrained {get; set;}
		///<summary>The anatomical or organ system that the vein flows into; a larger structure that the vein connects to.</summary>
		public AnatomicalStructure Tributary {get; set;}
	}
}
