using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A medical trial is a type of medical study that uses scientific process used to compare the safety and efficacy of medical therapies or medical procedures. In general, medical trials are controlled and subjects are allocated at random to the different treatment and/or control groups.</summary>
	public class MedicalTrial: MedicalStudy
	{
		///<summary>The phase of the trial.</summary>
		public Text Phase {get; set;}
		///<summary>Specifics about the trial design (enumerated).</summary>
		public MedicalTrialDesign TrialDesign {get; set;}
	}
}
