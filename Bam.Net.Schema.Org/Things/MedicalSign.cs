using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any physical manifestation of a person's medical condition discoverable by objective diagnostic tests or physical examination.</summary>
	public class MedicalSign: MedicalSignOrSymptom
	{
		///<summary>A physical examination that can identify this sign.</summary>
		public PhysicalExam IdentifyingExam {get; set;}
		///<summary>A diagnostic test that can identify this sign.</summary>
		public MedicalTest IdentifyingTest {get; set;}
	}
}
