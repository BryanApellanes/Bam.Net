/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any indication of the existence of a medical condition or disease.</summary>
	public class MedicalSignOrSymptom: MedicalEntity
	{
		///<summary>An underlying cause. More specifically, one of the causative agent(s) that are most directly responsible for the pathophysiologic process that eventually results in the occurrence.</summary>
		public MedicalCause Cause {get; set;}
		///<summary>A possible treatment to address this condition, sign or symptom.</summary>
		public MedicalTherapy PossibleTreatment {get; set;}
	}
}
