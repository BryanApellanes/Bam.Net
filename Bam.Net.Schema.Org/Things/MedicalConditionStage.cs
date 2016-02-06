using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A stage of a medical condition, such as 'Stage IIIa'.</summary>
	public class MedicalConditionStage: MedicalIntangible
	{
		///<summary>The stage represented as a number, e.g. 3.</summary>
		public Number StageAsNumber {get; set;}
		///<summary>The substage, e.g. 'a' for Stage IIIa.</summary>
		public Text SubStageSuffix {get; set;}
	}
}
