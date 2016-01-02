/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A common pathway for the electrochemical nerve impulses that are transmitted along each of the axons.</summary>
	public class Nerve: AnatomicalStructure
	{
		///<summary>The branches that delineate from the nerve bundle.</summary>
		public AnatomicalStructure Branch {get; set;}
		///<summary>The neurological pathway extension that involves muscle control.</summary>
		public Muscle NerveMotor {get; set;}
		///<summary>The neurological pathway extension that inputs and sends information to the brain or spinal cord.</summary>
		public ThisOrThat<SuperficialAnatomy , AnatomicalStructure> SensoryUnit {get; set;}
		///<summary>The neurological pathway that originates the neurons.</summary>
		public BrainStructure SourcedFrom {get; set;}
	}
}
