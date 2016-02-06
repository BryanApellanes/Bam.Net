using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A muscle is an anatomical structure consisting of a contractile form of tissue that animals use to effect movement.</summary>
	public class Muscle: AnatomicalStructure
	{
		///<summary>The muscle whose action counteracts the specified muscle.</summary>
		public Muscle Antagonist {get; set;}
		///<summary>The blood vessel that carries blood from the heart to the muscle.</summary>
		public Vessel BloodSupply {get; set;}
		///<summary>The place of attachment of a muscle, or what the muscle moves.</summary>
		public AnatomicalStructure Insertion {get; set;}
		///<summary>The movement the muscle generates. Supersedes action.</summary>
		public Text MuscleAction {get; set;}
		///<summary>The underlying innervation associated with the muscle.</summary>
		public Nerve Nerve {get; set;}
		///<summary>The place or point where a muscle arises.</summary>
		public AnatomicalStructure Origin {get; set;}
	}
}
