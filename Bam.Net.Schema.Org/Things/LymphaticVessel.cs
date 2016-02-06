using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A type of blood vessel that specifically carries lymph fluid unidirectionally toward the heart.</summary>
	public class LymphaticVessel: Vessel
	{
		///<summary>The vasculature the lymphatic structure originates, or afferents, from.</summary>
		public Vessel OriginatesFrom {get; set;}
		///<summary>The anatomical or organ system drained by this vessel; generally refers to a specific part of an organ.</summary>
		public OneOfThese<AnatomicalSystem , AnatomicalStructure> RegionDrained {get; set;}
		///<summary>The vasculature the lymphatic structure runs, or efferents, to.</summary>
		public Vessel RunsTo {get; set;}
	}
}
